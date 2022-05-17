using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Rooms
{
    public class Closet : Room
    {
        public Closet()
        {
            // Items for Closet
            Shadowgate.Closet.Sling sling = new Shadowgate.Closet.Sling("Sling");
            Shadowgate.Closet.Sword sword = new Shadowgate.Closet.Sword("Sword");

            // All POI for the Closet
            PointOfInterest closetShelf = new PointOfInterest("Shelf");
            Entry entryFromClosetToEntranceHall = new Entry("Closet Door back to Entrance Hall", true, true, false, false, "Entrance Hall");
            var closetPOI = new List<PointOfInterest>() { closetShelf, sword, sling, entryFromClosetToEntranceHall };

            RoomName = "Closet";
            FirstEntry = "Oh! As you enter, you can see a sword and a sling inside.";
            SubsequentEntry = "You are in a small, cramped closet.";
            PointsOfInterest = closetPOI;
        }

        public override void LookAt(string objectName)
        {
            switch(objectName)
            {
                case "Shelf":
                    Console.WriteLine("\nA very sturdy shelf rests against the wall.");
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
                case "Shelf":
                    ThatSmartsMessage();
                    break;
                default:
                    base.HitObject(objectName);
                    break;
            }
        }
    }
}
