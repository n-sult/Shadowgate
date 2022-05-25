using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Rooms
{
    public class Alcove : Room
    {
        public Alcove()
        {
            Shadowgate.Alcove.Jewel whiteGem = new Shadowgate.Alcove.Jewel("White Gem");
            Shadowgate.Alcove.Jewel redGem = new Shadowgate.Alcove.Jewel("Red Gem");
            Shadowgate.Alcove.Jewel blueGem = new Shadowgate.Alcove.Jewel("Blue Gem");
            List<Item> jewels = new List<Item>() { whiteGem, redGem, blueGem };
            Bag bag1 = new Bag("Bag 1", true, jewels);

            PointOfInterest alcoveWall = new PointOfInterest("Alcove Wall");
            PointOfInterest alcoveRock = new PointOfInterest("Rock jutting from the wall", false);
            Entry openingFromAlcoveToWaterfall = new Entry("Opening back to Waterfall", false, true, false, false, "Waterfall");
            var alcovePOI = new List<PointOfInterest>() { bag1, alcoveWall, alcoveRock, openingFromAlcoveToWaterfall };

            RoomName = "Alcove";
            FirstEntry = "The walls in this room are much too close for comfort. \n\nThe damp walls of this eerie cavern are rough and irregular.";
            SubsequentEntry = "You're in a cave behind the waterfall.";
            PointsOfInterest = alcovePOI;
        }

        public override void LookAt(string objectName)
        {
            switch(objectName)
            {
                case "Alcove Wall":
                    GameFunctions.WriteLine("\nThe wall juts out in multiple places.");
                    break;
                case "Rock jutting from the wall":
                    GameFunctions.WriteLine("\nThe rock is quite loose.");
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
                case "Alcove Wall":
                    ThatSmartsMessage();
                    GameFunctions.ReduceTorchFire();
                    break;
                case "Rock jutting from the wall":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    GameFunctions.WriteLine("\nYou hit the rock as hard as you can. \nPOW! \nThe loose rock falls down as if hinged to the wall.");
                    PointsOfInterest.Remove(GameFunctions.FindObject("Rock jutting from the wall", PointsOfInterest)); // make the rock disappear forever
                    GameFunctions.FindObject("Bag 1", PointsOfInterest).IsHidden = false; // reveal the bag
                    GameFunctions.ReduceTorchFire();
                    break;
                default:
                    base.HitObject(objectName);
                    break;
            }
        }
    }
}
