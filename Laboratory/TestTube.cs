using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Laboratory
{
    public class TestTube : Item
    {
        public TestTube(string objectName)
        {
            ObjectName = objectName;
        }

        public override void Look()
        {
            Console.WriteLine("\nIt's an empty test tube on a wooden rack.");
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
