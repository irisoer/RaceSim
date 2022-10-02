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
         Console.BackgroundColor = ConsoleColor.Black;
         Data.Initialize();
         Visualisation.Initialize(Data.CurrentRace.Track);
        

         for (; ; )
         {
            Thread.Sleep(100);
         }
      }


   }
}