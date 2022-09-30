using Model;
using Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Versioning;

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
      public static Direction _direction;
      public static Direction _newDirection; 
      public static Race _currentRace;
      public static SectionTypes _lastSection;
      public static int XPos = 40;
      public static int YPos = 20;

      //start position

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


      private static string[] _finishHorizontal = {
         "───────",
         "░      ",
         "░      ",
         "───────"
      };
      private static string[] _finishVertical = { 
         "│     │",
         "│░░░░░│",
         "│░░░░░│", 
         "│     │" };
      private static string[] _startHorizontal = {
         "───────",
         " [ ]   ",
         "   [ ] ",
         "───────"};
      private static string[] _startVertical = { 
         "│  │", 
         " [] ", 
         " [] ", 
         "│  │" };
      private static string[] _straightHorizontal = {
         "───────", 
         "       ", 
         "       ",
         "───────"};
      private static string[] _straightVertical = { 
         "│     │", 
         "│     │", 
         "│     │", 
         "│     │" };
      private static string[] _cornerRightDown = {
         "──────┐", 
         "      │", 
         "      │", 
         "┐     │"};
      private static string[] _cornerDownLeft = { 
         "┘     │", 
         "      │", 
         "      │",
         "──────┘"};
      private static string[] _cornerLeftUp = {
         "│     └", 
         "│      ", 
         "│      ",
         "└──────" };
      private static string[] _cornerRightUp = {
         "┌──────", 
         "│      ", 
         "│      ",
         "│     ┌"};
      #endregion


      public static void Initialize(Track track)
      {
         _direction = Direction.East;
         Console.CursorVisible = false;
         DrawTrack(track);
        }

      public static void DrawTrack(Track t)
      {
         foreach (Sections section in t.Sections)
         {
            Console.CursorLeft = XPos;
            Console.CursorTop = YPos;
            #region SwitchCaseSectionTypes
            switch (section.SectionType)
            {
               #region StartGrid
               case SectionTypes.StartGrid:
                  _lastSection = SectionTypes.StartGrid;
                  if (_direction == Direction.East || _direction == Direction.West)
                  {
                     foreach (var line in _startHorizontal)
                     {
                        Console.CursorLeft = XPos;
                        Console.WriteLine($"{line}");
                     }
                  }
                  else
                  {
                     foreach (var line in _startVertical)
                     {
                        Console.CursorLeft = XPos;
                        Console.WriteLine($"{line}");
                     }
                  }
                  
                  break;
               #endregion
               #region Straight
               case SectionTypes.Straight:
                  _lastSection = SectionTypes.Straight;
                  if (_direction == Direction.East || _direction == Direction.West)
                  {
                     foreach (var line in _straightHorizontal)
                     {
                        Console.CursorLeft = XPos;
                        Console.WriteLine($"{line}");
                     }
                  }
                  else
                  {
                     foreach (var line in _straightVertical)
                     {
                        Console.CursorLeft = XPos;
                        Console.WriteLine($"{line}");
                     }
                  }
                  break;
               #endregion
               #region LeftCorner
               case SectionTypes.LeftCorner:
                  _lastSection = SectionTypes.LeftCorner;
                  if (_direction == Direction.East)
                  {
                     foreach (var line in _cornerDownLeft)
                     {
                        Console.CursorLeft = XPos;
                        
                        Console.WriteLine($"{line}");
                     }
                     _direction = Direction.North;
                  }
                  else if (_direction == Direction.North)
                  {
                     foreach (var line in _cornerRightDown)
                     {
                        Console.CursorLeft = XPos;
                        Console.WriteLine($"{line}");
                     }
                     _direction = Direction.West;
                  }
                  else if (_direction == Direction.West)
                  {
                     foreach (var line in _cornerRightUp)
                     {
                        Console.CursorLeft = XPos;
                        Console.WriteLine($"{line}");
                     }
                     _direction = Direction.South;
                  }
                  else //south
                  {
                     foreach (var line in _cornerLeftUp)
                     {
                        Console.CursorLeft = XPos;
                        Console.WriteLine($"{line}");
                     }
                     _direction = Direction.East;
                  }
                  break;
               #endregion
               #region RightCorner
               case SectionTypes.RightCorner:
                  _lastSection = SectionTypes.RightCorner;
                  if (_direction == Direction.East)
                  {
                     foreach (var line in _cornerRightDown)
                     {
                        Console.CursorLeft = XPos;
                        Console.WriteLine($"{line}");
                     }
                     _direction = Direction.South;
                     
                  }
                  else if (_direction == Direction.West)
                  {
                     foreach (var line in _cornerLeftUp)
                     {
                        Console.CursorLeft = XPos;
                        Console.WriteLine($"{line}");
                     }
                     _direction = Direction.North;
                     
                  }
                  else if (_direction == Direction.North)
                  {
                     foreach (var line in _cornerRightUp)
                     {
                        Console.CursorLeft = XPos;
                        Console.WriteLine($"{line}");
                     }
                     _direction = Direction.East;
                     
                  }
                  else if (_direction == Direction.South)
                  {
                     foreach (var line in _cornerDownLeft)
                     {
                        Console.CursorLeft = XPos;
                        Console.WriteLine($"{line}");
                     }
                     _direction = Direction.West;
                    
                  }
                  break;
               #endregion
               #region Finish
               case SectionTypes.Finish:
                  _lastSection = SectionTypes.Finish; 
                  if (_direction == Direction.East || _direction == Direction.West)
                  {
                     foreach (var line in _finishHorizontal)
                     {
                        Console.CursorLeft = XPos;
                        Console.WriteLine($"{line}");
                     }
                  }
                  else
                  {
                     foreach (var line in _finishVertical)
                     {
                        Console.CursorLeft = XPos;
                        Console.WriteLine($"{line}");
                     }
                  }
                  break;
                  #endregion
                  
            }
            #endregion
            CalculateCursurPosition();
         }
         
      }

      public static void CalculateCursurPosition()
      {
            if (_direction == Direction.East)
            {
               XPos=XPos +7;
            }
            if (_direction == Direction.West)
            {
               XPos=XPos-7;
            }
            if (_direction == Direction.North)
            {
               
                  YPos = YPos - 4;
               
            }
            if (_direction == Direction.South)
            {
               
               YPos = YPos + 4;
           

            }
      }


   }
}
