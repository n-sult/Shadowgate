using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Study
{
    public class Goblet : Item
    {
        public Goblet(string objectName)
        {
            ObjectName = objectName;
        }

        public override void Look()
        {
            Console.WriteLine("\nIt's a pewter goblet, which glows with a lustrous shine.");
        }

        public override bool Take()
        {
            return true;
        }

        public override void Use()
        {
            this.WhatDidYouExpectMessage();
        }

        public override void Leave()
        {
            LeaveInFountainMessage(ObjectName);
        }
    }
}
