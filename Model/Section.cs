using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{      public enum SectionTypes
      {
         Straight,
         LeftCorner,
         RightCorner,
         StartGrid,
         Finish
      }
   public class Sections
   {


      public SectionTypes SectionType { get; set; }

      public Sections(SectionTypes section)
      {
         SectionType = section;
      }
   }
}
