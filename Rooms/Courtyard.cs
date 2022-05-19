using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Rooms
{
    public class Courtyard : Room
    {
        public bool CyclopsUnconcious;
        public bool CyclopsDead;
        public bool BucketRaised;

        public Courtyard()
        {
            // Items for Courtyard
            Shadowgate.Courtyard.Gauntlet gauntlet = new Shadowgate.Courtyard.Gauntlet("Gauntlet", true);

            // All POI for Courtyard
            PointOfInterest courtyardWell = new PointOfInterest("Well");
            PointOfInterest wellRope = new PointOfInterest("Well Rope");
            PointOfInterest crank = new PointOfInterest("Well Crank");
            PointOfInterest bucket = new PointOfInterest("Well Bucket", true);
            PointOfInterest cyclops = new PointOfInterest("Cyclops");
            Entry doorBehindCyclops = new Entry("Door behind the cyclops", true, false, false, false, "Drafty Hallway");
            Entry doorFromCourtyardToTrollBridge = new Entry("Doorway to Troll Bridge", false, true, false, false, "Troll Bridge");
            var courtyardPOI = new List<PointOfInterest>() { courtyardWell, wellRope, crank, bucket, gauntlet, cyclops, doorBehindCyclops, doorFromCourtyardToTrollBridge };

            RoomName = "Courtyard";
            FirstEntry = "The moon casts a brilliant shadow over the grounds of the courtyard.";
            SubsequentEntry = "The castle Shadowgate looms before you.";
            PointsOfInterest = courtyardPOI;

            GameFunctions.RoomEnteredEvent += (roomName) => { if (!CyclopsDead) CyclopsUnconcious = false; };
        }

        public static void DieToCyclops()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nA battle cry dies in your throat, as the cyclops curshes your skull with his club.");
            GameFunctions.GameOver();
        }

        public override void MoveTo(string objectName)
        {
            var activeObject = GameFunctions.FindObject(objectName, PointsOfInterest);
            switch(objectName)
            {
                case "Well":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nWith a mighty leap, you jump head first into the well. On the way down, you see no water below. " +
                        "\nThe well was deeper than you imagined. You have just broken every bone in your body.");
                    GameFunctions.GameOver();
                    break;
                case "Door behind the cyclops":
                    if (!CyclopsUnconcious &&!CyclopsDead)
                        activeObject.AfraidToGetNearMessage();
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
                case "Well":
                    Console.WriteLine("\nIt's a finely crafted well, made of stone and mortar.");
                    break;
                case "Well Rope":
                    Console.WriteLine("\nThe teeth marks of water rats are evident on this rope.");
                    break;
                case "Well Crank":
                    Console.WriteLine("\nIt's a finely crafted well, made of stone and mortar.");
                    break;
                case "Well Bucket":
                    Console.WriteLine("\nThis small bucket is used to fetch water from the depths.");
                    break;
                case "Cyclops":
                    if (!CyclopsUnconcious)
                        Console.WriteLine("\nThe Cyclops stands before you, ready for battle!");
                    else if (CyclopsUnconcious && !CyclopsDead)
                        Console.WriteLine("\nYou can almost see the stars revolving around the cyclops' head. He is down, but not out.");
                    else if (CyclopsDead)
                        Console.WriteLine("\nIt's a dead cyclops. What do you expect after stabbing him with your sword?");
                    break;
                default:
                    base.LookAt(objectName);
                    break;
            }
        }

        public override void OpenObject(string objectName)
        {
            switch (objectName)
            {
                case "Well Bucket":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\nThe Bucket is open.");
                    if (PointsOfInterest.Contains(GameFunctions.FindObject("Gauntlet", PointsOfInterest)))
                    {
                        Console.WriteLine("There's a pair of Gauntlets inside.");
                        GameFunctions.FindObject("Gauntlet", PointsOfInterest).IsHidden = false;
                    }
                    GameFunctions.ReduceTorchFire();
                    break;
                case "Door behind the cyclops":
                    if (!CyclopsUnconcious)
                        GameFunctions.FindObject(objectName, PointsOfInterest).AfraidToGetNearMessage();
                    else
                        base.OpenObject(objectName);
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
                case "Well Bucket":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\nThe Bucket is closed.");
                    if (PointsOfInterest.Contains(GameFunctions.FindObject("Gauntlet", PointsOfInterest)))
                        GameFunctions.FindObject("Gauntlet", PointsOfInterest).IsHidden = true;
                    GameFunctions.ReduceTorchFire();
                    break;
                default:
                    base.CloseObject(objectName);
                    break;
            }
        }

        public override void UseObject(string objectName)
        {
            switch(objectName)
            {
                case "Well Rope":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nThe rope is loose. You reach out for it but as you do, you slip and fall down the well! " +
                        "\nThe well was deeper than you imagined. You have just broken every bone in your body.");
                    GameFunctions.GameOver();
                    break;
                case "Well Crank":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    if (!BucketRaised)
                    {
                        Console.WriteLine("\nThe crank turns rather easily. At the end of the rope there is a small bucket.");
                        GameFunctions.FindObject("Well Bucket", PointsOfInterest).IsHidden = false;
                        BucketRaised = true;
                    }
                    else
                    {
                        Console.WriteLine("\nYou turn the brank. The rope and bucket lower to the bottom of the well.");
                        GameFunctions.FindObject("Well Bucket", PointsOfInterest).IsHidden = true;
                        BucketRaised = false;
                        if (PointsOfInterest.Contains(GameFunctions.FindObject("Gauntlet", PointsOfInterest)))
                            GameFunctions.FindObject("Gauntlet", PointsOfInterest).IsHidden = true;
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
                case "Cyclops":
                    if (!CyclopsUnconcious && !CyclopsDead)
                        DieToCyclops();
                    else
                        base.HitObject(objectName);
                    break;
                case "Well Crank":
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
                case "Cyclops":
                    GameFunctions.FindObject(objectName, PointsOfInterest).DoesNotUnderstandMessage();
                    break;
                default:
                    base.SpeakTo(objectName);
                    break;
            }
        }
    }
}
