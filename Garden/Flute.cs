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
            GameFunctions.WriteLine("\nIt's a small, wooden flute. It looks like it could make wonderful music.");
        }

        public override bool Take()
        {
            if (!Globals.currentPlayer.IsGauntletEquipped)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                GameFunctions.WriteLine("\nAs you reach for the flute, you touch the water and pain explodes through your hand! \nThe water is extremely " +
                    "acidic and obviously not good for drinking.");
                GameFunctions.ReduceTorchFire();
                return false;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                GameFunctions.WriteLine("\nBy using the silver gauntlet, you remove the flute easily. \nThe sound of the water splashing is music to your ears.");
                ObjectName = "Flute";
                return true;
            }
        }

        public override void Use()
        {
            if (Globals.clonedRoom.RoomName != "Garden" || Globals.clonedRoom.RoomName == "Garden" && (Globals.clonedRoom as Rooms.Garden).FluteUsed)
            {
                Console.ForegroundColor = ConsoleColor.White;
                GameFunctions.WriteLine("\nThe flute's music could possibly lead you to an endless dream.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                GameFunctions.WriteLine("\nThe sound of the flute is very pretty, indeed. \nIt seems like you wake from a dream only to find " +
                    "a hole in the tree! \nIs it real? The flute's music is like magic.");
                (Globals.clonedRoom as Rooms.Garden).FluteUsed = true;
                GameFunctions.FindObject("Tree Hole", Globals.clonedRoom.PointsOfInterest).IsHidden = false;
                GameFunctions.FindObject("Ring", Globals.clonedRoom.PointsOfInterest).IsHidden = false;
            }
            GameFunctions.ReduceTorchFire();
        }
    }
}
