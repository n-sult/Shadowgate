using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Shadowgate.Rooms
{
    public class FireBridge : Room
    {
        public FireBridge()
        {
            // All POI for Fire Bridge
            PointOfInterest fireOrOil = new PointOfInterest("Fire raging under the bridge"); // Name is set here. Toggled upon using the sphere
            PointOfInterest fireBridge = new PointOfInterest("Bridge");
            Entry doorOnTheOtherSideOfTheBridge = new Entry("Door on the other side of the bridge", true, false, false, false, "Troll Bridge");
            Entry doorFromFireBridgeToMirrorRoom = new Entry("Door to Mirror Room", true, true, false, false, "Mirror Room");
            var fireBridgePOI = new List<PointOfInterest>() { fireOrOil, fireBridge, doorOnTheOtherSideOfTheBridge, doorFromFireBridgeToMirrorRoom };

            RoomName = "Fire Bridge";
            FirstEntry = "This room is incredibly hot! This must be what the lower levels of Hell are like.";
            SubsequentEntry = "It's so hot, you begin to sweat profusely.";
            PointsOfInterest = fireBridgePOI;

            GameFunctions.RoomEnteredEvent += (roomName) => 
            { 
                if (roomName == RoomName) 
                { 
                    if (!Globals.currentPlayer.IsCloakEquipped)
                    {
                        HeatIsUnbearableMessage();
                        GameFunctions.MoveRooms("Mirror Room"); //TODO: CHANGE BACK TO GLOBALS.PREVIOUSROOM.ROOMNAME
                    } 
                } 
            } ;
        }

        void HeatIsUnbearableMessage()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nThe heat is unbearable! Returning to the previous room...");
            Thread.Sleep(7000);
        }
        
        public static void DieToFiredrake()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nThe Firedrake screams triumphantly and gives you an eternal sunburn.");
            GameFunctions.GameOver();
        }
        
        public override void MoveTo(string objectName)
        {
            switch(objectName)
            {
                case "Door on the other side of the bridge":
                    if (PointsOfInterest.Contains(GameFunctions.FindObject(objectName, PointsOfInterest)))
                        OpenObject(objectName);
                    else
                        base.MoveTo(objectName);
                    break;
                case "Firedrake":
                    DieToFiredrake();
                    break;
                case "Fire raging under the bridge":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nBellowing like a fool, you leap off the bridge and into the blaze! You are instantly fried.");
                    GameFunctions.GameOver();
                    break;
                case "Oil-soaked floor beneath the bridge":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nSuicide obviously does not solve problems.");
                    GameFunctions.GameOver();
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
                case "Bridge":
                    Console.WriteLine("\nIt's a long, thin bridge forged of fine metal.");
                    break;
                case "Fire raging under the bridge":
                    Console.WriteLine("\nThis tireless fire burns with such heat that this room seems to be in the belly of Hell itself.");
                    break;
                case "Oil-soaked floor beneath the bridge":
                    Console.WriteLine("\nYour view of the floor is totally obscured by a thick, black oil.");
                    break;
                case "Firedrake":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nA flaming horror appears at the end of the bridge!");
                    break;
                default:
                    base.LookAt(objectName);
                    break;
            }
        }

        public override void OpenObject(string objectName)
        {
            switch(objectName)
            {
                case "Door on the other side of the bridge":
                    if (PointsOfInterest.Contains(GameFunctions.FindObject(objectName, PointsOfInterest)))
                    {
                        base.OpenObject(objectName);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\nSuddenly, you feel a gust of wind! A searing blast of heat knocks you across the room! A firedrake has emerged from the door!");
                        GameFunctions.FindObject(objectName, PointsOfInterest).ObjectName = "Firedrake";
                        GameFunctions.FindObject("Firedrake", PointsOfInterest).IsHidden = false;
                        GameFunctions.ReduceTorchFire();
                    }
                    else
                        base.OpenObject(objectName);
                    break;
                case "Firedrake":
                    DieToFiredrake();
                    break;
                default:
                    base.OpenObject(objectName);
                    break;
            }
        }

        public override void CloseObject(string objectName)
        {
            switch(objectName)
            {
                case "Firedrake":
                    DieToFiredrake();
                    break;
                default:
                    base.CloseObject(objectName);
                    break;
            }
        }

        public override void HitObject(string objectName)
        {
            switch(objectName)
            {
                case "Fire raging under the bridge":
                case "Oil-soaked floor beneath the bridge":
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nYou now have terrific second-degree burns on your hands.");
                    break;
                case "Firedrake":
                    DieToFiredrake();
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
                case "Firedrake":
                    GameFunctions.FindObject(objectName, Globals.clonedRoom.PointsOfInterest).DoesNotUnderstandMessage();
                    break;
                default:
                    base.SpeakTo(objectName);
                    break;
            }
        }
    }
}
