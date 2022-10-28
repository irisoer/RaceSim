using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Drawing;
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
      public static bool RacesOver{ get; set; }
      #endregion

      #region Methods
      public static void Initialize()
      {
         Competition = new Competition();
         RacesOver = false;
         AddParticipants();
         AddTracks();
         if (Competition.Tracks.Count < 2) //check if there are at least 2 tracks in competition
         {
            throw new ArgumentException("There must be at least 2 tracks in a competition");
         }
         NextRace();
      }

      public static void AddParticipants()
      {
         Competition.Participants.Add(new Driver("Lola", TeamColors.Blue));
         Competition.Participants.Add(new Driver("Abel", TeamColors.Red));
         Competition.Participants.Add(new Driver("Marco", TeamColors.Yellow));
         Competition.Participants.Add(new Driver("Perry", TeamColors.White));
      }

      public static void AddTracks()
      {
         #region Zandvoort
         Competition.Tracks.Enqueue(new Track("Zandvoort", 2, new[]
            {
            SectionTypes.StartGrid,
            SectionTypes.StartGrid,
            SectionTypes.Finish,
            SectionTypes.RightCorner,
            SectionTypes.Straight,
            SectionTypes.Straight,
            SectionTypes.Straight,
            SectionTypes.RightCorner,
            SectionTypes.Straight,
            SectionTypes.Straight,
            SectionTypes.Straight,
            SectionTypes.Straight,
            SectionTypes.LeftCorner,
            SectionTypes.Straight,
            SectionTypes.LeftCorner,
            SectionTypes.Straight,
            SectionTypes.Straight,
            SectionTypes.Straight,
            SectionTypes.Straight,
            SectionTypes.RightCorner,
            SectionTypes.RightCorner,
            SectionTypes.Straight,
            SectionTypes.Straight,
            SectionTypes.Straight,
            SectionTypes.Straight,
            SectionTypes.Straight,
            SectionTypes.RightCorner,
            SectionTypes.Straight,
            SectionTypes.Straight,
            SectionTypes.Straight,
            SectionTypes.RightCorner,
            SectionTypes.Straight,
            SectionTypes.Straight,
            SectionTypes.Straight,
            SectionTypes.Straight,
            SectionTypes.LeftCorner,
            SectionTypes.Straight,
            SectionTypes.LeftCorner,
            SectionTypes.Straight,
            SectionTypes.Straight,
            SectionTypes.Straight,
            SectionTypes.Straight,
            SectionTypes.RightCorner,
            SectionTypes.RightCorner,
            SectionTypes.Straight,
            SectionTypes.Straight
         }));
         
         #endregion
         #region Assen
         Competition.Tracks.Enqueue(new Track("Assen", 3, new[]
         {
            SectionTypes.StartGrid,
            SectionTypes.StartGrid,
            SectionTypes.Finish,
            SectionTypes.RightCorner,
            SectionTypes.Straight,
            SectionTypes.Straight,
            SectionTypes.Straight,
            SectionTypes.Straight,
            SectionTypes.Straight,
            SectionTypes.Straight,
            SectionTypes.RightCorner,
            SectionTypes.RightCorner,
            SectionTypes.Straight,
            SectionTypes.Straight,
            SectionTypes.LeftCorner,
            SectionTypes.Straight,
            SectionTypes.Straight,
            SectionTypes.Straight,
            SectionTypes.LeftCorner,
            SectionTypes.Straight,
            SectionTypes.Straight,
            SectionTypes.RightCorner,
            SectionTypes.RightCorner,
            SectionTypes.Straight,
            SectionTypes.Straight,
            SectionTypes.Straight,
            SectionTypes.Straight,
            SectionTypes.Straight,
            SectionTypes.Straight,
            SectionTypes.RightCorner,
            SectionTypes.Straight,
            SectionTypes.Straight
         })); ;
         #endregion

      }

      public static void NextRace()
      {
         Track newTrack = Data.Competition.NextTrack();
         if (newTrack != null)
         {
            CurrentRace = new Race(newTrack, Competition.Participants);
            CurrentRace.Start();
         }
         else
         {
            RacesOver = true; 
            CalculateWinner();
            Console.WriteLine("No more races");
         }
      }

      public static string PeekInTrackQueue()
      {
         try
         {
            return Competition.Tracks.Peek().Name.ToString();

         } catch (System.InvalidOperationException) { 
            return "no tracks left";
         }
      }

      public static void CalculateWinner()
      {
         int highestpoints = 0;
         IParticipant participant1 = null;
         foreach(var participant in Competition.Participants)
         {
            if (participant.Points > highestpoints)
            {
               highestpoints = participant.Points;
               participant1 = participant;   
            }
         }
         Competition.Winner = $"The winner is {participant1.Name} from team {participant1.TeamColor}";
      }
      #endregion


   }
}
