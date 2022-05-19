using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.LookoutPoint
{
    public class GoldCoin : Item
    {
        public GoldCoin(string objectName)
        {
            ObjectName = objectName;
        }

        public override void Look()
        {
            Console.WriteLine("\nThis coin has a mark on it that looks like a human skull.");
        }

        public override bool Take()
        {
            return true;
        }

        public override void Use()
        {
            var result = GameFunctions.UseOn(ObjectName);

            if (result is not null)
            {
                switch(result)
                {
                    case "Troll":
                        if ((Globals.clonedRoom as Rooms.TrollBridge).CoinsGivenToTroll == 0)
                            Rooms.TrollBridge.TriedToGiveCopperCoin();
                        else
                            Rooms.TrollBridge.TriedToGiveGoldCoin();
                        break;
                    case "Sphinx":
                        Rooms.SphinxChamber.UseItemOnSphinx(ObjectName);
                        break;
                    case "Ferryman":
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\nThe ferryman takes the coin and gestures you to board quickly.");
                        Rooms.RiverStyx.GoldCoinGiven = true;
                        Globals.currentPlayer.PlayerInventory.Remove(this);
                        GameFunctions.ReduceTorchFire();
                        break;
                    default:
                        base.Use();
                        break;
                }
            }
        }
    }
}
