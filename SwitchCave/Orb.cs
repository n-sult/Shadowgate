using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.SwitchCave
{
    public class Orb : Item
    {
        public Orb(string objectName, bool isHidden)
        {
            ObjectName = objectName;
            IsHidden = isHidden;
        }

        public override void Look()
        {
            GameFunctions.WriteLine("\nAha! It's an orb made of silver. Its glowing surface causes your skin to tingle.");
        }

        public override bool Take()
        {
            Console.ForegroundColor = ConsoleColor.White;
            GameFunctions.WriteLine("\nAs soon as you remove the orb, the cylinder closes.");
            return true;
        }

        public override void Use()
        {
            var result = GameFunctions.UseOn(ObjectName);
            if (result is not null)
            {
                switch(result)
                {
                    case "Staff":
                        SnakeCave.Staff theStaff = (SnakeCave.Staff)GameFunctions.FindObject(result, null, Globals.currentPlayer.PlayerInventory);
                        if (!theStaff.HasBlade)
                            base.Use();
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            GameFunctions.WriteLine("\nLight cascades through the room as the staff becomes a living entity!");
                            theStaff.HasOrb = true;
                            Globals.currentPlayer.PlayerInventory.Remove(this);
                            GameFunctions.ReduceTorchFire();
                        }
                        break;
                    default:
                        base.Use();
                        break;
                }
            }
        }
    }
}
