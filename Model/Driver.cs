using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class Driver : IParticipant
   {
      #region Constructor
      public Driver(string name, TeamColors teamcolor)
      {
         Name = name;
         Points = 0;
         TeamColor = teamcolor;

      }
      #endregion
      #region Get-Set
      public string Name { get; set; }
      public int Points { get; set; }
      public IEquipment Equipment { get; set; }
      public TeamColors TeamColor { get; set; }
      public LinkedListNode<Section> CurrentSection { get; set; }

      #endregion
   }
}
