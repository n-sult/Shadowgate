using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Rooms
{
    public class SmallCorridor : Room
    {
        public SmallCorridor()
        {
            // POI for Small Corridor
            Torch corridorTorch1 = new Torch("Left Torch");
            Torch corridorTorch2 = new Torch("Right Torch");
            Entry leftArchway = new Entry("Left Archway", false, true, false, false, "Balcony");
            Entry rightArchway = new Entry("Right Archway", false, true, false, false, "Throne Room");
            Entry doorFromSmallCorridorToBanquetHall = new Entry("Doorway to Banquet Hall", true, true, false, false, "Banquet Hall");
            var smallCorridorPOI = new List<PointOfInterest>() { corridorTorch1, corridorTorch2, leftArchway, rightArchway, doorFromSmallCorridorToBanquetHall };

            RoomName = "Small Corridor";
            FirstEntry = "You have entered a small corridor. Two arched doorways wait patiently for you.";
            SubsequentEntry = "It's a passageway with two arches.";
            PointsOfInterest = smallCorridorPOI;
        }
    }
}
