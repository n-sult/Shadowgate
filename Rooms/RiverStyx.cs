using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Rooms
{
    public class RiverStyx : Room
    {
        public bool GoldCoinGiven;
        
        public RiverStyx()
        {
            // POI for River Styx
            PointOfInterest gong = new PointOfInterest("Gong");
            PointOfInterest mallet = new PointOfInterest("Mallet");
            PointOfInterest specter = new PointOfInterest("Ferryman", true);
            PointOfInterest riverWater = new PointOfInterest("River");
            Entry raft = new Entry("Raft", false, true, false, true, "Vault");
            Entry fromRiverStyxToWellRoom = new Entry("Back to Well Room", false, true, false, false, "Well Room");
            var riverStyxPOI = new List<PointOfInterest>() { gong, mallet, riverWater, specter, raft, fromRiverStyxToWellRoom };

            RoomName = "River Styx";
            FirstEntry = "The swirling winds carry you down the deep well and set you gently into the cavern below. " +
                "\n\nYou stand above a beach, looking down upon a river.";
            SubsequentEntry = "You're standing on the bank of the River Styx. It's still waters support no life.";
            PointsOfInterest = riverStyxPOI;
        }

        public override void MoveTo(string objectName)
        {
            switch(objectName)
            {
                case "River":
                    Console.ForegroundColor = ConsoleColor.Red;
                    GameFunctions.WriteLine("\nAs soon as you jump in, you find that you cannot escape the strong current of this river. " +
                        "\nYour cries for help are cut off as your lungs fill with water!");
                    GameFunctions.GameOver();
                    break;
                case "Raft":
                    if (!GoldCoinGiven)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        GameFunctions.WriteLine("\nThe ferryman will not let you board. He is still waiting for a fare.");
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
                case "Gong":
                    GameFunctions.WriteLine("\nIt's a great gold gong. Its beauty is enhanced by the intricate stand that supports it.");
                    break;
                case "Mallet":
                    GameFunctions.WriteLine("\nThis mallet is made from, what appears to be, Centaur hide.");
                    break;
                case "River":
                    GameFunctions.WriteLine("\nThe river's water is dead calm. It wouldn't surprise you if this were the River Styx.");
                    break;
                case "Ferryman":
                    GameFunctions.WriteLine("\nThe specter is wrapped in tattered rags. You can see a skeleton within the cloak. " +
                        "\nJust gazing at this apparition is enough to give you the creeps!");
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
                case "Mallet":
                    var result = GameFunctions.UseOn(objectName);

                    if (result is not null)
                    {
                        switch(result)
                        {
                            case "Gong":
                                if (GameFunctions.FindObject("Ferryman", PointsOfInterest).IsHidden)
                                {
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    GameFunctions.WriteLine("\nAfter the gong sounds, a specter materializes right before your eyes. " +
                                        "\nThe ghostly ferryman doesn't look friendly. You hear a faint voice ask for a fare.");
                                    GameFunctions.FindObject("Ferryman", PointsOfInterest).IsHidden = false;
                                    GameFunctions.FindObject("Raft", PointsOfInterest).IsHidden = false;
                                    GameFunctions.ReduceTorchFire();
                                }
                                else
                                    GameFunctions.FindObject(objectName, PointsOfInterest).ThumpMessage();
                                break;
                            default:
                                base.UseObject(result);
                                break;
                        }
                    }
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
                case "Gong":
                    GameFunctions.FindObject(objectName, PointsOfInterest).ThumpMessage();
                    break;
                case "Mallet":
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
                case "Ferryman":
                    GameFunctions.FindObject(objectName, PointsOfInterest).DoesNotUnderstandMessage();
                    break;
                default:
                    base.SpeakTo(objectName);
                    break;
            }
        }
    }
}
