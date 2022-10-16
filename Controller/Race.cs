using Model;
using System;
using System.Collections.Generic;
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
      private int _finishedDrivers { get; set; }
      private Dictionary<Section, SectionData> _positions { get; set; }
      private int _rounds { get; set; }
      public Timer _timer { get; set; }

      public int FinishedDrivers
      {
         get
         {
            return _finishedDrivers;
         }
         set
         {
            _finishedDrivers = value;
         }
      }

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
         _positions = new Dictionary<Section, SectionData>();
         _timer = new Timer(250);
         _timer.Elapsed += OnTimedEvent;
         FinishedDrivers = 0;
         _rounds = track.Rounds + 1; //because you pass finish 1 more time than the number of rounds should be

         _random = new Random(DateTime.Now.Millisecond);
         RandomizeEquipment();
         ParticipantsStartPosition(track, participants);
      }
      #endregion

      #region Methods
      public void Start()
      {
         _timer.Start();
         DriversChanged?.Invoke(this, new DriversChangedEventArgs(Track));
      }
      public void RandomizeEquipment()
      {
         foreach (var participant in Participants)
         {
            Car car = new Car(_random.Next(1, 10), _random.Next(5, 10), _random.Next(6, 10));
            participant.Equipment = car;
            participant.Rounds = 0;
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
         if(participants.Count < 3) //check if there are 3 or more drivers
         {
            throw new ArgumentException("There must be at least 3 drivers in a race");
         }
         if(participants.Count > 4)
         {
            throw new ArgumentException("There can not be more than 4 drivers");
         }
         foreach(IParticipant participant in participants) //check if teamcolor is unique
         {
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
         foreach (IParticipant participant in Participants)
         {
            if (participant.Rounds == _rounds) continue; 
            int distance = (participant.Equipment.Speed * participant.Equipment.Performance);
            LinkedListNode<Section> currentSection = participant.CurrentSection;
            SectionData sectiondata = GetSectionData(currentSection.ValueRef);
            int newPosition = distance + sectiondata.GetParticipantPosition(participant);
            bool next = newPosition >= Section.SectionLength;
            if (next)
               {
               
                  participant.CurrentSection = currentSection.Next ?? currentSection.List.First;
                  newPosition -= Section.SectionLength;
                  
                  if (participant == sectiondata.Left) //remove driver from last section
                  {
                     sectiondata.Left = null;
                     sectiondata.DistanceLeft = 0;
                  }
                  else if (participant == sectiondata.Right)
                  {
                     sectiondata.Right = null;
                     sectiondata.DistanceRight = 0;
                  }


               if (sectiondata.IsFull())
               {
                  participant.CurrentSection = participant.CurrentSection.Next;
                  sectiondata = GetSectionData(participant.CurrentSection.ValueRef);
               }
               if (sectiondata.Left is null)
               {
                  sectiondata.Left = participant;
                  sectiondata.DistanceLeft += newPosition;
               }
               else if (sectiondata.Right is null)
               {
                  sectiondata.Right = participant;
                  sectiondata.DistanceRight += newPosition;
               }

            else
            {
               if (participant == sectiondata.Left)
               {
                  sectiondata.DistanceLeft += distance;
               }
               else if (participant == sectiondata.Right)
               {
                  sectiondata.DistanceRight += distance;
               }
            }
                     sectiondata.Left = participant;
         DriversChanged?.Invoke(this, new DriversChangedEventArgs(this.Track));
      }
      public void ClearEvents()
               foreach (var var in DriversChanged.GetInvocationList())
               {
                  DriversChanged -= (EventHandler<DriversChangedEventArgs>)var;
               }
            }
            if (RaceChanged.GetInvocationList() != null)
            {
               foreach (var var in RaceChanged.GetInvocationList())
               {
                  RaceChanged -= (RaceChangedDelegate)var;
               }
            }
         }
         catch (NullReferenceException)
         {
            return;
         }
         
      }

      public void End()
      {
         _timer.Stop();
         RaceChanged?.Invoke(this, Data.CurrentRace);
      }

      #endregion
   }
}
