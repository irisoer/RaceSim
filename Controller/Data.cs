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
         Competition.Participants.Add(new Driver("Lola", new Car(10, 10, 10), TeamColors.Blue));
         Competition.Participants.Add(new Driver("Abel", new Car(10, 10, 10), TeamColors.Red));
         Competition.Participants.Add(new Driver("Marco", new Car(10, 10, 10), TeamColors.Yellow));
         Competition.Participants.Add(new Driver("Perry", new Car(10, 10, 10), TeamColors.Turqoise));
         Competition.Participants.Add(new Driver("Willem", new Car(10, 10, 10), TeamColors.Orange));
         Competition.Participants.Add(new Driver("Elizabeth", new Car(10, 10, 10), TeamColors.White));
      }

      public static void AddTracks()
      {
         #region Zandvoort
         Competition.Tracks.Enqueue(new Track("Zandvoort", new[]
            {
            SectionTypes.StartGrid,
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
      } 
      #endregion


   }
}
