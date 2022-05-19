using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Rooms
{
    public class LimestoneCave : Room
    {
        public bool BlueGemUsed = false;
        
        public LimestoneCave()
        {
            // Items for Limestone Cave
            Scroll scroll2 = new Scroll("Scroll 2", true);

            // All POI for Limestone Cave
            PointOfInterest floorStone = new PointOfInterest("Floor Stone");
            PointOfInterest limestoneWall = new PointOfInterest("Stone Slab");
            Entry doorwayFromLimestoneCaveToEporRoom = new Entry("Doorway back to Epor room", true, true, false, false, "Epor Room");
            var limestoneCavePOI = new List<PointOfInterest>() { scroll2, floorStone, limestoneWall, doorwayFromLimestoneCaveToEporRoom };

            RoomName = "Limestone Cave";
            FirstEntry = "The cold water from the limestone drops on your neck, sending shivers down your spine!";
            SubsequentEntry = "A huge, man-made slab of granite seals the far side of the cavern.";
            PointsOfInterest = limestoneCavePOI;
        }

        public override void LookAt(string objectName)
        {
            switch(objectName)
            {
                case "Stone Slab":
                    Console.WriteLine("\nIt's a stone wall.");
                    break;
                case "Floor Stone":
                    if (!BlueGemUsed)
                        Console.WriteLine("\nThis is a concave polygon. It seems to have been carefully carved into the stone.");
                    else
                        Console.WriteLine("\nThe gem fits perfectly into the hole.");
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
                case "Floor Stone":
                    ThatSmartsMessage();
                    break;
                default:
                    base.HitObject(objectName);
                    break;
            }
        }
    }
}
