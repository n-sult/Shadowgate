using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Rooms
{
    public class EporRoom : Room
    {
        public static bool IsEporActive;
        public static int LookedAtSign = 0;

        public EporRoom()
        {
            // Items for Epor room
            Bottle bottle1 = new Bottle("Bottle 1");
            Bottle eporBottle2 = new Bottle("Bottle 2");
            Scroll scroll1 = new Scroll("Scroll 1", false);

            // All POI for Epor room
            PointOfInterest cage1 = new PointOfInterest("Closer Cage");
            PointOfInterest cage2 = new PointOfInterest("Farther Cage");
            PointOfInterest rope = new PointOfInterest("Rope");
            PointOfInterest eporSign = new PointOfInterest("Sign");
            Torch eporTorch = new Torch("Torch on the left wall");
            Entry holeInTheCeiling = new Entry("Hole in the ceiling", false, true, false, false, "Mirror Room");
            Entry eporFarDoor = new Entry("Far Wall with a door-shaped outline", true, false, false, false, "Limestone Cave");
            Entry doorFromEporRoomToWraithRoom = new Entry("Door back to Wraith Room", true, true, false, false, "Wraith Room");
            var eporRoomPOI = new List<PointOfInterest>() { cage1, cage2, rope, eporSign, bottle1, eporBottle2, scroll1, 
                eporTorch, holeInTheCeiling, eporFarDoor, doorFromEporRoomToWraithRoom };

            RoomName = "Epor Room";
            FirstEntry = "This small stone chamber is lined on one side by two barred portals.";
            SubsequentEntry = "You're inside a small room.";
            PointsOfInterest = eporRoomPOI;

            GameFunctions.RoomEnteredEvent += (roomName) => // if 3 bottle2s have been used, replenish the bottle2 items in the room
            {
                if (Globals.NumberOfBottle2Consumed > 1 && Globals.NumberOfBottle2Consumed % 3 == 0)
                    if (!PointsOfInterest.Contains(eporBottle2))
                        PointsOfInterest.Insert(4, eporBottle2);
            };
        }

        public override void MoveTo(string objectName)
        {
            switch (objectName)
            {
                case "Hole in the ceiling":
                    if (!IsEporActive)
                    {
                        GameFunctions.FindObject(objectName, this.PointsOfInterest).CannotReachMessage();
                        GameFunctions.ReduceTorchFire();
                    }
                    else
                        base.MoveTo(objectName);
                    break;
                default:
                    base.MoveTo(objectName);
                    break;
            }
        }

        public override void LookAt(string objectName)
        {
            switch(objectName)
            {
                case "Closer Cage":
                case "Farther Cage":
                    Console.WriteLine("\nSturdy bars seal this cage. Your nose detects the presence of a concealed animal.");
                    break;
                case "Sign":
                    if (LookedAtSign == 0)
                    {
                        Console.WriteLine("\nThis sign reads \"Epor\"");
                        LookedAtSign++;
                    }
                    else if (LookedAtSign == 1)
                    {
                        Console.WriteLine("\nEpor, Epor, Epor... You got it! It seems to be some sort of magic word!");
                        Globals.currentPlayer.Spellbook.Add(new Spell("Epor"));
                        Console.WriteLine("You've learned one magic spell."); // added line
                        LookedAtSign++;
                    }
                    else
                        Console.WriteLine("\nIt's a strange sounding word, indeed!");
                    break;
                case "Rope":
                    if (!IsEporActive)
                        Console.WriteLine("\nIt's a hemp of rope.");
                    else
                        Console.WriteLine("\nThe rope is stretched toward the ceiling and resists all efforts to move it.");
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
                case "Closer Cage":
                case "Farther Cage":
                    GameFunctions.FindObject(objectName, PointsOfInterest).ThumpMessage();
                    break;
                case "Sign":
                    base.ThatSmartsMessage();
                    break;
                default:
                    base.LookAt(objectName);
                    break;
            }
        }
    }
}
