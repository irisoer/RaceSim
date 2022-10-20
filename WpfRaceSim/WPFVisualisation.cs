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
      private const string _start = "Media/start.png";
      private const string _straight = "Media/straight.png";
      private const string _finish = "Media/finish.png";
      private const string _cornerRight = "Media/cornerRight.png";
      private const string _cornerLeft = "Media/cornerLeft.png";
      private const string _carBlue = "Media/carBlue.png";
      private const string _carBlueBroken = "Media/carBlueBroken.png";
      private const string _carOrange = "Media/carOrange.png";
      private const string _carOrangeBroken = "Media/carOrangeBroken.png";
      private const string _carPurple = "Media/carPurple.png";
      private const string _carPurpleBroken = "Media/carPurpleBroken.png";
      private const string _carRed = "Media/carRed.png";
      private const string _carRedBroken = "Media/carRedBroken.png";
      private const string _carWhite = "Media/carWhite.png";
      private const string _carWhiteBroken = "Media/carWhiteBroken.png";
      private const string _carYellow = "Media/carYellow.png";
      private const string _carYellowBroken = "Media/carYellowBroken.png";
      #endregion

      private enum Direction
      {
         North, East, South, West
      }
      private const int _size = 100;
      private static int _xpos;
      private static int _ypos; 
      private static int _width;
      private static int _height;

      private static Direction _direction { get; set; }


      public static BitmapSource DrawTrack(Track track)
      {
         PreDrawTrack(track);
         Bitmap emptyImage = Images.CreateBitmap((_width * _size), (_height * _size));
         Bitmap trackImage = PlaceSections(track, emptyImage);
        
         return Images.CreateBitmapSourceFromGdiBitmap(Images.CreateBitmap(300,300));
      }

      public static void PreDrawTrack(Track t)
      {
         int x = 0;
         int y = 0;
         int i = 0;
         int j = 0;
         _direction = Direction.East;
         foreach(Section section in t.Sections)
         {
            if(section.SectionType == SectionTypes.LeftCorner)
            {
               CalculateDirection(section.SectionType);
            }
            else if(section.SectionType == SectionTypes.RightCorner)
            {
               CalculateDirection(section.SectionType);
            }

            CalculateDrawPosition(_direction, x, y);
            if (j < y) { j = y; }
            else if (_ypos > y) { _ypos = y; }
            if (i < x) { i = x; }
            else if (_xpos > x) { _xpos = x; }
         }
         _direction = Direction.East;
         _width = i - _xpos + 1;
         _height = j - _ypos + 1;
      }


      public static Bitmap PlaceSections(Track t, Bitmap bitmap)
      {
         int x = _xpos;
         int y = _ypos;
         Graphics graphics = Graphics.FromImage(bitmap);
         foreach(Section section in t.Sections)
         {
            switch (section.SectionType)
            {
               case SectionTypes.StartGrid:
                  DrawSection(graphics, Images.GetImageOutOfFolder(_start), x, y, _direction);
                  break;
               case SectionTypes.Straight:
                  DrawSection(graphics, Images.GetImageOutOfFolder(_straight), x, y, _direction);
                  break;
               case SectionTypes.Finish:
                  DrawSection(graphics, Images.GetImageOutOfFolder(_finish), x, y, _direction);
                  break;
               case SectionTypes.LeftCorner:
                  DrawSection(graphics, Images.GetImageOutOfFolder(_cornerLeft), x, y, _direction);
                  CalculateDirection(SectionTypes.LeftCorner);
                  break;
               case SectionTypes.RightCorner:
                  DrawSection(graphics, Images.GetImageOutOfFolder(_cornerRight), x, y, _direction);
                  CalculateDirection(SectionTypes.RightCorner);
                  break;
            }

            CalculateDrawPosition(_direction, -_xpos, -_ypos);
         }
         _direction = Direction.East;
         return bitmap;
      }

      private static void DrawSection(Graphics g, Bitmap bitmap, int x, int y, Direction r)                                                                 
      {
         Bitmap bitm = new Bitmap(bitmap);
         switch (r)
         {
            case Direction.North:
               g.DrawImage(bitm, new Point(x * _size, y * _size));
               break;
            case Direction.East:
               bitm.RotateFlip(RotateFlipType.Rotate90FlipNone);
               g.DrawImage(bitm, new Point(x * _size, y * _size));
               break;
            case Direction.South:
               bitm.RotateFlip(RotateFlipType.Rotate180FlipNone);
               g.DrawImage(bitm, new Point(x * _size, y * _size));
               break;
            case Direction.West:
               bitm.RotateFlip(RotateFlipType.Rotate270FlipNone);
               g.DrawImage(bitm, new Point(x * _size, y * _size));
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

      private static void CalculateDrawPosition(Direction direction, int x, int y)
      {
         switch (direction)
         {
            case Direction.North:
               y--;
               break;
            case Direction.East:
               x++;
               break;
            case Direction.South:
               y++;
               break;
            case Direction.West:
               x--;
               break;
         }
      }

   }
}
