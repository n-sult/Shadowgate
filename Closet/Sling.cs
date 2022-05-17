using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Closet
{
    public class Sling : Item
    {
        public static bool HasStone;
        public static int StonesThrown;
        
        public Sling(string objectName)
        {
            ObjectName = objectName;
        }

        public override void Look()
        {
            Console.WriteLine("\nIt's a small leather sling. This would come in handy for long-range battles!");
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
                if (result == "Sphinx")
                    Rooms.SphinxChamber.UseItemOnSphinx(ObjectName);
                else
                {
                    if (!HasStone)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\nWaving a sling around in the air doesn't seem to be very useful. It won't work without stones.");
                    }
                    else
                    {
                        if (result == "Troll")
                        {
                            if (HasStone)
                                if (!Rooms.TrollBridge.SpearThrown)
                                {
                                    Rooms.TrollBridge.TriedToTrickTroll();
                                    Rooms.TrollBridge.TrollDestroysBridge();
                                }
                                else
                                    Rooms.TrollBridge.TrollKillsWithSpear();
                            else
                                base.Use();
                        }
                        else if (result == "Cyclops")
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("\nAs soon as you start twirling the sling, a magical influence takes over your body! " +
                                "\nYou cry out, \"Death to the Philistine!\", and release the stone. " +
                                "\nBullseye!! The cyclops has been rendered unconcious."); // altered line
                            Rooms.Courtyard.CyclopsUnconcious = true;
                            HasStone = false;
                            StonesThrown++;
                        }
                        else if (result == "Hellhound")
                            Rooms.BrazierRoom.DieToHound();
                        else if (result == "Behemoth" || result == "Warlock Lord")
                            Rooms.Chasm.DieToWarlock();
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("\nYou whirl the sling over your head and release the stone. Not bad for a beginner!");
                            HasStone = false;
                            StonesThrown++;
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
