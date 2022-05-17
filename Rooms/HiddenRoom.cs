using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Rooms
{
    public class HiddenRoom : Room
    {
        public static bool IsLeverPulled;
        public static bool DoorOnLedgeAccessed;
        
        public HiddenRoom()
        {
            // Items for Hidden Room
            Shadowgate.HiddenRoom.Arrow arrow = new Shadowgate.HiddenRoom.Arrow("Arrow");

            // All POI for Hidden Room
            PointOfInterest hiddenRoomLedge = new PointOfInterest("Ledge");
            PointOfInterest supports = new PointOfInterest("Pair of supports under the ledge");
            PointOfInterest hiddenRoomRubble = new PointOfInterest("Rubble", true);
            PointOfInterest fakeTorch1 = new PointOfInterest("Left Torch");
            PointOfInterest fakeTorch2 = new PointOfInterest("Right Torch");
            Entry hiddenStairway = new Entry("Hidden Stairway", false, false, false, true, "Bridge Room");
            PointOfInterest doorwayOnALedge = new PointOfInterest("Doorway on an elevated ledge");
            Entry openingFromHiddenRoomToCrampedHallway = new Entry("Opening to Cramped Hallway", false, true, false, false, "Cramped Hallway");
            var hiddenRoomPOI = new List<PointOfInterest>() { hiddenRoomLedge, supports, arrow, hiddenRoomRubble, fakeTorch1, fakeTorch2, 
                hiddenStairway, doorwayOnALedge, openingFromHiddenRoomToCrampedHallway };

            RoomName = "Hidden Room";
            FirstEntry = "As you enter the room, you see an arrow on the front wall.";
            SubsequentEntry = "Cold air rushes into this chamber from an opening some ten feet above the floor.";
            PointsOfInterest = hiddenRoomPOI;
        }

        public override void MoveTo(string objectName)
        {
            switch(objectName)
            {
                case "Doorway on an elevated ledge":
                    var theLedge = GameFunctions.FindObject(objectName, PointsOfInterest);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    if (!DoorOnLedgeAccessed) // if player hasn't tried to access this door yet...
                    {
                        Console.WriteLine("\nThe ledge wasn't strong enough to hold you. You fall to the ground and land hard on your rump."); // alert player they fell
                        DoorOnLedgeAccessed = true;
                        theLedge.IsHidden = false; // make rubble POI visible
                        PointsOfInterest.Remove(theLedge); // remove ledge from POI list
                        GameFunctions.ReduceTorchFire();
                    }
                    else // otherwise, alert player they can't reach
                        theLedge.CannotReachMessage();
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
                case "Left Torch":
                    Console.WriteLine("\nThis torch seems to be fastened to the wall with rather modern-looking nails.");
                    break;
                case "Right Torch":
                    Console.WriteLine("\nThis torch is attached securely to the wall.");
                    break;
                case "Ledge":
                    Console.WriteLine("\nA slab of concrete rests upon two stone supports, some ten feet from the floor.");
                    break;
                case "Rubble":
                    Console.WriteLine("\nIt's rubble from the broken ledge.");
                    break;
                case "Pair of supports under the ledge":
                    Console.WriteLine("\nIt's part of the wall.");
                    break;
                default:
                    base.LookAt(objectName);
                    break;
            }
        }

        public override void UseObject(string objectName)
        {
            switch(objectName)
            {
                case "Left Torch":
                    var theHiddenStairway = GameFunctions.FindObject("Hidden Stairway", PointsOfInterest);
                    if (!IsLeverPulled)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\nYou moved the torch. On the left wall, a hidden door opens. There is a spiral staircase leading down.");
                        theHiddenStairway.IsHidden = false;
                        (theHiddenStairway as Entry).IsDoorOpen = true;
                        IsLeverPulled = true;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\nYou moved the torch. The hidden door on the left wall is closed.");
                        theHiddenStairway.IsHidden = true;
                        (theHiddenStairway as Entry).IsDoorOpen = false;
                        IsLeverPulled = false;
                    }
                    GameFunctions.ReduceTorchFire();
                    break;
                default:
                    base.UseObject(objectName);
                    break;
            }
        }

        public override void HitObject(string objectName)
        {
            switch(objectName)
            {
                case "Ledge":
                case "Rubble":
                case "Left Torch":
                case "Right Torch":
                    ThatSmartsMessage();
                    break;
            }
        }
    }
}
