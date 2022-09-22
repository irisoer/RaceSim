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
      Green,
      Blue,
      Yellow,
      Grey
   } 
   #endregion

   public interface IParticipant
   {


      public string Name { get; set; }
      public int Points { get; set; }
      public IEquipment Equipment { get; set; }
      public TeamColors TeamColor { get; set; }

   }
}
