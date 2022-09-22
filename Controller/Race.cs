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
      #region Properties
      public Track Track { get; set; }
      public List<IParticipant> Participants { get; set; }
      public DateTime StartTime { get; set; }
      private Random _random;
      private Dictionary<Section, SectionData> _positions;
      public Random Random { get; set; }
      public Dictionary<Section, SectionData> Positions { get; set; }
      #endregion

      #region Constructors
      public Race(Track track, List<IParticipant> participants)
      {
         Track = track;
         Participants = participants;

         _random = new Random(DateTime.Now.Millisecond);
      }
      #endregion

      #region Methods
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
            int p = participant.Equipment.Performance = _random.Next();
            int q = participant.Equipment.Quality = _random.Next();
         }
      } 
      #endregion
   }
}
