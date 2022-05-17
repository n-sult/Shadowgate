using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate
{
    public class Key : Item
    {
        public Key(string objectName, bool isHidden, bool canBeDiscarded)
        {
            ObjectName = objectName;
            IsHidden = isHidden;
            CanBeDiscarded = canBeDiscarded;
        }

        
        
        void CannotFindKeyholeMessage()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nYou can't seem to find a keyhole.");
        }
        
        public override void Look()
        {
            Console.ForegroundColor = ConsoleColor.White;
            if (ObjectName == "Key 1" || ObjectName == "Key 5")
                Console.WriteLine("\nIt's a small iron key.");
            else if (ObjectName == "Key 2")
                Console.WriteLine("\nThis key bears a skull. This must be a skeleton key.");
            else if (ObjectName == "Key 3")
                Console.WriteLine("\nIt's a small brass key.");
            else if (ObjectName == "Key 4")
                Console.WriteLine("\nThis rusty key doesn't seem to have been used for a long time.");
            else // for key 6
                Console.WriteLine("\nIt's a jet black skeleton key.");
        }

        public override bool Take()
        {
            if (ObjectName == "Key 3")
            {
                if (!Rooms.SharkPond.WasSphereUsed)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nYou can't reach it from here. Swimming the shark-infested pool would be suicidal!");
                    return false;
                }
                else
                {
                    Rooms.SharkPond.IsKeyTaken = true;
                    return true;
                }
            }
            else
                return true;
        }

        public override void Use()
        {
            List<PointOfInterest> list = GameFunctions.PutPOIsItemsAndSelfInOneList(); 

            var result = GameFunctions.UseOn(ObjectName); // run UseOn to get name of object you are using key on
            var activeObject = GameFunctions.FindObject(result, list);
            if (result is not null)
            {
                if (activeObject is not Entry)
                    CannotFindKeyholeMessage();
                else // if the object is an entry...
                {
                    if ((activeObject as Entry).IsDoorOpen) // if entry is open, run lock command
                        (activeObject as Entry).Lock(this);
                    else if (!(activeObject as Entry).IsDoorOpen) // if entry is closed, run unlock command
                        (activeObject as Entry).Unlock(this);
                }
            }
        }

        public override void Leave()
        {
            if (CanBeDiscarded)
                LeaveInFountainMessage(ObjectName);
            else
                DoNotDoThatMessage();
        }
    }
}
