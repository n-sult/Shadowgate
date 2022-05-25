using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.WraithRoom
{
    public class Cloak : Item
    {
        public Cloak(string objectName)
        {
            ObjectName = objectName;
        }

        public override void Look()
        {
            GameFunctions.WriteLine("\nThis heavy cloak contains no frivolous adornments, such as pockets or a hood.");
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
                    case "Self":
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        GameFunctions.WriteLine("\nYou try on the cloak and find it very unbecoming. It barely fits over your armor.");
                        Globals.currentPlayer.IsCloakEquipped = true; // mark player status as having the cloak equipped
                        Globals.currentPlayer.PlayerInventory.Remove(this); // remove cloak from inventory
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
