using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate
{
    public class OutsideView : PointOfInterest
    {
        public OutsideView(string objectName)
        {
            ObjectName = objectName;
        }

        public override void Look()
        {
            if (ObjectName == "The Night Sky" || ObjectName == "Lightning-filled Sky")
                GameFunctions.WriteLine("\nThe sky foretells the coming of a great storm.");
            else
                GameFunctions.WriteLine("\nThrough this portal you can see the moon hovering over the darkened mountains.");
        }

        public override void Move()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            GameFunctions.WriteLine("\nWith a cry you jump to your death! \nIt takes only a couple of seconds before you hit the bottom with a thud.");
            GameFunctions.GameOver();
        }
    }
}
