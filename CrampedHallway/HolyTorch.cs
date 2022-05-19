using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.CrampedHallway
{
    public class HolyTorch : Item
    {
        public HolyTorch(string objectName)
        {
            ObjectName = objectName;
        }

        public override void Look()
        {
            Console.WriteLine("\nThere is something out of the ordinary about this torch but you can't put a finger on it.");
        }

        public override bool Take()
        {
            ObjectName = "Holy Torch";
            return true;
        }

        public override void Use()
        {
            var result = GameFunctions.UseOn(ObjectName);
            
            if (result is not null)
            {
                switch (result)
                {
                    case "Wraith":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\nYou put the torch close to the wraith but nothing happens. \nHmm. There must be more than one way to do it.");
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
