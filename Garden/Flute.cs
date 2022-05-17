using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Garden
{
    public class Flute : Item
    {
        public Flute(string objectName)
        {
            ObjectName = objectName;
        }

        public override void Look()
        {
            Console.WriteLine("\nIt's a small, wooden flute. It looks like it could make wonderful music.");
        }

        public override bool Take()
        {
            if (!Globals.currentPlayer.IsGauntletEquipped)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nAs you reach for the flute, you touch the water and pain explodes through your hand! \nThe water is extremely " +
                    "acidic and obviously not good for drinking.");
                GameFunctions.ReduceTorchFire();
                return false;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\nBy using the silver gauntlet, you remove the flute easily. \nThe sound of the water splashing is music to your ears.");
                this.ObjectName = "Flute";
                return true;
            }
        }

        public override void Use()
        {
            if (Globals.currentRoom.RoomName != "Garden" || Globals.currentRoom.RoomName == "Garden" && Rooms.Garden.FluteUsed)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\nThe flute's music could possibly lead you to an endless dream.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\nThe sound of the flute is very pretty, indeed. \nIt seems like you wake from a dream only to find " +
                    "a hole in the tree! \nIs it real? The flute's music is like magic.");
                Rooms.Garden.FluteUsed = true;
                GameFunctions.FindObject("Tree Hole", Globals.currentRoom.PointsOfInterest).IsHidden = false;
                GameFunctions.FindObject("Ring", Globals.currentRoom.PointsOfInterest).IsHidden = false;
            }
            GameFunctions.ReduceTorchFire();
        }
    }
}
