﻿using System;
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
      public LinkedList<Section> Sections { get; set; }
      #endregion

      #region Constructors
      public Track(string name, Section.SectionTypes[] sections)
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

      public LinkedList<Section> ConvertSectionsArrayToList(Section.SectionTypes[] sectionsArray)
      {
         LinkedList<Section> sectionsList = new LinkedList<Section>();
         for (int i = 0; i < sectionsArray.Length; i++)
         {
            Section section = new Section(sectionsArray[i]);
            sectionsList.AddLast(section);
         }

         return sectionsList;
      }



      #endregion

   }
}
