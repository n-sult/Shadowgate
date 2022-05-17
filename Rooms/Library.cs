using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Rooms
{
    public class Library : Room
    {
        public static bool RedGemUsed;

        public Library()
        {
            // Items for Library
            Shadowgate.Library.BookOnDesk bookOnDesk = new Shadowgate.Library.BookOnDesk("Book on the desk");
            Shadowgate.Library.LibraryMap libraryMap = new Shadowgate.Library.LibraryMap("Map");
            Shadowgate.Library.LibrarySkull librarySkull = new Shadowgate.Library.LibrarySkull("Skull");
            Scroll scroll3 = new Scroll("Scroll 3", true);
            Scroll scroll4 = new Scroll("Scroll 4", true);
            Key key5 = new Key("Key 5", true, false);
            Shadowgate.Library.Glasses glasses = new Shadowgate.Library.Glasses("Glasses", true);

            // All POI for Library
            PointOfInterest leftBookcase = new PointOfInterest("Left Bookcase");
            PointOfInterest rightBookcase = new PointOfInterest("Right Bookcase");
            PointOfInterest book1 = new PointOfInterest("Book 1");
            PointOfInterest book2 = new PointOfInterest("Book 2");
            PointOfInterest book3 = new PointOfInterest("Book 3");
            PointOfInterest book4 = new PointOfInterest("Book 4");
            PointOfInterest book5 = new PointOfInterest("Book 5");
            PointOfInterest book6 = new PointOfInterest("Book 6");
            PointOfInterest book7 = new PointOfInterest("Book 7");
            PointOfInterest encyclopediaSet = new PointOfInterest("Encyclopedia Set");
            PointOfInterest libraryDesk = new PointOfInterest("Library Desk");
            PointOfInterest libraryHole = new PointOfInterest("Hole next to bookcase");
            Entry doorwayBehindBookcase = new Entry("Doorway behind the bookcase", false, true, false, true, "Study");
            Entry doorFromLibraryToDraftyHall = new Entry("Door to Drafty Hallway", false, true, false, false, "Drafty Hallway");
            var libraryPOI = new List<PointOfInterest>() { glasses, key5, scroll3, scroll4, leftBookcase, rightBookcase, librarySkull, 
                book1, book2, book3, book4, book5, book6, book7, encyclopediaSet, libraryDesk, bookOnDesk,
                libraryHole, libraryMap, doorwayBehindBookcase, doorFromLibraryToDraftyHall};

            RoomName = "Library";
            FirstEntry = "You stand in a small library.";
            SubsequentEntry = "The bookcase in front of you is full of books.";
            PointsOfInterest = libraryPOI;
        }

        public override void LookAt(string objectName)
        {
            switch(objectName)
            {
                case "Left Bookcase":
                    Console.WriteLine("\nThe bookcase in front of you is full of books.");
                    break;
                case "Right Bookcase":
                    Console.WriteLine("\nIt's full of books. You don't have time to read every one of them. Think of your quest!");
                    break;
                case "Book 1":
                    Console.WriteLine("\nThis book's title is \"Westland\". You open the book and read it. \n\n'Areas not mentioned in this book are " +
                        "still not known to neither man nor elf.' \n'The chief borders are the Torlin Forest to the south, the Westland Sea to the " +
                        "west, Swamplands to the north and the Darken Range to the east.'");
                    break;
                case "Book 2":
                    Console.WriteLine("\nThis book's title is \"The Castle Shadowgate\". You open the book and read it. " +
                        "\n\n'The Castle Shadowgate is not the largest lands on Tarkus, but is the most powerful. " +
                        "The castle is surrounded by an impenetrable mountain known as Gatekeeper.' " +
                        "\n'Only members of the now lost Circle of Twelve can enter it.' " +
                        "\n'It is said that the walls of Shadowgate themselves are quite alive.'");
                    break;
                case "Book 3":
                    Console.WriteLine("\nThis book's title is \"Blue Dragon\". You open the book and read it. \n\n'And then there shall come a day when " +
                        "things will be lost and people won't know where.' \n'And brothers will run away for absolutely no reason at all and things " +
                        "will happen in the sky.' \n'And people will marvel at friends who have new names and birds will be different!'");
                    break;
                case "Book 4":
                    Console.WriteLine("\nThe book is opened and examined. The words are like none you have ever seen!");
                    break;
                case "Book 5":
                    Console.WriteLine("\nThis book's title is \"Gods\". You open the book and read it. " +
                        "\n\n'And when these days appear, the people will call upon their gods and there will be no answer.' " +
                        "\n'Nay, only he who carries the sword shall truly be given the scales to judge them by.'");
                    break;
                case "Book 6":
                    Console.WriteLine("\nThis book's title is \"The Circle of Twelve\". You open the book and read it. " +
                        "\n\n'The Circle of Twelve was formed before most things began to be. Their names are...Framas, Garolin, Talotin, Ronlin, " +
                        "Talimar, Magnas, Wontave, Butwik, Tenmakk, Sharnir, Lakmir, and Turgor.' " +
                        "\n'The Circle was broken when Talimar took a new name: The Warlock Lord!'");
                    break;
                case "Book 7":
                    Console.WriteLine("\nThis book's title is \"The History of the War\". You open the book and read it. " +
                        "\n\n'And when the Warlock Lord had finally gained power, he went up against the great kings!' " +
                        "\n'The evil one would have succeeded if it were not for the Circle of Twelve.' " +
                        "\n'If he ever returns to power, Tarkus will not live to see the rising sun...'");
                    break;
                case "Encyclopedia Set":
                    Console.WriteLine("\nThis is a complete twelve-volume set of 'Encyclopedia Druidica.'");
                    break;
                case "Library Desk":
                    Console.WriteLine("\nIt's a strong, wooden desk fit for a king. There are a couple of drawers in it.");
                    break;
                case "Hole next to bookcase":
                    if (!RedGemUsed)
                        Console.WriteLine("\nIt's a small hole in the wall some three inches deep.");
                    else
                        Console.WriteLine("\nThe gem fits perfectly into the hole.");
                    break;
                default:
                    base.LookAt(objectName);
                    break;
            }
        }

        public override void OpenObject(string objectName)
        {
            switch(objectName)
            {
                case "Book 1":
                case "Book 2":
                case "Book 3":
                case "Book 4":
                case "Book 5":
                case "Book 6":
                case "Book 7":
                case "Encyclopedia Set":
                    Console.ForegroundColor = ConsoleColor.White;
                    LookAt(objectName);
                    GameFunctions.ReduceTorchFire();
                    break;
                case "Library Desk":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\nThe desk is opened.");

                    string[] deskItems = { "Glasses", "Key 5", "Scroll 3", "Scroll 4" };

                    foreach (string item in deskItems) // search desk drawer to see if the item is in it. if so, unhide it.
                        if (GameFunctions.FindObject(item, PointsOfInterest) is not null)
                            GameFunctions.FindObject(item, PointsOfInterest).IsHidden = false;
                    GameFunctions.ReduceTorchFire();
                    break;
                default:
                    base.OpenObject(objectName);
                    break;
            }
        }

        public override void CloseObject(string objectName)
        {
            switch(objectName)
            {
                case "Library Desk":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\nThe desk is closed.");

                    string[] deskItems = { "Glasses", "Key 5", "Scroll 3", "Scroll 5" };

                    foreach (string item in deskItems) // search desk drawer to see if the item is in it. if so, hide it.
                        if (GameFunctions.FindObject(item, PointsOfInterest) is not null)
                            GameFunctions.FindObject(item, PointsOfInterest).IsHidden = true;
                    GameFunctions.ReduceTorchFire();
                    break;
                default:
                    base.CloseObject(objectName);
                    break;
            }
        }

        public override void HitObject(string objectName)
        {
            switch(objectName)
            {
                case "Hole next to bookcase":
                    GameFunctions.FindObject(objectName, PointsOfInterest).ThatSmartsMessage();
                    break;
                default:
                    base.HitObject(objectName);
                    break;
            }    
        }
    }
}
