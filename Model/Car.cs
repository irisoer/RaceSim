﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class Car : IEquipment
   {
      #region Properties
      public int Quality { get; set; }
      public int Performance { get; set; }
      public int Speed { get; set; }
      public bool IsBroken { get; set; }
      #endregion

      #region Constructors
      public Car(int quality, int performance, int speed)
      {
         Quality = quality;
         Performance = performance;
         Speed = speed;
         IsBroken = false;
      } 

      #endregion
   }
}
