using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   #region Enum
   public enum TeamColors
   {
      Red,
      Orange,
      Blue,
      Yellow,
      White,
      Turqoise,

   } 
   #endregion

   public interface IParticipant
   {


      public string Name { get; set; }
      public int Points { get; set; }
      public IEquipment Equipment { get; set; }
      public TeamColors TeamColor { get; set; }
      public LinkedListNode<Section> CurrentSection { get; set; }
      public int Rounds { get; set; }

   }
}
