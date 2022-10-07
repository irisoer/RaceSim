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
         NextRace();
      }

      public static void AddParticipants()
      {
         Competition.Participants.Add(new Driver("Lola", TeamColors.Blue));
         Competition.Participants.Add(new Driver("Abel", TeamColors.Red));
         Competition.Participants.Add(new Driver("Marco", TeamColors.Yellow));
         Competition.Participants.Add(new Driver("Perry", TeamColors.Turqoise));
      }

      public static void AddTracks()
      {
         #region Zandvoort
         Competition.Tracks.Enqueue(new Track("Zandvoort", new[]
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
         Competition.Tracks.Enqueue(new Track("Assen", new[]
         {
            SectionTypes.StartGrid,
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
            SectionTypes.Straight
         }));
         #endregion
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
            Console.WriteLine("No more races");
         }

      } 
      #endregion


   }
}
