using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.MirrorRoom
{
    public class Broom : Item
    {
        public Broom(string objectName)
        {
            ObjectName = objectName;
        }

        public override void Look()
        {
            Console.WriteLine("\nThis broom looks remarkably like the one owned by the Sirens of the isle of Yeklum Iret.");
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
                    (Globals.clonedRoom as Rooms.SphinxChamber).UseItemOnSphinx(ObjectName);
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nI know neatness counts, but there would seem to be better ways to spend your time.");
                    GameFunctions.ReduceTorchFire();
                }
            }
        }

        public override void Leave()
        {
            DoNotDoThatMessage();
        }
    }
}
