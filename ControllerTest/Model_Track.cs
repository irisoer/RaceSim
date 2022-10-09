using Model;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ControllerTest
{
   [TestFixture]
   public class Model_Track
   {

      [Test]
      public void CheckSectionOrder_ShouldPass()
      {
         try
         {
            Track track = new Track("test", new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.Finish });
         }
         catch (Exception)
         {
            Assert.Fail("Exception was thrown");
         }
      }

      [Test]
      public void CheckSectionOrder_ShouldThrowArgumentException_FirstSectionShouldBeStartGrid()
      {
         try
         {
            Track track1 = new Track("test1", new SectionTypes[] { SectionTypes.Finish, SectionTypes.StartGrid, SectionTypes.StartGrid });
         }
         catch (System.ArgumentException e)
         {
            StringAssert.Contains(e.Message, "First section should be a start grid, review track test1");
            return;
         }
         Assert.Fail("The expected exception was not found");

      }

      [Test]
      public void CheckSectionOrder_ShouldThrowArgumentException_ThridSectionShouldBeFinish()
      {
         try
         {
            Track track2 = new Track("test2", new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.StartGrid });
         }
         catch (System.ArgumentException e)
         {
            StringAssert.Contains(e.Message, "Third section should be a finish, review track test2");
            return;
         }
         Assert.Fail("The expected exception was not found");

      }

      public void CountStartGrids_ShouldPass()
      {
         try
         {
            Track track = new Track("test", new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.Finish, SectionTypes.Straight });
         }
         catch (Exception)
         {
            Assert.Fail("Exception thrown");
         }
      }
      [Test]
      public void CountStartGrids_ShouldThrowArgumentException_Only2StartGrids()
      {
         try
         {
            Track track = new Track("test", new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.Finish, SectionTypes.StartGrid });
         }
         catch(ArgumentException e)
         {
            StringAssert.Contains(e.Message, "There can only be 2 startgrids, review track test");
            return;
         }
         Assert.Fail("Exception was not found");
      }

      [Test]
      public void CountFinishes_ShouldThrowArgumentException_Only1Finish()
      {
         try
         {
            Track track = new Track("test", new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.Finish, SectionTypes.Finish });
         }
         catch (ArgumentException e)
         {
            StringAssert.Contains(e.Message, "There can only be 1 finish, review track test");
            return;
         }
         Assert.Fail("Exception was not found");
      }

   }
}
