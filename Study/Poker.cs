﻿using System;
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
            Console.WriteLine("\nIt's a black iron poker. It is used to stir the embers of an ongoing fire.");
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
                        if (!Rooms.TrollBridge.SpearThrown)
                        {
                            Rooms.TrollBridge.TriedToTrickTroll();
                            Rooms.TrollBridge.TrollDestroysBridge();
                        }
                        else
                            Rooms.TrollBridge.TrollKillsWithSpear();
                        break;
                    case "Cyclops":
                        if (!Rooms.Courtyard.CyclopsUnconcious)
                            Rooms.Courtyard.DieToCyclops();
                        else
                            base.Use();
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
            LeaveInFountainMessage(ObjectName);
        }
    }
}
