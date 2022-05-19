using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Rooms
{
    public class Waterfall : Room
    {
        public Waterfall()
        {
            // Items for Waterfall
            Shadowgate.Waterfall.Stone stone1 = new Shadowgate.Waterfall.Stone("Stone");
            Shadowgate.Waterfall.Stone stone2 = new Shadowgate.Waterfall.Stone("Stone");
            Shadowgate.Waterfall.Stone stone3 = new Shadowgate.Waterfall.Stone("Stone");
            Shadowgate.Waterfall.Stone stone4 = new Shadowgate.Waterfall.Stone("Stone");
            Shadowgate.Waterfall.Stone stone5 = new Shadowgate.Waterfall.Stone("Stone");

            // All POI for the Waterfall
            PointOfInterest waterfall = new PointOfInterest("Waterfall");
            Entry rockPile = new Entry("Rock Pile", false, false, false, false);
            Entry openingBehindRockPile = new Entry("Opening behind rock pile", false, true, false, false, "Alcove");
            Entry doorFromWaterfallToSharkPond = new Entry("Door back to Shark Pond", true, true, false, false, "Shark Pond");
            var waterfallPOI = new List<PointOfInterest>() { stone1, stone2, stone3, stone4, stone5, waterfall, rockPile, 
                openingBehindRockPile, doorFromWaterfallToSharkPond };

            RoomName = "Waterfall";
            FirstEntry = "Water cascades over a subterranean cliff into a cool, clean stream.";
            SubsequentEntry = "You're standing in a dark, underground cavern.";
            PointsOfInterest = waterfallPOI;

            GameFunctions.RoomEnteredEvent += (roomName) => { 
                if (roomName == RoomName && 
                Globals.currentPlayer.PlayerInventory.Contains(GameFunctions.FindObject("Sling", null, Globals.currentPlayer.PlayerInventory))) //check if sling is in inventory
                {
                    Shadowgate.Closet.Sling theSling = (Shadowgate.Closet.Sling)GameFunctions.FindObject("Sling", null, Globals.currentPlayer.PlayerInventory); // if so, find it
                    if (theSling.StonesThrown == 5) // check if all stones have been used and if so, replenish them
                    {
                        Globals.clonedRoom.PointsOfInterest.Insert(0, stone1);
                        Globals.clonedRoom.PointsOfInterest.Insert(1, stone2);
                        Globals.clonedRoom.PointsOfInterest.Insert(2, stone3);
                        Globals.clonedRoom.PointsOfInterest.Insert(3, stone4);
                        Globals.clonedRoom.PointsOfInterest.Insert(4, stone5);
                        theSling.StonesThrown = 0;
                    };
                }
            };
        }

        public override void MoveTo(string objectName)
        {
            switch(objectName)
            {
                case "Rock Pile":
                    base.LookAt(objectName);
                    GameFunctions.ReduceTorchFire();
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
                case "Waterfall":
                    Console.WriteLine("\nCold water cascades down a cliff into a small stream.");
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
                case "Waterfall":
                case "Rock Pile":
                    ThatSmartsMessage();
                    break;
                default:
                    base.HitObject(objectName);
                    break;
            }
        }
    }
}
