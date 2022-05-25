using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Rooms
{
    public class Lair : Room
    {
        int numberOfDragonBreaths = 0;
        
        public Lair()
        {
            Shadowgate.Lair.Shield shield = new Shadowgate.Lair.Shield("Shield");
            Shadowgate.Lair.Spear spear = new Shadowgate.Lair.Spear("Spear");
            Shadowgate.Lair.Hammer hammer = new Shadowgate.Lair.Hammer("Hammer");
            Shadowgate.Lair.LairBone lairBone = new Shadowgate.Lair.LairBone("Bone");
            Shadowgate.Lair.LairSkull skull1 = new Shadowgate.Lair.LairSkull("Skull");
            Shadowgate.Lair.LairSkull skull2 = new Shadowgate.Lair.LairSkull("Skull");
            Shadowgate.Lair.Helmet helmet = new Shadowgate.Lair.Helmet("Helmet");

            PointOfInterest pileOfGold = new PointOfInterest("Pile of Gold");
            PointOfInterest lairChest = new PointOfInterest("Large Chest");
            Torch lairTorch = new Torch("Torch above the chest");
            PointOfInterest doorwayWithEyes = new PointOfInterest("Dark doorway with a pair of eyes glowing in the dark");
            Entry doorFromLairToColdRoom = new Entry("Door to Cold Room", true, true, false, false, "Cold Room");
            var lairPOI = new List<PointOfInterest>() { shield, spear, skull1, helmet, lairBone, hammer, skull2, pileOfGold, lairChest, lairTorch, 
                doorwayWithEyes, doorFromLairToColdRoom };

            RoomName = "Lair";
            FirstEntry = "Fear grips you as you enter this hot room!";
            SubsequentEntry = "This room is terribly hot!";
            PointsOfInterest = lairPOI;
        }

        public override void SetRoomStuff()
        {
            numberOfDragonBreaths = 0;
        }

        public static void DieToDragon()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            GameFunctions.WriteLine("\nDragon flame engulfs your body. You pay for your curiosity with your life.");
            GameFunctions.GameOver();
        }
        
        public void TakeInLair()
        {
            var result = GameFunctions.FindObject("Shield", null, Globals.currentPlayer.PlayerInventory); // find out if shield is in player inventory

            if (result is null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                GameFunctions.Write("\nWhoosh! Flames suddenly shoot from the dragon's mouth!");
                DieToDragon();
                return;
            }
            if (numberOfDragonBreaths == 0)
            {
                if (result is not null) // if inventory has shield, then block flames
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    GameFunctions.WriteLine("\nFlames spew out from the dragon's mouth!");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    GameFunctions.WriteLine("You raise your shield just in time to block the dragon flame.");
                    numberOfDragonBreaths++;
                }
            }
            else if (numberOfDragonBreaths > 0 && numberOfDragonBreaths < 4) // if inventory has shield, block flames up to 4 times
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                GameFunctions.WriteLine("\nAgain flame spews forth! You use the shield for protection.");
                Console.ForegroundColor = ConsoleColor.Yellow;
                GameFunctions.WriteLine("It's getting hot! You don't know how much longer you can stand it.");
                numberOfDragonBreaths++;
            }
            else // if inventory has shield and flame has been blocked 4 times already, die
            {
                Console.ForegroundColor = ConsoleColor.Red;
                GameFunctions.Write("\nFlames spew out yet again. The shield melts under the intensity of the dragon flame. Your body fares no better! " +
                    "Not even your best friend could recognize your burning body.");
                DieToDragon();
            }
        }

        public override void MoveTo(string objectName)
        {
            switch(objectName)
            {
                case "Dark doorway with a pair of eyes glowing in the dark":
                    DieToDragon();
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
                case "Pair of eyes glowing in a dark doorway":
                    GameFunctions.WriteLine("\nAll you can see are two eyes in the darkness. They seem to be watching every move you make.");
                    break;
                case "Large Chest":
                    GameFunctions.WriteLine("\nThis is an extremely heavy iron-bound chest. It is securly locked.");
                    break;
                case "Pile of Gold":
                    GameFunctions.WriteLine("\nThis pile of gold is worth a king's ransom! The pieces have been melted together.");
                    break;
                default:
                    base.LookAt(objectName);
                    break;
            }
        }

        public override bool TakeObject(string objectName)
        {
            var activeObject = GameFunctions.FindObject(objectName, PointsOfInterest);

            if (activeObject is Item)
            {
                base.TakeObject(objectName);
                TakeInLair();
                return true;
            }
            else
            {
                base.TakeObject(objectName);
                return false;
            }
        }

        public override void HitObject(string objectName)
        {
            switch(objectName)
            {
                case "Large Chest":
                case "Pile of Gold":
                    ThatSmartsMessage();
                    break;
                default:
                    base.HitObject(objectName);
                    break;
            }
        }
    }
}
