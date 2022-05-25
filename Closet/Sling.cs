using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Closet
{
    public class Sling : Item
    {
        public bool HasStone;
        public int StonesThrown;
        
        public Sling(string objectName)
        {
            ObjectName = objectName;
        }

        public override void Look()
        {
            GameFunctions.WriteLine("\nIt's a small leather sling. This would come in handy for long-range battles!");
        }

        public override bool Take()
        {
            return true;
        }

        public override void Use()
        {
            var result = GameFunctions.UseOn(ObjectName); // choosen something to use the sling on
            if (result is not null) // if player chose something to use it on (didn't select "never mind")....
            {
                if (result == "Sphinx") // if used on the sphinx, player will be teleported
                    (Globals.clonedRoom as Rooms.SphinxChamber).UseItemOnSphinx(ObjectName);
                else
                {
                    if (!HasStone) // otherwise, show this message if there's no stone in the sling
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        GameFunctions.WriteLine("\nWaving a sling around in the air doesn't seem to be very useful. It won't work without stones.");
                    }
                    else // if there is a stone in the sling...
                    {
                        if (result == "Troll") // if used on troll, die
                            (Globals.clonedRoom as Rooms.TrollBridge).TriedToTrickTroll();
                        else if (result == "Cyclops") // if used on cyclops, flag it as unconcious
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            GameFunctions.WriteLine("\nAs soon as you start twirling the sling, a magical influence takes over your body! " +
                                "\nYou cry out, \"Death to the Philistine!\", and release the stone. " +
                                "\nBullseye!! The cyclops has been rendered unconcious."); // altered line
                            (Globals.clonedRoom as Rooms.Courtyard).CyclopsUnconcious = true;
                            HasStone = false; // mark sling as no longer having a stone
                            StonesThrown++; // increment number of stones thrown for stone replenish check
                        }
                        else if (result == "Hellhound") // if used on hellhound, die
                            Rooms.BrazierRoom.DieToHound();
                        else if (result == "Behemoth" || result == "Warlock Lord") // if used on behemoth/warlock, die
                            Rooms.Chasm.DieToWarlock();
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            GameFunctions.WriteLine("\nYou whirl the sling over your head and release the stone. Not bad for a beginner!");
                            HasStone = false; // mark sling as no longer having a stone
                            StonesThrown++; // increment number of stones thrown for stone replenish check
                        }
                    }
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
