using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Waterfall
{
    public class Stone : Item
    {
        public Stone(string objectName)
        {
            ObjectName = objectName;
        }

        public override void Look()
        {
            Console.WriteLine("\nThis stone is almost perfectly round.");
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
                if (result == "Sling")
                {
                    Closet.Sling theSling = (Closet.Sling)GameFunctions.FindObject(result, null, Globals.currentPlayer.PlayerInventory);
                    if (theSling.HasStone)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\nYou can't do that.");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\nYou've put the small stone in the sling.");
                        theSling.HasStone = true; // set sling status to have a stone in it
                        Globals.currentPlayer.PlayerInventory.Remove(this); // remove stone from inventory
                        GameFunctions.ReduceTorchFire();
                    }
                }
                else if (result == "Sphinx")
                    Rooms.SphinxChamber.UseItemOnSphinx(ObjectName);
                else if (result == null)
                    return;
                else
                    base.Use();
            }
        }

        public override void Leave()
        {
            if (CanBeDiscarded)
                LeaveInFountainMessage(ObjectName);
            else
                DoNotDoThatMessage();
        }
    }
}
