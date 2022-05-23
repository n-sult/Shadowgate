using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Watchtower
{
    public class Blade : Item
    {
        public Blade(string objectName)
        {
            ObjectName = objectName;
        }

        public override void Look()
        {
            Console.WriteLine("\nIt's some sort of spike that is made of precious metals. Ouch! The tips are as sharp as needles.");
        }

        public override bool Take()
        {
            if (!(Globals.clonedRoom as Rooms.Watchtower).WomanDead)
            {
                (Globals.clonedRoom as Rooms.Watchtower).DieToWerewolf();
                return false;
            }
            else
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
                    case "Staff":
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\nSuddenly, lightning begins to flash in the room! \nThen, the golden spike slides smoothly onto the staff " +
                            "and locks into place.");
                        SnakeCave.Staff currentStaff = (SnakeCave.Staff)GameFunctions.FindObject(result, null, Globals.currentPlayer.PlayerInventory);
                        currentStaff.HasBlade = true;
                        // SnakeCave.Staff.HasBlade = true;
                        Globals.currentPlayer.PlayerInventory.Remove(this);
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
