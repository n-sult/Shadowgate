using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Rooms
{
    public class Garden : Room
    {
        public bool FluteUsed;
        
        public Garden()
        {
            // Items for Garden
            Shadowgate.Garden.Flute flute = new Shadowgate.Garden.Flute("Flute in the water fountain");
            Shadowgate.Garden.Ring ring = new Shadowgate.Garden.Ring("Ring", true);

            // All POI for Garden
            PointOfInterest tree = new PointOfInterest("Tree");
            PointOfInterest waterFountain = new PointOfInterest("Water Fountain");
            PointOfInterest treeHole = new PointOfInterest("Tree Hole", true);
            Entry doorFromGardenToLab = new Entry("Door to Laboratory", false, true, false, false, "Laboratory");
            var gardenPOI = new List<PointOfInterest>() { tree, ring, treeHole, waterFountain, flute, doorFromGardenToLab };

            RoomName = "Garden";
            FirstEntry = "You stand in a small garden within the castle walls. The outside air is cool and moist.";
            SubsequentEntry = "You stand in a small garden within the castle walls. The outside air is cool and moist.";
            PointsOfInterest = gardenPOI;
        }

        public override void LookAt(string objectName)
        {
            switch(objectName)
            {
                case "Tree Hole":
                    GameFunctions.WriteLine("\nSuddenly, a small hole appears in the side of the tree!");
                    break;
                case "Tree":
                    GameFunctions.WriteLine("\nThe bark on this tree shows no hint of disease and it's leaves are an awesome gold color.");
                    break;
                case "Water Fountain":
                    GameFunctions.WriteLine("\nThis exquisite marble fountain is shaped into the image of a sea serpent. " +
                        "\nFrom it's mouth spews an acidic liquid.");
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
                case "Tree":
                    GameFunctions.FindObject("Tree", PointsOfInterest).ThatSmartsMessage();
                    break;
                default:
                    base.HitObject(objectName);
                    break;
            }
        }
    }
}
