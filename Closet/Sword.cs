using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Closet
{
    public class Sword : Item
    {
        public Sword(string objectName)
        {
            ObjectName = objectName;
        }

        public override void Look()
        {
            Console.WriteLine("\nIt's a double-edged broadsword. The handle has druidic script written upon it.");
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
                        if (!(Globals.clonedRoom as Rooms.TrollBridge).SpearThrown)
                        {
                            Rooms.TrollBridge.TriedToTrickTroll();
                            Rooms.TrollBridge.TrollDestroysBridge();
                        }
                        else
                            Rooms.TrollBridge.TrollKillsWithSpear();
                        break;
                    case "Cyclops":
                        if (!(Globals.clonedRoom as Rooms.Courtyard).CyclopsUnconcious)
                            Rooms.Courtyard.DieToCyclops();
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("\nYou drive the sword deep into the cyclops. Blood pours out of the wound and onto the grass.");
                            (Globals.clonedRoom as Rooms.Courtyard).CyclopsDead = true;
                            foreach (Item item in Globals.currentPlayer.PlayerInventory) // once the cyclops is dead, the sword, sling and any stones can be discarded
                                if (item.ObjectName == "Sword" || item.ObjectName == "Sling" || item.ObjectName == "Stone")
                                    item.CanBeDiscarded = true;
                            GameFunctions.ReduceTorchFire();
                        }
                        break;
                    case "Sphinx":
                        Rooms.SphinxChamber.UseItemOnSphinx(ObjectName);
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
            if (CanBeDiscarded)
                LeaveInFountainMessage(ObjectName);
            else
                DoNotDoThatMessage();
        }
    }
}
