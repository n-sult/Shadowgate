using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.BanquetHall
{
    public class BanquetMirror : Item
    {
        public BanquetMirror(string objectName)
        {
            ObjectName = objectName;
        }

        public override void Look()
        {
            Console.WriteLine("\nThe frame of this fine mirror is laced with silver and gold.");
        }

        public override bool Take()
        {
            return true;
        }

        public override void Use()
        {
            var result = GameFunctions.UseOn(ObjectName);

            switch(result)
            {
                case "Sphinx":
                    Rooms.SphinxChamber.UseItemOnSphinx(ObjectName);
                    break;
                default:
                    base.Use();
                    break;
            }
        }

        public override void Leave()
        {
            DoNotDoThatMessage();
        }
    }
}
