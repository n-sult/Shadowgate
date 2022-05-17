using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Rooms
{
    public class BanquetHall : Room
    {
        public static bool DoorUnderBalconyLocked;
        
        public BanquetHall()
        {
            // Items for Banquet Hall
            Key key4 = new Key("Key 4", true, false);
            Shadowgate.BanquetHall.BanquetMirror banquetMirror = new Shadowgate.BanquetHall.BanquetMirror("Mirror");
            Shadowgate.BanquetHall.Crest crest = new Shadowgate.BanquetHall.Crest("Crest");

            // POI for Banquet Hall
            PointOfInterest balcony = new PointOfInterest("Balcony");
            PointOfInterest tapestry = new PointOfInterest("Tapestry");
            PointOfInterest banquetRug = new PointOfInterest("Banquet Rug");
            Entry doorUnderTheBalcony = new Entry("Door under the balcony", true, false, true, false, "Key 4", "Small Corridor");
            Entry doorAtTheHeadOfTheStairs = new Entry("Door at the head of the stairs", true, false, true, false, "Key 5", "Sphinx's Chamber");
            Entry doorOnTheBalcony = new Entry("Door on the balcony", true, false, true, false, "Key 6", "Brazier Room");
            Entry doorFromBanquetHallToDraftyHall = new Entry("Door back to Drafty Hall", true, true, false, false, "Drafty Hallway");
            var banquetHallPOI = new List<PointOfInterest>() { key4, balcony, banquetMirror, crest, tapestry, banquetRug, doorUnderTheBalcony, 
                doorAtTheHeadOfTheStairs, doorOnTheBalcony, doorFromBanquetHallToDraftyHall };

            RoomName = "Banquet Hall";
            FirstEntry = "You are awed by the majestic beauty of this immense banquet hall.";
            SubsequentEntry = "It's a large banquet hall.";
            PointsOfInterest = banquetHallPOI;
        }

        public override void LookAt(string objectName)
        {
            switch(objectName)
            {
                case "Balcony":
                    Console.WriteLine("\nIt's a sturdy stone balcony.");
                    break;
                case "Tapestry":
                    Console.WriteLine("\nIt's a silk tapestry.");
                    break;
                case "Banquet Rug":
                    Console.WriteLine("\nIt's a beautifully woven rug.");
                    break;
                default:
                    base.LookAt(objectName);
                    break;
            }
        }

        public override bool TakeObject(string objectName)
        {
            switch(objectName)
            {
                case "Tapestry":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nYou can't reach it from here.");
                    return false;
                default:
                    base.TakeObject(objectName);
                    return false;
            }
        }
    }
}
