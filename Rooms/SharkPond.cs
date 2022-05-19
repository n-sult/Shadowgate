using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Rooms
{
    public class SharkPond : Room
    {
        public bool WasSphereUsed = false;
        public bool IsSphereInPond = false;
        public bool IsKeyTaken = false;
        public bool UsedTorchOnPond = false;

        public SharkPond()
        {
            // Items for Shark Pond
            Key key3 = new Key("Key 3", false, false);

            // All POI for the Shark Pond
            PointOfInterest sharkPondWater = new PointOfInterest("Pond");
            PointOfInterest sharkPondSkeleton = new PointOfInterest("Skeleton in the pond");
            Entry doorNextToPond = new Entry("Door next to the pond", true, false, false, false, "Waterfall");
            Entry doorFromSharkPondToDwarvenHall = new Entry("Doorway to Dwarven Hall", true, true, false, false, "Dwarven Hall");
            var sharkPondPOI = new List<PointOfInterest>() { sharkPondWater, sharkPondSkeleton, key3, doorNextToPond, doorFromSharkPondToDwarvenHall };

            RoomName = "Shark Pond";
            FirstEntry = "A shark swims by as if patrolling this calm pool.";
            SubsequentEntry = "This subterranean cavern has been carved by centuries of supernatural erosion.";
            PointsOfInterest = sharkPondPOI;
        }

        public override void MoveTo(string objectName)
        {
            switch (objectName)
            {
                case "Pond":
                    if (!WasSphereUsed)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nAs you swim toward the skeleton, you feel the jaws of a shark grab you and pull you under. \nYou curse yourself for using " +
                            "your body as bait! \nEven before the life has left your body, the lake will be filled with your blood.");
                        GameFunctions.GameOver();
                    }
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
                case "Pond":
                    if (!WasSphereUsed)
                        Console.WriteLine("\nThe waters of this subterranean lake are as still as a corpse.");
                    else
                        Console.WriteLine("\nThe lake has become a solid sheet of ice.");
                    break;
                case "Skeleton in the pond":
                    Console.WriteLine("\nA lime covered skeleton stares at you through eyeless sockets.");
                    if (!IsKeyTaken)
                        Console.WriteLine("It's holding a key in its right hand.");
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
                case "Skeleton in the pond":
                    if (!WasSphereUsed)
                        Console.WriteLine("\nYou can't reach it from here. Swimming the shark-infested pool would be suicidal.");
                    else
                        ThatSmartsMessage();
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
                case "Skeleton in the pond":
                    GameFunctions.FindObject(objectName, PointsOfInterest).DoesNotUnderstandMessage();
                    break;
                default:
                    base.SpeakTo(objectName);
                    break;
            }
        }
    }
}
