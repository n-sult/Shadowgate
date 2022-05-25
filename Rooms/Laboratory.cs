using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Rooms
{
    public class Laboratory : Room
    {
        public bool DrankFromPot;
        public bool StoneInFloorUsed;

        public Laboratory()
        {
            // Items for Laboratory
            Bottle labBottle2First = new Bottle("Bottle 2");
            Bottle labBottle2Second = new Bottle("Bottle 2");
            Bottle bottle3 = new Bottle("Bottle 3");
            Bottle bottle4 = new Bottle("Bottle 4");
            Shadowgate.Laboratory.Horseshoe horseshoe = new Shadowgate.Laboratory.Horseshoe("Horseshoe");
            Shadowgate.Laboratory.TestTube testTube = new Shadowgate.Laboratory.TestTube("Test Tube");
            Shadowgate.Laboratory.HolyWater holyWater = new Shadowgate.Laboratory.HolyWater("Water", true);

            // All POI for Laboratory
            PointOfInterest stoneInFloor = new PointOfInterest("Stone protruding from the Floor");
            PointOfInterest labCage = new PointOfInterest("Cage");
            PointOfInterest labPot = new PointOfInterest("Green Pot");
            Entry doorwayNextToPot = new Entry("Doorway next to the pot", false, true, false, false, "Garden");
            Entry doorFromLabToDraftyHallway = new Entry("Door to Drafty Hallway", false, true, false, false, "Drafty Hallway");
            var laboratoryPOI = new List<PointOfInterest>() { holyWater, labBottle2First, labBottle2Second, bottle3, bottle4, horseshoe, testTube, 
                stoneInFloor, labCage, labPot, doorwayNextToPot, doorFromLabToDraftyHallway };

            RoomName = "Laboratory";
            FirstEntry = "It smells like a kennel in here and there are no windows through which to circulate air.";
            SubsequentEntry = "You're in a small, stuffy laboratory.";
            PointsOfInterest = laboratoryPOI;
        }

        public override void SetRoomStuff()
        {
            if (Globals.currentPlayer.NumberOfBottle2Consumed > 1 && Globals.currentPlayer.NumberOfBottle2Consumed % 3 == 0) // if 3 bottle2s have been used, replenish the bottle2 items in the room
            {
                Bottle labBottle2First = new Bottle("Bottle 2");
                Bottle labBottle2Second = new Bottle("Bottle 2");

                PointsOfInterest.Insert(0, labBottle2First);
                PointsOfInterest.Insert(0, labBottle2Second);
            }
                
        }

        public override void LookAt(string objectName)
        {
            switch(objectName)
            {
                case "Cage":
                    GameFunctions.WriteLine("\nThis steel mesh cage rattles constantly. A simple latch secures it.");
                    break;
                case "Green Pot":
                    GameFunctions.WriteLine("\nUgh! There's a strange, poisonous-looking liquid in the pot. It really stinks!");
                    break;
                case "Stone protruding from the Floor":
                    GameFunctions.WriteLine("\nLab animals can be chained to this stone while performing experiments on them.");
                    break;
                default:
                    base.LookAt(objectName);
                    break;
            }
        }

        public override void OpenObject(string objectName)
        {
            switch(objectName)
            {
                case "Cage":
                    Console.ForegroundColor = ConsoleColor.Red;
                    GameFunctions.WriteLine("\nYou remove the latch and a mutated dog pounces on you! \nIt looks like the doctor " +
                        "put something strange in the dog's water. \nBefore you can do anything else, the mutation quickly rips you apart!");
                    GameFunctions.GameOver();
                    break;
                default:
                    base.OpenObject(objectName);
                    break;
            }
        }

        public override void UseObject(string objectName)
        {
            switch(objectName)
            {
                case "Green Pot":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    if (!DrankFromPot)
                    {
                        GameFunctions.WriteLine("\nSlurp! You taste the poisonous-looking liquid in the pot. \nYou notice small blue hairs being " +
                            "to grow on the palms of your hand. \nThe viscous liquid seems to contain body altering ingredients.");
                        DrankFromPot = true;
                        GameFunctions.ReduceTorchFire();
                    }
                    else
                        GameFunctions.WriteLine("\nLooking at the blue hair covering your hands, you hesistate to drink the awful, stinking liquid.");
                    break;
                case "Stone protruding from the Floor":
                    if (!StoneInFloorUsed)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        GameFunctions.WriteLine("\nThe stone rises slowly out of the floor. A shining vial is inside it.");
                        GameFunctions.FindObject("Water", PointsOfInterest).IsHidden = false;
                        StoneInFloorUsed = true;
                        GameFunctions.ReduceTorchFire();
                    }
                    else
                        base.UseObject(objectName);
                    break;
                default:
                    base.UseObject(objectName);
                    break;
            }
        }

        public override void HitObject(string objectName)
        {
            switch(objectName)
            {
                case "Green Pot":
                    GameFunctions.FindObject(objectName, PointsOfInterest).ThumpMessage();
                    break;
                case "Stone protruding from the Floor":
                    GameFunctions.FindObject(objectName, PointsOfInterest).ThatSmartsMessage();
                    break;
                default:
                    base.HitObject(objectName);
                    break;
            }
        }
    }
}
