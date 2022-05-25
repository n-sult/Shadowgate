using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate
{
    public class Scroll : Item
    {
        public Scroll(string objectName, bool isHidden)
        {
            ObjectName = objectName;
            IsHidden = isHidden;
        }

        public override void Look()
        {
            GameFunctions.WriteLine("\nIt's an ancient, leather bound parchment.");
        }

        public override bool Take()
        {
            return true;
        }

        public void InitialOpenMessage()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            GameFunctions.WriteLine($"\nThe {ObjectName} is open.");
            GameFunctions.ReduceTorchFire();
        }

        public void CloseScrollMessage()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            GameFunctions.WriteLine($"\nThe {ObjectName} is closed.");
        }

        public void RemoveScrollFromRoomOrInventory(Scroll scroll)
        {
            if (Globals.clonedRoom.PointsOfInterest.Contains(scroll))
                Globals.clonedRoom.PointsOfInterest.Remove(scroll);
            else
                Globals.currentPlayer.PlayerInventory.Remove(scroll);
        }
        
        public override void Open()
        {
            InitialOpenMessage();
            switch (ObjectName)
            {
                case "Scroll 1":
                    Console.ForegroundColor = ConsoleColor.White;
                    GameFunctions.WriteLine("\nYour hands begin to sweat because of your extreme excitement... " +
                        "\n\n'Five to find, three are one.' \n'One gives access, the bladed sun.' " +
                        "\n\n'The Silver Orb, to banish below.' \n'The Staff of Ages, to vanquish the foe.' " +
                        "\n\n'Joining two, the Golden Blade.' \n'Last to invoke, the Platinum Horn.'");
                    break;
                case "Scroll 2":
                    Console.ForegroundColor = ConsoleColor.White;
                    GameFunctions.WriteLine("\nYou've read the scroll. The scroll reads, \n\n'As the shadow of the wind, thou shalt be!' " +
                        "\n\n'Humana' \n\nYou've learned one magic spell. \nAs the spell was chanted, the Scroll 2 quickly vanished.");
                    Globals.currentPlayer.Spellbook.Add(new Spell("Humana"));
                    RemoveScrollFromRoomOrInventory(this);
                    break;
                case "Scroll 3":
                    Console.ForegroundColor = ConsoleColor.White;
                    GameFunctions.WriteLine("\nYour hands begin to sweat because of your extreme excitement... \n\n'Lands under the heavens;' " +
                        "\n'the key to the world.' \n\n'Terra Terrakk' \n\nYou've learned one magic spell. As the spell was chanted, the Scroll 3 quickly vanished.");
                    Globals.currentPlayer.Spellbook.Add(new Spell("Terrakk"));
                    RemoveScrollFromRoomOrInventory(this);
                    break;
                case "Scroll 4":
                    Console.ForegroundColor = ConsoleColor.White;
                    GameFunctions.WriteLine("\nYou've read the scroll. \n\n'To move the sun from far to near, light is what the darkness fears.' " +
                        "\n\n'Instantum Illumina' \n\nYou've learned one magic spell. As the spell was chanted, the Scroll 4 quickly vanished.");
                    Globals.currentPlayer.Spellbook.Add(new Spell("Illumina"));
                    RemoveScrollFromRoomOrInventory(this);
                    break;
                case "Scroll 5":
                    Console.ForegroundColor = ConsoleColor.White;
                    GameFunctions.WriteLine("\nYou've read the scroll. \n\nObserving the stars: the throne constellation appears once " +
                        "every five summers. \nLegend says that it is a portal to another land.");
                    break;
                default:
                    base.Open();
                    break;
            }
        }

        public override void Close()
        {
            CloseScrollMessage();
        }

        public override void Leave()
        {
            DoNotDoThatMessage();
        }
    }
}
