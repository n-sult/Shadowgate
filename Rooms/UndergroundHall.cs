using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Rooms
{
    public class UndergroundHall : Room
    {
        public UndergroundHall()
        {
            // POI for Underground Hall
            Torch underGroundLeftTorch1 = new Torch("Closer torch to the left");
            Torch underGroundLeftTorch2 = new Torch("Farther torch to the left");
            Torch underGroundRightTorch1 = new Torch("Closer torch to the right");
            Torch underGroundRightTorch2 = new Torch("Closer torch to the right");
            Entry undergroundLeftDoorway = new Entry("Doorway to the left", false, true, false, false);
            Entry doorwayAtTheEndOfTheHall = new Entry("Doorway at the end of the hall", false, true, false, false, "Gargoyle Cave");
            Entry passagewayFromUndergroundHallToThroneRoom = new Entry("Passageway to Throne Room", false, true, false, false, "Throne Room");
            var undergroundHallPOI = new List<PointOfInterest>() { underGroundLeftTorch1, underGroundLeftTorch2, underGroundRightTorch1, underGroundRightTorch2,
                undergroundLeftDoorway, doorwayAtTheEndOfTheHall, passagewayFromUndergroundHallToThroneRoom};

            RoomName = "Underground Hall";
            FirstEntry = "This hallway is made of large granite slabs.";
            SubsequentEntry = "It's a stone passageway.";
            PointsOfInterest = undergroundHallPOI;
        }

        public override void MoveTo(string objectName)
        {
            switch(objectName)
            {
                case "Doorway to the left":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nWithout thinking, you jump through the opening and immediately hear a loud click. " +
                        "\nSuddenly, the granite slab above you gives way and crushes you beneath it. " +
                        "\nIt breaks every bone in your body.");
                    GameFunctions.GameOver();
                    break;
                default:
                    base.MoveTo(objectName);
                    break;
            }
        }
    }
}
