using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Rooms
{
    public class Observatory : Room
    {
        public bool StarMapOpen;

        public Observatory()
        {
            // Items for Observatory
            Scroll scroll5 = new Scroll("Scroll 5", false);
            Shadowgate.Observatory.Star star = new Shadowgate.Observatory.Star("Star");
            Shadowgate.Observatory.Rod rod = new Shadowgate.Observatory.Rod("Rod", true);

            // POI for Observatory
            PointOfInterest observatoryTable = new PointOfInterest("Table");
            OutsideView observatoryWindow1 = new OutsideView("Left Window");
            OutsideView observatoryWindow2 = new OutsideView("Window next to the telescope");
            PointOfInterest telescope = new PointOfInterest("Telescope");
            PointOfInterest starMap = new PointOfInterest("Star Map");
            Entry woodenLadder = new Entry("Wooden ladder to the next floor", false, true, false, false, "Watchtower");
            Entry doorFromObservatoryToSphinxChamber = new Entry("Stairs to Sphinx Chamber", false, true, false, false, "Sphinx's Chamber");
            var observatoryPOI = new List<PointOfInterest>() { observatoryTable, scroll5, observatoryWindow1, observatoryWindow2, telescope, 
                star, rod, starMap, woodenLadder, doorFromObservatoryToSphinxChamber };

            RoomName = "Observatory";
            FirstEntry = "A telescope is beside the window. A star map is on the wall. This must be an observatory.";
            SubsequentEntry = "It's an observatory.";
            PointsOfInterest = observatoryPOI;
        }

        public override void LookAt(string objectName)
        {
            switch(objectName)
            {
                case "Table":
                    GameFunctions.WriteLine("\nIt's a round wooden table.");
                    break;
                case "Star Map":
                    GameFunctions.WriteLine("\nIt's a map of the known galaxy. You can see billions and billions of stars. " +
                        "\nThe map seems to be only loosely attached to the wall.");
                    break;
                case "Telescope":
                    GameFunctions.WriteLine("\nAs you peer through the telescope, you are amazed by the clarity of the night sky.");
                    break;
                default:
                    base.LookAt(objectName);
                    break;
            }
        }

        public override void OpenObject(string objectName)
        {
            var theRod = GameFunctions.FindObject("Rod", PointsOfInterest);
            switch (objectName)
            {
                case "Star Map":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    GameFunctions.WriteLine("\nThe Star Map is open.");
                    if (!PointsOfInterest.Contains(GameFunctions.FindObject(theRod.ObjectName, PointsOfInterest)))
                        GameFunctions.WriteLine("There's a rod behind the poster!");
                    if (!StarMapOpen)
                    {
                        if (PointsOfInterest.Contains(theRod))
                            theRod.IsHidden = false;
                        StarMapOpen = true;
                    }
                    GameFunctions.ReduceTorchFire();
                    break;
                default:
                    base.OpenObject(objectName);
                    break;
            }
        }

        public override void CloseObject(string objectName)
        {
            var theRod = GameFunctions.FindObject("Rod", PointsOfInterest);

            switch(objectName)
            {
                case "Star Map":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    GameFunctions.WriteLine("\nThe Star Map is closed");
                    if (StarMapOpen)
                    {
                        if (PointsOfInterest.Contains(theRod))
                            theRod.IsHidden = true;
                        StarMapOpen = false;
                    }
                    GameFunctions.ReduceTorchFire();
                    break;
                default:
                    base.CloseObject(objectName);
                    break;
            }
        }

        public override void HitObject(string objectName)
        {
            switch(objectName)
            {
                case "Telescope":
                case "Table":
                    GameFunctions.FindObject(objectName, PointsOfInterest).ThatSmartsMessage();
                    break;
                default:
                    HitObject(objectName);
                    break;
            }
        }
    }
}
