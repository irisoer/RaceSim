using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   enum TeamColors
   {
      Red,
      Green,
      Blue,
      Yellow,
      Grey
   }
   internal interface IParticipant
   {
      public String Name { get; set; }
      public int Points { get; set; }
      public IEquipment equipment { get; set; }
      public TeamColors TeamColor { get; set; }

   }
}
