using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Library
{
    public class BookOnDesk : Item
    {
        public BookOnDesk(string objectName)
        {
            ObjectName = objectName;
        }

        public override void Look()
        {
            if (Globals.currentPlayer.IsGlassesEquipped) // if you have the glasses eqipped
            {
                Console.ForegroundColor = ConsoleColor.Cyan; // show message of reading the book
                Console.WriteLine("\nWow! With these glasses, you can understand and read what you could not before!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\n'The light grows faint' \n'the path winds down.' \n'Where life is lost,' \n'wisdom is found.' \n'The seed of the dream,' " +
                    "\n'fore the evil is free,' \n'where the sword is hung,' \n'he must place the key.' \n'A bridge to form,' \n'amidst burning death.' " +
                    "\n'A demon to guard.' \n\n'Motari Riseth' \n\nYou've learned one magic spell. As the spell was chanted, the book quickly vanished.");
                Globals.currentPlayer.Spellbook.Add(new Spell("Motari")); // add Motari spell to player spellbook

                if (Globals.clonedRoom.PointsOfInterest.Contains(this)) // if the book was in the room, remove the book from the room
                    Globals.clonedRoom.PointsOfInterest.Remove(this);
                else
                    Globals.currentPlayer.PlayerInventory.Remove(this); // otherwise, remove the book from player inventory
                GameFunctions.ReduceTorchFire();
            }
            else // show this message if glasses are not equipped
                Console.WriteLine("\nThis book looks quite old. The words \"The Prophecy\" are written upon it.");
        }

        public override bool Take()
        {
            ObjectName = "Library Book"; // update name when taking the book to be cleaner
            return true;
        }

        public override void Open()
        {

            if (Globals.currentPlayer.IsGlassesEquipped)
                Look();
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\nThe book is opened.");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nYou can't read the strange writing in the book.");
                GameFunctions.ReduceTorchFire();
            }
        }

        public override void Close()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nThe book is closed.");
            GameFunctions.ReduceTorchFire();
        }

        public override void Use()
        {
            var result = GameFunctions.UseOn(ObjectName);

            if (result is not null)
            {
                switch (result)
                {
                    case "Sphinx":
                        (Globals.clonedRoom as Rooms.SphinxChamber).UseItemOnSphinx(ObjectName);
                        break;
                    default:
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
