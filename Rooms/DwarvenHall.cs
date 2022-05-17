using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Rooms
{
    public class DwarvenHall : Room
    {
        public DwarvenHall()
        {
            // All POI for the dwarven hall
            Entry dwarfLeftDoor = new Entry("Left Door", true, false, false, false, "Cold Room");
            Entry dwarfCenterDoor = new Entry("Center Door", true, false, false, false, "Tomb");
            Entry dwarfRightDoor = new Entry("Right Door", true, false, false, false, "Shark Pond");
            Entry hallFromDwarvenHallToCrampedHallway = new Entry("Hall to Cramped Hallway", false, true, false, false, "Cramped Hallway");
            var dwarvenHallPOI = new List<PointOfInterest>() { dwarfLeftDoor, dwarfCenterDoor, dwarfRightDoor, hallFromDwarvenHallToCrampedHallway };

            RoomName = "Dwarven Hall";
            FirstEntry = "The stones in these walls were probably cut by the hands of enslaved mountain dwarves.";
            SubsequentEntry = "The stones in these walls were probably cut by the hands of enslaved mountain dwarves.";
            PointsOfInterest = dwarvenHallPOI;
        }
    }
}
