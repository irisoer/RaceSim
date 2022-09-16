using System;
using System.Threading;
using System.Threading.Channels;
using Model;
using Controller;

namespace RaceSimSolution
{
   class Program
   {
      static void Main(string[] args)
      {
         Data.Initialize();
         Data.NextRace();
         Console.WriteLine(Data.CurrentRace.Track.Name);

         for (; ; )
         {
            Thread.Sleep(100);
         }
      }


   }
}