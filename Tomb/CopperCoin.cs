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
            Console.WriteLine("\nHey! Wait a minute! This is no gold coin. It's but a brass slug. What a royal rip!");
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
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("\nThe troll says that the toll has just been raised to two gold coins.");
                            Globals.currentPlayer.PlayerInventory.Remove(this);
                            (Globals.clonedRoom as Rooms.TrollBridge).CoinsGivenToTroll++;
                            GameFunctions.ReduceTorchFire();
                        }
                        else
                        {
                            Rooms.TrollBridge.TriedToGiveCopperCoin();
                            Rooms.TrollBridge.TrollDestroysBridge();
                        }
                            
                        break;
                    case "Sphinx":
                        Rooms.SphinxChamber.UseItemOnSphinx(ObjectName);
                        break;
                    case "Ferryman":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\nThe ferryman will not take the copper coin as a fare. Suddenly, he disappears!");
                        GameFunctions.FindObject(result, Globals.currentRoom.PointsOfInterest).IsHidden = true;
                        GameFunctions.FindObject("Raft", Globals.currentRoom.PointsOfInterest).IsHidden = true;
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
