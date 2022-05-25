using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Rooms
{
    public class SwitchCave : Room
    {
        public bool CylinderOpen;
        public List<string> SwitchSequence = new List<string>() { };
        

        public SwitchCave()
        {
            // Items for Switch Cave
            Shadowgate.SwitchCave.Orb orb = new Shadowgate.SwitchCave.Orb("Orb", true);

            // POI for Switch Cave
            PointOfInterest cylinder = new PointOfInterest("Metal Cylinder");
            Shadowgate.SwitchCave.Switch leftSwitch = new Shadowgate.SwitchCave.Switch("Left Switch", false);
            Shadowgate.SwitchCave.Switch middleSwitch = new Shadowgate.SwitchCave.Switch("Middle Switch", false);
            Shadowgate.SwitchCave.Switch rightSwitch = new Shadowgate.SwitchCave.Switch("Right Switch", false);
            PointOfInterest pit = new PointOfInterest("Pit");
            Entry doorwayFromSwitchCaveToLavaCave = new Entry("Doorway to Lava Cave", false, true, false, false, "Lava Cave");
            var switchCavePOI = new List<PointOfInterest>() { cylinder, orb, leftSwitch, middleSwitch, rightSwitch, pit, doorwayFromSwitchCaveToLavaCave };

            RoomName = "Switch Cave";
            FirstEntry = "Stalagmites surround this room like the cavernous jaws of a huge beast.";
            SubsequentEntry = "It's a dark and eerie cave.";
            PointsOfInterest = switchCavePOI;
        }

        public override void MoveTo(string objectName)
        {
            switch (objectName)
            {
                case "Pit":
                    Console.ForegroundColor = ConsoleColor.Red;
                    GameFunctions.WriteLine("\nAs you approach the pit, an enormous monster rises from it! It's a furry, winged beast sporting killer teeth " +
                        "and nails! \nIt appears that you woke the sleeping guard from his beauty sleep. \nHe decides to eat you for breakfast."); // altered line
                    GameFunctions.GameOver();
                    break;
                default:
                    base.MoveTo(objectName);
                    break;
            }
        }

        public override void LookAt(string objectName)
        {
            switch (objectName)
            {
                case "Pit":
                    GameFunctions.WriteLine("\nIt looks like a large, very deep pit.");
                    break;
                case "Metal Cylinder":
                    GameFunctions.WriteLine("\nIt's a strong-looking metal cylinder.");
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
                case "Metal Cylinder":
                    GameFunctions.FindObject(objectName, PointsOfInterest).ThatSmartsMessage();
                    break;
                default:
                    base.HitObject(objectName);
                    break;
            }
        }
    }
}
