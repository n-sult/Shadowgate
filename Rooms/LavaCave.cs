using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Rooms
{
    public class LavaCave : Room
    {
        public bool MotariUsed;
        
        public LavaCave()
        {
            // POI for Lava Cave
            PointOfInterest bridgeway = new PointOfInterest("Bridgeway");
            PointOfInterest lava = new PointOfInterest("Lava Pit");
            PointOfInterest statue = new PointOfInterest("Giant Statue");
            Entry doorwayBehindStatue = new Entry("Doorway behind the giant statue", false, true, false, false, "Switch Cave");
            Entry doorwayFromLavaCaveToGargoyleCave = new Entry("Door to Gargoyle Cave", false, true, false, false, "Gargoyle Cave");
            var lavaCavePOI = new List<PointOfInterest>() { bridgeway, lava, statue, doorwayBehindStatue, doorwayFromLavaCaveToGargoyleCave };

            RoomName = "Lava Cave";
            FirstEntry = "Sulfurous fumes rise from the hot molten lava some thirty feet below you. \n\nSwimming would not be wise.";
            SubsequentEntry = "This room is filled with lava.";
            PointsOfInterest = lavaCavePOI;
        }

        void DieToLava()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            GameFunctions.WriteLine("\nShouting a battle cry, you catapult yourself off of the platform. " +
                "\nYou are brave, warrior, but stupid! Your body explodes as you plunge into the lava.");
            GameFunctions.GameOver();
        }

        public override void MoveTo(string objectName)
        {
            switch(objectName)
            {
                case "Lava Pit":
                    DieToLava();
                    break;
                case "Doorway behind the giant statue":
                    if (!MotariUsed)
                        DieToLava();
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
                case "Lava Pit":
                    GameFunctions.WriteLine("\nThis dark-red lava comes from the earth's core.");
                    break;
                case "Bridgeway":
                    GameFunctions.WriteLine("\nIt's a narrow stone bridgeway.");
                    break;
                case "Giant Statue":
                    if (!MotariUsed)
                        GameFunctions.WriteLine("\nThis huge statue is made of precious metals. It holds a basin of smoldering coals.");
                    else
                        GameFunctions.WriteLine("\nThe eerie statue descends into the lava.");
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
                case "Bridgeway":
                    GameFunctions.FindObject(objectName, PointsOfInterest).ThatSmartsMessage();
                    break;
                default:
                    base.HitObject(objectName);
                    break;
            }
        }
    }
}
