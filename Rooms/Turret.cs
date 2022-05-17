using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Rooms
{
    public class Turret : Room
    {
        public static bool WyvernDead;

        public Turret()
        {
            // Items for Turret
            Shadowgate.Turret.Talisman talisman = new Shadowgate.Turret.Talisman("Talisman");

            // POI for Turret
            OutsideView turretSky = new OutsideView("The Night Sky");
            PointOfInterest turretPedestal = new PointOfInterest("Pedestal");
            PointOfInterest wyvern = new PointOfInterest("Wyvern");
            Entry fromTurretToBrazierRoom = new Entry("Stairs to Brazier Room", false, true, false, false, "Brazier Room");
            var turretPOI = new List<PointOfInterest>() { wyvern, turretSky, talisman, turretPedestal, fromTurretToBrazierRoom };

            RoomName = "Turret";
            FirstEntry = "As you stand on the turret, an eerie blue dragon appears in the clear starry sky.";
            SubsequentEntry = "You're standing on a turret.";
            PointsOfInterest = turretPOI;
        }

        public static void DieToWyvern()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nWith the speed of lightning, the wyvern wraps its tail around your neck. \nYou die, screaming silently.");
            GameFunctions.GameOver();
        }

        public override void LookAt(string objectName)
        {
            switch(objectName)
            {
                case "Wyvern":
                    Console.WriteLine("\nIt's a wyvern! This beastie is a distant cousin of a dragon but is smaller and fiercer!");
                    break;
                case "Pedestal":
                    Console.WriteLine("\nThis pedestal is some thirteen inches tall, and made of some unknown metal.");
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
                case "Wyvern":
                    DieToWyvern();
                    break;
                case "Pedestal":
                    GameFunctions.FindObject(objectName, PointsOfInterest).ThatSmartsMessage();
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
                case "Wyvern":
                    GameFunctions.FindObject(objectName, PointsOfInterest).DoesNotUnderstandMessage();
                    break;
                default:
                    base.SpeakTo(objectName);
                    break;
            }
        }
    }
}
