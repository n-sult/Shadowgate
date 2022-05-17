﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Rooms
{
    public class ColdRoom : Room
    {
        public static bool isWhiteGemUsed = false;
        bool isTrapDoorOpen = false;
        
        public ColdRoom()
        {
            // Items for Cold Room
            Shadowgate.ColdRoom.Sphere sphere = new Shadowgate.ColdRoom.Sphere("Sphere", true);

            // All POI for Cold Room
            PointOfInterest coldRoomHole = new PointOfInterest("Small hole next to the wooden door");
            PointOfInterest coldRoomPedestal = new PointOfInterest("Pedestal");
            Torch coldRoomLeftTorch = new Torch("Left Torch");
            Torch coldRoomRightTorch = new Torch("Right Torch");
            Entry coldRoomTrapDoor = new Entry("Trap Door", true, false, false, false);
            Entry smallWoodenDoor = new Entry("Small Wooden Door", true, false, false, false, "Lair");
            Entry doorFromColdRoomToDwarvenHall = new Entry("Door to Dwarven Hall", true, true, false, false, "Dwarven Hall");
            var coldRoomPOI = new List<PointOfInterest>() { sphere, coldRoomHole, coldRoomPedestal, coldRoomLeftTorch, coldRoomRightTorch, 
                coldRoomTrapDoor, smallWoodenDoor, doorFromColdRoomToDwarvenHall };

            RoomName = "Cold Room";
            FirstEntry = "You enter a cold room. The stench of flesh in decay pervades the small chamber. You begin to shiver. This room is really cold!";
            SubsequentEntry = "The room stinks of rotten meat.";
            PointsOfInterest = coldRoomPOI;
        }

        public override void MoveTo(string objectName)
        {
            switch(objectName)
            {
                case "Trap Door":
                    if (!isTrapDoorOpen)
                        OpenObject(objectName);
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nA broken fragment of a wooden ladder hangs from the opening." +
                            "\nAs you go down the trap door, you realize you took a big step. The fall is quite fatal.");
                        GameFunctions.GameOver();
                    }
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
                case "Small hole next to the wooden door":
                    if (!isWhiteGemUsed)
                        Console.WriteLine("\nIt's a small hole in the wall some three inches deep.");
                    else
                        Console.WriteLine("\nThe gem fits perfectly in the hole.");
                    break;
                case "Pedestal":
                    Console.WriteLine("\nIt's a large pedestal with iron trim.");
                    break;
                default:
                    base.LookAt(objectName);
                    break;
            }
        }

        public override void OpenObject(string objectName)
        {
            switch (objectName)
            {
                case "Trap Door":
                    base.OpenObject(objectName);
                    if (!isTrapDoorOpen)
                        isTrapDoorOpen = true;
                    GameFunctions.ReduceTorchFire();
                    break;
                default:
                    base.MoveTo(objectName);
                    break;
            }
        }

        public override void CloseObject(string objectName)
        {
                switch(objectName)
                {
                    case "Trap Door":
                        base.CloseObject(objectName);
                        if (isTrapDoorOpen)
                            isTrapDoorOpen = false;
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
                case "Small hole next to the wooden door":
                case "Pedestal":
                    ThatSmartsMessage();
                    break;
                default:
                    base.HitObject(objectName);
                    break;
            }
        }
    }
}