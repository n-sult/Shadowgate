using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Garden
{
    public class Ring : Item
    {
        public Ring(string objectName, bool isHidden)
        {
            ObjectName = objectName;
            IsHidden = isHidden;
        }

        public override void Look()
        {
            Console.WriteLine("\nIt's a ring! Set with a large, black sapphire.");
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
                    case "Hole in the Right Pillar":
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\nThe ring fits perfectly. The throne magically rises, revealing a secret passageway.");
                        GameFunctions.FindObject("Trap Door hidden under the Throne", Globals.clonedRoom.PointsOfInterest).IsHidden = false;
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
