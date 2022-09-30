using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class Track
   {
      #region Properties
      public string Name { get; set; }
      public LinkedList<Sections> Sections { get; set; }
      #endregion

      #region Constructors
      public Track(string name, SectionTypes[] sections)
      {
         Name = name;
         Sections = ConvertSectionsArrayToList(sections);
      }      
      
      public Track(string name)
      {
         Name = name;
      }
      #endregion

      #region Methods

      public LinkedList<Sections> ConvertSectionsArrayToList(SectionTypes[] sectionsArray)
      {
         LinkedList<Sections> sectionsList = new LinkedList<Sections>();
         for (int i = 0; i < sectionsArray.Length; i++)
         {
            Sections section = new Sections(sectionsArray[i]);
            sectionsList.AddLast(section);
         }

         return sectionsList;
      }



      #endregion

   }
}
