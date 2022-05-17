using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.SnakeCave
{
    public class Staff : Item
    {
        public bool HasBlade;
        public bool HasOrb;

        public Staff(string objectName, bool isHidden)
        {
            ObjectName = objectName;
            IsHidden = isHidden;
        }

        public override void Look()
        {
            if (HasOrb)
                Console.WriteLine("\nPower emanates from the staff! The three are, now and forever, one.");
            else if (HasBlade)
                Console.WriteLine("\nThe golden thorn is permanently bonded onto the staff.");
            else
                Console.WriteLine("\nDruidic script winds it's way around this staff. You can feel power emanating from it!");
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
                if (HasBlade && HasOrb)
                {
                    switch (result)
                    {
                        case "Behemoth":
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("\nYou pray, as you raise the Staff of Ages, that it has the power that the prophets claimed! " +
                                "\nThe staff pulsates with power and a beam of light explodes from it striking the behemoth! " +
                                "\nThe creature screams in agony, thrashing back and forth in great pain! " +
                                "\nIn his rage, he grabs the Warlock Lord, and descends into the depths of forever. " +
                                "\nYou can hear the Warlock Lord's screams fade into silence. Suddenly, it is very quiet. " +
                                "\nA beautiful light seems to fill the cavern. \"The morning sun\", you say to yourself, \"It is over.\" " +
                                "\n\nAlthough exhausted, you lean on the Staff of Ages and begin your long journey home.");
                            GameFunctions.GameEnding();
                            break;
                        default:
                            base.Use();
                            break;
                    }
                }
                else
                    base.Use();
            }
        }
    }
}
