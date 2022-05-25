using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Rooms
{
    public class Vault : Room
    {
        public bool TalismanUsed;
        public bool HornUsed;
        
        public Vault()
        {
            // POI for Vault
            PointOfInterest giantSkull = new PointOfInterest("Giant Skull");
            PointOfInterest leftVaultPillar = new PointOfInterest("Left Pillar");
            PointOfInterest leftPillarHole = new PointOfInterest("Left Pillar Hole");
            PointOfInterest middleVaultPillar = new PointOfInterest("Middle Pillar");
            PointOfInterest middlePillarHole = new PointOfInterest("Middle Pillar Hole");
            PointOfInterest rightVaultPillar = new PointOfInterest("Right Pillar");
            PointOfInterest rightPillarHole = new PointOfInterest("Right Pillar Hole");
            Entry skullDoor = new Entry("Skull Door", false, false, true, false, "The Chasm");
            Entry fromVaultToRiverStyx = new Entry("Back to River Styx", false, true, false, false, "River Styx");
            var vaultPOI = new List<PointOfInterest>() { leftVaultPillar, leftPillarHole, middleVaultPillar, middlePillarHole, 
                rightVaultPillar, rightPillarHole, giantSkull, skullDoor, fromVaultToRiverStyx };

            RoomName = "Vault";
            FirstEntry = "You climb aboard the tiny raft and soon reach the opposite bank. \n\nA stone skull stands against the far wall, " +
                "screaming silently. \n\nFor some reason, you get the feeling you are standing on sacred ground.";
            SubsequentEntry = "This chamber has been hewn out of solid rock and is very hot.";
            PointsOfInterest = vaultPOI;
        }

        public override void MoveTo(string objectName)
        {
            switch(objectName)
            {
                case "Skull Door":
                    if (!HornUsed)
                        GameFunctions.FindObject(objectName, PointsOfInterest).BumpedHeadMessage();
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
                case "Giant Skull":
                    GameFunctions.WriteLine("\nHot, dry air emanates from the hole.");
                    break;
                case "Left Pillar":
                    GameFunctions.WriteLine("\nThe shape of a sword is carved into the left pillar.");
                    break;
                case "Middle Pillar":
                    GameFunctions.WriteLine("\nThe shape of a crown is carved into the middle pillar.");
                    break;
                case "Right Pillar":
                    GameFunctions.WriteLine("\nThe shape of a jewel is carved into the right pillar.");
                    break;
                case "Left Pillar Hole":
                case "Middle Pillar Hole":
                case "Right Pillar Hole":
                    GameFunctions.WriteLine("\nIt's a polished stone slab with an odd-shaped niche cut out of it.");
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
                case "Giant Skull":
                case "Left Pillar":
                case "Left Pillar Hole":
                case "Middle Pillar":
                case "Middle Pillar Hole":
                case "Right Pillar":
                case "Right Pillar Hole":
                    GameFunctions.FindObject(objectName, PointsOfInterest).ThatSmartsMessage();
                    break;
                default:
                    base.HitObject(objectName);
                    break;
            }
        }
    }
}
