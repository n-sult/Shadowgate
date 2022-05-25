using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Rooms
{
    public class EntranceHall : Room
    {
        public EntranceHall()
        {
            Torch entranceHallTorch1 = new Torch("Left Torch");
            Torch entranceHallTorch2 = new Torch("Right Torch");
            PointOfInterest entranceHallRug = new PointOfInterest("Red Rug");
            Entry entranceFarDoor = new Entry("Far Door", true, false, true, false, "Key 1", "Cramped Hallway");
            Entry closetDoor = new Entry("Closet Door", true, false, true, false, "Key 2", "Closet");
            Entry entryToOutsideCastle = new Entry("Door to Outside the Castle", true, true, false, false, "Outside the Castle");
            var entranceHallPOI = new List<PointOfInterest>() { entranceHallTorch1, entranceHallTorch2, entranceHallRug, entranceFarDoor, 
                closetDoor, entryToOutsideCastle };

            RoomName = "Entrance Hall";
            FirstEntry = "\"That pitiful wizard Lakmir was a fool to send a buffoon like you to stop me. " +
                "You will surely regret it for the only thing here for you is a horrible death!\" \n\nThe sound of maniacal laughter echoes in your ears.";
            SubsequentEntry = "You stand in a long corridor. Huge stone archways line the entire hall.";
            PointsOfInterest = entranceHallPOI;
        }

        public override void LookAt(string objectName)
        {
            switch(objectName)
            {
                case "Red Rug":
                    GameFunctions.WriteLine("\nIt's a beautifully woven rug.");
                    break;
                default:
                    base.LookAt(objectName);
                    break;
            }
        }
    }
}
