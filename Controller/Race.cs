using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Controller
{
   public class Race
   {
      #region Properties
      private Random _random;
      private Dictionary<Sections, SectionData> _positions;
      private static System.Timers.Timer _timer; 

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
         _timer = new System.Timers.Timer(500);
         _timer.Elapsed += OnTimedEvent;

         _random = new Random(DateTime.Now.Millisecond);
         RandomizeEquipment();
         ParticipantsPosition(track, participants);
         Start(); 
      }
      #endregion

      #region Methods
      public void Start()
      {
         _timer.AutoReset = true;
         _timer.Enabled = true;
      }

      public void OnTimedEvent(object o, EventArgs e)
      {
         DriversChanged?.Invoke(this, new DriversChangedEventArgs(this.Track));
      }

      public SectionData GetSectionData(Sections currentSection)
      {
         if (!_positions.ContainsKey(currentSection))
         {
            _positions.Add(currentSection, new SectionData());
         }
         return _positions[currentSection];
      }

      public void RandomizeEquipment()
      {
         foreach (var participant in Participants)
         {
            int p = participant.Equipment.Performance = _random.Next(50);
            int q = participant.Equipment.Quality = _random.Next(50);
         }
      }

      public void ParticipantsPosition(Track track, List<IParticipant> participants)
      {
         List<IParticipant> tempParticipants = new List<IParticipant>(participants);

         foreach (Sections section in track.Sections)
         {
            if (section.SectionType != SectionTypes.StartGrid)
               continue;
            if (tempParticipants.Count == 0)
               return;

            SectionData data = GetSectionData(section);
            if (tempParticipants.Count > 0)
            {
               data.Left = tempParticipants[_random.Next(tempParticipants.Count)];
               tempParticipants.Remove(data.Left);
            }

            if (tempParticipants.Count > 0)
            {
               data.Right = tempParticipants[_random.Next(tempParticipants.Count)];
               tempParticipants.Remove(data.Right);
            }
         }

      }
      #endregion
   }
}
