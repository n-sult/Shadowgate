using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate
{
    public class Torch : Item
    {
        public Torch(string objectName)
        {
            ObjectName = objectName;
        }

        public static void LightTorch(ActiveTorch activeTorch)
        {
            activeTorch.FireRemaining = 60;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nThe Torch is lit.");
        }

        public static void RemoveTorch()
        {
            Globals.currentPlayer.torchCount--;

            if (Globals.currentPlayer.torchCount == 0)
            {
                var activeObject = Globals.currentPlayer.PlayerInventory.FirstOrDefault(x => x is Torch);
                Globals.currentPlayer.PlayerInventory.Remove(activeObject);
                Globals.currentPlayer.containsTorch = false;
            }
            GameFunctions.ReduceTorchFire();
        }

        public override void Look()
        {
            Console.WriteLine("\nIt's a torch. An oil soaked rag is wrapped around it.");
        }

        public override bool Take()
        {
            return true;
        }

        public override void Use()
        {
            ActiveTorch activeTorch1 = (ActiveTorch)Globals.currentPlayer.PlayerInventory[0];
            ActiveTorch activeTorch2 = (ActiveTorch)Globals.currentPlayer.PlayerInventory[1];

            if (activeTorch1.FireRemaining <= 15)
            {
                if (activeTorch2.FireRemaining < activeTorch1.FireRemaining)
                    LightTorch(activeTorch2);
                else
                    LightTorch(activeTorch1);
                RemoveTorch();
            }
            else if (activeTorch2.FireRemaining <= 15)
            {
                if (activeTorch1.FireRemaining < activeTorch2.FireRemaining)
                    LightTorch(activeTorch1);
                else
                    LightTorch(activeTorch2);
                RemoveTorch();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nThe torches are burning strongly. You need not light any more.");
            }
        }

        public override void Leave()
        {
            base.Leave();
        }
    }
}
