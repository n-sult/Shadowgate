using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate
{
    public class Bag : Item
    {
        bool isBagCurrentlyOpen = false;
        
        List<Item> bagInventory = new List<Item>();

        public Bag(string objectName, bool isHidden, List<Item> inventory)
        {
            ObjectName = objectName;
            IsHidden = isHidden;
            bagInventory = inventory;
        }

        public void PrintItems(bool showSelectionNumber)
        {
            Globals.selection = 1;
            Console.ForegroundColor = ConsoleColor.Gray;
            foreach (Item item in bagInventory)
            {
                if (showSelectionNumber) // if player wants to look at or take something, show a selection number
                    Console.Write($"{Globals.selection} - ");
                Console.WriteLine($"{item.ObjectName}");
                if (showSelectionNumber) // if players wants to look at or take something, increment the selection number
                    Globals.selection++;
            }
        }

        public override void Look()
        {
            if (ObjectName == "Bag 3" || ObjectName == "Pouch")
                Console.WriteLine("\nThis canvas pouch looks to be quite light. Close inspection reveals some druidic script on it.");
            else
                Console.WriteLine("\nIt's a leather pouch.");
        }

        public override bool Take()
        {
            if (ObjectName == "Pouch")
                ObjectName = "Bag 3";
            return true;
        }

        public override void Open()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\nThe {ObjectName} is open.");
            isBagCurrentlyOpen = true;
            GameFunctions.ReduceTorchFire();

            while (isBagCurrentlyOpen)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\nYou take note of the following in the bag: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                if (bagInventory.Count > 0)
                    PrintItems(false);
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("EMPTY");
                }

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\nWhat would you like to do?");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("1 - Look \n2 - Take \n3 - Close the bag");
                
                string userInput = Console.ReadLine();

                if (userInput == "1")
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nWhat would you like to look at?");
                    PrintItems(true);
                    Console.WriteLine($"\n{Globals.selection} - Never mind");
                    
                    string lookInput = Console.ReadLine();

                    GameFunctions.UserInputResult inputResult = GameFunctions.CheckUserInput(lookInput, null, bagInventory);
                    if (inputResult.Result < 1)
                        continue;
                    else if (inputResult.Result > 0 && inputResult.Result <= bagInventory.Count)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        bagInventory[inputResult.Result - 1].Look();
                    }
                        
                }
                else if (userInput == "2")
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nWhat would you like to take");
                    PrintItems(true);
                    Console.WriteLine($"\n{Globals.selection} - Never mind");

                    string takeInput = Console.ReadLine();

                    GameFunctions.UserInputResult inputResult = GameFunctions.CheckUserInput(takeInput, null, bagInventory);
                    if (inputResult.Result < 1)
                        continue;
                    else if (inputResult.Result > 0 && inputResult.Result <= bagInventory.Count)
                    {
                        if (bagInventory[inputResult.Result - 1].Take())
                        {
                            Globals.currentPlayer.PlayerInventory.Add(bagInventory[inputResult.Result - 1]); // add the item to inventory

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\nThe " + bagInventory[inputResult.Result - 1].ObjectName + " is in hand."); // message for taking item

                            bagInventory.Remove(bagInventory[inputResult.Result - 1]);   // remove item from bag

                            GameFunctions.ReduceTorchFire();
                        }
                    }
                }
                else if (userInput == "3")
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\nThe {ObjectName} is closed.");
                    isBagCurrentlyOpen = false;
                    GameFunctions.ReduceTorchFire();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInvalid selection. Try again.");
                }
            }
        }

        public override void Leave()
        {
            if (bagInventory.Count == 0)
                LeaveInFountainMessage(ObjectName);
            else
                DoNotDoThatMessage();
        }
    }
}
