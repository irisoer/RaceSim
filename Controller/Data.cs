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
      public static Competition competition { get; set; }
      public static Race CurrentRace { get; set; }

      public static void Initialize()
      {
         competition = new Competition();
         addParticipants();
         addTracks();
      }

      public static void addParticipants()
      {
         competition.Participants.Add(new Driver("Lola", new Car(10, 10, 10), IParticipant.TeamColors.Blue));
         competition.Participants.Add(new Driver("Abel", new Car(10, 10, 10), IParticipant.TeamColors.Red));
         competition.Participants.Add(new Driver("Marco", new Car(10, 10, 10), IParticipant.TeamColors.Yellow));

      }

      public static void addTracks()
      {
         competition.Tracks.Enqueue(new Track("Zandvoort"));
         competition.Tracks.Enqueue(new Track("Assen"));
      }

      public static void NextRace()
      {
         Track newTrack = competition.NextTrack();
         if(newTrack != null)
         {
            CurrentRace = new Race(newTrack, competition.Participants);
         }
         else
         {
            CurrentRace = null;
         }
      }
      
      
   }
}
