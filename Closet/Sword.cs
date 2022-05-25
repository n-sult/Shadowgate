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
            GameFunctions.WriteLine("\nIt's a double-edged broadsword. The handle has druidic script written upon it.");
        }

        public override bool Take()
        {
            return true;
        }

        public override void Use()
        {
            var result = GameFunctions.UseOn(ObjectName); // decide what to use the sword on
            if (result is not null) // if player selected something to use the sword on...
            {
                switch (result)
                {
                    case "Firedrake": 
                        Rooms.FireBridge.DieToFiredrake(); // if it's the firedrake, player dies
                        break;
                    case "Troll": 
                        (Globals.clonedRoom as Rooms.TrollBridge).TriedToTrickTroll(); // if it's the troll, die
                        break;
                    case "Cyclops": 
                        if (!(Globals.clonedRoom as Rooms.Courtyard).CyclopsUnconcious) // if the cyclops is alive, player dies
                            Rooms.Courtyard.DieToCyclops();
                        else // but if it's unconcious, kill it
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            GameFunctions.WriteLine("\nYou drive the sword deep into the cyclops. Blood pours out of the wound and onto the grass.");
                            (Globals.clonedRoom as Rooms.Courtyard).CyclopsDead = true;
                            foreach (Item item in Globals.currentPlayer.PlayerInventory) // once the cyclops is dead, the sword, sling and any stones can be discarded
                                if (item.ObjectName == "Sword" || item.ObjectName == "Sling" || item.ObjectName == "Stone")
                                    item.CanBeDiscarded = true;
                            GameFunctions.ReduceTorchFire();
                        }
                        break;
                    case "Sphinx":
                        (Globals.clonedRoom as Rooms.SphinxChamber).UseItemOnSphinx(ObjectName); // if used on sphinx, player will be teleported
                        break;
                    case "Hellhound": 
                        Rooms.BrazierRoom.DieToHound(); // if used on hellhound, player dies
                        break;
                    case "Behemoth":
                    case "Warlock Lord": 
                        Rooms.Chasm.DieToWarlock(); // if used on behemoth/warlock, player dies
                        break;
                    case "Self": // if used on self, die
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
