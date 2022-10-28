using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using Section = Model.Section;
using Timer = System.Timers.Timer;

namespace Controller
{
   public delegate void RaceChangedDelegate(Race previousRace, Race newRace); 
   public class Race
   {
      #region Properties
      private Random _random { get; set; }
      private Queue<IParticipant> _finishedDrivers { get; set; }
      private Dictionary<Section, SectionData> _positions { get; set; }
      private Dictionary<IParticipant, string> _oldName { get; set; }
      public int Rounds { get; set; }
      public Timer _timer { get; set; }

      public int PlaceOnSection { get; set; }
      public Track Track { get; set; }
      public DateTime StartTime { get; set; }
      public Random Random { get; set; }

      public List<IParticipant> Participants { get; set; }
      public Dictionary<Section, SectionData> Positions { get; set; }

      public event EventHandler<DriversChangedEventArgs> DriversChanged;
      public event RaceChangedDelegate RaceChanged; 
      #endregion

      #region Constructors
      public Race(Track track, List<IParticipant> participants)
      {
         Track = track;
         Participants = participants;
         foreach (var participant in Participants)
         {
            participant.CurrentSection = null;
         }
         _finishedDrivers = new Queue<IParticipant>();   
         _positions = new Dictionary<Section, SectionData>();
         _timer = new Timer(150);
         _timer.Elapsed += OnTimedEvent;
         Rounds = track.Rounds;
         _oldName = new Dictionary<IParticipant, string>();

         _random = new Random(DateTime.Now.Millisecond);
         RandomizeEquipment();
         ParticipantsStartPosition(track, participants);
      }
      #endregion

      #region Methods
      public void Start()
      {
         _timer.Start();
      }
      public void RandomizeEquipment()
      {
         foreach (var participant in Participants)
         {
            Car car = new Car(_random.Next(1, 10), _random.Next(5, 10), _random.Next(6, 10));
            participant.Equipment = car;
            participant.Rounds = 0;
            _oldName.Add(participant, participant.Name); 
         }
      }

      public SectionData GetSectionData(Section currentSection)
      {
         if (!_positions.ContainsKey(currentSection))
         {
            _positions.Add(currentSection, new SectionData());
         }
         return _positions[currentSection];
      }

      public void ParticipantsStartPosition(Track track, List<IParticipant> participants) //todo methodes werkwoorden meegeven
      {
         if (participants.Count < 3) //check if there are 3 or more drivers
         {
            throw new ArgumentException("There must be at least 3 drivers in a race");
         }
         if(participants.Count > 4)
         {
            throw new ArgumentException("There can not be more than 4 drivers");
         }
         foreach(IParticipant participant in participants) //check if teamcolor is unique
         {
            participant.PassedFinishCounter = 0;
            participant.Rounds = 0; 
            participant.IsFinished = false;
            foreach (IParticipant obj in participants) {
               if (participant.TeamColor.Equals(obj.TeamColor) && participant != obj)
               {
                  throw new ArgumentException("Team Color must be unique");
               }
             }
         } 
         List<IParticipant> tempParticipants = new List<IParticipant>(participants);
         for (LinkedListNode<Section> section = track.Sections.Last;
            section != null && participants.Count > 0; section = section.Previous) //go backwards so you fill in the startgrids from the first position to the last 
         {
            if (section.Value.SectionType != SectionTypes.StartGrid)
               continue; //if sectiontype isn't startgrid continue looking for startgrids
            if (tempParticipants.Count == 0)
               return;

            SectionData data = GetSectionData(section.ValueRef);
            if (tempParticipants.Count > 0)
            {
               int randParticipantNumber = _random.Next(tempParticipants.Count);
               data.Left = tempParticipants[randParticipantNumber];
               tempParticipants[randParticipantNumber].CurrentSection = section;
               tempParticipants.Remove(data.Left);
            }

            if (tempParticipants.Count > 0)
            {
               int randParticipantNumber = _random.Next(tempParticipants.Count);
               data.Right = tempParticipants[randParticipantNumber];
               tempParticipants[randParticipantNumber].CurrentSection = section;
               tempParticipants.Remove(data.Right);
            }                  
         }
      }

