using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Library
{
    public class Glasses : Item
    {
        public Glasses(string objectName, bool isHidden)
        {
            ObjectName = objectName;
            IsHidden = isHidden;
        }
        
        public override void Look()
        {
            Console.WriteLine("\nThese glasses are worn. They've probably been used for a long time.");
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
                        Console.ForegroundColor = ConsoleColor.Cyan; // show message of equipping glasses
                        Console.WriteLine("\nYou try the glasses on and they fit perfectly. Hmm, you can see very well.");
                        Globals.currentPlayer.IsGlassesEquipped = true; // mark player status as glasses equipped
                        Globals.currentPlayer.PlayerInventory.Remove(this); // remove glasses from inventory
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
