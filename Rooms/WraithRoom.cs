using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Rooms
{
    public class WraithRoom : Room
    {
        public WraithRoom()
        {
            // Items for Wraith Room
            Shadowgate.WraithRoom.Cloak cloak = new Shadowgate.WraithRoom.Cloak("Cloak");

            // All POI for Wraith Room
            PointOfInterest wraith = new PointOfInterest("Wraith");
            Torch wraithRoomTorch1 = new Torch("Left Torch");
            Torch wraithRoomTorch2 = new Torch("Right Torch");
            PointOfInterest doorOnAHighLedge = new PointOfInterest("Door on a high ledge");
            Entry doorToEporRoom = new Entry("Door at floor level", true, false, false, false, "Epor Room");
            Entry doorFromWraithRoomToBridgeRoom = new Entry("Door back to Bridge Room", false, true, false, false, "Bridge Room");
            var wraithRoomPOI = new List<PointOfInterest>() { cloak, wraith, wraithRoomTorch1, wraithRoomTorch2, doorOnAHighLedge, 
                doorToEporRoom, doorFromWraithRoomToBridgeRoom };

            RoomName = "Wraith Room";
            FirstEntry = "What's this? A wraith is standing in your way, barring your path!";
            SubsequentEntry = "A stone archway opens into a small chamber. This room is very cold.";
            PointsOfInterest = wraithRoomPOI;
        }

        public override void MoveTo(string objectName)
        {
            var activeObject = GameFunctions.FindObject(objectName, PointsOfInterest);
            switch(objectName)
            {
                case "Door at floor level":
                    if (PointsOfInterest.Contains(GameFunctions.FindObject("Wraith", PointsOfInterest)))
                        activeObject.AfraidToGetNearMessage();
                    else
                        base.MoveTo(objectName);
                    break;
                case "Door on a high ledge":
                    GameFunctions.FindObject("Door on a high ledge", PointsOfInterest).CannotReachMessage();
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
                case "Wraith":
                    Console.WriteLine("\nIt's a shadow wraith, a hideous spectre, who eternally walks the line between life and death.");
                    break;
                default:
                    base.LookAt(objectName);
                    break;
            }
        }

        public override bool TakeObject(string objectName)
        {
            var activeObject = GameFunctions.FindObject(objectName, PointsOfInterest);
            switch (objectName)
            {
                case "Cloak":
                    if (PointsOfInterest.Contains(GameFunctions.FindObject("Wraith", PointsOfInterest)))
                    {
                        activeObject.AfraidToGetNearMessage();
                        return false;
                    }
                    else
                    {
                        base.TakeObject(objectName);
                        return true;
                    }
                default:
                    return base.TakeObject(objectName);
            }
        }

        public override void OpenObject(string objectName)
        {
            var activeObject = GameFunctions.FindObject(objectName, PointsOfInterest);
            switch (objectName)
            {
                case "Door at floor level":
                    if (PointsOfInterest.Contains(GameFunctions.FindObject("Wraith", PointsOfInterest)))
                        activeObject.AfraidToGetNearMessage();
                    else
                        base.OpenObject(objectName);
                    break;
                default:
                    base.OpenObject(objectName);
                    break;
            }
        }

        public override void HitObject(string objectName)
        {
            var activeObject = GameFunctions.FindObject(objectName, PointsOfInterest);
            switch (objectName)
            {
                case "Wraith":
                    activeObject.AfraidToGetNearMessage();
                    break;
                default:
                    base.HitObject(objectName);
                    break;
            }
        }

        public override void SpeakTo(string objectName)
        {
            switch(objectName)
            {
                case "Wraith":
                    GameFunctions.FindObject("Wraith", PointsOfInterest).DoesNotUnderstandMessage();
                    break;
                default:
                    base.SpeakTo(objectName);
                    break;
            }
        }
    }
}
