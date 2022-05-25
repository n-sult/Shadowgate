using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Rooms
{
    public class Chasm : Room
    {
        public Chasm()
        {
            // POI for Chasm
            PointOfInterest behemoth = new PointOfInterest("Behemoth");
            PointOfInterest braziers = new PointOfInterest("Line of braziers");
            PointOfInterest chasmPillar = new PointOfInterest("Stone Platform");
            PointOfInterest warlockLord = new PointOfInterest("Warlock Lord");
            Entry doorFromChasmToVault = new Entry("Door to Vault", false, true, false, false, "Vault");
            var chasmPOI = new List<PointOfInterest>() { behemoth, warlockLord, braziers, chasmPillar, doorFromChasmToVault };

            RoomName = "The Chasm";
            FirstEntry = "The cavern that you have entered is by far the largest your eyes have ever gazed upon. " +
                "\nFrom the depths rises the most powerful creature that has ever existed: the Behemoth! " +
                "\nYour stomach knots up as you stare at this new horror. The beast is indeed incredible! " +
                "\nYou wonder, for a moment, how you can defeat such a creature as this!";
            SubsequentEntry = FirstEntry;
            PointsOfInterest = chasmPOI;
        }

        public static void DieToWarlock()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            GameFunctions.WriteLine("\nThe Warlock Lord feels your presence and knows that you are the seed that must be destroyed. " +
                "\nFlame shoots forth from his staff and engulfs your body. You have failed!");
            GameFunctions.GameOver();
        }

        public override void LookAt(string objectName)
        {
            switch(objectName)
            {
                case "Behemoth":
                    GameFunctions.WriteLine("\nYour jaw drops and you stare in awed silence at the sight of the Great Titan! " +
                        "\nAcid drips from his jaws and sizzles on the ledge below!");
                    break;
                case "Warlock Lord":
                    GameFunctions.WriteLine("\nAlthough his back is turned, you know beyond a shadow of a doubt that it's the Warlock Lord. " +
                        "\nIt seems that his staff is controlling the creature, keeping it at bay!");
                    break;
                case "Line of braziers":
                    GameFunctions.WriteLine("\nFlame burns intensely within the braziers as if in celebration of the Dark One's victory!");
                    break;
                case "Stone Platform":
                    GameFunctions.WriteLine("\nIt's a huge stone platform with stairs descending from it.");
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
                case "Behemoth":
                case "Warlock Lord":
                    DieToWarlock();
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
                case "Behemoth":
                case "Warlock Lord":
                    GameFunctions.FindObject(objectName, PointsOfInterest).DoesNotUnderstandMessage();
                    break;
                default:
                    base.SpeakTo(objectName);
                    break;
            }
        }
    }
}
