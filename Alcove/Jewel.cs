using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Alcove
{
    public class Jewel : Item
    {
        public Jewel(string objectName)
        {
            ObjectName = objectName;
        }

        public Jewel(string objectName, bool isHidden)
        {
            ObjectName = objectName;
            IsHidden = isHidden;
        }

        public static void DoesNothingMessage()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nThe gem seems to fit but nothing happens.");
        }
        
        public override void Look()
        {
            if (ObjectName == "White Gem")
                Console.WriteLine("\nIt's a white stone of unknown origin. A fine thing to gamble away in a good card game!");
            else if (ObjectName == "Red Gem")
                Console.WriteLine("\nIt's a fine red ruby! \nIt's color reminds you of your adventure across the sea of blood.");
            else
                Console.WriteLine("\nIt's a dark blue gemstone that is as big as the pommel of a sword!");
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
                switch (ObjectName)
                {
                    case "White Gem":
                        if (result == "Small hole next to the wooden door")
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan; // show message of using the correct gem
                            Console.WriteLine("\nThe gem fits perfectly in the hole. \nA small crystal sphere magically appears on the stand!");
                            Rooms.ColdRoom.isWhiteGemUsed = true; 
                            Globals.currentPlayer.PlayerInventory.Remove(this); // permanently remove gem from inventory
                            GameFunctions.FindObject("Sphere", Globals.currentRoom.PointsOfInterest).IsHidden = false; // unhide the sphere
                            GameFunctions.ReduceTorchFire();
                        }
                        else if (result == "Floor Stone" || result == "Hole next to bookcase")
                            DoesNothingMessage();
                        else
                            base.Use();
                        break;
                    case "Blue Gem":
                        if (result == "Small hole next to the wooden door" || result == "Hole next to bookcase")
                            DoesNothingMessage();
                        else if (result == "Floor Stone")
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan; // show message of using the correct gem
                            Console.WriteLine("\nAs soon as you place the blue gem in the hole, you hear the sound of grinding stone. " +
                                "\nThe wall slowly rises to reveal a magical image of an old wizard. " +
                                "\n\n\"Listen, warrior! The Warlock Lord can only be defeated by thy courage and the Staff of Ages. " +
                                "\nRemember, five to find, three for the staff, one to be the key, and one to be they pathway. " +
                                "\nHave they wits about thee, warrior! Fare thee well.\" " +
                                "\n\nThe wall slides back into place, hiding the image from your sight. A scroll appears!");
                            Globals.currentPlayer.PlayerInventory.Remove(this); // permanently remove gem from inventory
                            GameFunctions.FindObject("Scroll 2", Globals.currentRoom.PointsOfInterest).IsHidden = false; // unhide scroll 2
                            GameFunctions.ReduceTorchFire();
                        }
                        else
                            base.Use();
                        break;
                    case "Red Gem":
                        if (result == "Small hole next to the wooden door" || result == "Floor Stone")
                            DoesNothingMessage();
                        else if (result == "Hole next to bookcase")
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan; // show message of using the correct gem
                            Console.WriteLine("\nThe right bookcase slowly slides away revealing a hidden passage.");
                            Globals.currentPlayer.PlayerInventory.Remove(this); // permanently remove gem from inventory
                            GameFunctions.FindObject("Doorway behind the bookcase", Globals.currentRoom.PointsOfInterest).IsHidden = false; // unhide entry behind the bookcase
                            GameFunctions.ReduceTorchFire();
                        }    
                        else if (result == "Water Fountain")
                            this.DoNotDoThatMessage();
                        else
                            base.Use();
                        break;
                }
            }
        }

        public override void Leave()
        {
            DoNotDoThatMessage();
        }
    }
}
