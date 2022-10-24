using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
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
         AddParticipantsToCompetitionStats();
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
         Competition.Participants.Add(new Driver("Perry", TeamColors.Purple));
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
         }
         else
         {
            CurrentRace = null;
            Console.WriteLine("No more races");

         }
         CurrentRace.Start();

      }


      public static void AddParticipantsToCompetitionStats()
      {
         foreach (var participant in Competition.Participants)
         {
            Competition.CompetitionStats.Add(participant, 0);
         }
      }

      #endregion


   }
}
