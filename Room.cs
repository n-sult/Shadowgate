using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate
{
    public class Room
    {
        public string RoomName;
        public string FirstEntry;
        public string SubsequentEntry;
        public List<PointOfInterest> PointsOfInterest;
        public bool Visited = false;
        string _ouchMessage = "Ouch! That smarts!";

        public Room(string roomName, string firstEntry, string subsequentEntry, List<PointOfInterest> pointsOfInterest)
        {
            RoomName = roomName;
            FirstEntry = firstEntry;
            SubsequentEntry = subsequentEntry;
            PointsOfInterest = pointsOfInterest;

            //foreach (PointOfInterest pointOfInterest in PointsOfInterest) TODO: may be able to delete
            //{
            //    pointOfInterest.RoomReference = this;
            //}
        }

        public Room() // remove?
        {

        }

        public string GetRoomName()
        {
            return RoomName;
        }

        public PointOfInterest GetPOI(string objectName)
        {
            return GameFunctions.FindObject(objectName, PointsOfInterest);
        }

        public virtual void LookAt(string objectName)
        {
            if (GameFunctions.FindObject(objectName, PointsOfInterest) is not null)
                GameFunctions.FindObject(objectName, PointsOfInterest).Look();
            else
                GameFunctions.FindObject(objectName, null, Globals.currentPlayer.PlayerInventory).Look();
        }
        
        public virtual void MoveTo(string objectName)
        {
            GameFunctions.FindObject(objectName, PointsOfInterest).Move();
        }

        public virtual void OpenObject(string objectName)
        {
            List<PointOfInterest> roomAndBagStuff = new List<PointOfInterest>();

            foreach (PointOfInterest POI in PointsOfInterest)
                roomAndBagStuff.Add(POI);
            foreach (Item item in Globals.currentPlayer.PlayerInventory)
                roomAndBagStuff.Add(item);

            GameFunctions.FindObject(objectName, roomAndBagStuff).Open();
        }

        public virtual void CloseObject(string objectName)
        {
            GameFunctions.FindObject(objectName, PointsOfInterest).Close();
        }

        public virtual bool TakeObject(string objectName)
        {
            var activeObject = GameFunctions.FindObject(objectName, PointsOfInterest);
            if (activeObject.Take() == true)
            {
                if (activeObject is Torch) // check if the item is a torch, if so...
                {
                    if (!Globals.containsTorch)
                        Globals.currentPlayer.PlayerInventory.Add((Item)activeObject); // if the global bool is false, add a spot for torches
                    activeObject.ObjectName = "Torch";
                    Globals.containsTorch = true;
                    Globals.torchCount++; // increase torch count
                }
                else                                                    // if it's not a torch...
                    Globals.currentPlayer.PlayerInventory.Add((Item)activeObject);  // add non-torch items to inventory

                Globals.currentRoom.PointsOfInterest.Remove(activeObject);   // remove item from POI
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nThe {activeObject.ObjectName} is in hand."); // message for taking item

                GameFunctions.ReduceTorchFire();

                return true;
            }
            else
                return false;
        }

        public virtual void LeaveObject(string objectName)
        {
            GameFunctions.FindObject(objectName, PointsOfInterest).Leave();
        }

        public virtual void HitObject(string objectName)
        {
            GameFunctions.FindObject(objectName, PointsOfInterest).Hit();
        }

        public virtual void UseObject(string objectName)
        {
            GameFunctions.FindObject(objectName, PointsOfInterest).Use();
        }

        public virtual void SpeakTo(string objectName)
        {
            GameFunctions.FindObject(objectName, PointsOfInterest).Speak();
        }

        public virtual void ThatSmartsMessage()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(_ouchMessage);
        }
    }
}
