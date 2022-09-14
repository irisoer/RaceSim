using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   internal class Track
   {
      public string Name;
      public LinkedList<Section> Sections;
      public Track(string name)
      {
         Name = name;
         Sections = new LinkedList<Section>();
      }
   }
}
