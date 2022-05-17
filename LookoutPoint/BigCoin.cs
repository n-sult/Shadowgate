using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.LookoutPoint
{
    public class BigCoin : Item
    {
        public BigCoin(string objectName)
        {
            ObjectName = objectName;
        }

        public override void Look()
        {
            Console.WriteLine("\nIt is a large gold coin with a well-engraved on it.");
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
                    case "Old Well":
                        if (Rooms.WellRoom.WellCoverOpen)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("\nAs soon as you throw the coin into the well, a huge wind erupts from within it. " +
                                "\nIt reminds you of the small 'dust devils' you see in the autumn months.");
                            Rooms.WellRoom.BigCoinUsed = true;
                            Globals.currentPlayer.PlayerInventory.Remove(this);
                            GameFunctions.ReduceTorchFire();
                        }
                        else
                            base.Use();
                        break;
                    default:
                        base.Use();
                        break;
                }
            }
        }
    }
}
