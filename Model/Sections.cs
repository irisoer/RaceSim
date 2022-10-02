using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

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

      public Sections(SectionTypes sectionType)
      {
         SectionType = sectionType;
      }


      public override bool Equals(object? obj)
      {
         if(obj is SectionTypes objSectionType)
            return SectionType.Equals(objSectionType);
         if (obj is Sections objSection)
            return SectionType.Equals(objSection.SectionType);
         return false;
      }

   }
}
