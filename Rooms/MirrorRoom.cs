using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Rooms
{
    public class MirrorRoom : Room
    {
        public MirrorRoom()
        {
            // Items for Mirror Room
            Shadowgate.MirrorRoom.Broom broom = new Shadowgate.MirrorRoom.Broom("Broom");

            // All POI for Mirror Room
            PointOfInterest leftMirror = new PointOfInterest("Left Mirror");
            PointOfInterest centerMirror = new PointOfInterest("Middle Mirror");
            PointOfInterest rightMirror = new PointOfInterest("Right Mirror");
            Torch mirrorTorch1 = new Torch("Left Torch");
            Torch mirrorTorch2 = new Torch("Right Torch");
            Entry doorBehindCenterMirror = new Entry("Door behind broken mirror", true, false, true, true, "Key 3", "Fire Bridge");
            Entry holeInFloorToEporRoom = new Entry("Hole in the floor", false, true, false, false, "Epor Room");
            Entry doorToTomb = new Entry("Door to Tomb", false, true, false, false, "Tomb");
            var mirrorRoomPOI = new List<PointOfInterest>() { leftMirror, centerMirror, rightMirror, mirrorTorch1, mirrorTorch2, broom,
                doorBehindCenterMirror, holeInFloorToEporRoom, doorToTomb };

            RoomName = "Mirror Room";
            FirstEntry = "This room, full of mirrors, reminds you of the elven fun house at King Otto's Fair.";
            SubsequentEntry = "This room, full of mirrors, reminds you of the elven fun house at King Otto's Fair.";
            PointsOfInterest = mirrorRoomPOI;
        }

        public override void MoveTo(string objectName)
        {
            switch(objectName)
            {
                case "Hole in the floor":
                    if (!Rooms.EporRoom.IsEporActive)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nYou jump down the hole and, after a couple moments, hit the floor! " +
                            "\nIt seems that you have broken both of your legs! It's only a matter of time before you die!");
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
                case "Left Mirror":
                case "Right Mirror":
                    Console.WriteLine("\nThe mirror has a carved oak frame.");
                    break;
                case "Middle Mirror":
                    Console.WriteLine("\nThis mirror throws back a fine reflection.");
                    break;
                default:
                    base.LookAt(objectName);
                    break;
            }
        }
    }
}
