﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Turret
{
    public class Talisman : Item
    {
        public Talisman(string objectName)
        {
            ObjectName = objectName;
        }

        public override void Look()
        {
            Console.WriteLine("\nThis rather heavy talisman is made of gold and is extremely sharp along it's edges. " +
                "\nIt shines with an incredible brilliance!");
        }

        public override bool Take()
        {
            if (!Rooms.Turret.WyvernDead)
            {
                Rooms.Turret.DieToWyvern();
                return false;
            }
            else
                return true;
        }

        public override void Use()
        {
            var result = GameFunctions.UseOn(ObjectName);

            if (result is not null)
            {
                switch(result)
                {
                    case "Middle Pillar Hole":
                    case "Right Pillar Hole":
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nGiant flames immediately pour out in your direction from the giant skull's hollow eyes. " + // added line
                            "\nYou have placed the Bladed Sun in the wrong hole. \nYou did not heed the warnings and now the Warlock " +
                            "Lord's defenses end your life!");
                        GameFunctions.GameOver();
                        break;
                    case "Left Pillar Hole":
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\nThe artifact, known as the Bladed Sun, is now secured and in place.");
                        Rooms.Vault.TalismanUsed = true;
                        Globals.currentPlayer.PlayerInventory.Remove(this);
                        GameFunctions.ReduceTorchFire();
                        break;    
                    default:
                        base.Use();
                        break;
                }
            }
        }
    }
}