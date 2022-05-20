using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Rooms
{
    public class ThroneRoom : Room
    {
        public bool IsScepterUsed;
        
        public ThroneRoom()
        {
            // POI for Throne Room
            PointOfInterest skeletalKing = new PointOfInterest("Skeletal King");
            PointOfInterest leftThronePillar = new PointOfInterest("Left Pillar");
            PointOfInterest rightThronePillar = new PointOfInterest("Right Pillar");
            PointOfInterest holeInPillar = new PointOfInterest("Hole in the Right Pillar", true);
            Entry trapDoorHiddenUnderThrone = new Entry("Trap Door hidden under the Throne", false, true, false, true, "Underground Hall");
            Entry doorwayFromThroneRoomToSmallCorridor = new Entry("Doorway to Small Corridor", false, true, false, false, "Small Corridor");
            var throneRoomPOI = new List<PointOfInterest>() { skeletalKing, leftThronePillar, rightThronePillar, holeInPillar, 
                trapDoorHiddenUnderThrone, doorwayFromThroneRoomToSmallCorridor };

            RoomName = "Throne Room";
            FirstEntry = "You're in a small throne room. A skeleton wearing a gold crown sits on a throne in front of you.";
            SubsequentEntry = "It's the royal throne room.";
            PointsOfInterest = throneRoomPOI;
        }

        public override void MoveTo(string objectName)
        {
            switch(objectName)
            {
                case "Skeletal King":
                    GameFunctions.FindObject(objectName, PointsOfInterest).BumpedHeadMessage();
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
                case "Skeletal King":
                    Console.WriteLine("\nAlthough he looks dead enough, this royal skeleton sends shivers down your spine!");
                    if (!IsScepterUsed)
                        Console.WriteLine("In his right hand is an axe, but his left hand is open, as if expecting something."); // altered line
                    break;
                case "Hole in the Right Pillar":
                    Console.WriteLine("\nA ring-shaped hole has appeared in the pillar.");
                    break;
                case "Left Pillar":
                    Console.WriteLine("\nIn the center of the pillar is a carving of a sword.");
                    break;
                case "Right Pillar":
                    Console.WriteLine("\nSir Dugan's royal seal is carved on the stone pillar in vivid colors.");
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
                case "Left Pillar":
                case "Right Pillar":
                    GameFunctions.FindObject(objectName, PointsOfInterest).ThatSmartsMessage();
                    break;
                default:
                    base.HitObject(objectName);
                    break;
            }
        }
    }
}
