using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class SectionData
   {
      public IParticipant Left { get; set; }
      public int DistanceLeft { get; set; }
      public IParticipant Right { get; set; }
      public int DistanceRight { get; set; }

      public bool IsFull()
      {
         return Right is not null && Left is not null;
      }


      public int GetParticipantPosition(IParticipant participant)
      {
         if (participant == Left)
         {
            return DistanceLeft;
         }
         else if (participant == Right)
         {
            return DistanceRight;
         }
         else
         {
            throw new Exception("There is no participant!");
         }
      }
   }
}
