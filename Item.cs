using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate
{
    public class Item : PointOfInterest
    {
        public override void Close()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            GameFunctions.WriteLine("\nYou seem to be wasting your time.");
        }

        public override void Use()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            GameFunctions.WriteLine("\nNothing happened.");
        }
    }
}
