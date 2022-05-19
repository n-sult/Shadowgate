using MoreLinq;
using Shadowgate.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Shadowgate
{
    public class GameFunctions
    {
        public static Action<string> RoomEnteredEvent;
        
        public static string UseOn(string objectName)
        {
            List<PointOfInterest> allPOIsItemsAndSelf = PutPOIsItemsAndSelfInOneList();
            
            var activeObject = GameFunctions.FindObject(objectName, allPOIsItemsAndSelf); // to be used later to remove object from list shown to player

            bool selectionMade = false;
            while (!selectionMade)
            {
                Globals.selection = 1;
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine($"\nWhat do you want to use {activeObject.ObjectName} on?");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\nIn the room: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                foreach (PointOfInterest POI in Globals.clonedRoom.PointsOfInterest)
                {
                    if (!POI.IsHidden)
                    {
                        Console.WriteLine($"{Globals.selection} - {POI.ObjectName}");
                        Globals.selection++;
                    }
                }

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\nIn your inventory: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                foreach (Item item in Globals.currentPlayer.PlayerInventory)
                {
                    Console.WriteLine($"{Globals.selection} - {item.ObjectName}");
                    Globals.selection++;
                }
                Console.Write($"\n{Globals.selection++} - Self");
                Console.WriteLine($"\n{Globals.selection} - Never mind");

                string userInput = Console.ReadLine();

                GameFunctions.UserInputResult inputResult = GameFunctions.CheckUserInput(userInput, allPOIsItemsAndSelf);
                if (inputResult.Result < 1 && inputResult.Result != -5)
                    continue;
                else if (inputResult.Result == -5)
                    selectionMade = true;
                else if (inputResult.Result > 0 && inputResult.Result <= allPOIsItemsAndSelf.Count)
                    return allPOIsItemsAndSelf[inputResult.Result - 1].ObjectName;
            }
            return null;
        }

        public static void ClonePlayer(bool didPlayerDie)
        {
            if (!didPlayerDie) // if the player changed rooms...
                Globals.permanentPlayer = Globals.currentPlayer.Clone();
            else if (didPlayerDie) // if the player died...
                Globals.currentPlayer = Globals.permanentPlayer.Clone();
        }

         // do the following when attempting to move rooms...
        public static void MoveRooms(string room = null, bool playerDied = false)
        {
            if (!playerDied) 
                ClonePlayer(false); // if player is moving rooms or game is launched, set permanentplayer to currentplayer
            else
                ClonePlayer(true); // if player died, set currentplayer to permanentplayer

            if (room is not null && Globals.clonedRoom is not null)  
                Globals.previousRoom = Globals.clonedRoom; // set the previous room to be the cloned room
                                                           // add .Clone() if necessary

            if (playerDied)
            {
                if (Globals.previousRoom is not null)
                {
                    // below: if the player died in the mirror room and were previously in the fire bridge, check if they had cloak equipped. 
                    // if not, move them back to the mirror room instead of the fire bridge. this is to avoid an endless loop.
                    if (Globals.currentRoom.RoomName == "Mirror Room" && Globals.previousRoom.RoomName == "Fire Bridge" && !Globals.permanentPlayer.IsCloakEquipped)
                        Globals.previousRoom = Globals.currentRoom.Clone();
                    else
                        Globals.currentRoom = Globals.previousRoom; // for all other rooms, set the current room to the previous room
                }
                else
                    Globals.previousRoom = Globals.currentRoom;      // (so you move back to same room)
            }
            else // if the game is launched OR player is just moving rooms, set current room to passed room name (to be moved to)
            {
                if (Globals.previousRoom is not null)
                {
                    Room thatRoom = Globals.rooms.Find(x => x.RoomName == Globals.clonedRoom.RoomName); // attempt to save the room in the list to the cloned room
                    Globals.rooms[Globals.rooms.IndexOf(thatRoom)] = Globals.clonedRoom;
                }
                Globals.currentRoom = Globals.rooms.Find(x => x.RoomName == room);
            }

            Globals.clonedRoom = Globals.currentRoom.Clone();
            Globals.clonedRoom.PointsOfInterest.RemoveRange(0, Globals.clonedRoom.PointsOfInterest.Count - Globals.currentRoom.PointsOfInterest.Count);

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\n***********************************************************************"); // show entry message for the room
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("Current location: " + Globals.clonedRoom.RoomName); // list off the current room

            Console.ForegroundColor = ConsoleColor.Gray;
            if (Globals.currentRoom.Visited)
                Console.WriteLine($"\n{Globals.clonedRoom.SubsequentEntry}");
            else
                Console.WriteLine($"\n{Globals.clonedRoom.FirstEntry}");
            Console.WriteLine("***********************************************************************");

            Globals.clonedRoom.Visited = true; // set to true to determine which entry message to show
            RoomEnteredEvent?.Invoke(Globals.clonedRoom.RoomName);
            ReduceTorchFire();
        }

        public static void PrintPOIs(List<PointOfInterest> pointOfInterests)
        {
            Globals.selection = 1;
            foreach (PointOfInterest pointOfInterest in pointOfInterests)
            {
                if (!pointOfInterest.IsHidden)
                    if (pointOfInterest is Player)
                        Console.Write($"\n{Globals.selection++} - {pointOfInterest.ObjectName}");
                    else
                        Console.WriteLine($"{Globals.selection++} - {pointOfInterest.ObjectName}");
            }
        }

        public static PointOfInterest FindObject(string objectName, List<PointOfInterest> pointOfInterests = null, List<Item> items = null)
        {
            if (pointOfInterests == null)
                return items.FirstOrDefault(x => x.ObjectName == objectName);
            return pointOfInterests.FirstOrDefault(x => x.ObjectName == objectName);
        }
        
        public static bool CheckInventory(Item item) // is passed item in inventory? if so it can be used, otherwise, let player know they must take it
        {
            if (Globals.currentPlayer.PlayerInventory.Contains(item))
                return true;
            return false;
        }

        public static void KillSelf(string objectName)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nYou thrust the {objectName} into your chest! Blood begins to flow! Suicide won't help in your quest!");
            GameOver();
        }

        public static void GameOver() // show default game over message and set player status to dead
        {
            Console.WriteLine("\nIt's a sad thing that your adventures have ended here!");
            Console.WriteLine("Game Over");

            Globals.currentPlayer.IsPlayerDead = true;
        }

        public static void GameEnding()
        {
            Console.WriteLine("\n\nWord of your historic quest has already reached the farthest parts of the land. " +
                "\nYou are triumphantly greeted as you enter the gates of the royal city of Stormhaven. " +
                "\nMoments later, you are ushered into the royal palace where you are greeted by the king! " +
                "\n\n'I know what thou hast done, brave one. The world would be dark forever without thee!' " +
                "\n\nYou are bestowed a kingtom to rule and the king's fair daughter's hand! " +
                "\nAs you leave the throne room, you know that although this quest is over, others await. " +
                "\nAfter all, the bards will need new legends to sing of and new tales to tell! " +
                "\nThe first story's end....");
            Globals.isGameBeat = true;
        }

        public static void ReduceTorchFire() // after a valid action has been taken, reduce each torch's level by 1
        {
            if (Globals.activeTorch1.FireRemaining != 0)
                Globals.activeTorch1.FireRemaining--;
            if (Globals.activeTorch2.FireRemaining != 0)
                Globals.activeTorch2.FireRemaining--;
        }

        public static void CheckTorchLevels(ActiveTorch activeTorch) // when listing torch levels, if it's above 15, list the number in yellow, otherwise, in red
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write($"\n{activeTorch.ObjectName} fire left: ");
            if (activeTorch.FireRemaining > 15)
                Console.ForegroundColor = ConsoleColor.Yellow;
            else
                Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(activeTorch.FireRemaining);
        }

        public static List<PointOfInterest> AddSelfToCurrentList(List<PointOfInterest> list) // add player to a list of interests for selection 
        {
            list.Add(Globals.currentPlayer);
            return list;
        }

        public static List<PointOfInterest> PutPOIsItemsAndSelfInOneList()
        {
            List<PointOfInterest> allThingsInRoomBagAndSelf = new List<PointOfInterest>() { };
            foreach (PointOfInterest POI in Globals.clonedRoom.PointsOfInterest)
                if (!POI.IsHidden)
                    allThingsInRoomBagAndSelf.Add(POI);
            foreach (Item item in Globals.currentPlayer.PlayerInventory)
                if (!item.IsHidden)
                    allThingsInRoomBagAndSelf.Add(item);
            allThingsInRoomBagAndSelf.Add(Globals.currentPlayer);
            return allThingsInRoomBagAndSelf;
        }

        public class UserInputResult
        {
            public bool IsInputNumeric;
            public int Result;
        }

        // checks if the input that was passed is valid
        public static UserInputResult CheckUserInput(string input, List<PointOfInterest> pointOfInterests = null, 
            List<Item> items = null, List<Spell> spells = null) 
        {
            List<object> list = new List<object>();

            if (pointOfInterests is null && items is null)
                list = spells.Cast<object>().ToList();
            else if (pointOfInterests is null && spells is null)
                list = items.Cast<object>().ToList();
            else
                list = pointOfInterests.Cast<object>().ToList();

            UserInputResult inputResult = new UserInputResult();
            int result;
            inputResult.IsInputNumeric = int.TryParse(input, out result); // check to see if the input is numeric

            if (inputResult.IsInputNumeric) // if the input was numeric...
            {
                if (result >= 1 && result <= list.Count) // if input was within the range of the list, return the input as the number to work with
                    inputResult.Result = result;
                else if (result < 1) // if the input was less than 1, alert player this is invalid
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInvalid selection. Try again.");
                    inputResult.Result = -5;
                }
                else if (result > list.Count) // if the input was greater than the number of elements in the list...
                {
                    if (result > Globals.selection) // if the input was greater than the current selection, alert player to invalid selection
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nInvalid selection. Try again.");
                    }
                    inputResult.Result = -5; // otherwise, it's assumed result == selection which is "cancel". either way, return negative number to return to previous menu
                }
            }
            else // if input was not numeric, alert player and return negative number to return to previous menu
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInvalid input. Try again.");
                inputResult.Result = -5;
            }
            return inputResult;
        }

        public static void RoomCreation()
        {
            OutsideTheCastle outsideCastle = new OutsideTheCastle();
            Globals.rooms.Add(outsideCastle);

            Rooms.CrampedHallway crampedHallway = new Rooms.CrampedHallway();
            Globals.rooms.Add(crampedHallway);

            EntranceHall entranceHall = new EntranceHall();
            Globals.rooms.Add(entranceHall);

            Rooms.Closet closet = new Rooms.Closet();
            Globals.rooms.Add(closet);

            DwarvenHall dwarvenHall = new DwarvenHall();
            Globals.rooms.Add(dwarvenHall);

            SharkPond sharkPond = new SharkPond();
            Globals.rooms.Add(sharkPond);

            Rooms.Waterfall waterfall = new Rooms.Waterfall();
            Globals.rooms.Add(waterfall);

            Rooms.Alcove alcove = new Rooms.Alcove();
            Globals.rooms.Add(alcove);

            Rooms.ColdRoom coldRoom = new Rooms.ColdRoom();
            Globals.rooms.Add(coldRoom);

            Rooms.Lair lairRoom = new Rooms.Lair();
            Globals.rooms.Add(lairRoom);

            Rooms.Tomb tombRoom = new Rooms.Tomb();
            Globals.rooms.Add(tombRoom);

            Rooms.MirrorRoom mirrorRoom = new Rooms.MirrorRoom();
            Globals.rooms.Add(mirrorRoom);

            Rooms.HiddenRoom hiddenRoom = new Rooms.HiddenRoom();
            Globals.rooms.Add(hiddenRoom);

            Rooms.BridgeRoom bridgeRoom = new Rooms.BridgeRoom();
            Globals.rooms.Add(bridgeRoom);

            Rooms.SnakeCave snakeCave = new Rooms.SnakeCave();
            Globals.rooms.Add(snakeCave);

            Rooms.WraithRoom wraithRoom = new Rooms.WraithRoom();
            Globals.rooms.Add(wraithRoom);

            Rooms.EporRoom eporRoom = new Rooms.EporRoom();
            Globals.rooms.Add(eporRoom);

            Rooms.LimestoneCave limestoneCave = new Rooms.LimestoneCave();
            Globals.rooms.Add(limestoneCave);

            Rooms.FireBridge fireBridge = new Rooms.FireBridge();
            Globals.rooms.Add(fireBridge);

            Rooms.TrollBridge trollBridge = new Rooms.TrollBridge();
            Globals.rooms.Add(trollBridge);

            Rooms.Courtyard courtyard = new Rooms.Courtyard();
            Globals.rooms.Add(courtyard);

            Rooms.DraftyHallway draftyHall = new Rooms.DraftyHallway();
            Globals.rooms.Add(draftyHall);

            Rooms.Library library = new Rooms.Library();
            Globals.rooms.Add(library);

            Rooms.Study study = new Rooms.Study();
            Globals.rooms.Add(study);

            Rooms.Laboratory laboratory = new Rooms.Laboratory();
            Globals.rooms.Add(laboratory);

            Rooms.Garden garden = new Rooms.Garden();
            Globals.rooms.Add(garden);

            Rooms.BanquetHall banquetHall = new Rooms.BanquetHall();
            Globals.rooms.Add(banquetHall);

            Rooms.SphinxChamber sphinxChamber = new Rooms.SphinxChamber();
            Globals.rooms.Add(sphinxChamber);

            Rooms.Observatory observatory = new Rooms.Observatory();
            Globals.rooms.Add(observatory);

            Rooms.Watchtower watchtower = new Rooms.Watchtower();
            Globals.rooms.Add(watchtower);

            Rooms.BrazierRoom brazierRoom = new Rooms.BrazierRoom();
            Globals.rooms.Add(brazierRoom);

            Rooms.Turret turret = new Rooms.Turret();
            Globals.rooms.Add(turret);

            Rooms.SmallCorridor smallCorridor = new SmallCorridor();
            Globals.rooms.Add(smallCorridor);

            Rooms.Balcony balcony = new Rooms.Balcony();
            Globals.rooms.Add(balcony);

            Rooms.LookoutPoint lookoutPoint = new Rooms.LookoutPoint();
            Globals.rooms.Add(lookoutPoint);

            Rooms.ThroneRoom throneRoom = new Rooms.ThroneRoom();
            Globals.rooms.Add(throneRoom);

            Rooms.UndergroundHall undergroundHall = new Rooms.UndergroundHall();
            Globals.rooms.Add(undergroundHall);

            Rooms.GargoyleCave gargoyleCave = new Rooms.GargoyleCave();
            Globals.rooms.Add(gargoyleCave);

            Rooms.LavaCave lavaCave = new Rooms.LavaCave();
            Globals.rooms.Add(lavaCave);

            Rooms.SwitchCave switchCave = new Rooms.SwitchCave();
            Globals.rooms.Add(switchCave);

            Rooms.WellRoom wellRoom = new Rooms.WellRoom();
            Globals.rooms.Add(wellRoom);

            Rooms.RiverStyx riverStyx = new Rooms.RiverStyx();
            Globals.rooms.Add(riverStyx);

            Rooms.Vault vault = new Rooms.Vault();
            Globals.rooms.Add(vault);

            Rooms.Chasm chasm = new Rooms.Chasm();
            Globals.rooms.Add(chasm);
        }
    }
}
