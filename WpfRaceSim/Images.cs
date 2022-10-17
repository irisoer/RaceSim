using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfRaceSim
{
   static class Images
   {
      private static Dictionary<string, Bitmap> _imageDictionary { get; set; }

      public static Bitmap GetImageOutOfFolder(string url)
      {
         if (_imageDictionary[url] == null)
         {
            Bitmap bitmap = new Bitmap(url);
            _imageDictionary.Add(url, bitmap);
            return bitmap;
         }
         else return _imageDictionary[url];
      }

      public static void ClearImageDictionary()
      {
         _imageDictionary.Clear();
      }

      public static Bitmap CreateBitmap(int x, int y)
      {
         string key = "empty";
         if (!_imageDictionary.ContainsKey(key))
         {
            _imageDictionary.Add(key, new Bitmap(x, y));
            Graphics g = Graphics.FromImage(_imageDictionary[key]);
            g.FillRectangle(new SolidBrush(System.Drawing.Color.AliceBlue), 0, 0, x, y);
         }
         return _imageDictionary[key];
      }
   }
}
