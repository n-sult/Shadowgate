using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Study
{
    public class Poker : Item
    {
        public Poker(string objectName)
        {
            ObjectName = objectName;
        }

        public override void Look()
        {
            GameFunctions.WriteLine("\nIt's a black iron poker. It is used to stir the embers of an ongoing fire.");
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
