using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace Shadowgate
{
    class Globals
    {
        // debugging items to add to list of inventory,
        // TODO DELETE THIS LIST FOR FINAL
        public static Torch torch = new Torch("Torch");
        public static Key key1 = new Key("Key 1", false, false);
        public static Closet.Sling sling = new Closet.Sling("Sling");
        public static Closet.Sword sword = new Closet.Sword("Sword");
        public static Key key2 = new Key("Key 2", false, false);
        public static CrampedHallway.HolyTorch holyTorch = new CrampedHallway.HolyTorch("Holy Torch");
        public static Key key3 = new Key("Key 3", false, false);
        public static Waterfall.Stone stone = new Waterfall.Stone("Stone");
        public static Alcove.Jewel whiteGem = new Alcove.Jewel("White Gem");
        public static Alcove.Jewel redGem = new Alcove.Jewel("Red Gem");
        public static Alcove.Jewel blueGem = new Alcove.Jewel("Blue Gem");
        public static ColdRoom.Sphere sphere = new ColdRoom.Sphere("Sphere", false);
        public static Lair.Shield shield = new Lair.Shield("Shield");
        public static Lair.Hammer hammer = new Lair.Hammer("Hammer");
        public static Lair.Spear spear = new Lair.Spear("Spear");
        public static Lair.LairSkull lairSkull = new Lair.LairSkull("Skull");
        public static Tomb.CopperCoin copperCoin1 = new Tomb.CopperCoin("Copper Coin");
        public static Tomb.CopperCoin copperCoin2 = new Tomb.CopperCoin("Copper Coin");
        public static Tomb.Scepter scepter = new Tomb.Scepter("Scepter", false);
        public static HiddenRoom.Arrow arrow = new HiddenRoom.Arrow("Arrow");
        public static Bottle bottle1 = new Bottle("Bottle 1");
        public static Bottle bottle2 = new Bottle("Bottle 2");
        public static Bottle bottle3 = new Bottle("Bottle 3");
        public static Bottle bottle4 = new Bottle("Bottle 4");
        public static Bottle bottle5 = new Bottle("Bottle 5");
        public static WraithRoom.Cloak cloak = new WraithRoom.Cloak("Cloak");
        public static MirrorRoom.Broom broom = new MirrorRoom.Broom("Broom");
        public static Scroll scroll2 = new Scroll("Scroll 2", false);
        public static Scroll scroll3 = new Scroll("Scroll 3", false);
        public static Scroll scroll4 = new Scroll("Scroll 4", false);
        public static Courtyard.Gauntlet gauntlet = new Courtyard.Gauntlet("Gauntlet", false);
        public static Library.BookOnDesk bookOnDesk = new Library.BookOnDesk("Library Book");
        public static Library.Glasses glasses = new Library.Glasses("Glasses", false);
        public static Library.LibraryMap libraryMap = new Library.LibraryMap("Map");
        public static Library.LibrarySkull librarySkull = new Library.LibrarySkull("Skull");
        public static Study.Bellows bellows = new Study.Bellows("Bellows");
        public static Study.Poker poker = new Study.Poker("Poker");
        public static Key key5 = new Key("Key 5", false, false);
        public static Key key6 = new Key("Key 6", false, false);
        public static Laboratory.HolyWater holyWater = new Laboratory.HolyWater("Water", false);
        public static Laboratory.Horseshoe horseshoe = new Laboratory.Horseshoe("Horseshoe");
        public static Garden.Flute flute = new Garden.Flute("Flute");
        public static Garden.Ring ring = new Garden.Ring("Ring", false);
        public static Key key4 = new Key("Key 4", false, false);
        public static BanquetHall.BanquetMirror mirror = new BanquetHall.BanquetMirror("Mirror");
        public static Observatory.Rod rod = new Observatory.Rod("Rod", false);
        public static Observatory.Star star = new Observatory.Star("Star");
        public static Watchtower.Blade blade = new Watchtower.Blade("Blade");
        public static BrazierRoom.Horn horn = new BrazierRoom.Horn("Horn");
        public static Turret.Talisman talisman = new Turret.Talisman("Talisman");
        public static Balcony.Wand wand = new Balcony.Wand("Wand", false);
        public static LookoutPoint.BigCoin bigCoin = new LookoutPoint.BigCoin("Big Coin");
        public static LookoutPoint.GoldCoin goldCoin1 = new LookoutPoint.GoldCoin("Gold Coin");
        public static LookoutPoint.GoldCoin goldCoin2 = new LookoutPoint.GoldCoin("Gold Coin");
        public static LookoutPoint.GoldCoin goldCoin3 = new LookoutPoint.GoldCoin("Gold Coin");
        public static SnakeCave.Staff staff = new SnakeCave.Staff("Staff", false);
        public static SwitchCave.Orb orb = new SwitchCave.Orb("Orb", false);
        // TODO: DELETE the above list FOR FINAL

        public static List<Room> rooms = new List<Room>();
        public static Room currentRoom;
        public static Room previousRoom;
        public static Room clonedRoom;

        public static int selection;
        public static Player currentPlayer = new("Self", false, false, false);
        public static Player permanentPlayer = new("Self", false, false, false);
        public static int NumberOfBottle2Consumed;

        public static bool isGameOver = false;
        public static bool isGameBeat;
    }

    public static class SystemExtension
    {
        public static T Clone<T>(this T source)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All,
                ContractResolver = new MyContractResolver()};
            var serialized = JsonConvert.SerializeObject(source, settings);
            return JsonConvert.DeserializeObject<T>(serialized, settings);
        }
    }

    public class MyContractResolver : Newtonsoft.Json.Serialization.DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var props = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                            .Select(p => base.CreateProperty(p, memberSerialization))
                        .Union(type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                                   .Select(f => base.CreateProperty(f, memberSerialization)))
                        .ToList();
            props.ForEach(p => { p.Writable = true; p.Readable = true; });
            return props;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            GameFunctions.CreateLog();
            
            ActiveTorch activeTorch1 = new ActiveTorch("Active Torch 1", 61);
            ActiveTorch activeTorch2 = new ActiveTorch("Active Torch 2", 0);
            
            List<Item> startingInventory = new List<Item>() { activeTorch1, activeTorch2,/* Globals.torch, Globals.key1, Globals.sling, Globals.sword,
                Globals.key2, Globals.holyTorch, Globals.key3, Globals.stone, Globals.whiteGem, Globals.redGem, Globals.blueGem, Globals.sphere,
                Globals.shield, Globals.hammer, Globals.spear, Globals.lairSkull, Globals.scepter, Globals.arrow, Globals.cloak, 
                Globals.broom, Globals.scroll2, Globals.scroll3, Globals.scroll4, Globals.gauntlet, Globals.bookOnDesk, Globals.glasses, 
                Globals.libraryMap, Globals.librarySkull, Globals.bellows, Globals.key5, Globals.key6, Globals.holyWater, Globals.horseshoe, 
                Globals.flute, Globals.ring, Globals.key4, Globals.mirror, Globals.rod, Globals.star, Globals.blade, Globals.horn, Globals.talisman, 
                Globals.wand, Globals.bigCoin, Globals.goldCoin1, Globals.staff, Globals.orb, Globals.bottle2, Globals.bottle2, Globals.bottle2,*/ };

            // Globals.currentPlayer.TorchCount = 8;
            
            Globals.currentPlayer.PlayerInventory.AddRange(startingInventory);
            
            GameFunctions.RoomCreation(); // add all rooms to global list of rooms
            GameFunctions.MoveRooms("Outside the Castle"); // TODO: in final, should be "Outside the Castle"

            while (!Globals.currentPlayer.IsPlayerDead && !Globals.isGameBeat) // as long as game is not over, run main game loop
            {
                Console.ForegroundColor = ConsoleColor.Gray; // here as a safeguard to return text back to normal

                GameFunctions.CheckTorchLevels((ActiveTorch)Globals.currentPlayer.PlayerInventory[0]);
                GameFunctions.CheckTorchLevels((ActiveTorch)Globals.currentPlayer.PlayerInventory[1]);

                Console.ForegroundColor = ConsoleColor.White;
                GameFunctions.WriteLine("\n\nYou take note of the following in the room: "); 
                Console.ForegroundColor = ConsoleColor.Gray;
                foreach (PointOfInterest pointOfInterest in Globals.clonedRoom.PointsOfInterest) // list off all POIs in the room as long as it's NOT marked as hidden
                    if (!pointOfInterest.IsHidden)
                        GameFunctions.WriteLine(pointOfInterest.ObjectName);
                    
                List<PointOfInterest> currentRoomInterests = new List<PointOfInterest>(); // create new list to add all POI from globals.currentroom (is this needed?)

                foreach (PointOfInterest pointOfInterest in Globals.clonedRoom.PointsOfInterest.Where(x => !x.IsHidden).ToList())
                    currentRoomInterests.Add(pointOfInterest);

                Console.ForegroundColor = ConsoleColor.White;
                GameFunctions.WriteLine("\nWhat would you like to do?");
                Console.ForegroundColor = ConsoleColor.Gray;
                GameFunctions.WriteLine("1 - Move \n2 - Look \n3 - Take \n4 - Open \n5 - Close \n6 - Use " +
                    "\n7 - Hit \n8 - Leave \n9 - Speak \n10 - Cast a Spell \n11 - View Inventory");  // prompt for player to select action

                string userInput = GameFunctions.ReadLine();

                if (userInput == "1") // player wants to move
                {
                    List<PointOfInterest> listOfThingsToMoveTo = new List<PointOfInterest>(GameFunctions.AddSelfToCurrentList(currentRoomInterests));

                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    GameFunctions.WriteLine("\nWhere would you like to move?");

                    Console.ForegroundColor = ConsoleColor.Gray;
                    GameFunctions.PrintPOIs(listOfThingsToMoveTo); // show list non-hidden POIs in the room
                    GameFunctions.WriteLine($"\n{Globals.selection} - Never mind"); // this visually reflects a 'cancel' option

                    string moveInput = GameFunctions.ReadLine();

                    GameFunctions.UserInputResult inputResult = GameFunctions.CheckUserInput(moveInput, listOfThingsToMoveTo);
                    if (inputResult.Result < 1)
                        continue;
                    else if (inputResult.Result > 0 && inputResult.Result <= listOfThingsToMoveTo.Count)
                    {
                        if (listOfThingsToMoveTo[inputResult.Result - 1] is Player)
                            Globals.currentPlayer.Move();
                        else
                            Globals.clonedRoom.MoveTo(listOfThingsToMoveTo[inputResult.Result - 1].ObjectName);
                    }
                        
                }
                else if (userInput == "2") // player wants to look at something
                {
                    List<PointOfInterest> roomAndBagStuff = new List<PointOfInterest>();
                    foreach (PointOfInterest pointOfInterest in currentRoomInterests)
                        roomAndBagStuff.Add(pointOfInterest);
                    foreach (Item item in Globals.currentPlayer.PlayerInventory)
                        roomAndBagStuff.Add(item);

                    List<PointOfInterest> listOfThingsToLookAt = GameFunctions.PutPOIsItemsAndSelfInOneList();

                    Globals.selection = 1;
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    GameFunctions.WriteLine("\nWhat would you like to look at?");

                    Console.ForegroundColor = ConsoleColor.White; // Show a list of all POIs in the room in a separate list
                    GameFunctions.WriteLine("\nIn the room: ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    GameFunctions.PrintPOIs(currentRoomInterests);

                    Console.ForegroundColor = ConsoleColor.White; // show a list of all items in the inventory in a separate list
                    GameFunctions.WriteLine("\nIn your inventory: ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    foreach (Item item in Globals.currentPlayer.PlayerInventory)
                    {
                        if (item is Torch)
                            GameFunctions.WriteLine($"{Globals.selection} - {item.ObjectName}: {Globals.currentPlayer.TorchCount}");
                        else
                            GameFunctions.WriteLine($"{Globals.selection} - {item.ObjectName}");
                        Globals.selection++;
                    }
                    GameFunctions.Write($"\n{Globals.selection++} - Self");
                    // GameFunctions.Write($"\n{Globals.selection++} - Self");
                    GameFunctions.WriteLine($"\n{Globals.selection} - Never mind"); // this visually reflects a 'cancel' option

                    string lookInput = GameFunctions.ReadLine();

                    GameFunctions.UserInputResult inputResult = GameFunctions.CheckUserInput(lookInput, listOfThingsToLookAt);
                    if (inputResult.Result < 1)
                        continue;
                    else if (inputResult.Result > 0 && inputResult.Result <= listOfThingsToLookAt.Count)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        if (listOfThingsToLookAt[inputResult.Result - 1] is Player)
                            Globals.currentPlayer.Look();
                        else
                            Globals.clonedRoom.LookAt(listOfThingsToLookAt[inputResult.Result - 1].ObjectName);
                    }
                }
                else if (userInput == "3") // player wants to take something
                {
                    List<PointOfInterest> listOfThingsToTake = new List<PointOfInterest>(GameFunctions.AddSelfToCurrentList(currentRoomInterests));

                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    GameFunctions.WriteLine("\nWhat would you like to take?");
                    Console.ForegroundColor = ConsoleColor.Gray;

                    GameFunctions.PrintPOIs(listOfThingsToTake); // show list non-hidden POIs in the room
                    GameFunctions.WriteLine($"\n{Globals.selection} - Never mind"); // this visually reflects a 'cancel' option

                    string takeInput = GameFunctions.ReadLine();

                    GameFunctions.UserInputResult inputResult = GameFunctions.CheckUserInput(takeInput, listOfThingsToTake);
                    if (inputResult.Result < 1)
                        continue;
                    else if (inputResult.Result > 0 && inputResult.Result <= listOfThingsToTake.Count)
                        if (listOfThingsToTake[inputResult.Result - 1] is Player)
                            Globals.currentPlayer.Take();
                        else
                            Globals.clonedRoom.TakeObject(listOfThingsToTake[inputResult.Result - 1].ObjectName);
                }
                else if (userInput == "4") // player wants to open something
                {
                    List<PointOfInterest> listOfThingsToOpen = GameFunctions.PutPOIsItemsAndSelfInOneList();

                    Globals.selection = 1;
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    GameFunctions.WriteLine("\nWhat would you like to Open?");

                    Console.ForegroundColor = ConsoleColor.White;
                    GameFunctions.WriteLine("\nIn the room: ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    foreach (PointOfInterest POI in currentRoomInterests)
                    {
                        GameFunctions.WriteLine($"{Globals.selection} - {POI.ObjectName}");
                        Globals.selection++;
                    }

                    Console.ForegroundColor = ConsoleColor.White;
                    GameFunctions.WriteLine("\nIn your inventory: ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    foreach (Item item in Globals.currentPlayer.PlayerInventory)
                    {
                        if (item is Torch)
                            GameFunctions.WriteLine($"{Globals.selection} - {item.ObjectName}: {Globals.currentPlayer.TorchCount}");
                        else
                            GameFunctions.WriteLine($"{Globals.selection} - {item.ObjectName}");
                        Globals.selection++;
                    }
                    GameFunctions.Write($"\n{Globals.selection++} - Self");
                    GameFunctions.WriteLine($"\n{Globals.selection} - Never mind"); // this visually reflects a 'cancel' option

                    string openInput = GameFunctions.ReadLine();

                    GameFunctions.UserInputResult inputResult = GameFunctions.CheckUserInput(openInput, listOfThingsToOpen);
                    if (inputResult.Result < 1)
                        continue;
                    else if (inputResult.Result > 0 && inputResult.Result <= listOfThingsToOpen.Count)
                        if (listOfThingsToOpen[inputResult.Result - 1] is Player)
                            Globals.currentPlayer.Open();
                        else
                            Globals.clonedRoom.OpenObject(listOfThingsToOpen[inputResult.Result - 1].ObjectName);
                }
                else if (userInput == "5") // player wants to close something
                {
                    List<PointOfInterest> listOfThingsToClose = new List<PointOfInterest>(GameFunctions.AddSelfToCurrentList(currentRoomInterests));

                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    GameFunctions.WriteLine("\nWhat would you like to close?");

                    Console.ForegroundColor = ConsoleColor.Gray;
                    GameFunctions.PrintPOIs(listOfThingsToClose); // show list non-hidden POIs in the room
                    GameFunctions.WriteLine($"\n{Globals.selection} - Never mind"); // this visually reflects a 'cancel' option

                    string closeInput = GameFunctions.ReadLine();

                    GameFunctions.UserInputResult inputResult = GameFunctions.CheckUserInput(closeInput, listOfThingsToClose);
                    if (inputResult.Result < 1)
                        continue;
                    else if (inputResult.Result > 0 && inputResult.Result <= listOfThingsToClose.Count)
                        if (listOfThingsToClose[inputResult.Result - 1] is Player)
                            Globals.currentPlayer.Close();
                        else
                            Globals.clonedRoom.CloseObject(listOfThingsToClose[inputResult.Result - 1].ObjectName);
                }
                else if (userInput == "6") // player wants to use something
                {
                    List<PointOfInterest> listOfThingsToUse = GameFunctions.PutPOIsItemsAndSelfInOneList();

                    Globals.selection = 1;
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    GameFunctions.WriteLine("\nWhat would you like to use?");

                    Console.ForegroundColor = ConsoleColor.White;
                    GameFunctions.WriteLine("\nIn the room: ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    foreach (PointOfInterest POI in currentRoomInterests)
                    {
                        GameFunctions.WriteLine($"{Globals.selection} - {POI.ObjectName}");
                        Globals.selection++;
                    }

                    Console.ForegroundColor = ConsoleColor.White;
                    GameFunctions.WriteLine("\nIn your inventory: ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    foreach (Item item in Globals.currentPlayer.PlayerInventory)
                    {
                        if (item is Torch)
                            GameFunctions.WriteLine($"{Globals.selection} - {item.ObjectName}: {Globals.currentPlayer.TorchCount}");
                        else
                            GameFunctions.WriteLine($"{Globals.selection} - {item.ObjectName}");
                        Globals.selection++;
                    }
                    GameFunctions.Write($"\n{Globals.selection++} - Self");
                    GameFunctions.WriteLine($"\n{Globals.selection} - Never mind"); // this visually reflects a 'cancel' option

                    string useInput = GameFunctions.ReadLine();

                    GameFunctions.UserInputResult inputResult = GameFunctions.CheckUserInput(useInput, listOfThingsToUse);
                    if (inputResult.Result < 1)
                        continue;
                    else if (listOfThingsToUse[inputResult.Result - 1] is Player)
                        listOfThingsToUse[inputResult.Result - 1].Use();
                    else if (inputResult.Result > 0 && inputResult.Result <= listOfThingsToUse.Count)
                    {
                        if (listOfThingsToUse[inputResult.Result - 1] is Item /*|| listOfThingsToUse[inputResult.Result - 1].ObjectName == "Mallet"*/)
                            if (inputResult.Result == Globals.selection) // check that item is in your inventory in order to use it BUT make an exception for the Mallet POI
                                continue;
                            else if (GameFunctions.CheckInventory((Item)listOfThingsToUse[inputResult.Result - 1]) || listOfThingsToUse[inputResult.Result - 1].ObjectName == "Mallet")
                                listOfThingsToUse[inputResult.Result - 1].Use();
                            else // if the item was not in your inventory, you get this message
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                GameFunctions.WriteLine("\nYou can't use what you didn't take.");
                            }
                        else if (listOfThingsToUse[inputResult.Result - 1] is PointOfInterest)
                            Globals.clonedRoom.UseObject(listOfThingsToUse[inputResult.Result - 1].ObjectName);
                    }
                }
                else if (userInput == "7") // player wants to hit something
                {
                    List<PointOfInterest> listOfThingsToHit = new List<PointOfInterest>(GameFunctions.AddSelfToCurrentList(currentRoomInterests));

                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    GameFunctions.WriteLine("\nWhat would you like to hit?");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    GameFunctions.PrintPOIs(listOfThingsToHit); // show list non-hidden POIs in the room
                    GameFunctions.WriteLine($"\n{Globals.selection} - Never mind"); // this visually reflects a 'cancel' option

                    string hitInput = GameFunctions.ReadLine();

                    GameFunctions.UserInputResult inputResult = GameFunctions.CheckUserInput(hitInput, listOfThingsToHit);
                    if (inputResult.Result < 1)
                        continue;
                    else if (inputResult.Result > 0 && inputResult.Result <= listOfThingsToHit.Count)
                        if (listOfThingsToHit[inputResult.Result - 1] is Player)
                            Globals.currentPlayer.Hit();
                        else
                            Globals.clonedRoom.HitObject(listOfThingsToHit[inputResult.Result - 1].ObjectName);
                }
                else if (userInput == "8") // player wants to drop something
                {
                    List<Item> listOfThingsToDrop = new List<Item>(Globals.currentPlayer.PlayerInventory);

                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    GameFunctions.WriteLine("\nWhat item would you like to drop?");
                    
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Globals.selection = 1;
                    foreach (Item item in Globals.currentPlayer.PlayerInventory)
                        GameFunctions.WriteLine($"{Globals.selection++} - {item.ObjectName}");
                    GameFunctions.WriteLine($"\n{Globals.selection} - Never mind"); // this visually reflects a 'cancel' option

                    string leaveInput = GameFunctions.ReadLine();

                    GameFunctions.UserInputResult inputResult = GameFunctions.CheckUserInput(leaveInput, null, listOfThingsToDrop);
                    if (inputResult.Result < 1)
                        continue;
                    else if (inputResult.Result > 0 && inputResult.Result <= listOfThingsToDrop.Count)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        GameFunctions.WriteLine("\nWhere should this be dropped?");

                        Globals.selection = 1;
                        Console.ForegroundColor = ConsoleColor.Gray;
                        foreach (PointOfInterest POI in Globals.clonedRoom.PointsOfInterest)
                            GameFunctions.WriteLine($"{Globals.selection++} - {POI.ObjectName}");
                        GameFunctions.WriteLine($"\n{Globals.selection} - Never mind"); // this visually reflects a 'cancel' option

                        string leaveWhere = GameFunctions.ReadLine();

                        GameFunctions.UserInputResult inputResult1 = GameFunctions.CheckUserInput(leaveWhere, Globals.clonedRoom.PointsOfInterest);
                        if (inputResult1.Result < 1)
                            continue;
                        else if (inputResult1.Result > 0 && inputResult1.Result <= Globals.clonedRoom.PointsOfInterest.Count)
                        {
                            if (Globals.clonedRoom.PointsOfInterest[inputResult1.Result - 1].ObjectName == "Water Fountain")
                                Globals.currentPlayer.PlayerInventory[inputResult.Result - 1].Leave();
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                GameFunctions.WriteLine("\nYou can't drop it here.");
                            }
                        }
                    }
                }
                else if (userInput == "9") // player wants to speak to something
                {
                    List<PointOfInterest> listOfThingsToSpeakTo = new List<PointOfInterest>(GameFunctions.AddSelfToCurrentList(currentRoomInterests));

                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    GameFunctions.WriteLine("\nTo whom would you like to speak?");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    GameFunctions.PrintPOIs(listOfThingsToSpeakTo); // show list non-hidden POIs in the room
                    GameFunctions.WriteLine($"\n{Globals.selection} - Never mind"); // this visually reflects a 'cancel' option

                    string speakInput = GameFunctions.ReadLine();

                    GameFunctions.UserInputResult inputResult = GameFunctions.CheckUserInput(speakInput, listOfThingsToSpeakTo);
                    if (inputResult.Result < 1)
                        continue;
                    else if (inputResult.Result > 0 && inputResult.Result <= listOfThingsToSpeakTo.Count)
                        if (listOfThingsToSpeakTo[inputResult.Result - 1] is Player)
                            Globals.currentPlayer.Speak();
                        else
                            Globals.clonedRoom.SpeakTo(listOfThingsToSpeakTo[inputResult.Result - 1].ObjectName);
                }
                else if (userInput == "10")
                {
                    if (Globals.currentPlayer.Spellbook.Count == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        GameFunctions.WriteLine("\nYou have no spells to cast!");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        GameFunctions.WriteLine("\nWhat spell would you like to cast?");

                        Globals.selection = 1;
                        Console.ForegroundColor = ConsoleColor.Gray;
                        foreach (Spell spell in Globals.currentPlayer.Spellbook)
                        {
                            GameFunctions.WriteLine($"{Globals.selection} - {spell.SpellName}");
                            Globals.selection++;
                        }
                        GameFunctions.WriteLine($"\n{Globals.selection} - Never mind");

                        string spellUseInput = GameFunctions.ReadLine();

                        GameFunctions.UserInputResult inputResult = GameFunctions.CheckUserInput(spellUseInput, null, null, Globals.currentPlayer.Spellbook);
                        if (inputResult.Result < 1)
                            continue;
                        else if (inputResult.Result > 0 && inputResult.Result <= Globals.currentPlayer.Spellbook.Count)
                            Spell.CastSpell(Globals.currentPlayer.Spellbook[inputResult.Result - 1].SpellName);
                    }
                }
                else if (userInput == "11")
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    GameFunctions.WriteLine("\nYour current inventory: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    foreach (Item item in Globals.currentPlayer.PlayerInventory)
                    {
                        if (item == activeTorch1)
                            GameFunctions.WriteLine($"{item.ObjectName}: Fire Left - {activeTorch1.FireRemaining}");
                        else if (item == activeTorch2)
                            GameFunctions.WriteLine($"{item.ObjectName}: Fire Left - {activeTorch2.FireRemaining}");
                        else if (item.ObjectName == "Torch")
                            GameFunctions.WriteLine($"{item.ObjectName} - {Globals.currentPlayer.TorchCount}");
                        else
                            GameFunctions.WriteLine(item.ObjectName);
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    GameFunctions.WriteLine("\nInvalid selection. Try again.");
                }

                if (activeTorch1.FireRemaining == 0 && activeTorch2.FireRemaining == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    GameFunctions.WriteLine("\nYour torch goes out with a fizzle. With out-stretched arms. You move slowly, looking for a light. " +
                        "\nSuddenly, you trip over something! SMASH! You fall face first to the floor!");
                    GameFunctions.GameOver();
                }

                if (Globals.currentPlayer.IsPlayerDead) // if player is dead, run sequence to ask if they wish to continue or not
                {
                    bool questionAnswered = false;
                    while (!questionAnswered)
                    {
                        GameFunctions.WriteLine("\nDo you want to continue? Y/N");
                        string userinput = GameFunctions.ReadLine();

                        if (userinput == "Y" || userinput == "y")
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            GameFunctions.WriteLine("\nGoing back to previous room...");
                            Thread.Sleep(1500);

                            Globals.currentPlayer.IsPlayerDead = false;

                            GameFunctions.MoveRooms(null, true); // move to previous room
                            questionAnswered = true;
                        }
                        else if (userinput == "N" || userinput == "n")
                        {
                            GameFunctions.WriteLine("\nThe Warlock Lord will surely triumph now...");
                            questionAnswered = true;
                            Globals.isGameOver = true;
                        }
                    }
                }
                else if (Globals.isGameBeat)
                    GameFunctions.WriteLine("\nCongratulations on completing Shadowgate! Thanks for playing!");
            }
        }
    }
}
