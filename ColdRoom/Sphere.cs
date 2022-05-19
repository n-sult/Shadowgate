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
            if (Globals.clonedRoom.RoomName == "Cold Room")
                return true;
            else if (Globals.clonedRoom.RoomName == "Shark Pond" && (Globals.clonedRoom as Rooms.SharkPond).UsedTorchOnPond)
                return true;
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
                        if (!(Globals.clonedRoom as Rooms.SharkPond).WasSphereUsed) // if the sphere hasn't been used yet...
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan; //show message of lake freezing
                            Console.WriteLine("\nYou drop the sphere into the lake and notice the ripples disappear as the water turns into ice.");
                            (Globals.clonedRoom as Rooms.SharkPond).WasSphereUsed = true; // mark this as true for later use

                            Globals.clonedRoom.PointsOfInterest.Insert(0, activeObject); // take the sphere from you inventory and add it to room POI list
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

                        PointOfInterest firedrake = (GameFunctions.FindObject("Firedrake", Globals.clonedRoom.PointsOfInterest));
                        
                        if (firedrake.IsHidden == false) // if the firedrake appeared...
                        {
                            Console.WriteLine("With nothing to feed itself on, the Firedrake immediately follows suit."); // also show message of it dying
                            Globals.clonedRoom.PointsOfInterest.Remove(firedrake); // remove firedrake from POI list
                            GameFunctions.FindObject("Firedrake", Globals.clonedRoom.PointsOfInterest).ObjectName = "Door on the other side of the bridge"; // change entry name back to it's default name
                        }

                        GameFunctions.FindObject("Fire raging under the bridge", Globals.clonedRoom.PointsOfInterest).ObjectName = "Oil-soaked floor beneath the bridge"; // change fire to oil
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
