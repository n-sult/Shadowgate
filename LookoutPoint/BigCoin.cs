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
            GameFunctions.WriteLine("\nIt is a large gold coin with a well-engraved on it.");
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
                        Entry theWell = (Entry)GameFunctions.FindObject("Old Well", Globals.clonedRoom.PointsOfInterest);
                        if (theWell.IsDoorOpen)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            GameFunctions.WriteLine("\nAs soon as you throw the coin into the well, a huge wind erupts from within it. " +
                                "\nIt reminds you of the small 'dust devils' you see in the autumn months.");
                            (Globals.clonedRoom as Rooms.WellRoom).BigCoinUsed = true;
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
