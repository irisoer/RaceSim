using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   enum SectionTypes
   {
      Straight,
      LeftCorner,
      RightCorner,
      StartGrid,
      Finish
   }
   internal class Section
   {
      public SectionTypes SectionType;
      public Section(SectionTypes sectionType)
      {
         SectionType = sectionType;
      }
   }
}
