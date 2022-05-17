using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Rooms
{
    public class GargoyleCave : Room
    {
        public static bool IlluminaUsed;
        
        public GargoyleCave()
        {
            // POI for Gargoyle Cave
            PointOfInterest leftGargoyle = new PointOfInterest("Gargoyle statue left of the Central Doorway");
            PointOfInterest rightGargoyle = new PointOfInterest("Gargoyle statue right of the Central Doorway");
            Entry centralDoorway = new Entry("Central Doorway", false, true, false, false, "Well Room");
            Entry sideDoorway = new Entry("Doorway to the right", false, true, false, false, "Lava Cave");
            Entry doorBackToUndergroundHall = new Entry("Doorway back to Underground Hall", false, true, false, false, "Underground Hall");
            var gargoyleCavePOI = new List<PointOfInterest>() { leftGargoyle, rightGargoyle, centralDoorway, sideDoorway, doorBackToUndergroundHall };

            RoomName = "Gargoyle Cave";
            FirstEntry = "On the opposite wall are a pair of stone beasts guarding a dark archway.";
            SubsequentEntry = "You are in a dark and gloomy cavern.";
            PointsOfInterest = gargoyleCavePOI;

            GameFunctions.RoomEnteredEvent += (roomName) => { if (roomName == RoomName) IlluminaUsed = false; };
        }

        void DieToGargoyle()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nSuddenly, the beasts begin to shudder and their eyes begin to glow red! " +
                    "\nThe gargoyles, angered at your presence, spring from their frozen state and rip you to pieces! " +
                    "\nThere's not enough left of you to even feed to the birds.");
            GameFunctions.GameOver();
        }

        public override void MoveTo(string objectName)
        {
            switch(objectName)
            {
                case "Central Doorway":
                    if (!IlluminaUsed)
                        DieToGargoyle();
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
                case "Gargoyle statue left of the Central Doorway":
                case "Gargoyle statue right of the Central Doorway":
                    Console.WriteLine("\nThis stone statue is some three and a half feet tall and ugly as all heck. " +
                        "\nIt is very cold to the touch.");
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
                case "Gargoyle statue left of the Central Doorway":
                case "Gargoyle statue right of the Central Doorway":
                    DieToGargoyle();
                    break;
                default:
                    base.HitObject(objectName);
                    break;
            }
        }
    }
}
