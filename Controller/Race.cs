using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Timer = System.Timers.Timer;

namespace Controller
{
   public class Race
   {
      #region Properties
      private Random _random;
      private Dictionary<Sections, SectionData> _positions;
      public Timer _timer { get; set; }
      
      public int PlaceOnSection { get; set; }
      public Track Track { get; set; }
      public DateTime StartTime { get; set; }
      public Random Random { get; set; }

      public List<IParticipant> Participants { get; set; }
      public Dictionary<Sections, SectionData> Positions { get; set; }

      public event EventHandler<DriversChangedEventArgs> DriversChanged; 
      #endregion

      #region Constructors
      public Race(Track track, List<IParticipant> participants)
      {
         Track = track;
         Participants = participants;
         _positions = new Dictionary<Sections, SectionData>();
         _timer = new Timer(250);
         _timer.Elapsed += OnTimedEvent;

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
            Car car = new Car(_random.Next(1, 10), _random.Next(2, 10), _random.Next(4, 10));
            participant.Equipment = car;
         }
      }

      public SectionData GetSectionData(Sections currentSection)
      {
         if (!_positions.ContainsKey(currentSection))
         {
            _positions.Add(currentSection, new SectionData());
         }
         return _positions[currentSection];
      }

      public void ParticipantsStartPosition(Track track, List<IParticipant> participants) //todo methodes werkwoorden meegeven
      {
         List<IParticipant> tempParticipants = new List<IParticipant>(participants);
         for (LinkedListNode<Sections> section = track.Sections.Last;
            section != null && participants.Count > 0; section = section.Previous)
         {
            if (section.Value.SectionType != SectionTypes.StartGrid)
               continue;
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
         foreach (IParticipant participant in Participants)
         {
            int distance = (participant.Equipment.Speed * participant.Equipment.Performance);
            LinkedListNode<Sections> currentSection = participant.CurrentSection;
            SectionData sectiondata = GetSectionData(currentSection.ValueRef);
            int newPosition = distance + sectiondata.GetParticipantPosition(participant);
            bool next = newPosition >= Sections.SectionLength;
            if (next)
            {

               participant.CurrentSection = currentSection.Next ?? currentSection.List.First;

               newPosition -= Sections.SectionLength;
               if (participant == sectiondata.Left) //remove driver from last section
               {
                  sectiondata.Left = null;
                  //sectiondata.DistanceLeft = 0;
                  sectiondata.IsFull = false;
               }
               else if (participant == sectiondata.Right)
               {
                  sectiondata.Right = null;
                  //sectiondata.DistanceRight = 0;
                  sectiondata.IsFull = false;
               }
               SectionData sd = GetSectionData(participant.CurrentSection.Value);
               if (sd.IsFull == true) //add participant 
               {
                  participant.CurrentSection = participant.CurrentSection.Next;
                  sd = GetSectionData(participant.CurrentSection.ValueRef);
                  if(participant == sectiondata.Left)
                  {
                     sectiondata.DistanceLeft = 0;
                  }
                  else
                  {
                     sectiondata.DistanceRight = 0;
                  }
               }
               else
               {
                  if (sd.Left == null || sd.Right != null)
                  {
                     sd.Left = participant;
                     sd.DistanceLeft = distance;
                  }
                  else if (sd.Right == null || sd.Left != null)
                  {
                     sd.Right = participant;
                     sd.DistanceRight = distance;
                  }
                  sd.IsFull = sd.Left != null && sd.Right != null;
                  participant.CurrentSection = currentSection.Next;
               }
               
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
               else
               {
                  throw new Exception("Driver not found");
               }
            }
         }
         DriversChanged?.Invoke(this, new DriversChangedEventArgs(this.Track));
       }


      #endregion
   }
}
