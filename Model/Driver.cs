using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class Driver : IParticipant
   {
      public Driver(string name, IEquipment equipment, IParticipant.TeamColors teamcolor)
      {
         Name = name;
         Points = 0;
         equipment = equipment; 
         TeamColor = teamcolor; 

      }
      public string Name { get; set; }
      public int Points { get; set; }
      public IEquipment equipment { get; set; }
      public IParticipant.TeamColors TeamColor { get; set; }
   }
}
