using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Lair
{
    public class Helmet : Item
    {
        public Helmet(string objectName)
        {
            ObjectName = objectName;
        }

        public override void Look()
        {
            GameFunctions.WriteLine("\nThis seems to be a helmet of the sort commonly worn by hobgoblins.");
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
