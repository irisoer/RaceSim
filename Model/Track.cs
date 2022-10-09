using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class Track
   {
      #region Properties
      public string Name { get; set; }
      public LinkedList<Section> Sections { get; set; }
      public Section FirstSection { get; set; }
      public Section LastSection { get; set; }
      #endregion

      #region Constructors
      public Track(string name, SectionTypes[] sections)
      {
         if (sections[0] != SectionTypes.StartGrid) throw new ArgumentException($"First section should be a start grid, review track {name}");
         if (sections[1] != SectionTypes.StartGrid) throw new ArgumentException($"Second section should be a start grid, review track {name}");
         if (sections[2] != SectionTypes.Finish) throw new ArgumentException($"Third section should be a finish, review track {name}");

         int countStart = 0;
         foreach(SectionTypes st in sections) //More than 2 startgrids check
         {
            if (st.Equals(SectionTypes.StartGrid))
            {
               countStart++;
            }
            if(countStart > 2)
            {
               throw new ArgumentException($"There can only be 2 startgrids, review track {name}");
            }
         }         
         int countFinish = 0;
         foreach(SectionTypes st in sections) //More than 1 finish check
         {
            if (st.Equals(SectionTypes.Finish))
            {
               countFinish++;
            }
            if(countFinish > 1)
            {
               throw new ArgumentException($"There can only be 1 finish, review track {name}");
            }
         }

         Name = name;
         Sections = ConvertSectionsArrayToList(sections);
      }  
      
      public Track(string name)
      {
         this.Name = name;
      }

      #endregion

      #region Methods

      public LinkedList<Section> ConvertSectionsArrayToList(SectionTypes[] sectionsArray)
      {
         LinkedList<Section> sectionsList = new LinkedList<Section>();
         for (int i = 0; i < sectionsArray.Length; i++)
         {
            Section section = new Section(sectionsArray[i]);
            sectionsList.AddLast(section);
            if(i == 0)
            {
               FirstSection = section;
            }
            if(i == sectionsArray.Length - 1)
            {
               LastSection = section;
            }
         }

         return sectionsList;
      }



      #endregion

   }
}
