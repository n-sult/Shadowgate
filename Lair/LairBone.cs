using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Lair
{
    public class LairBone : Item
    {
        public LairBone(string objectName)
        {
            ObjectName = objectName;
        }

        public override void Look()
        {
            Console.WriteLine("\nThis bone has been picked clean.");
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
            LeaveInFountainMessage(ObjectName);
        }
    }
}
