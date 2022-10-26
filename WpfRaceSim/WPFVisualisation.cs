using Model;
using Controller;
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
      private const string _start = "C:\\Users\\iris_\\source\\repos\\RaceSim\\WpfRaceSim\\Media\\start.png";
      private const string _straight = "C:\\Users\\iris_\\source\\repos\\RaceSim\\WpfRaceSim\\Media\\straight.png";
      private const string _finish = "C:\\Users\\iris_\\source\\repos\\RaceSim\\WpfRaceSim\\Media\\finish.png";
      private const string _cornerRight = "C:\\Users\\iris_\\source\\repos\\RaceSim\\WpfRaceSim\\Media\\cornerRight.png";
      private const string _cornerLeft = "C:\\Users\\iris_\\source\\repos\\RaceSim\\WpfRaceSim\\Media\\cornerLeft.png";
      private const string _carBlue = "C:\\Users\\iris_\\source\\repos\\RaceSim\\WpfRaceSim\\Media\\carBlue.png";
      private const string _carBlueBroken = "C:\\Users\\iris_\\source\\repos\\RaceSim\\WpfRaceSim\\Media\\carBlueBroken.png";
      private const string _carOrange = "C:\\Users\\iris_\\source\\repos\\RaceSim\\WpfRaceSim\\Media\\carOrange.png";
      private const string _carOrangeBroken = "C:\\Users\\iris_\\source\\repos\\RaceSim\\WpfRaceSim\\Media\\carOrangeBroken.png";
      private const string _carPurple = "C:\\Users\\iris_\\source\\repos\\RaceSim\\WpfRaceSim\\Media\\carPurple.png";
      private const string _carPurpleBroken = "C:\\Users\\iris_\\source\\repos\\RaceSim\\WpfRaceSim\\Media\\carPurpleBroken.png";
      private const string _carRed = "C:\\Users\\iris_\\source\\repos\\RaceSim\\WpfRaceSim\\Media\\carRed.png";
      private const string _carRedBroken = "C:\\Users\\iris_\\source\\repos\\RaceSim\\WpfRaceSim\\Media\\carRedBroken.png";
      private const string _carWhite = "C:\\Users\\iris_\\source\\repos\\RaceSim\\WpfRaceSim\\Media\\carWhite.png";
      private const string _carWhiteBroken = "C:\\Users\\iris_\\source\\repos\\RaceSim\\WpfRaceSim\\Media\\carWhiteBroken.png";
      private const string _carYellow = "C:\\Users\\iris_\\source\\repos\\RaceSim\\WpfRaceSim\\Media\\carYellow.png";
      private const string _carYellowBroken = "C:\\Users\\iris_\\source\\repos\\RaceSim\\WpfRaceSim\\Media\\carYellowBroken.png";
      #endregion

      private enum Direction
      {
         North, East, South, West
      }
      private const int _size = 128;
      private static int _xpos;
      private static int _ypos;
      private static int posLeft;
      private static int posRight;  


      private static Direction _direction { get; set; }


      public static BitmapSource DrawTrack(Track track)
      {

         Bitmap emptyImage = Images.CreateBitmap(1920, 1080);

         _xpos = (7 * _size);
         _ypos = 20;
         posLeft = _xpos + 50;
         posRight = _xpos + 100;

         Bitmap trackImage = PlaceSections(track, emptyImage);
         return Images.CreateBitmapSourceFromGdiBitmap(trackImage);
      }

      public static Bitmap PlaceSections(Track t, Bitmap bitmap)
      {
         int x = _xpos;
         int y = _ypos;
         
         Graphics graphics = Graphics.FromImage(bitmap);
         foreach (Section section in t.Sections)
         {
            SectionData sd = Data.CurrentRace.GetSectionData(section);

            switch (section.SectionType) //switchcase which section to draw
            {
               case SectionTypes.StartGrid:
                  DrawDefaults(graphics, Images.GetImageOutOfFolder(_start), x, y, _direction);
                  if (sd.Left is not null)
                  {
                     IParticipant participant = sd.Left;
                     DrawDriver(graphics, participant, sd, x, y, _direction);
                  }
                  if (sd.Right is not null)
                  {
                     IParticipant participant = sd.Right;
                     DrawDriver(graphics, participant, sd, x, y, _direction);
                  }
                  break;
               case SectionTypes.Straight:
                  DrawDefaults(graphics, Images.GetImageOutOfFolder(_straight), x, y, _direction);
                  if (sd.Left is not null)
                  {
                     IParticipant participant = sd.Left;
                     DrawDriver(graphics, participant, sd, x, y, _direction);
                  }
                  if (sd.Right is not null)
                  {
                     IParticipant participant = sd.Right;
                     DrawDriver(graphics, participant, sd, x, y, _direction);
                  }
                  break;
               case SectionTypes.Finish:
                  DrawDefaults(graphics, Images.GetImageOutOfFolder(_finish), x, y, _direction);
                  if (sd.Left != null)
                  {
                     IParticipant participant = sd.Left;
                     DrawDriver(graphics, participant, sd, x, y, _direction);
                  }
                  if (sd.Right != null)
                  {
                     IParticipant participant = sd.Right;
                     DrawDriver(graphics, participant, sd, x, y, _direction);
                  }
                  break;
               case SectionTypes.LeftCorner:
                  DrawDefaults(graphics, Images.GetImageOutOfFolder(_cornerLeft), x, y, _direction);
                  CalculateDirection(SectionTypes.LeftCorner);
                  if (sd.Left != null)
                  {
                     IParticipant participant = sd.Left;
                     DrawDriver(graphics, participant, sd, x, y, _direction);
                  }
                  if (sd.Right != null)
                  {
                     IParticipant participant = sd.Right;
                     DrawDriver(graphics, participant, sd, x, y, _direction);
                  }
                  break;
               case SectionTypes.RightCorner:
                  DrawDefaults(graphics, Images.GetImageOutOfFolder(_cornerRight), x, y, _direction);
                  CalculateDirection(SectionTypes.RightCorner);
                  if (sd.Left != null)
                  {
                     IParticipant participant = sd.Left;
                     DrawDriver(graphics, participant, sd, x, y, _direction);
                  }
                  if (sd.Right != null)
                  {
                     IParticipant participant = sd.Right;
                     DrawDriver(graphics, participant, sd, x, y, _direction);
                  }
                  break;
            }
            switch (_direction)
            {
               case Direction.North:
                  y = y - _size;
                  break;
               case Direction.East:
                  x = x + _size;
                  _xpos = x;
                  break;
               case Direction.South:
                  y = y + _size;
                  break;
               case Direction.West:
                  x = x - _size;
                  _xpos = x;
                  break;
            } // calculate xpos ypos

         }
         _direction = Direction.East;
         return bitmap;
      }

      private static void DrawDriver(Graphics g, IParticipant p, SectionData sd, int x, int y, Direction direction)
      {
         if (p == sd.Left)
         {
            DrawDefaults(g, Images.GetImageOutOfFolder(GetDriverImage(p)), XCorrection(x, direction, "left"), YCorrection(y, direction, "left"), direction);
         }
         if(p == sd.Right)
         {
            DrawDefaults(g, Images.GetImageOutOfFolder(GetDriverImage(p)), XCorrection(x, direction, "right"), YCorrection(y, direction, "right"), direction);
         }
      }

      private static int XCorrection(int x, Direction direction, string side)
      {
         if (side.Equals("left"))
         {
            if (direction == Direction.East || direction == Direction.West)
            {
               x = x + 10;
            }
            else x = x + 80;
         }
         if (side.Equals("right"))
         {
            if (direction == Direction.East || direction == Direction.West)
            {
               //x stays x
            }
            else x = x + 10;
         }
         return x;
      }

      private static int YCorrection(int y, Direction direction, string side)
      {
         if (side.Equals("left"))
         {
            if (direction == Direction.East || direction == Direction.West)
            {
               y = y + 10;
            }
            else y = y + 50;
         }
         if (side.Equals("right"))
         {
            if (direction == Direction.East || direction == Direction.West)
            {
               y = y + 60;
            }
            else y = y + 20;
         }
         return y;
      }


      private static string GetDriverImage(IParticipant driver)
      {
            switch (driver.TeamColor)
            {
               case TeamColors.Purple:
                  if (driver.Equipment.IsBroken == false) { return _carPurple; } else { return _carPurpleBroken; }
               case TeamColors.Red:
                  if (driver.Equipment.IsBroken == false) { return _carRed; } else { return _carRedBroken; }
               case TeamColors.Orange:
                  if (driver.Equipment.IsBroken == false) { return _carOrange; } else { return _carOrangeBroken; }
               case TeamColors.Blue:
                  if(driver.Equipment.IsBroken == false) { return _carBlue; } else { return _carBlueBroken; }
               case TeamColors.White:
                  if(driver.Equipment.IsBroken == false) { return _carWhite; } else { return _carWhiteBroken; }
               case TeamColors.Yellow:
                  if(driver.Equipment.IsBroken == false) { return _carYellow; } else { return _carYellowBroken; }
               default:
               return "";
            } 
      }

      private static void DrawDefaults(Graphics g, Bitmap bitmap, int x, int y, Direction r)
      {
         Bitmap bitm = new Bitmap(bitmap);
         switch (r)
         {
            case Direction.North:
               bitm.RotateFlip(RotateFlipType.Rotate270FlipNone);
               g.DrawImage(bitm, new Point(x, y));
               break;
            case Direction.East:
               g.DrawImage(bitm, new Point(x, y));
               break;
            case Direction.South:
               bitm.RotateFlip(RotateFlipType.Rotate90FlipNone);
               g.DrawImage(bitm, new Point(x, y));
               break;
            case Direction.West:
               bitm.RotateFlip(RotateFlipType.Rotate180FlipNone);
               g.DrawImage(bitm, new Point(x, y));
               break;
         }
      }

      private static void CalculateDirection(SectionTypes sectionType)
      {
         switch (sectionType)
         {
            case SectionTypes.LeftCorner:
               if(_direction == Direction.East) { _direction = Direction.North; break; }
               if(_direction == Direction.North) { _direction = Direction.West; break; }
               if (_direction == Direction.West) { _direction = Direction.South; break; }
               else { _direction = Direction.East; break; }
            case SectionTypes.RightCorner:
               if(_direction == Direction.East) { _direction = Direction.South; break; }
               if(_direction == Direction.North) { _direction = Direction.East; break; }
               if(_direction == Direction.West){_direction = Direction.North; break; }
               else { _direction = Direction.West; break; }
         }
      }
   }
}
