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
      public Track Track;
      public List<IParticipant> Participants;
      public DateTime StartTime;
      private Random _random;
      private Dictionary<Section, SectionData> _positions;

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
