using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{

   public class Section
   {
      public enum SectionTypes
      {
         Straight,
         LeftCorner,
         RightCorner,
         StartGrid,
         Finish
      }

      public SectionTypes SectionType { get; set; }

      public Section(SectionTypes section)
      {
         SectionType = section;
      }
   }
}
