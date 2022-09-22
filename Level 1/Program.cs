using System;
using System.Threading;
using System.Threading.Channels;
using Model;
using Controller;

namespace RaceSim
{
   class Program
   {
      static void Main(string[] args)
      {
         Data.Initialize();
         Data.NextRace();
         Console.WriteLine(Visualisation.DrawTrack);

         for (; ; )
         {
            Thread.Sleep(100);
         }
      }


   }
}