using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Rooms
{
    public class Tomb : Room
    {
        public bool IsMummyBurned = false;
        public bool IsScepterTaken = false;
        bool _isMiddleLeftCoffinOpen = false;
        bool _isMiddleRightCoffinOpen = false;
        bool _isClosestRightCoffinOpen = false;

        public Tomb()
        {
            // Items for Tomb
            Shadowgate.Tomb.Scepter scepter = new Shadowgate.Tomb.Scepter("Scepter", true);
            Shadowgate.Tomb.CopperCoin copperCoin1 = new Shadowgate.Tomb.CopperCoin("Copper Coin");
            Shadowgate.Tomb.CopperCoin copperCoin2 = new Shadowgate.Tomb.CopperCoin("Copper Coin");
            Shadowgate.Tomb.CopperCoin copperCoin3 = new Shadowgate.Tomb.CopperCoin("Copper Coin");
            List<Item> copperCoins = new List<Item>() { copperCoin1, copperCoin2, copperCoin3 };
            Bag bag2 = new Bag("Bag 2", true, copperCoins);

            // All POI for Tomb
            PointOfInterest closestCoffinOnTheLeft = new PointOfInterest("Closest coffin on the left");
            PointOfInterest middleCoffinFromTheLeft = new PointOfInterest("Middle coffin on the left");
            PointOfInterest closestCoffinOnTheRight = new PointOfInterest("Closest coffin on the right");
            PointOfInterest middleCoffinOnTheRight = new PointOfInterest("Middle coffin on the right");
            PointOfInterest farthestCoffinOnTheRight = new PointOfInterest("Farthest coffin on the right");
            PointOfInterest slime = new PointOfInterest("Green slime", true);
            PointOfInterest mummy = new PointOfInterest("Mummy", true);
            Entry doorFromTombToMirrorRoom = new Entry("Coffin leading to Mirror Room", false, true, false, false, "Mirror Room");
            Entry doorFromTombToDwarvenHall = new Entry("Wooden door to Dwarven Hall", true, true, false, false, "Dwarven Hall");
            var tombPOI = new List<PointOfInterest>() { closestCoffinOnTheLeft, middleCoffinFromTheLeft, slime, closestCoffinOnTheRight, mummy, scepter,
                middleCoffinOnTheRight, bag2, farthestCoffinOnTheRight, doorFromTombToMirrorRoom, doorFromTombToDwarvenHall};

            RoomName = "Tomb";
            FirstEntry = "This long, cold hallway is lined on either side by half a dozen coffins.";
            SubsequentEntry = "The walls, the floor, and the coffins are all made of stone.";
            PointsOfInterest = tombPOI;
        }

        public void LidOpenMessage()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nThe lid of the coffin is open.");
            GameFunctions.ReduceTorchFire();
        }

        public void LidCloseMessage()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nThe lid of the coffin is closed.");
            GameFunctions.ReduceTorchFire();
        }

        void DieToSlime()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nYou try to pass the slime but it engulfs your body, dissolving it in seconds. " +
                "\n...you die instantly. No pain, no nothing. You were slimed.");
            GameFunctions.GameOver();
        }

        public override void MoveTo(string objectName)
        {
            switch(objectName)
            {
                case "Coffin leading to Mirror Room":
                    if (_isMiddleLeftCoffinOpen)
                        DieToSlime();
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
            switch (objectName)
            {
                case "Closest coffin on the left":
                    Console.WriteLine("\nThe cold, marble coffin lid seals an ancient death bed.");
                    break;
                case "Middle coffin on the left":
                    Console.WriteLine("\nThis tomb is sealed with a silver lid.");
                    break;
                case "Closest coffin on the right":
                    Console.WriteLine("\nThis standing sarcophagus is sealed with a dragon scale cover.");
                    break;
                case "Middle coffin on the right":
                    Console.WriteLine("\nThe lid to this coffin is made of solid gold. It must be worth a fortune.");
                    break;
                case "Farthest coffin on the right":
                    Console.WriteLine("\nIt's a cold stone coffin.");
                    break;
                case "Green slime":
                    Console.WriteLine("\nThe green slime is very thick and is warm to the touch.");
                    break;
                case "Mummy":
                    Console.WriteLine("\nThis carefully embalmed six-footer stands straight and still.");
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
                case "Closest coffin on the left":
                    LidOpenMessage();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nAs you open the tomb, a banshee flies out and emits an ear-shattering scream. " +
                            "\nYou're all right, but it is very hard to hear.");
                    break;
                case "Middle coffin on the left":
                    LidOpenMessage();
                    if (!_isMiddleLeftCoffinOpen)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\nGreen slime has covered the floor of the coffin you opened. It's quite disgusting!");
                        _isMiddleLeftCoffinOpen = true;
                        GameFunctions.FindObject("Green slime", PointsOfInterest).IsHidden = false;
                    }
                    break;
                case "Closest coffin on the right":
                    LidOpenMessage();
                    if (!_isClosestRightCoffinOpen)
                    {
                        if (!IsMummyBurned)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("\nA mummy stands silently before you.");
                            GameFunctions.FindObject("Mummy", PointsOfInterest).IsHidden = false;
                        }
                        if (IsMummyBurned && !IsScepterTaken)
                            GameFunctions.FindObject("Scepter", PointsOfInterest).IsHidden = false;
                        _isClosestRightCoffinOpen = true;
                    }
                    break;
                case "Middle coffin on the right":
                    LidOpenMessage();
                    if (!_isMiddleRightCoffinOpen)
                    {
                        var theBag = GameFunctions.FindObject("Bag 2", PointsOfInterest);
                        if (theBag.IsHidden)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("\nA small bag falls out of the coffin.");
                            theBag.IsHidden = false;
                        }
                        _isMiddleRightCoffinOpen = true;
                    }
                    break;
                case "Farthest coffin on the right":
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
                case "Closest coffin on the left":
                    LidCloseMessage();
                    break;
                case "Middle coffin on the left":
                    if (!_isMiddleLeftCoffinOpen)
                        LidCloseMessage();
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\nYou can't close the tomb. The slime blocks the door.");
                    }
                    break;
                case "Closest coffin on the right":
                    LidCloseMessage();
                    if (!IsMummyBurned)
                        GameFunctions.FindObject("Mummy", PointsOfInterest).IsHidden = true;
                    if (IsMummyBurned && !IsScepterTaken)
                        GameFunctions.FindObject("Scepter", PointsOfInterest).IsHidden = true;
                    _isClosestRightCoffinOpen = false;
                    break;
                case "Middle coffin on the right":
                    LidCloseMessage();
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
                case "Green slime":
                    DieToSlime();
                    break;
                default:
                    base.HitObject(objectName);
                    break;
            }
        }
    }
}
