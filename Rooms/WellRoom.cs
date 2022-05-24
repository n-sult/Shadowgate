using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Rooms
{
    public class WellRoom : Room
    {
        public bool BigCoinUsed;

        public WellRoom()
        {
            // POI for Well Room
            PointOfInterest handle = new PointOfInterest("Well Handle");
            PointOfInterest wellCover = new PointOfInterest("Well Cover");
            Entry well = new Entry("Old Well", false, false, false, false, "River Styx");
            PointOfInterest wellRoomDoor = new PointOfInterest("Door next to the Well");
            Entry doorwayBackToGargoyleCave = new Entry("Doorway back to Gargoyle Cave", false, true, false, false, "Gargoyle Cave");
            var wellRoomPOI = new List<PointOfInterest>() { handle, wellCover, well, wellRoomDoor, doorwayBackToGargoyleCave };

            RoomName = "Well Room";
            FirstEntry = "The room seems to be made solely for the purpose of housing the well.";
            SubsequentEntry = "This room is dominated by a sophisticated, yet ancient well.";
            PointsOfInterest = wellRoomPOI;
        }

        public void OpenWellMessage()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nThe cover of the well is open.");
            GameFunctions.ReduceTorchFire();
        }

        public void CloseWellMessage()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nThe cover of the well is closed.");
            GameFunctions.ReduceTorchFire();
        }
        
        public override void MoveTo(string objectName)
        {
            
            switch(objectName)
            {
                case "Old Well":
                case "Well Cover":
                    Entry theWell = (Entry)GameFunctions.FindObject("Old Well", PointsOfInterest);
                    if (!theWell.IsDoorOpen) // if well is closed
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\nYou can't do that. \nYou have to open the door before you go through it.");
                    }
                    else // if well is open...
                    {
                        if (!BigCoinUsed) // check if big coin was used. if not, player dies
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nWith a mighty leap, you jump head first into the well. On the way down, you see no water below.");
                            GameFunctions.GameOver();
                        }
                        else // otherwise, they go to next room
                            base.MoveTo(objectName);
                    }
                    break;
                case "Door next to the Well":
                    GameFunctions.FindObject(objectName, PointsOfInterest).BumpedHeadMessage();
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
                case "Well Handle":
                    Console.WriteLine("\nIt's a small handle attached to an assortment of gears.");
                    break;
                case "Well Cover":
                    Entry theWell = (Entry)GameFunctions.FindObject("Old Well", PointsOfInterest);
                    if (!theWell.IsDoorOpen)
                        Console.WriteLine("\nThese wooden planks act as a cover for the well.");
                    else
                        Console.WriteLine("\nThe cover of the well is opened.");
                    break;
                case "Door next to the Well":
                    Console.WriteLine("\nThis door is covered with dust and dirt.");
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
                case "Well Cover":
                    OpenWellMessage();
                    Entry theWell = (Entry)GameFunctions.FindObject("Old Well", PointsOfInterest);
                    if (!theWell.IsDoorOpen)
                        theWell.IsDoorOpen = true;
                    break;
                case "Door next to the Well":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nThe door is locked.");
                    GameFunctions.ReduceTorchFire();
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
                case "Well Cover":
                    CloseWellMessage();
                    Entry theWell = (Entry)GameFunctions.FindObject("Old Well", PointsOfInterest);
                    if (theWell.IsDoorOpen)
                        theWell.IsDoorOpen = false;
                    break;
                default:
                    base.CloseObject(objectName);
                    break;
            }
        }

        public override void UseObject(string objectName)
        {
            switch(objectName)
            {
                case "Well Handle":
                    Entry theWell = (Entry)GameFunctions.FindObject("Old Well", PointsOfInterest);
                    if (!theWell.IsDoorOpen)
                    {
                        OpenWellMessage();
                        theWell.IsDoorOpen = true;
                    }
                    else
                    {
                        CloseWellMessage();
                        theWell.IsDoorOpen = false;
                    }
                    GameFunctions.ReduceTorchFire();
                    break;
                default:
                    base.UseObject(objectName);
                    break;
            }
        }

        public override void HitObject(string objectName)
        {
            switch(objectName)
            {
                case "Well Handle":
                    GameFunctions.FindObject(objectName, PointsOfInterest).ThatSmartsMessage();
                    break;
                default:
                    base.HitObject(objectName);
                    break;
            }
        }
    }
}
