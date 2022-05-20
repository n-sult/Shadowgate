using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Lair
{
    public class Hammer : Item
    {
        public Hammer(string objectName)
        {
            ObjectName = objectName;
        }

        public override void Look()
        {
            Console.WriteLine("\nIt's an ancient Gnome war hammer. This weapon does not show the signs of battle.");
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
                    case "Left Mirror":
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nAs soon as you break the mirror, shards of glass fly through the air and slice into your body! " +
                            "\nBlood pours from your wounds and your body slumps to the floor.");
                        GameFunctions.GameOver();
                        break;
                    case "Right Mirror":
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nBehind the broken mirror, you find a magic portal into deep space. You are immediately sucked through. " + // altered line
                            "\nThe lack of air causes you to quickly lose consciousness. \nThe Grim Reaper quickly embraces you!");
                        GameFunctions.GameOver();
                        break;
                    case "Middle Mirror":
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\nBellowing like some Norse God, you smash the hammer into the mirror. You shatter the mirror, revealing an iron door!");
                        GameFunctions.FindObject("Door behind broken mirror", Globals.clonedRoom.PointsOfInterest).IsHidden = false; // unhide the door behind the mirror
                        Globals.clonedRoom.PointsOfInterest.Remove(GameFunctions.FindObject("Middle Mirror", Globals.clonedRoom.PointsOfInterest)); // remove middle mirror from POI
                        CanBeDiscarded = true; // once the mirror is broken, the hammer can be discarded
                        GameFunctions.ReduceTorchFire();
                        break;
                    case "Firedrake":
                        Rooms.FireBridge.DieToFiredrake();
                        break;
                    case "Troll":
                        (Globals.clonedRoom as Rooms.TrollBridge).TriedToTrickTroll();
                        break;
                    case "Cyclops":
                        if (!(Globals.clonedRoom as Rooms.Courtyard).CyclopsUnconcious)
                            Rooms.Courtyard.DieToCyclops();
                        else
                            base.Use();
                        break;
                    case "Sphinx":
                        (Globals.clonedRoom as Rooms.SphinxChamber).UseItemOnSphinx(ObjectName);
                        break;
                    case "Hellhound":
                        Rooms.BrazierRoom.DieToHound();
                        break;
                    case "Behemoth":
                    case "Warlock Lord":
                        Rooms.Chasm.DieToWarlock();
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

        public override void Leave()
        {
            LeaveInFountainMessage(ObjectName);
        }
    }
}
