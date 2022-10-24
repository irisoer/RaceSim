using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class Competition
   {
      #region Properties
      public List<IParticipant> Participants { get; set; }
      public Dictionary<IParticipant, int> CompetitionStats { get; set; }
      public Queue<Track> Tracks { get; set; }
      #endregion

      #region Constructors
      public Competition()
      {
         Participants = new List<IParticipant>();
         CompetitionStats = new Dictionary<IParticipant, int>();
         Tracks = new Queue<Track>();
      } 
      #endregion
      #region Methods
      public Track NextTrack()
      {
         if (Tracks.Count > 0)
         {
            return Tracks.Dequeue();
         }
         else
         {
            return null;
         }

      } 


      #endregion
   }
}
