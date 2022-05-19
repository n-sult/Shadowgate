using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Rooms
{
    public class BrazierRoom : Room
    {
        public BrazierRoom()
        {
            // Items for Brazier Room
            Shadowgate.BrazierRoom.Horn horn = new Shadowgate.BrazierRoom.Horn("Horn");

            // POI for Brazier Room
            PointOfInterest leftBrazier = new PointOfInterest("Left Brazier");
            PointOfInterest rightBrazier = new PointOfInterest("Right Brazier");
            PointOfInterest leftPillar = new PointOfInterest("Left Pillar");
            PointOfInterest rightPillar = new PointOfInterest("Right Pillar");
            OutsideView windowBetweenPillars = new OutsideView("Window between the pillars");
            PointOfInterest hellhound = new PointOfInterest("Hellhound", true);
            Entry ladderToTheNextFloor = new Entry("Ladder to the next floor", false, true, false, false, "Turret");
            Entry doorFromBrazierRoomToBanquetHall = new Entry("Door back to Banquet Hall", true, true, false, false, "Banquet Hall");
            var brazierRoomPOI = new List<PointOfInterest>() { hellhound, leftBrazier, rightBrazier, leftPillar, rightPillar, windowBetweenPillars, horn,
            ladderToTheNextFloor, doorFromBrazierRoomToBanquetHall };

            RoomName = "Brazier Room";
            FirstEntry = "Although the evening air is cool, this small circular room radiates a fervent heat.";
            SubsequentEntry = "You're in a room with two braziers.";
            PointsOfInterest = brazierRoomPOI;
        }

        public static PointOfInterest ReturnHellhound()
        {
            return GameFunctions.FindObject("Hellhound", Globals.clonedRoom.PointsOfInterest);
        }
        
        public static void HoundAppears()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nA large fireball suddenly appears in the room and causes you to shield your eyes. " +
                "\nWhen you open them, you notice that the fire has changed into something far more menacing: a hellhound!"); // altered line
            ReturnHellhound().IsHidden = false;
        }
        
        public static void DieToHound()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nThe demon dog snarls and pounces on you. Its teeth sink deep into your flesh.");
            GameFunctions.GameOver();
        }
        
        public override void MoveTo(string objectName)
        {
            switch(objectName)
            {
                case "Ladder to the next floor":
                    if (ReturnHellhound().IsHidden == true)
                    {
                        HoundAppears();
                        GameFunctions.ReduceTorchFire();
                    }
                    else if (PointsOfInterest.Contains(ReturnHellhound()))
                        DieToHound();
                    else
                        base.MoveTo(objectName);
                    break;
                default:
                    base.MoveTo(objectName);
                    break;
            }
        }

        public override void LookAt(string objectName)
        {
            switch(objectName)
            {
                case "Left Brazier":
                case "Right Brazier":
                    Console.WriteLine("\nA flame burns within this brazier, lighting the entire room.");
                    break;
                case "Left Pillar":
                case "Right Pillar":
                    Console.WriteLine("\nThis marble pillar seems to be supporting the ceiling.");
                    break;
                case "Hellhound":
                    Console.WriteLine("\nThe hellhound makes this hot room even hotter. \nThere must be a way to cool the room off " +
                        "before you roast!");
                    break;
                default:
                    base.LookAt(objectName);
                    break;
            }
        }

        public override void HitObject(string objectName)
        {
            switch(objectName)
            {
                case "Hellhound":
                    DieToHound();
                    break;
                default:
                    base.HitObject(objectName);
                    break;
            }
        }

        public override void SpeakTo(string objectName)
        {
            switch(objectName)
            {
                case "Hellhound":
                    GameFunctions.FindObject("Hellhound", PointsOfInterest).DoesNotUnderstandMessage();
                    break;
                default:
                    base.SpeakTo(objectName);
                    break;
            }
        }
    }
}
