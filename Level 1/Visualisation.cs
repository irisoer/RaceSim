using Model;
using Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceSim
{
   public enum Direction
   {
      North,
      East,
      South,
      West
   }
   public static class Visualisation
   {
      #region Graphics
      #region SymbolNumbers
      /*Numlock numbers
         * ♥ 3
         * ♦ 4
         * ░ 176
         * │ 179
         * ┐ 191
         * └ 192
         * ─ 196
         * ┘ 217
         * ┌ 218
      * */ 
      #endregion


      private static string[] _finishHorizontal = { "────", "░░♥░", "░░♦░", "────" };
      private static string[] _finishVertical = { "│  │", "│░░│",  "│░░│", "│  │" };
      private static string[] _startHorizontal = { "────", "[ ] ", " [ ]", "────"};
      private static string[] _startVertical = { "│  │", " [] ", " [] ", "│  │" };
      private static string[] _straightHorizontal = {"────", "    ", "    ", "────"};
      private static string[] _straightVertical = { "│  │", "│  │", "│  │", "│  │" };
      private static string[] _cornerRightDown = {"───┐", "   │", "   │", "┐  │"};
      private static string[] _cornerDownLeft = { "┘  │", "   │", "   │", "───┘"};
      private static string[] _cornerLeftUp = {"│  └", "│   ", "│   ", "└───" };
      private static string[] _cornerUpRight = {"│  ┌", "│   ", "│   ", "┌───"};
      #endregion


      public static void Initialize()
      {

      }

      public static void DrawTrack(Track t)
      {

      }

   }
}