      public void OnTimedEvent(object o, EventArgs e)
      {
         _timer.Stop();
         foreach (IParticipant participant in Participants.Where(x => !_finishedDrivers.Contains(x)))
         {
               EquipmentBroke();

               int distance = (participant.Equipment.Speed * participant.Equipment.Performance);
               LinkedListNode<Section> currentSection = participant.CurrentSection;
               SectionData sectiondata = GetSectionData(currentSection.ValueRef);
               int newPosition = distance + sectiondata.GetParticipantPosition(participant);
               bool next = newPosition >= Section.SectionLength;

               if (next)
               {
                  participant.CurrentSection = currentSection.Next ?? currentSection.List.First;
                  if (participant.CurrentSection.Value.SectionType == SectionTypes.Finish) //check if driver passed finish
                  {
                     participant.PassedFinishCounter++;
                     if (participant.Rounds != Rounds && participant.PassedFinishCounter > 1)
                     {
                        participant.Rounds++;
                     }
                  }
                  newPosition -= Section.SectionLength;
                  sectiondata.RemoveParticipantFromSection(participant);

                  sectiondata = GetSectionData(participant.CurrentSection.ValueRef); //get new sectiondata
                  if (sectiondata.IsFull())
                  {
                     participant.CurrentSection = participant.CurrentSection.Next ?? participant.CurrentSection.List.First;
                     if (participant.CurrentSection.Value.SectionType == SectionTypes.Finish) //check if driver passed finish
                     {
                        participant.PassedFinishCounter++;
                        if (participant.Rounds != Rounds && participant.PassedFinishCounter > 1) { participant.Rounds++; }
                     }
                     sectiondata = GetSectionData(participant.CurrentSection.ValueRef);
                  }
                  sectiondata.AddParticipantToSection(participant, newPosition);
               }
               else { sectiondata.MoveParticipantOnSection(participant, distance); }
               if ((participant.Rounds == Rounds) && (participant.CurrentSection.Value.SectionType == SectionTypes.Finish)) //if driver finished and over finish
               {
                  if (!participant.IsFinished && !(_finishedDrivers.Contains(participant)))
                  {
                     _finishedDrivers.Enqueue(participant);
                     GetSectionData(participant.CurrentSection.ValueRef).RemoveParticipantFromSection(participant);
                     participant.IsFinished = true;
                     if (NoDriversleftOnTrack() == true) 
                     {
                        End();
                        return;
                     };
                  }
               }
             DriversChanged?.Invoke(this, new DriversChangedEventArgs(this.Track));
             EquipmentRepair();
         }
         _timer.Start();
      }

      public void EquipmentBroke()
      {
         foreach (var driver in Participants)
         {
            if(!driver.Equipment.IsBroken && _random.Next(0, 1000) < 20)
            {
               driver.Equipment.EquipmentStatus = "broken";
               driver.Equipment.IsBroken = true;
               driver.Equipment.Speed = 0;
               driver.Name = $"!{driver.Name}";
            }
         }
      }

      public void EquipmentRepair()
      {
         foreach(var driver in Participants)
            {
               if(driver.Equipment.IsBroken && _random.Next(0,20) < 5)
               {
               driver.Equipment.EquipmentStatus = "fixed";
               driver.Equipment.IsBroken = false;
               driver.Name = _oldName[driver];
               driver.Equipment.Speed = _random.Next(6, 10);
               driver.Equipment.Performance = _random.Next(4, 9);
               }
         }
      }

      public Boolean NoDriversleftOnTrack() //check if track empty
      {
         foreach(var section in Track.Sections)
         {
            if((GetSectionData(section).Left != null) || (GetSectionData(section).Right != null))
            {
               return false;
            }
         }
         return true; 
      }

      public int ReturnFinishedDrivers()
      {
         int x = _finishedDrivers.Count();
         return x; 
      }

      public void End()
      {
         int PointsToGive = 10;
         _timer.Stop();
         _timer.Elapsed -= OnTimedEvent;
         while(_finishedDrivers.Count > 0)
         {
            IParticipant participant = _finishedDrivers.Dequeue();
            participant.Points += PointsToGive;
            PointsToGive = PointsToGive / 2;
         }
         Thread.Sleep(500);
         DriversChanged?.Invoke(this, new DriversChangedEventArgs(this.Track));
         Data.NextRace();
         RaceChanged?.Invoke(this, Data.CurrentRace); 
      }

      public void Cleanup()
      {
         DriversChanged = null;
         RaceChanged = null;
      }

      #endregion
   }
}
