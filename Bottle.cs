using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate
{
    public class Bottle : Item
    {
        public Bottle(string objectName, bool isHidden = false)
        {
            ObjectName = objectName;
            IsHidden = isHidden;
        }

        void DrinkBottle3Or4()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nGlug! You swallow the viscous liquid. It's like drinking tar.");
        }

        void RemoveBottleFromInventory(Item item)
        {
            Globals.currentPlayer.PlayerInventory.Remove(item);
        }

        public override void Look()
        {
            switch(ObjectName)
            {
                case "Bottle 1":
                    Console.WriteLine("\nIt's a small silver bottle. What is it? It sure smells terrible!");
                    break;
                case "Bottle 2":
                    Console.WriteLine("\nThis small silver vial glows with a lustrous shine. \nYou notice that the bottle is impossibly light!");
                    break;
                case "Bottle 3":
                    Console.WriteLine("\nIt's a silver vial.");
                    break;
                case "Bottle 4":
                    Console.WriteLine("\nThis jar is extremely slimy.");
                    break;
                case "Bottle 5":
                    Console.WriteLine("\nIt's a small black bottle with a cork on top.");
                    break;
                default:
                    base.Look();
                    break;
            }
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
                var bottleThatWasUsed = GameFunctions.FindObject(ObjectName, null, Globals.currentPlayer.PlayerInventory);
                
                switch (result)
                {
                    case "Self":
                        if (ObjectName == "Bottle 1")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nAs you consume the liquid in the vial, your body convulses and death spasms quickly follow.");
                            GameFunctions.GameOver();
                        }
                        else if (ObjectName == "Bottle 2")
                        {
                            if (Globals.currentRoom.RoomName == "Bridge Room")
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine("\nYou drink the liquid and immediately begin to rise in the air!");
                                RemoveBottleFromInventory((Item)bottleThatWasUsed);
                                (Globals.clonedRoom as Rooms.BridgeRoom).Bottle2Used = true;
                                Globals.NumberOfBottle2Consumed++;
                                GameFunctions.ReduceTorchFire();
                            }
                            else
                                DoNotDoThatMessage();
                        }
                        else if (ObjectName == "Bottle 3" || ObjectName == "Bottle 4")
                        {
                            DrinkBottle3Or4();
                            RemoveBottleFromInventory((Item)bottleThatWasUsed);
                            GameFunctions.ReduceTorchFire();
                        }
                        else if (ObjectName == "Bottle 5")
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("\nYou drink the liquid in the bottle. It's as sweet as sugar.");
                            RemoveBottleFromInventory((Item)bottleThatWasUsed);
                            GameFunctions.ReduceTorchFire();
                        }
                        break;
                    case null: // if selection is "Never mind" or invalid, just continue without doing anything
                        break;
                    default:
                        base.Use();
                        break;
                }
            }
        }

        public override void Leave()
        {
            if (ObjectName == "Bottle 2")
                DoNotDoThatMessage();
            else
                LeaveInFountainMessage(ObjectName);
        }
    }
}
