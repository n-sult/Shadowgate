using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Laboratory
{
    public class HolyWater : Item
    {
        public HolyWater(string objectName, bool isHidden)
        {
            ObjectName = objectName;
            IsHidden = isHidden;
        }

        public override void Look()
        {
            Console.WriteLine("\nThe glass vial is filled with a clear liquid. The sign of the cross is on it.");
        }

        public override bool Take()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nAs you grab the water, the stone descends back into place."); // altered line
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
                        Rooms.SphinxChamber.UseItemOnSphinx(ObjectName);
                        break;
                    case "Hellhound":
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\nThe holy water has sent the hellhound back to the place where it was spawned. " +
                            "\nIt's engulfed by a huge flame! The flame dies out. The room is quiet, as though nothing had happened.");
                        Globals.currentRoom.PointsOfInterest.Remove(GameFunctions.FindObject(result, Globals.currentRoom.PointsOfInterest));
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
