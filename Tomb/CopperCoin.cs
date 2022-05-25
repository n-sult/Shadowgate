using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Tomb
{
    public class CopperCoin : Item
    {
        public CopperCoin(string objectName)
        {
            ObjectName = objectName;
        }

        public override void Look()
        {
            GameFunctions.WriteLine("\nHey! Wait a minute! This is no gold coin. It's but a brass slug. What a royal rip!");
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
                switch (result)
                {
                    case "Troll":
                        if ((Globals.clonedRoom as Rooms.TrollBridge).CoinsGivenToTroll == 0)
                        {
                            Rooms.TrollBridge.GaveTrollFirstCoin(); // show message of giving troll a coin
                            Globals.currentPlayer.PlayerInventory.Remove(this); // remove coin from inventory
                            (Globals.clonedRoom as Rooms.TrollBridge).CoinsGivenToTroll++; // increment number of coins given to troll
                            GameFunctions.ReduceTorchFire();
                        }
                        else
                            (Globals.clonedRoom as Rooms.TrollBridge).TriedToGiveCopperCoin();
                        break;
                    case "Sphinx":
                        (Globals.clonedRoom as Rooms.SphinxChamber).UseItemOnSphinx(ObjectName);
                        break;
                    case "Ferryman":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        GameFunctions.WriteLine("\nThe ferryman will not take the copper coin as a fare. Suddenly, he disappears!");
                        GameFunctions.FindObject(result, Globals.clonedRoom.PointsOfInterest).IsHidden = true;
                        GameFunctions.FindObject("Raft", Globals.clonedRoom.PointsOfInterest).IsHidden = true;
                        GameFunctions.ReduceTorchFire();
                        break;
                    default:
                        base.Use();
                        break;
                }
            }
        }

        public override void Leave()
        {
            LeaveInFountainMessage(ObjectName);
        }
    }
}
