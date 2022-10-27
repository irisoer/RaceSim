using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class SectionData
   {
      public IParticipant? Left { get; set; }
      public int DistanceLeft { get; set; }
      public IParticipant? Right { get; set; }
      public int DistanceRight { get; set; }

      public SectionData()
      {
         DistanceLeft = 0;
         DistanceRight = 0;
         Left = null;
         Right = null;
      }

      public bool IsFull()
      {
         return Right is not null && Left is not null;
      }


      public int GetParticipantPosition(IParticipant participant)
      {
         if (participant == Left)
         {
            return DistanceLeft;
         }
         else if (participant == Right)
         {
            return DistanceRight;
         }
         else
         {
            throw new Exception("There is no participant!");
         }
      }


      public void AddParticipantToSection(IParticipant participant, int newPosition)
      {
         if (Left is null)
         {
            Left = participant;
            DistanceLeft += newPosition;
         }
         else if (Right is null)
         {
            Right = participant;
            DistanceRight += newPosition;
         } else
         {
            throw new Exception("Jo");
         }
      }

      public void MoveParticipantOnSection(IParticipant participant, int distance)
      {
         if (participant == Left)
         {
            DistanceLeft += distance;
         }
         else if (participant == Right)
         {
            DistanceRight += distance;
         }
      }

      public void RemoveParticipantFromSection(IParticipant participant)
      {
         if (participant == Left) //remove driver from last section
         {
            Left = null;
            DistanceLeft = 0;
         }
         else if (participant == Right)
         {
            Right = null;
            DistanceRight = 0;
         }
      }

   }
}
