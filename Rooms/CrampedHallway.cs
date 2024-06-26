﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Rooms
{
    public class CrampedHallway : Room
    {
        bool _isBookOpen = false;

        public CrampedHallway()
        {
            // Items for Cramped Hallways
            Key key2 = new Key("Key 2", true, false);

            // All POI for Cramped Hallway
            PointOfInterest bookOnAPedestal = new PointOfInterest("Book on a pedestal");
            PointOfInterest crampedHallwayCandle = new PointOfInterest("Candle");
            Shadowgate.CrampedHallway.HolyTorch holyTorch = new Shadowgate.CrampedHallway.HolyTorch("Closer Torch");
            PointOfInterest backWall = new PointOfInterest("Back wall");
            Torch crampedHallwayTorch = new Torch("Farther Torch");
            Entry crampedHallwayOpening = new Entry("White Stone on the wall", true, false, false, false, "Hidden Room");
            Entry hallwayToDwarvenHall = new Entry("Hallway around the corner", false, true, false, false, "Dwarven Hall");
            Entry entryFromCrampedHallwayToEntranceHall = new Entry("Door to Entrance Hall", true, true, false, false, "Entrance Hall");
            var crampedHallwayPOI = new List<PointOfInterest>() { key2, bookOnAPedestal, crampedHallwayCandle, holyTorch, crampedHallwayTorch, backWall,
                crampedHallwayOpening, hallwayToDwarvenHall, entryFromCrampedHallwayToEntranceHall};

            RoomName = "Cramped Hallway";
            FirstEntry = "The stone walls seem uncomfortably close as you walk down the stairs.";
            SubsequentEntry = "The stone passage winds to an unseen end.";
            PointsOfInterest = crampedHallwayPOI;
        }

        void DieToTrap()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            GameFunctions.WriteLine("\nWhen you remove the book from its pedestal, the floor collapses, and you fall to your death.");
            GameFunctions.GameOver();
        }
        
        public override void MoveTo(string objectName)
        {
            switch(objectName)
            {
                case "White Stone on the wall":
                    OpenObject(objectName);
                    break;
                case "Book on a pedestal":
                    DieToTrap();
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
                case "Book on a pedestal":
                    GameFunctions.WriteLine("\nIt's an ancient tome. It seems that no one has disturbed it's pages for centuries.");
                    break;
                case "Candle":
                    GameFunctions.WriteLine("\nIt's a small candle, perfect for reading.");
                    break;
                case "Back wall":
                    GameFunctions.WriteLine("\nThese stones seem to be set loosely in the mortar.");
                    break;
                default:
                    base.LookAt(objectName);
                    break;
            }
        }

        public override bool TakeObject(string objectName)
        {
            switch (objectName)
            {
                case "Book on a pedestal":
                    DieToTrap();
                    return false;
                default:
                    if (base.TakeObject(objectName))
                        return true;
                    else
                        return false;
            }
        }

        public override void OpenObject(string objectName)
        {
            var activeObject = GameFunctions.FindObject(objectName, PointsOfInterest);
            switch (objectName)
            {
                case "White Stone on the wall":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    GameFunctions.WriteLine("\nThe stone falls away to reveal a secret passage!");
                    activeObject.ObjectName = "Wall Opening";
                    (activeObject as Entry).IsDoorOpen = true;
                    GameFunctions.ReduceTorchFire();
                    break;
                case "Wall Opening":
                    Console.ForegroundColor = ConsoleColor.White;
                    GameFunctions.WriteLine("\nThe wall is opened.");
                    break;
                case "Book on a pedestal":
                    if (!_isBookOpen)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        GameFunctions.WriteLine("\nThe book is opened and examined. \nA rectangular hole has been cut out of the inside of the book.");
                        
                        var key = GameFunctions.FindObject("Key 2", PointsOfInterest);
                        if (key != null)
                        {
                            GameFunctions.WriteLine("There's a key inside!");
                            key.IsHidden = false;
                        }

                        _isBookOpen = true;
                        GameFunctions.ReduceTorchFire();
                        break;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        GameFunctions.WriteLine("\nThe book is opened.");
                        break;
                    }
                default:
                    base.OpenObject(objectName);
                    break;
            }
        }

        public override void CloseObject(string objectName)
        {
            var activeObject = GameFunctions.FindObject(objectName, PointsOfInterest);
            switch (objectName)
            {
                case "White Stone on the wall":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    GameFunctions.WriteLine("\nThe wall is closed.");
                    break;
                case "Wall Opening":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    GameFunctions.WriteLine("\nThe wall is closed.");
                    activeObject.ObjectName = "White Stone on the wall";
                    (activeObject as Entry).IsDoorOpen = false;
                    GameFunctions.ReduceTorchFire();
                    break;
                case "Book on a pedestal":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    GameFunctions.WriteLine("\nThe book is closed");
                    
                    var key = GameFunctions.FindObject("Key 2", PointsOfInterest);
                    if (key != null)
                        key.IsHidden = true;

                    if (_isBookOpen)
                        _isBookOpen = false;
                    GameFunctions.ReduceTorchFire();
                    break;
                default:
                    base.CloseObject(objectName);
                    break;
            }
        }

        public override void HitObject(string objectName)
        {
            switch (objectName)
            {
                case "White Stone on the wall":
                    OpenObject(objectName);
                    break;
                case "Back wall":
                    ThatSmartsMessage();
                    break;
                case "Book on a pedestal":
                    DieToTrap();
                    break;
                default:
                    base.HitObject(objectName);
                    break;
            }
        }

        public override void UseObject(string objectName)
        {
            switch(objectName)
            {
                case "Book on a pedestal":
                    DieToTrap();
                    break;
                default:
                    base.UseObject(objectName);
                    break;
            }
        }
    }
}
