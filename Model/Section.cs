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
   public class Section
   {

      public SectionTypes SectionType { get; set; }
      public static int SectionLength = 100;

      public Section(SectionTypes sectionType)
      {
         SectionType = sectionType;
      }


      public override bool Equals(object? obj)
      {
         if(obj is SectionTypes objSectionType)
            return SectionType.Equals(objSectionType);
         if (obj is Section objSection)
            return SectionType.Equals(objSection.SectionType);
         return false;
      }

   }
}
