using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Rooms
{
    public class DraftyHallway : Room
    {
        public DraftyHallway()
        {
            // All POI for Drafty Hall
            PointOfInterest draftyHallRug = new PointOfInterest("Rug expanding the hallway");
            Torch draftyHallTorch = new Torch("Torch on the left hall");
            Entry closerDoorOnTheLeft = new Entry("Closer door on the left", false, true, false, false, "Library");
            Entry fartherDoorOnTheLeft = new Entry("Farther door on the left", false, true, false, false, "Laboratory");
            Entry doorAtTheEndOfTheHall = new Entry("Door at the end of the hall", true, false, false, false, "Banquet Hall");
            Entry doorFromDraftyHallwayToCourtyard = new Entry("Door to the Courtyard", true, true, false, false, "Courtyard");
            var draftyHallwayPOI = new List<PointOfInterest>() { draftyHallRug, draftyHallTorch, closerDoorOnTheLeft, fartherDoorOnTheLeft, 
                doorAtTheEndOfTheHall, doorFromDraftyHallwayToCourtyard };

            RoomName = "Drafty Hallway";
            FirstEntry = "It's a long, drafy hallway with one flight of stairs and several open passages.";
            SubsequentEntry = "It's a long, drafy hallway with one flight of stairs and several open passages.";
            PointsOfInterest = draftyHallwayPOI;
        }

        public override void LookAt(string objectName)
        {
            switch(objectName)
            {
                case "Rug expanding the hallway":
                    GameFunctions.WriteLine("\nThis finely woven rug spans the entire hallway.");
                    break;
                default:
                    base.LookAt(objectName);
                    break;
            }
        }
    }
}
