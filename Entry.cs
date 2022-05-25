using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate
{
    public class Entry : PointOfInterest
    {
        // Universal entry properties
        public bool IsDoorOpen;
        protected bool _canDoorBeClosed;
        protected bool _isDoorLocked = false;
        protected string _keyName;
        protected string _destination;

        public Entry()
        {

        }
        
        public Entry(string objectName, bool canDoorBeClosed, bool isDoorOpen, bool isDoorLocked, bool isHidden) // red herring doors with no destination
        {
            ObjectName = objectName;
            _canDoorBeClosed = canDoorBeClosed;
            IsDoorOpen = isDoorOpen;
            _isDoorLocked = isDoorLocked;
            IsHidden = isHidden;
        }

        public Entry(string objectName, bool canDoorBeClosed, bool isDoorOpen, bool isDoorLocked, bool isHidden, string destination) // doors that can be locked without a key
        {
            ObjectName = objectName;
            _canDoorBeClosed = canDoorBeClosed;
            IsDoorOpen = isDoorOpen;
            _isDoorLocked = isDoorLocked;
            IsHidden = isHidden;
            _destination = destination;
        }

        public Entry(string objectName, bool canDoorBeClosed, bool isDoorOpen, bool isDoorLocked, bool isHidden, string keyName, string destination) // doors locked with a key
        {
            ObjectName = objectName;
            _canDoorBeClosed = canDoorBeClosed;
            IsDoorOpen = isDoorOpen;
            _isDoorLocked = isDoorLocked;
            IsHidden = isHidden;
            _keyName = keyName;
            _destination = destination;
        }

        public static bool GetDoorLockStatus(Entry entry)
        {
            return entry._isDoorLocked;
        }

        public override void Look()
        {
            // first, check all doors that cannot be closed
            if (ObjectName == "Hallway around the corner" || ObjectName == "Hall to Cramped Hallway")
                GameFunctions.WriteLine("\nA hallway lies around the corner.");
            else if (ObjectName == "Wall Opening" || ObjectName == "Opening to Cramped Hallway")
                GameFunctions.WriteLine("\nThe wall is opened.");
            else if (ObjectName == "Rock Pile")
                GameFunctions.WriteLine("\nThis landslide looks like it occurred ages ago. It would take you months to clear it away.");
            else if (ObjectName == "Opening behind rock pile" || ObjectName == "Opening back to Waterfall" || ObjectName == "Hole in the floor" ||
                ObjectName == "Doorway on an elevated ledge" || ObjectName == "Trap Door hidden under the Throne" || ObjectName == "Doorway to the left" || 
                ObjectName == "Doorway to the right" || ObjectName == "Doorway behind the giant statue" || ObjectName == "Door back to Bridge Room" || 
                ObjectName == "Passageway to Throne Room" || ObjectName == "Door to Gargoyle Cave" || ObjectName == "Doorway to Lava Cave")
                GameFunctions.WriteLine("\nIt is very dark.");
            else if (ObjectName == "Left Bridge")
                GameFunctions.WriteLine("\nJudging by the intricate workmanship, this bridge seems to be quite sturdy.");
            else if (ObjectName == "Right Bridge")
            {
                if (Globals.previousRoom.RoomName == "Snake Cave")
                    GameFunctions.WriteLine("\nThe bridge is swinging.");
                else
                    GameFunctions.WriteLine("\nThis shabby bridge is held together with nothing by frayed ropes and rotten planks. " +
                        "\nThe ropes are indeed in bad condition.");
            }
            else if (ObjectName == "Door on a high ledge")
                CannotReachMessage();
            else if (ObjectName == "Hole in the ceiling")
                GameFunctions.WriteLine("\nIt's a small hole in the ceiling.");
            else if (ObjectName == "Door at the end of the wooden bridge" || ObjectName == "Doorway to Troll Bridge" || ObjectName == "Doorway next to the pot" || 
                ObjectName == "Doorway at the end of the hall" || ObjectName == "Central Doorway" || ObjectName == "Door to Laboratory" || 
                ObjectName == "Doorway back to Underground Hall")
                    GameFunctions.WriteLine("\nIt's a doorway.");
            else if (ObjectName == "Closer door on the left" || ObjectName == "Farther door on the left" || ObjectName == "Left Archway" ||
                ObjectName == "Right Archway" || ObjectName == "Door to Drafty Hallway" || ObjectName == "Doorway to Small Corridor" || ObjectName == "Doorway to Small Corridor")
                    GameFunctions.WriteLine("\nThe archway has no door.");
            else if (ObjectName == "Chamber Stairs with markings on the side" || ObjectName == "Stairs to Sphinx Chamber")
            {
                GameFunctions.WriteLine("\nIt's a stairway leading upward. There seem to be some strange marks scratched into its side.");
                GameFunctions.WriteLine("The markings appear as follows: ");
                GameFunctions.WriteLine("\n|||  ||   |    | |");
                GameFunctions.WriteLine("---  ---  ---  ---");
                GameFunctions.WriteLine("       |   ||   | ");
            }
            else if (ObjectName == "Wooden ladder to the next floor" || ObjectName == "Ladder to the next floor" || ObjectName == "Ladder to Observatory" ||
                ObjectName == "Stairs to Brazier Room")
                GameFunctions.WriteLine("\nIt's a wooden ladder.");
            else if (ObjectName == "Stone Stairs" || ObjectName == "Stairs to Balcony")
                GameFunctions.WriteLine("\nThe stone stairs connect the balcony to the look-out point.");
            else if (ObjectName == "Doorway to Bridge Room")
                GameFunctions.WriteLine("\nThe bridge is swinging.");
            else if (ObjectName == "Raft" || ObjectName == "Back to River Styx")
                GameFunctions.WriteLine("\nIt's a wooden raft.");
            else if (ObjectName == "Back to Well Room")
                GameFunctions.WriteLine("\nIt's the well leading back to the well room.");

            // next, check unique entries
            else if (ObjectName == "Far Wall with a door-shaped outline") // exception made for hidden door in epor room
            {
                if (!IsDoorOpen)
                    GameFunctions.WriteLine("\nDamp air is blowing out of the gap in the stone wall.");
                else
                    GameFunctions.WriteLine("\nThe wall is opened.");
            }
            else if (ObjectName == "Doorway back to Epor room")
            {
                if (!IsDoorOpen)
                    GameFunctions.WriteLine("\nIt's part of the wall.");
                else
                    GameFunctions.WriteLine("\nThe wall is opened.");
            }
            else if (ObjectName == "Doorway behind the bookcase" || ObjectName == "Door to Library") // exception for bookcase door
                GameFunctions.WriteLine("\nThe bookcase is opened.");
            else if (ObjectName == "Old Well")
                GameFunctions.WriteLine("\nThis fine well is made of both stone and mortar.");
            else if (ObjectName == "Skull Door" || ObjectName == "Door to Vault")
                GameFunctions.WriteLine("\nThe jaw of the skull is made of polished stone.");
            else if (IsDoorOpen) // default response if door CAN be closed but is OPEN
                GameFunctions.WriteLine("\nThe door is opened.");

            // different descriptions for closed doors
            else if (ObjectName == "Castle Door" || ObjectName == "Door to Outside the Castle" || ObjectName == "Left Door" || 
                ObjectName == "Door at floor level" || ObjectName == "doorAtTheEndOfTheHall" || ObjectName == "Door at the end of the hall" || 
                ObjectName == "Door at the head of the stairs" || ObjectName == "Door to Dwarven Hall" || ObjectName == "Door back to Wraith Room" || 
                ObjectName == "Door to Fire Bridge" || ObjectName == "Door back to Drafty Hall" || ObjectName == "Door to Banquet Hall" || 
                ObjectName == "Door on the other side of the bridge")
                GameFunctions.WriteLine("\nIt's a heavy wooden door with iron hinges.");
            else if (ObjectName == "Far Door" || ObjectName == "Door to Entrance Hall")
                GameFunctions.WriteLine("\nThis wooden door is reinforced with heavy sheets of steel.");
            else if (ObjectName == "Closet Door" || ObjectName == "Closet Door back to Entrance Hall")
                GameFunctions.WriteLine("\nEven though this door is only an inch thick, it is very sturdy.");
            else if (ObjectName == "White Stone on the wall")
                GameFunctions.WriteLine("\nIt's part of the wall.");
            else if (ObjectName == "Door next to the pond" || ObjectName == "Door back to Shark Pond")
                GameFunctions.WriteLine("\nThis metal door shows significant signs of rust.");
            else if (ObjectName == "Right Door" || ObjectName == "Door on the balcony" || ObjectName == "Doorway to Dwarven Hall" || ObjectName == "Door back to Banquet Hall")
                GameFunctions.WriteLine("\nThis door seems to be made of solid oak.");
            else if (ObjectName == "Center Door" || ObjectName == "Small Wooden Door" || ObjectName == "Door behind the cyclops" || ObjectName == "Door under the balcony"
                || ObjectName == "Door to Cold Room" || ObjectName == "Wooden door to Dwarven Hall" || ObjectName == "Door to the Courtyard" || ObjectName == "Doorway to Banquet Hall")
                GameFunctions.WriteLine("\nIt's a finely crafted wooden door.");
            else if (ObjectName == "Trap Door")
                GameFunctions.WriteLine("\nIt's a small trap door made of polished metal.");
            else if (ObjectName == "Hidden Stairway")
                GameFunctions.WriteLine("\nThe door is open.");
            else if (ObjectName == "Door behind broken mirror" || ObjectName == "Door to Mirror Room")
            {
                if (!IsDoorOpen)
                    GameFunctions.WriteLine("\nA solid iron door lies beyond the broken edges of the mirror.");
                else
                    GameFunctions.WriteLine("\nThe door is open.");
            }
        }

        public override void Move()
        {
            if (!IsDoorOpen)
                Open();
            else
                GameFunctions.MoveRooms(_destination);    
        }

        public override void Hit()
        {
            if (ObjectName == "White Stone on the wall")
                Open();
            else if (ObjectName == "Wall Opening")
                Look();
            else if (ObjectName == "Left Bridge")
                ThatSmartsMessage();
            else
                base.Hit();
        }

        public override void Open()
        {
            if (!_canDoorBeClosed) // check if door can not be opened
                base.Open();
            else if (IsDoorOpen) // check if door is already open
            {
                Console.ForegroundColor = ConsoleColor.White;
                GameFunctions.WriteLine("\nThe door is opened.");
            }
            else if (_isDoorLocked) // check if door is locked
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                GameFunctions.WriteLine("\nThe door is locked.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                if (ObjectName == "Far Wall with a door-shaped outline") // exception for hidden door in epor room
                    GameFunctions.WriteLine("\nYou feel the ground shake as the rock moves slowly aside to reveal a passageway.");
                else if (ObjectName == "Doorway back to Epor room") // exception for limestone door back to epor room
                    GameFunctions.WriteLine("\nThe wall is open.");
                else
                    GameFunctions.WriteLine("\nThe door is open.");
                IsDoorOpen = true;
                GameFunctions.ReduceTorchFire();
            }
        }

        public override void Close()
        {
            if (!_canDoorBeClosed) // check if door can not be opened
                base.Close();
            else // if door is open, closed, or locked, then we "close" it anyway
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                if (ObjectName == "Far Wall with a door-shaped outline" || ObjectName == "Doorway back to Epor room")
                    GameFunctions.WriteLine("\nThe wall is closed.");
                else if (ObjectName == "Right Bookcase")
                    GameFunctions.WriteLine("The bookcase is closed.");
                else
                    GameFunctions.WriteLine("\nThe door is closed.");
                GameFunctions.ReduceTorchFire();
                IsDoorOpen = false;
            }
        }

        public void Unlock(Key key)
        {
            if (key.ObjectName == _keyName)
            {
                if (_isDoorLocked) // if key name matches with key associated with door AND door is locked, unlock and open it
                {
                    _isDoorLocked = false;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    GameFunctions.WriteLine("\nThe door is unlocked.");
                    key.CanBeDiscarded = true;
                }
                Open();
            }
            else // if the key name does not match key associated with door, alert player
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                GameFunctions.WriteLine("\nThe key doesn't fit into the keyhole!");
            }
        }

        public void Lock(Key key)
        {
            if (key.ObjectName == _keyName) // if key name matches with key assocated with door AND the door is locked, lock it and close it
            {
                if (!_isDoorLocked)
                {
                    _isDoorLocked = true;
                    Console.ForegroundColor = ConsoleColor.White;
                    GameFunctions.WriteLine("\nYou use the key to lock the door."); // altered message. original: "The key is closed."
                    key.CanBeDiscarded = false;
                }
                Close();
            }
            else // if the key name does not match key assocated with door, alert player
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                GameFunctions.WriteLine("\nThe key doesn't fit into the keyhole!");
            }
        }
    }
}
