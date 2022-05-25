using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.BanquetHall
{
    public class Crest : Item
    {
        public Crest(string objectName)
        {
            ObjectName = objectName;
        }

        public override void Look()
        {
            GameFunctions.WriteLine("\nIt's the family crest of Sir Dugan himself.");
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
