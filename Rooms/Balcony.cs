using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Rooms
{
    public class Balcony : Room
    {
        public static bool IsRodUsed;
        
        public Balcony()
        {
            // Items for Balcony
            Shadowgate.Balcony.Wand wand = new Shadowgate.Balcony.Wand("Wand", true);

            // POI for Balcony
            PointOfInterest mount = new PointOfInterest("Hole on the balcony wall");
            PointOfInterest skeletalHand = new PointOfInterest("Skeletal Hand", true);
            OutsideView balconySky = new OutsideView("Lightning-filled Sky");
            Entry stairsToLookoutPoint = new Entry("Stone Stairs", false, true, false, false, "Lookout Point");
            Entry doorwayFromBalconyToSmallCorridor = new Entry("Doorway to Small Corridor", false, true, false, false, "Small Corridor");
            var balconyPOI = new List<PointOfInterest>() { skeletalHand, wand, mount, balconySky, stairsToLookoutPoint, doorwayFromBalconyToSmallCorridor };

            RoomName = "Balcony";
            FirstEntry = "From this windy ledge, you can get an idea of the size and strength of the castle.";
            SubsequentEntry = "It's a balcony.";
            PointsOfInterest = balconyPOI;
        }

        public override void LookAt(string objectName)
        {
            switch(objectName)
            {
                case "Hole on the balcony wall":
                    if (!IsRodUsed)
                        Console.WriteLine("\nThis appears to be a mount of some sort, perhaps for a flagpole.");
                    else
                        Console.WriteLine("\nThe small hole in the center is perfectly round.");
                    break;
                case "Skeletal Hand":
                    Console.WriteLine("\nAlthough the hand is skeletal, it holds the wand rather tightly.");
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
                case "Hole on the balcony wall":
                case "Skeletal Hand":
                    GameFunctions.FindObject(objectName, PointsOfInterest).ThatSmartsMessage();
                    break;
                default:
                    base.HitObject(objectName);
                    break;
            }
        }
    }
}
