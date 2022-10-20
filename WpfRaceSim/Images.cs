﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfRaceSim
{
   static class Images
   {
      private static Dictionary<string, Bitmap> _imageDictionary = new Dictionary<string, Bitmap>();

      public static Bitmap GetImageOutOfFolder(string url)
      {
         if (_imageDictionary.ContainsKey(url))
         {
            return _imageDictionary[url];
         }
         else { 
            Bitmap bitmap = new Bitmap(url);
            _imageDictionary.Add(url, bitmap);
            return (Bitmap)bitmap.Clone(); 
         }
      }

      public static void ClearImageDictionary()
      {
         _imageDictionary?.Clear();
      }

      public static Bitmap CreateBitmap(int x, int y)
      {
         Bitmap returnBitmap;
         string key = "empty";
         if (_imageDictionary.ContainsKey(key))
         {
            return (Bitmap)GetImageOutOfFolder(key).Clone();
         }
         returnBitmap = new Bitmap(x, y);
         Graphics g = Graphics.FromImage(returnBitmap);
         SolidBrush solidbrush = new SolidBrush(System.Drawing.Color.AliceBlue);
         g.FillRectangle(solidbrush, 0, 0, x, y);
         _imageDictionary.Add(key, returnBitmap);
         return (Bitmap)_imageDictionary[key].Clone();
      }

      public static BitmapSource CreateBitmapSourceFromGdiBitmap(Bitmap bitmap) //converts bitmap to bitmapsource
      {
         if (bitmap == null)
            throw new ArgumentNullException("bitmap");

         var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

         var bitmapData = bitmap.LockBits(
             rect,
             ImageLockMode.ReadWrite,
             System.Drawing.Imaging.PixelFormat.Format32bppArgb);

         try
         {
            var size = (rect.Width * rect.Height) * 4;

            return BitmapSource.Create(
                bitmap.Width,
                bitmap.Height,
                bitmap.HorizontalResolution,
                bitmap.VerticalResolution,
                PixelFormats.Bgra32,
                null,
                bitmapData.Scan0,
                size,
                bitmapData.Stride);
         }
         finally
         {
            bitmap.UnlockBits(bitmapData);
         }
      }

   }
}
