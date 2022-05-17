using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Rooms
{
    public class SnakeCave : Room
    {
        static bool _lookedAtSnake;
        
        public SnakeCave()
        {
            // Items for Snake Cave
            Shadowgate.SnakeCave.Staff staff = new Shadowgate.SnakeCave.Staff("Staff", true);

            // POI for Snake Cave
            PointOfInterest snake = new PointOfInterest("Giant Snake");
            Entry doorwayFromSnakeCaveToBridgeRoom = new Entry("Doorway to Bridge Room", false, true, false, false, "Bridge Room");
            var snakeCavePOI = new List<PointOfInterest>() { staff, snake, doorwayFromSnakeCaveToBridgeRoom };

            RoomName = "Snake Cave";
            FirstEntry = "A giant snake confronts you in this small cave!";
            SubsequentEntry = "You're inside a narrow alcove.";
            PointsOfInterest = snakeCavePOI;
        }

        public override void LookAt(string objectName)
        {
            switch(objectName)
            {
                case "Giant Snake":
                    if (!_lookedAtSnake)
                    {
                        Console.Write("\nIt's a giant snake. It doesn't move. Perhaps it's getting ready to strike! " +
                            "\nYou wait for the creature to kill you but it still has yet to move. " +
                            "\nUpon closer inspection, you laugh at your foolishness. It is only a statue!");
                        _lookedAtSnake = true;
                    }
                    Console.WriteLine("\nThis huge statue is carved in the shape of a giant snake. It is extremely life-like.");
                    break;
                default:
                    base.LookAt(objectName);
                    break;
            }
        }
    }
}
