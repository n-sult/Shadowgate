using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Laboratory
{
    public class Horseshoe : Item
    {
        public Horseshoe(string objectName)
        {
            ObjectName = objectName;
        }

        public override void Look()
        {
            Console.WriteLine("\nThis horseshoe seems to have taken quite a beating.");
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
                    case "Sphinx":
                        (Globals.clonedRoom as Rooms.SphinxChamber).UseItemOnSphinx(ObjectName);
                        break;
                    default:
                        base.Use();
                        break;
                }
            }
        }

        public override void Leave()
        {
            DoNotDoThatMessage();
        }
    }
}
