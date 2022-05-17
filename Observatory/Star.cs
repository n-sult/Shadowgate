using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Observatory
{
    public class Star : Item
    {
        public Star(string objectName)
        {
            ObjectName = objectName;
        }

        public override void Look()
        {
            Console.WriteLine("\nIt's an ornate carving of a shooting star. The object is made of silver and is very heavy.");
        }

        public override bool Take()
        {
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
                    case "Wyvern":
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\nThe Star becomes a flash of light as you launch it. \nCrash! It strikes the wyvern and it explodes " +
                            "into a million pieces! \nThe wyvern falls out of sight.");
                        Globals.currentRoom.PointsOfInterest.Remove(GameFunctions.FindObject(result, Globals.currentRoom.PointsOfInterest));
                        Globals.currentPlayer.PlayerInventory.Remove(this);
                        Rooms.Turret.WyvernDead = true;
                        GameFunctions.ReduceTorchFire();
                        break;
                    default:
                        base.Use();
                        break;
                }
            }
        }

        public override void Leave()
        {
            DoNotDoThatMessage();
        }
    }
}
