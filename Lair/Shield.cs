using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Lair
{
    public class Shield : Item
    {
        public Shield(string objectName)
        {
            ObjectName = objectName;
        }

        public override void Look()
        {
            Console.WriteLine("\nIt's a heavy shield. There are only a few dents on it.");
        }

        public override bool Take()
        {
            return true;
        }

        public override void Use()
        {
            WhatDidYouExpectMessage();
        }

        public override void Leave()
        {
            DoNotDoThatMessage();
        }
    }
}
