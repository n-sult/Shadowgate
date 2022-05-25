using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Rooms
{
    public class LookoutPoint : Room
    {
        public LookoutPoint()
        {
            // Items for Lookout Point
            Shadowgate.LookoutPoint.BigCoin bigCoin = new Shadowgate.LookoutPoint.BigCoin("Big Coin");
            Shadowgate.LookoutPoint.GoldCoin goldCoin1 = new Shadowgate.LookoutPoint.GoldCoin("Gold Coin");
            Shadowgate.LookoutPoint.GoldCoin goldCoin2 = new Shadowgate.LookoutPoint.GoldCoin("Gold Coin");
            Shadowgate.LookoutPoint.GoldCoin goldCoin3 = new Shadowgate.LookoutPoint.GoldCoin("Gold Coin");
            List<Item> bag3Coins = new List<Item>() { bigCoin, goldCoin1, goldCoin2, goldCoin3 };
            Bag bag3 = new Bag("Pouch", false, bag3Coins);

            // POI for Lookout Point
            OutsideView lookoutSky = new OutsideView("Lightning-filled Sky");
            PointOfInterest lookoutPoint = new PointOfInterest("Lookout Point");
            PointOfInterest potOfGold = new PointOfInterest("Pot of Gold");
            Entry stairsFromLookoutPointToBalcony = new Entry("Stairs to Balcony", false, true, false, false, "Balcony");
            var lookoutPointPOI = new List<PointOfInterest>() { lookoutSky, lookoutPoint, bag3, potOfGold, stairsFromLookoutPointToBalcony };

            RoomName = "Lookout Point";
            FirstEntry = "Lightning lights up the countryside as you stand on a lookout point.";
            SubsequentEntry = "Heavy stone stairs lead down to a sturdy lookout point.";
            PointsOfInterest = lookoutPointPOI;
        }

        public static void DieToGold()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            GameFunctions.WriteLine("\nAs you move the pot, you realize that you have fallen for the oldest trick in the book. " +
                "\nYou suddenly find yourself knee-deep in the moat. \nIt seems that the alligators really enjoy your company!");
            GameFunctions.GameOver();
        }

        public override void MoveTo(string objectName)
        {
            switch(objectName)
            {
                case "Pot of Gold":
                    DieToGold();
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
                case "Lookout Point":
                    GameFunctions.WriteLine("\nHeavy stone stairs lead down to a sturdy lookout point.");
                    break;
                case "Pot of Gold":
                    GameFunctions.WriteLine("\nIt's a pot of gold! The leprechaun must have skipped town.");
                    break;
                default:
                    base.LookAt(objectName);
                    break;
            }
        }

        public override bool TakeObject(string objectName)
        {
            switch(objectName)
            {
                case "Pot of Gold":
                    DieToGold();
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
            switch(objectName)
            {
                case "Pot of Gold":
                    DieToGold();
                    break;
                default:
                    base.OpenObject(objectName);
                    break;
            }
        }

        public override void CloseObject(string objectName)
        {
            switch (objectName)
            {
                case "Pot of Gold":
                    DieToGold();
                    break;
                default:
                    base.OpenObject(objectName);
                    break;
            }
        }

        public override void HitObject(string objectName)
        {
            switch(objectName)
            {
                case "Lookout Point":
                    GameFunctions.FindObject(objectName, PointsOfInterest).ThatSmartsMessage();
                    break;
                case "Pot of Gold":
                    DieToGold();
                    break;
                default:
                    base.HitObject(objectName);
                    break;
            }
        }
    }
}
