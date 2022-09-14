using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   internal class SectionData
   {
      public IParticipant Left;
      public IParticipant Right;
      public int DistanceLeft;
      public int DistanceRight;

      public SectionData(IParticipant left, IParticipant right, int distanceLeft, int distanceRight)
      {
         Left = left;
         Right = right;
         DistanceLeft = distanceLeft;
         DistanceRight = distanceRight;
      }
   }
}
