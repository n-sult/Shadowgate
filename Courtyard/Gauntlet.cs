using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Courtyard
{
    public class Gauntlet : Item
    {
        public Gauntlet(string objectName, bool isHidden)
        {
            ObjectName = objectName;
            IsHidden = isHidden;
        }

        public override void Look()
        {
            GameFunctions.WriteLine("\nIt's a gauntlet of silver plate. It bears the symbol of the Circle of Twelve.");
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
                    case "Sphinx":
                        (Globals.clonedRoom as Rooms.SphinxChamber).UseItemOnSphinx(ObjectName);
                        break;
                    case "Self":
                        Console.ForegroundColor = ConsoleColor.Cyan; // show message of eqipping gauntlet
                        GameFunctions.WriteLine("\nYou place the gauntlet on your hand. It feels like it was made just for you.");
                        Globals.currentPlayer.IsGauntletEquipped = true; // set player to have gauntlets eqipped
                        Globals.currentPlayer.PlayerInventory.Remove(this); // remove gauntlet from inventory
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
            DoNotDoThatMessage();
        }
    }
}
