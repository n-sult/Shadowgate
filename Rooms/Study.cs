using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Rooms
{
    public class Study : Room
    {
        public static bool TerrakkUsed;
        public static bool GlobeOpen;
        public static bool WoodpileLit;

        public Study()
        {
            // Items for Study
            Shadowgate.Study.Bellows bellows = new Shadowgate.Study.Bellows("Bellows");
            Bottle bottle5 = new Bottle("Bottle 5", true);
            Shadowgate.Study.Goblet goblet = new Shadowgate.Study.Goblet("Goblet");
            Shadowgate.Study.Poker poker = new Shadowgate.Study.Poker("Poker");
            Key key6 = new Key("Key 6", true, false);

            // POI for Study
            PointOfInterest fireplace = new PointOfInterest("Fireplace");
            PointOfInterest woodpile = new PointOfInterest("Woodpile");
            PointOfInterest globe = new PointOfInterest("Globe");
            PointOfInterest studyRug = new PointOfInterest("Blue Rug");
            OutsideView studyWindow = new OutsideView("Window");
            Entry doorFromStudyToLibrary = new Entry("Door to Library", false, true, false, false, "Library");
            var studyPOI = new List<PointOfInterest>() { key6, bottle5, fireplace, woodpile, bellows, poker, globe, goblet, 
                studyRug, studyWindow, doorFromStudyToLibrary };

            RoomName = "Study";
            FirstEntry = "This room is dominated by a large fireplace set in a red brick wall.";
            SubsequentEntry = "You're in a room with a fireplace.";
            PointsOfInterest = studyPOI;
        }

        public override void LookAt(string objectName)
        {
            switch(objectName)
            {
                case "Fireplace":
                    Console.WriteLine("\nThis fireplace is quite large.");
                    break;
                case "Globe":
                    Console.WriteLine("\nIt is a globe mounted on a stand for display. It shows all of the known lands.");
                    if (!TerrakkUsed)
                        Console.WriteLine("Looking closely, you can see a seam along the equator.");
                    break;
                case "Blue Rug":
                    Console.WriteLine("\nIt's a beautifully woven rug.");
                    break;
                case "Woodpile":
                    Console.WriteLine("\nKindling rests within the fireplace.");
                    break;
                default:
                    base.LookAt(objectName);
                    break;
            }
        }

        public override void OpenObject(string objectName)
        {
            switch(objectName)
            {
                case "Globe":
                    if (TerrakkUsed && !GlobeOpen)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\nThe globe is open.");
                        GlobeOpen = true;

                        string[] globeItems = { "Key 6", "Bottle 5" };

                        foreach (string item in globeItems) // search desk drawer to see if the item is in it. if so, unhide it.
                            if (GameFunctions.FindObject(item, PointsOfInterest) is not null)
                                GameFunctions.FindObject(item, PointsOfInterest).IsHidden = false;
                        GameFunctions.ReduceTorchFire();
                    }
                    else if (GlobeOpen)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\nThe globe is opened.");
                        GameFunctions.ReduceTorchFire();
                    }
                    else
                        base.OpenObject(objectName);
                    break;
                default:
                    base.OpenObject(objectName);
                    break;
            }
        }

        public override void CloseObject(string objectName)
        {
            switch(objectName)
            {
                case "Globe":
                    
                    if (GlobeOpen)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\nYou closed the globe.");
                        GlobeOpen = false;

                        string[] globeItems = { "Key 6", "Bottle 5" };

                        foreach (string item in globeItems) // search desk drawer to see if the item is in it. if so, unhide it.
                            if (GameFunctions.FindObject(item, PointsOfInterest) is not null)
                                GameFunctions.FindObject(item, PointsOfInterest).IsHidden = true;
                        GameFunctions.ReduceTorchFire();
                    }
                    else if (GlobeOpen)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\nThe globe is closed.");
                        GameFunctions.ReduceTorchFire();
                    }
                    else
                        base.CloseObject(objectName);
                    break;
            }
        }
    }
}
