using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Rooms
{
    public class OutsideTheCastle : Room
    {
        bool skullOpen = false; // skull starts as closed

        public OutsideTheCastle()
        {
            // Items for Outside Castle
            Key key1 = new Key("Key 1", true, false);

            // All POI for Outside Castle
            PointOfInterest stoneSkull = new PointOfInterest("Stone Skull");
            PointOfInterest stoneWall = new PointOfInterest("Wall");
            Entry outdoorCastleDoor = new Entry("Castle Door", true, false, false, false, "Entrance Hall");
            var outsideCastlePOI = new List<PointOfInterest>() { key1, stoneSkull, stoneWall, outdoorCastleDoor };

            RoomName = "Outside the Castle";
            FirstEntry = "The last thing that you remember is standing before the wizard Lakmir as he waved his hands. " +
                "\n\nNow you find yourself staring at an entryway which lies at the edge of a forest. " +
                "\n\nThe Druid's words ring in your ears: \"Within the castle Shadowgate, lies your quest. " +
                "The dreaded Warlock Lord will use his Black Magic to raise the Behemoth from the dark depths. " +
                "The combination of his evil arts and the great Titan's power will surely destroy us all! " +
                "You are the last of the line of Kings, the seed of prophecy that was foretold eons ago. " +
                "Only you can stop the evil one from darkening our world forever! Fare thee well.\" " +
                "\n\nGritting your teeth, you swear by your god's name that you will destroy the Warlock Lord!";
            SubsequentEntry = "It's the entrance to Shadowgate. You can hear wolves howling deep in the forest behind you...";
            PointsOfInterest = outsideCastlePOI;
        }

        public override void MoveTo(string objectName)
        {
            var activeObject = GameFunctions.FindObject(objectName, PointsOfInterest);
            switch (objectName)
            {
                case "Castle Door":
                    if (!(activeObject as Entry).IsDoorOpen)
                        OpenObject(objectName);
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
            Console.ForegroundColor = ConsoleColor.White;
            switch (objectName)
            {
                case "Stone Skull":
                    Console.WriteLine("\nIt's the skull of some creature. It's meaning seems quite clear: death lurks inside.");
                    break;
                case "Wall":
                    Console.WriteLine("\nIt's a stone wall.");
                    break;
                default:
                    base.LookAt(objectName);
                    break;
            }
        }

        public override void OpenObject(string objectName)
        {
            var activeObject = GameFunctions.FindObject(objectName, PointsOfInterest);
            switch (objectName)
            {

                case "Castle Door":
                    if (!(activeObject as Entry).IsDoorOpen)
                    {
                        base.OpenObject(objectName);
                        Console.WriteLine("It's the door leading into the castle Shadowgate.");
                    }
                    else
                        base.OpenObject(objectName);
                    break;
                case "Stone Skull":
                    if (!skullOpen)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\nAs if by magic, the skull rises.");

                        PointOfInterest key = GetPOI("Key 1");
                        if (PointsOfInterest.Contains(key))
                        {
                            Console.WriteLine("There's a key inside!");
                            key.IsHidden = false;
                        }
                        skullOpen = true;
                        if (key != null)
                            key.IsHidden = false;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("\nThe Skull is opened.");
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
            var activeObject = GameFunctions.FindObject(objectName, PointsOfInterest);
            switch (objectName)
            {
                case "Stone Skull":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\nThe Skull is closed.");
                    if (skullOpen)
                    {
                        skullOpen = false;
                        PointOfInterest key = GetPOI("Key 1");
                        if (key != null)
                            key.IsHidden = true;
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
                case "Wall":
                    ThatSmartsMessage();
                    break;
                default:
                    base.HitObject(objectName);
                    break;
            }
        }
    }
}
