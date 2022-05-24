using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Rooms
{
    public class BridgeRoom : Room
    {
        public BridgeRoom()
        {
            // All POI for Bridge Room
            PointOfInterest chasm = new PointOfInterest("Chasm");
            Entry leftBridge = new Entry("Left Bridge", false, true, false, false, "Wraith Room");
            Entry rightBridge = new Entry("Right Bridge", false, true, false, false, "Snake Cave");
            Entry doorFromBridgeRoomToHiddenRoom = new Entry("Door back to Hidden Room", false, true, false, false, "Hidden Room");
            var bridgeRoomPOI = new List<PointOfInterest>() { chasm, leftBridge, rightBridge, doorFromBridgeRoomToHiddenRoom };

            RoomName = "Bridge Room";
            FirstEntry = "You stand at the edge of a deep chasm. From the darkness below arise the screams of the undead. This cave is hewn roughly in the chasm's wall.";
            SubsequentEntry = "There are two bridges that span the chasm.";
            PointsOfInterest = bridgeRoomPOI;
        }

        public override void SetRoomStuff()
        {
            Globals.currentPlayer.Bottle2Used = false;
        }

        public override void MoveTo(string objectName)
        {
            switch(objectName)
            {
                case "Right Bridge":
                    if (!Globals.currentPlayer.Bottle2Used)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nAs you reach the middle of the bridge, it collapses under your feet! " +
                            "\nThe bridge won't hold you. You can't cross unless you lose some weight!");
                        GameFunctions.GameOver();
                    }
                    else
                        base.MoveTo(objectName);
                    break;
                case "Chasm":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nWith a loud cry, you take the big plunge. \nThe Grim Reaper stands below, waiting to catch you.");
                    GameFunctions.GameOver();
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
                case "Chasm":
                    Console.WriteLine("\nYou hear moans coming from the bottom of the chasm.");
                    break;
                default:
                    base.LookAt(objectName);
                    break;
            }
        }
    }
}
