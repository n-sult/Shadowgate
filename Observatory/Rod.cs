using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Observatory
{
    public class Rod : Item
    {
        public Rod(string objectName, bool isHidden)
        {
            ObjectName = objectName;
            IsHidden = isHidden;
        }

        public override void Look()
        {
            Console.WriteLine("\nThis rod is made out of cast iron.");
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
                    case "Hole on the balcony wall":
                        (Globals.clonedRoom as Rooms.Balcony).IsRodUsed = true;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\nSuddenly the sky seems to be on fire as a bolt of pure lightning strikes the rod! " +
                            "\nYou are startled to see a skeletal hand rise from a hole that has formed at your feet.");
                        GameFunctions.FindObject("Skeletal Hand", Globals.clonedRoom.PointsOfInterest).IsHidden = false;
                        GameFunctions.FindObject("Wand", Globals.clonedRoom.PointsOfInterest).IsHidden = false;
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
