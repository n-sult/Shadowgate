using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Tomb
{
    public class Scepter : Item
    {
        public Scepter(string objectName, bool isHidden)
        {
            ObjectName = objectName;
            IsHidden = isHidden;
        }

        public override void Look()
        {
            GameFunctions.WriteLine("\nThis jewel-studded scepter is truly made for a king!");
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
                    case "Skeletal King":
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        GameFunctions.WriteLine("\nAs soon as you give the scepter to the skeleton, the seal on the right pillar lowers. " +
                            "\nYou can now see a ringe-shaped hole where the seal was!"); // altered line
                        GameFunctions.FindObject("Hole in the Right Pillar", Globals.clonedRoom.PointsOfInterest).IsHidden = false;
                        (Globals.clonedRoom as Rooms.ThroneRoom).IsScepterUsed = true;
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
