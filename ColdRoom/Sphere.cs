using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.ColdRoom
{
    public class Sphere : Item
    {
        public Sphere(string objectName, bool isHidden)
        {
            ObjectName = objectName;
            IsHidden = isHidden;
        }

        public override void Look()
        {
            Console.WriteLine("\nThis crystal sphere is as cold as ice.");
        }

        public override bool Take()
        {
            if (Globals.currentRoom.RoomName == "Cold Room")
                return true;
            else if (Globals.currentRoom.RoomName == "Shark Pond" && Rooms.SharkPond.UsedTorchOnPond)
            {
                Rooms.SharkPond.IsSphereInPond = false;
                return true;
            }
            else
                base.Take();
                return false;
        }

        public override void Use()
        {
            var result = GameFunctions.UseOn(ObjectName);

            if (result is not null)
            {
                var activeObject = GameFunctions.FindObject("Sphere", null, Globals.currentPlayer.PlayerInventory);

                switch (result)
                {
                    case "Pond":
                        if (!Rooms.SharkPond.WasSphereUsed) // if the sphere hasn't been used yet...
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan; //show message of lake freezing
                            Console.WriteLine("\nYou drop the sphere into the lake and notice the ripples disappear as the water turns into ice.");
                            Rooms.SharkPond.WasSphereUsed = true; 
                            Rooms.SharkPond.IsSphereInPond = true; // mark these as true for later use

                            Globals.currentRoom.PointsOfInterest.Insert(0, activeObject); // take the sphere from you inventory and add it to room POI list
                            Globals.currentPlayer.PlayerInventory.Remove((Item)activeObject); // remove sphere from inventory
                            GameFunctions.ReduceTorchFire();
                        }
                        else // otherwise, sphere can't be used again in the lake
                            base.Use();
                        break;
                    case "Firedrake":
                    case "Fire raging under the bridge":
                        Console.ForegroundColor = ConsoleColor.Cyan; // if the sphere is used on the firedrake or the fire underneath the bridge...
                        Console.WriteLine("\nYou hurl the sphere into the fire below you. The hell-spawned flames quickly vanish as soon as the sphere touches them."); // show message of sphere expelling flames
                        if (Rooms.FireBridge.FiredrakeAppeared) // if the firedrake appeared...
                        {
                            Console.WriteLine("With nothing to feed itself on, the Firedrake immediately follows suit."); // also show message of it dying
                            Rooms.FireBridge.FiredrakeAppeared = false; // mark firedrake as no longer present
                            GameFunctions.FindObject("Firedrake", Globals.currentRoom.PointsOfInterest).ObjectName = "Door on the other side of the bridge"; // change entry name back to it's default name
                        }
                        Rooms.FireBridge.FiredrakeDead = true; // mark firedrake as dead

                        GameFunctions.FindObject("Fire raging under the bridge", Globals.currentRoom.PointsOfInterest).ObjectName = "Oil-soaked floor beneath the bridge"; // change fire to oil
                        Globals.currentPlayer.PlayerInventory.Remove((Item)activeObject); // permanently remove sphere from inventory
                        GameFunctions.ReduceTorchFire();
                        break;
                    default:
                        base.Use();
                        break;
                }
            }
        }
    }
}
