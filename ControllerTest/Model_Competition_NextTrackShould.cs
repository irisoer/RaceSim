using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using NUnit.Framework;

namespace ControllerTest
{
   [TestFixture]
   public class Model_Competition_NextTrackShould
   {
      #region Properties
      private Competition _competition { get; set; } 
      #endregion

      #region Constructors
      public Competition Competition
      {
         get { return _competition; }
         set { _competition = value; }
      } 
      #endregion

      #region Methods
      [SetUp]
      public void SetUp()
      {
         Competition = new Competition();
      }

      #region Tests
      [Test]
      public void Next_Track_EmptyQueue_ReturnNull()
      {
         Track result = Competition.NextTrack();
         Assert.IsNull(result);
      }

      [Test]
      public void NextTrack_OneInQueue_ReturnTrack()
      {
         Track track1 = new Track("Madrid");
         Competition.Tracks.Enqueue(track1);
         Track result = Competition.NextTrack();
         Assert.AreEqual(track1, result);
      }

      [Test]
      public void NextTrack_OneInQueue_RemoveTrackFromQueue()
      {
         Track track2 = new Track("Milan");
         Competition.Tracks.Enqueue(track2);
         Track result = Competition.NextTrack();
         result = Competition.NextTrack();
         Assert.IsNull(result);
      }

      [Test]
      public void NextTrack_TwoInQueue_ReturnNextTrack()
      {
         Track track1 = new Track("Paris");
         Track track2 = new Track("Porto");
         Competition.Tracks.Enqueue(track1);
         Competition.Tracks.Enqueue(track2);
         Track result = Competition.NextTrack();
         Assert.AreEqual(result, track1);
         result = Competition.NextTrack();
         Assert.AreEqual(result, track2);
      } 
      #endregion

      #endregion
   }
}
