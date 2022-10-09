using Model;
using Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Versioning;
using System.Diagnostics;

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
      public static Direction _direction { get; set; } 
      public static Direction _newDirection { get; set; }
      public static Race _currentRace { get; set; }
      public static SectionTypes _lastSection { get; set; }
      public static int XPos = 40; 
      public static int YPos = 2;


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
         "░ 2    ",
         "░   1  ",
         "───────"
      };
      private static string[] _finishVertical = { 
         "│     │",
         "│░░░░░│",
         "│ 2   │", 
         "│   1 │" };
      private static string[] _startHorizontal = { //misschien 1 en 2 omdraaien ivm logica 
         "───────",
         " [2]   ",
         "   [1] ",
         "───────"};
      private static string[] _startVertical = { 
         "│     │",
         "│  [2]│",
         "│[1]  │", 
         "│     │" };
      private static string[] _straightHorizontal = {
         "───────", 
         "  2    ", 
         "    1  ",
         "───────"};
      private static string[] _straightVertical = { 
         "│     │", 
         "│ 2   │", 
         "│   1 │", 
         "│     │" };
      private static string[] _cornerRightDown = {
         "──────┐", 
         "  2   │", 
         "    1 │", 
         "┐     │"};
      private static string[] _cornerDownLeft = { 
         "┘ 2   │", 
         "   1  │", 
         "      │",
         "──────┘"};
      private static string[] _cornerLeftUp = {
         "│     └", 
         "│  2   ", 
         "│    1 ",
         "└──────" };
      private static string[] _cornerRightUp = {
         "┌──────", 
         "│ 2    ", 
         "│   1  ",
         "│     ┌"};
      #endregion


      public static void Initialize()
      {
         _direction = Direction.East;
         Console.CursorVisible = false;
         Data.CurrentRace.DriversChanged += DriversChangedEventHandlerMethod;
      }

      public static void DrawTrack(Track t)
      {
         Console.CursorLeft = 30;
         Console.CursorTop = 1;
         Console.WriteLine($"The next Track in this competition is: {t.Name}");
         foreach (Section section in t.Sections)
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
                     DrawSection(_startHorizontal, section);
                  }
                  else
                  {
                     DrawSection(_startVertical, section);
                  }
                  
                  break;
               #endregion
               #region Straight
               case SectionTypes.Straight:
                  _lastSection = SectionTypes.Straight;
                  if (_direction == Direction.East || _direction == Direction.West)
                  {
                     DrawSection(_straightHorizontal, section);
                  }
                  else
                  {
                     DrawSection(_straightVertical, section);
                  }
                  break;
               #endregion
               #region LeftCorner
               case SectionTypes.LeftCorner:
                  _lastSection = SectionTypes.LeftCorner;
                  if (_direction == Direction.East)
                  {
                      DrawSection(_cornerDownLeft, section);
                    
                     _direction = Direction.North;
                  }
                  else if (_direction == Direction.North)
                  {
                     DrawSection(_cornerRightDown, section);
                     
                     _direction = Direction.West;
                  }
                  else if (_direction == Direction.West)
                  {
                     DrawSection(_cornerRightUp, section);
                     _direction = Direction.South;
                  }
                  else //south
                  {
                     DrawSection(_cornerLeftUp, section);
                     _direction = Direction.East;
                  }
                  break;
               #endregion
               #region RightCorner
               case SectionTypes.RightCorner:
                  _lastSection = SectionTypes.RightCorner;
                  if (_direction == Direction.East)
                  {
                     DrawSection(_cornerRightDown, section);
                     _direction = Direction.South;
                     
                  }
                  else if (_direction == Direction.West)
                  {
                     DrawSection(_cornerLeftUp, section);
                     _direction = Direction.North;
                     
                  }
                  else if (_direction == Direction.North)
                  {
                     DrawSection(_cornerRightUp, section);
                     _direction = Direction.East;
                     
                  }
                  else if (_direction == Direction.South)
                  {
                     DrawSection(_cornerDownLeft, section);
                     _direction = Direction.West;
                    
                  }
                  break;
               #endregion
               #region Finish
               case SectionTypes.Finish:
                  _lastSection = SectionTypes.Finish; 
                  if (_direction == Direction.East || _direction == Direction.West)
                  {
                     DrawSection(_finishHorizontal, section);
                  }
                  else
                  {
                     DrawSection(_finishVertical, section);
                  }
                  break;
                  #endregion
                  
            }
            #endregion
            CalculateCursurPosition();
         }
         
      }

      public static void DrawSection(string[] sectionStrings, Section section)
      {
         IParticipant left, right; 
         foreach(string s in sectionStrings)
         {
            Console.CursorLeft = XPos;
            string ss = s;
            left = Data.CurrentRace.GetSectionData(section).Left;
            right = Data.CurrentRace.GetSectionData(section).Right;
            ss = PlaceParticipants(s, left, right);
            Console.WriteLine(ss);
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

      public static string PlaceParticipants(string letter, IParticipant left, IParticipant right)
      {
         if( left != null)
         {
            letter = letter.Replace("2", left.Name.Substring(0, 1));
         }
         else
         {
            letter = letter.Replace("2", " ");
         }

         if( right != null)
         {
            letter = letter.Replace("1", right.Name.Substring(0, 1));
         }
         else
         {
            letter = letter.Replace("1", " ");
         }

         return letter; 
      }

      public static void DriversChangedEventHandlerMethod(object? obj,DriversChangedEventArgs e)
      {
         DrawTrack(e.Track);
      }


   }
}
