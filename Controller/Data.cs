using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Controller
{
   public static class Data
   {
      #region Properties
      public static Competition Competition { get; set; }
      public static Race CurrentRace { get; set; }
      #endregion

      #region Methods
      public static void Initialize()
      {
         Competition = new Competition();
         AddParticipants();
         AddTracks();
      }

      public static void AddParticipants()
      {
         Competition.Participants.Add(new Driver("Lola", new Car(10, 10, 10), TeamColors.Blue));
         Competition.Participants.Add(new Driver("Abel", new Car(10, 10, 10), TeamColors.Red));
         Competition.Participants.Add(new Driver("Marco", new Car(10, 10, 10), TeamColors.Yellow));

      }

      public static void AddTracks()
      {
         Competition.Tracks.Enqueue(new Track("Zandvoort"));
         Competition.Tracks.Enqueue(new Track("Assen"));
      }

      public static void NextRace()
      {
         Track newTrack = Competition.NextTrack();
         if (newTrack != null)
         {
            CurrentRace = new Race(newTrack, Competition.Participants);
         }
         else
         {
            CurrentRace = null;
         }
      } 
      #endregion


   }
}
