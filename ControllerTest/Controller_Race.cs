using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerTest
{
   [TestFixture]
   public class Controller_Race
   {
      private Race _testRace { get; set; }
      private Track track { get; set; }
      private List<IParticipant> Participants { get; set; }

      [SetUp]
      public void SetUp()
      {
      }

      [Test]
      public void GetSectionData_ReturnsData()
      {
         track = new Track("test", 2,new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.Finish });
         Participants = new List<IParticipant>();
         Participants.Add(new Driver("Lola", TeamColors.Blue));
         Participants.Add(new Driver("Thijmen", TeamColors.Yellow));
         Participants.Add(new Driver("Joost", TeamColors.White));
         _testRace = new Race(track, Participants);
         Section section = _testRace.Track.FirstSection;
         SectionData data = _testRace.GetSectionData(section);
         Assert.NotNull(data); 
      }
      [Test]
      public void ParticipantsCount_3OrMore_ShouldPass()
      {
         try
         {
            track = new Track("test", 2, new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.Finish });
            Participants = new List<IParticipant>();
            Participants.Add(new Driver("Lola", TeamColors.Blue));
            Participants.Add(new Driver("Thijmen", TeamColors.Yellow));
            Participants.Add(new Driver("Tom", TeamColors.White));
            _testRace = new Race(track, Participants);
            _testRace.ParticipantsStartPosition(track, Participants);
         }
         catch (Exception)
         {
            Assert.Fail("Exception thrown");
         }
      }

      [Test]
      public void ParticipantsCount_LessThan3_ReturnException()
      {
         try
         {
            track = new Track("test", 2,new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.Finish });
            Participants = new List<IParticipant>();
            Participants.Add(new Driver("Lola", TeamColors.Blue));
            Participants.Add(new Driver("Thijmen", TeamColors.Yellow));
            _testRace = new Race(track, Participants);
            _testRace.ParticipantsStartPosition(track, Participants);
         }
         catch(System.ArgumentException e)
         {
            StringAssert.Contains(e.Message, "There must be at least 3 drivers in a race");
            return;
         }
         Assert.Fail("No Exception thrown");
      }

      [Test]
      public void ParticipantsCount_MoreThan4_ReturnException()
      {
         try
         {
            track = new Track("test", 2, new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.Finish });
            Participants = new List<IParticipant>();
            Participants.Add(new Driver("Lola", TeamColors.Blue));
            Participants.Add(new Driver("Thijmen", TeamColors.Yellow));
            Participants.Add(new Driver("Joost", TeamColors.Turqoise));
            Participants.Add(new Driver("Tom", TeamColors.White));
            Participants.Add(new Driver("Tom", TeamColors.Red));
            _testRace = new Race(track, Participants);
            _testRace.ParticipantsStartPosition(track, Participants);
         }
         catch (System.ArgumentException e)
         {
            StringAssert.Contains(e.Message, "There can not be more than 4 drivers");
            return;
         }
         Assert.Fail("No Exception thrown");
      }

      [Test]
      public void ParticipantsCount_TeamColorUnique_ShouldThrowException()
      {
         try
         {
            track = new Track("test", 2, new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.Finish });
            Participants = new List<IParticipant>();
            Participants.Add(new Driver("Lola", TeamColors.Blue));
            Participants.Add(new Driver("Thijmen", TeamColors.Yellow));
            Participants.Add(new Driver("Tom", TeamColors.Blue));
            _testRace = new Race(track, Participants);
            _testRace.ParticipantsStartPosition(track, Participants);
         }
         catch (ArgumentException e)
         {
            StringAssert.Contains(e.Message, "Team Color must be unique");
            return;
         }
         Assert.Fail("No Exception thrown");
      }








   }
}
