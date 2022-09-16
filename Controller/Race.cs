using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
   public class Race
   {
      public Track Track { get; set; }
      public List<IParticipant> Participants { get; set; }
      public DateTime StartTime { get; set; }
      private Random _random { get; set; }   
      private Dictionary<Section, SectionData> _positions { get; set; } 

      public Race(Track track, List<IParticipant> participants)
      {
         Track = track;
         Participants = participants;

         _random = new Random(DateTime.Now.Millisecond);
      }

      public SectionData GetSectionData(Section currentSection)
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
            int p = participant.equipment.Performance = _random.Next();
            int q = participant.equipment.Quality = _random.Next();
         }
      }
   }
}
