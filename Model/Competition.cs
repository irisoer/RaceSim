using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class Competition
   {
      #region Properties
      public List<IParticipant> Participants { get; set; }
      public string Winner { get; set; }
      public Queue<Track> Tracks { get; set; }
      #endregion

      #region Constructors
      public Competition()
      {
         Participants = new List<IParticipant>();
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
