using Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;
using Track = Model.Track;

namespace WpfRaceSim
{
   static class WPFVisualisation
   {

      #region Graphics
      private static readonly string _startHorizontal = ".\\Images\\startHorizontal.png";
      private static readonly string _straightHorizontal = ".\\Images\\straightHorizontal.png";
      private static readonly string _straightVertical = ".\\Images\\straightVertical.png";
      private static readonly string _finishHorizontal = ".\\Images\\finishHorizontal.png";
      private static readonly string _cornerRightUp = ".\\Images\\cornerRightUp.png";
      private static readonly string _cornerRightDown = ".\\Images\\cornerRightDown.png";
      private static readonly string _cornerLeftUp = ".\\Images\\cornerLeftUp.png";
      private static readonly string _cornerDownLeft = ".\\Images\\cornerDownLeft.png";
      private static readonly string _carBlue = ".\\Images\\carBlue.png";
      private static readonly string _carBlueBroken = ".\\Images\\carBlueBroken.png";
      private static readonly string _carOrange = ".\\Images\\carOrange.png";
      private static readonly string _carOrangeBroken = ".\\Images\\carOrangeBroken.png";
      private static readonly string _carPurple = ".\\Images\\carPurple.png";
      private static readonly string _carPurpleBroken = ".\\Images\\carPurpleBroken.png";
      private static readonly string _carRed = ".\\Images\\carRed.png";
      private static readonly string _carRedBroken = ".\\Images\\carRedBroken.png";
      private static readonly string _carWhite = ".\\Images\\carWhite.png";
      private static readonly string _carWhiteBroken = ".\\Images\\carWhiteBroken.png";
      private static readonly string _carYellow = ".\\Images\\carYellow.png";
      private static readonly string _carYellowBroken = ".\\Images\\carYellowBroken.png"; 
      #endregion

      public static BitmapSource DrawTrack(Track track)
      {
         Bitmap bitmap = null;
         return Images.CreateBitmapSourceFromGdiBitmap(bitmap);
      }
   }
}
