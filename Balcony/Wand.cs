using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Balcony
{
    public class Wand : Item
    {
        public Wand(string objectName, bool isHidden)
        {
            ObjectName = objectName;
            IsHidden = isHidden;
        }

        public override void Look()
        {
            Console.WriteLine("\nIt's a wand of sorts. Carved on the side of the wand is a small picture of a serpent.");
        }

        public override bool Take()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nAs you take the wand from the skeletal hand, it begins to descend. \nThe hole then closes up as if it had never been.");
            GameFunctions.FindObject("Skeletal Hand", Globals.currentRoom.PointsOfInterest).IsHidden = true;
            return true;
        }

        public override void Use()
        {
            var result = GameFunctions.UseOn(ObjectName);
             
            if (result is not null)
            {
                switch (result)
                {
                    case "Sphinx":
                        Rooms.SphinxChamber.UseItemOnSphinx(ObjectName);
                        break;
                    case "Giant Snake":
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\nThe snake begins to shake and shutter. Is it just your eyes or is it shrinking? \n" +
                            "The serpentine statue begins to change! It grows smaller and smaller! " +
                            "\nIt dematerializes and forms anew as a staff of tremendous beauty!");
                        Globals.currentRoom.PointsOfInterest.Remove(GameFunctions.FindObject(result, Globals.currentRoom.PointsOfInterest));
                        GameFunctions.FindObject("Staff", Globals.currentRoom.PointsOfInterest).IsHidden = false;
                        Globals.currentPlayer.PlayerInventory.Remove(this);
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
