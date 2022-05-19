using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Rooms
{
    public class TrollBridge : Room
    {
        public bool TrollAppearedFirstTime;
        public bool SpearThrown;
        public bool CanTrollReappear;
        public bool TrollReappeared;
        public int CoinsGivenToTroll;

        public TrollBridge()
        {
            // All POI for Troll Bridge
            PointOfInterest woodenBridge = new PointOfInterest("Wooden Bridge");
            PointOfInterest troll = new PointOfInterest("Troll", true);

            PointOfInterest trollBridgeChasm = new PointOfInterest("Chasm");
            Entry doorAtTheEndOfTheWoodenBridge = new Entry("Door at the end of the wooden bridge", false, true, false, false, "Courtyard");
            Entry doorFromTrollBridgeToFireBridge = new Entry("Door to Fire Bridge", true, true, false, false, "Fire Bridge");
            var trollBridgePOI = new List<PointOfInterest>() { trollBridgeChasm, woodenBridge, troll, doorAtTheEndOfTheWoodenBridge, doorFromTrollBridgeToFireBridge };

            RoomName = "Troll Bridge";
            FirstEntry = "A sharp, cold wind whips up over the ledge of the deep, dark chasm.";
            SubsequentEntry = FirstEntry;
            PointsOfInterest = trollBridgePOI;

            GameFunctions.RoomEnteredEvent += (roomName) => { CoinsGivenToTroll = 0; };
        }

        public static void TriedToGiveCopperCoin()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nThe troll shouts, \"Hey, what's this? It isn't gold! Are you trying to cheat me?\"");
        }

        public static void TriedToGiveGoldCoin()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nThe troll says, \"I've changed my mind! I won't let you cross my bridge after all!");
            TrollDestroysBridge();
        }

        public static void TriedToTrickTroll()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nThe troll cries, \"You can't trick me!\"");
        }

        public static void TrollDestroysBridge()
        {
            Console.WriteLine("The troll then picks up the bridge causing you to fall into the chasm!");
            GameFunctions.GameOver();
        }

        public static void TrollKillsWithSpear()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("With one swift motion, the troll launches his spear and runs you through! " +
                "The spear pierces your chest and exits through your back!");
            GameFunctions.GameOver();
        }
        
        public override void MoveTo(string objectName)
        {
            var theTroll = GameFunctions.FindObject("Troll", PointsOfInterest);
            switch (objectName)
            {
                case "Chasm":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nWith a loud cry, you take the big plunge. \nThe Grim Reaper stands below, waiting to catch you.");
                    GameFunctions.GameOver();
                    break;
                case "Door at the end of the wooden bridge":
                    if (!TrollAppearedFirstTime)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\nAs you step on the bridge, a troll appears and says, \"This bridge is mine! It'll cost you a gold coin to cross!\"");
                        TrollAppearedFirstTime = true;
                        theTroll.IsHidden = false;
                        GameFunctions.ReduceTorchFire();
                    }
                    else if (TrollAppearedFirstTime && theTroll.IsHidden == false && !TrollReappeared)
                    {
                        TriedToTrickTroll();
                        TrollDestroysBridge();
                    }
                    else if (CanTrollReappear && !TrollReappeared)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\nThe troll reappears, your spear in its hand, and demands you to pay a toll of one gold coin."); // altered line
                        TrollReappeared = true;
                        theTroll.IsHidden = false;
                        GameFunctions.ReduceTorchFire();
                    }
                    else if (TrollReappeared)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nThe troll blows up like a volcano and throws his spear at your chest! " +
                            "The spear pierces your chest and exits through your back!");
                        GameFunctions.GameOver();
                    }
                    else
                    {
                        base.MoveTo(objectName);
                        CanTrollReappear = true;
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
                case "Wooden Bridge":
                    Console.WriteLine("\nIt's a sturdy wooden bridge.");
                    break;
                case "Chasm":
                    Console.WriteLine("\nA strong, cold wind blows up from the chasm.");
                    break;
                case "Troll":
                    Console.WriteLine("\nThe troll stares at you.");
                    break;
                case "Door at the end of the wooden bridge":
                    var theTroll = GameFunctions.FindObject("Troll", PointsOfInterest);
                    if (theTroll.IsHidden == false)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\nA terrible looking troll is standing in the way.");
                    }
                    else
                        Console.WriteLine("\nIt's a doorway.");
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
                case "Troll":
                    Console.ForegroundColor = ConsoleColor.Red;
                    if (!SpearThrown)
                    {
                        TriedToTrickTroll();
                        TrollDestroysBridge();
                    }
                    else
                    {
                        TriedToTrickTroll();
                        TrollKillsWithSpear();
                    }
                    break;
                default:
                    base.HitObject(objectName);
                    break;
            }
        }

        public override void SpeakTo(string objectName)
        {
            switch(objectName)
            {
                case "Troll":
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nThe troll says with a strained face, \"I've nothing to say to you. Go away!\"");
                    GameFunctions.ReduceTorchFire();
                    break;
                default:
                    base.SpeakTo(objectName);
                    break;
            }
        }
    }
}
