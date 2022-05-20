using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.BrazierRoom
{
    public class Horn : Item
    {
        public Horn(string objectName)
        {
            ObjectName = objectName;
        }

        public override void Look()
        {
            Console.WriteLine("\nThe horn is forged of flawless platinum. Its beauty is unbelievable!");
        }

        public override bool Take()
        {
            if (Globals.clonedRoom.PointsOfInterest.Contains(GameFunctions.FindObject("Hellhound", Globals.clonedRoom.PointsOfInterest)))
                if (GameFunctions.FindObject("Hellhound", Globals.clonedRoom.PointsOfInterest).IsHidden == false)
                {
                    Rooms.BrazierRoom.HoundAppears();
                    return false;
                }
                else
                {
                    Rooms.BrazierRoom.DieToHound();
                    return false;
                }
            else
                return true;
        }

        public override void Use()
        {
            if (Globals.clonedRoom.RoomName == "Vault" && (Globals.clonedRoom as Rooms.Vault).TalismanUsed && !(Globals.clonedRoom as Rooms.Vault).HornUsed)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\nThe sound of the horn echoes loudly in your ears. \nSuddenly, you hear the sound of grinding rock, " +
                    "the jaw of the skull begins to descend. \nHot wind erupts from the mouth creating the illusion that the stone skull is alive!");
                (GameFunctions.FindObject("Skull Door", Globals.clonedRoom.PointsOfInterest) as Entry).IsDoorOpen = true;
                (Globals.clonedRoom as Rooms.Vault).HornUsed = true;
                GameFunctions.ReduceTorchFire();
            }
            else
                base.Use();
        }
    }
}
