using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Lair
{
    public class Spear : Item
    {
        public Spear(string objectName)
        {
            ObjectName = objectName;
        }

        public override void Look()
        {
            Console.WriteLine("\nThis spear is some seven feet long. The tip seems to be made of finely forged silver.");
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
                    case "Firedrake":
                        Rooms.FireBridge.DieToFiredrake();
                        break;
                    case "Troll": // when using on the troll...
                        Console.ForegroundColor = ConsoleColor.Cyan; // show message of the troll disappearing
                        Console.WriteLine("\nThe troll falls silently into the dark cavern. You listen, but you do not hear him crash.");
                        GameFunctions.FindObject("Troll", Globals.clonedRoom.PointsOfInterest).IsHidden = true; // hide the troll
                        Globals.currentPlayer.PlayerInventory.Remove(this); // remove spear from the inventory
                        (Globals.clonedRoom as Rooms.TrollBridge).SpearThrown = true; // mark these 2 bools for later use
                        GameFunctions.FindObject(result, Globals.clonedRoom.PointsOfInterest).IsHidden = false; 
                        GameFunctions.ReduceTorchFire();
                        break;
                    case "Sphinx":
                        Rooms.SphinxChamber.UseItemOnSphinx(ObjectName);
                        break;
                    case "Self":
                        GameFunctions.KillSelf(ObjectName);
                        break;
                    default:
                        base.Use();
                        break;
                }
            }
        }
    }
}
