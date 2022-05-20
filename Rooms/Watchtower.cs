using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Rooms
{
    public class Watchtower : Room
    {
        public bool WomanDead;
        
        public Watchtower()
        {
            // Items for Watchtower
            Shadowgate.Watchtower.Blade blade = new Shadowgate.Watchtower.Blade("Blade");

            // POI for Watchtower
            PointOfInterest captiveWoman = new PointOfInterest("Captive Woman");
            PointOfInterest chain = new PointOfInterest("Chain");
            OutsideView towerWindow = new OutsideView("Window");
            Entry ladderFromWatchtowerToObservatory = new Entry("Ladder to Observatory", false, true, false, false, "Observatory");
            var watchtowerPOI = new List<PointOfInterest>() { captiveWoman, chain, blade, towerWindow, ladderFromWatchtowerToObservatory };

            RoomName = "Watchtower";
            FirstEntry = "As you enter, you see a woman being held captive on the floor! You are so captivated by the woman's beauty that you momentarily " +
                "forget her predicament. \n\nYes, in the moonlight she is even more beautiful.";
            SubsequentEntry = "This small, plain room is lit only by the light of the moon itself.";
            PointsOfInterest = watchtowerPOI;
        }

        public void DieToWerewolf()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nThe woman suddenly transforms into a werewolf! \nWith a loud roar, the wolf pounces on you, taking your life!");
            GameFunctions.GameOver();
        }

        public override void LookAt(string objectName)
        {
            switch(objectName)
            {
                case "Captive Woman":
                    Console.WriteLine("\nThe fine lass lies upon the floor, chained to the wall. She is extremely beautiful.");
                    break;
                case "Dead Werewolf":
                    Console.WriteLine("\nThis looks like your typical dead werewolf. Your arrow is deeply lodged in it's body.");
                    break;
                case "Chain":
                    if (WomanDead)
                        Console.WriteLine("\nIt must have taken super strength to have ripped the chain apart!");
                    else
                        Console.WriteLine("\nThis silver chain seems to be strongly secured to the wall.");
                    break;
                default:
                    base.LookAt(objectName);
                    break;
            }
        }

        public override void HitObject(string objectName)
        {
            switch(objectName)
            {
                case "Captive Woman":
                    if (!WomanDead)
                        DieToWerewolf();
                    break;
                case "Chain":
                    GameFunctions.FindObject(objectName, PointsOfInterest).ThatSmartsMessage();
                    break;
                default:
                    base.HitObject(objectName);
                    break;
            }
        }

        public override void SpeakTo(string objectName)
        {
            switch(objectName)
            {
                case "Captive Woman":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nShe doesn't seem to understand what you say.");
                    GameFunctions.ReduceTorchFire();
                    break;
                default:
                    base.SpeakTo(objectName);
                    break;
            }
        }
    }
}
