using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.HiddenRoom
{
    public class Arrow : Item
    {
        public Arrow(string objectName)
        {
            ObjectName = objectName;
        }

        public override void Look()
        {
            Console.WriteLine("\nA finely crafted silver arrow is not uncommon in the Elven lands.");
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
                    case "Captive Woman": // if used on the captive woman
                        Console.ForegroundColor = ConsoleColor.Cyan; // show message of killing the werewolf
                        Console.WriteLine("\nAs you ready your arrow, the beautiful lady suddenly transforms into a wolf! \nIt breaks the chain as it attempts to lunge " +
                            "at you! \nHowever, your aim is true as you plunge the silver arrow into the wolf."); // heavily altered line
                        GameFunctions.FindObject("Captive Woman", Globals.clonedRoom.PointsOfInterest).ObjectName = "Dead Werewolf"; // change object name
                        (Globals.clonedRoom as Rooms.Watchtower).WomanDead = true; // mark woman/wolf as dead
                        Globals.currentPlayer.PlayerInventory.Remove(this); // remove arrow from inventory
                        GameFunctions.ReduceTorchFire();
                        break;
                    case "Sphinx":
                        (Globals.clonedRoom as Rooms.SphinxChamber).UseItemOnSphinx(ObjectName);
                        break;
                    case "Hellhound":
                        Rooms.BrazierRoom.DieToHound();
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
            DoNotDoThatMessage();
        }
    }
}
