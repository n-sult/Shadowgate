using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.Rooms
{
    public class SphinxChamber : Room
    {
        public bool RiddleAsked;
        public bool RiddleAnswered;
        public static string[] ItemsForRiddle = { "Mirror", "Broom", "Horseshoe", "Map", "Skull", "Bellows" };
        public string AnswerForRiddle;
        int _riddleIndex = 0;

        // Riddles for the Sphinx
        Random rnd = new Random(); // to allow a riddle to be randomly selected in Sphinx Chamber
        Dictionary<int, string> riddles = new Dictionary<int, string>()
        {
            {0, "\n'You look at me, I look back,' \n'Your right hand raises, I my left.' \n'You speak, but I in vain.'" },        // mirror
            {1, "\n'Long neck, no hands,' \n'100 legs, cannot stand.' \n'Born of a forest nest,' \n'Against a wall I rest.'" },   // broom
            {2, "\n'First burnt and beaten', \n'Drowned and pierced with nails', \n'Then stepped on by long-faced animals.'" },   // horseshoe
            {3, "\n'It has towns, but no houses.' \n'Forests, but no trees.' \n'Rivers, but no fish.'" },                         // map
            {4, "\n'I've no eyes, but once did see,' \n'Thoughts had I, but now I'm white and empty.'" },                         // skull
            {5, "\n'I'm a fire's friend,' \n'My body swells with wind.' \n'With my nose I blow,' \n'How the embers glow.'" }      // bellows
        };

        public SphinxChamber()
        {
            // POI for Sphinx Chamber
            OutsideView chamberWindow = new OutsideView("Chamber Windows");
            PointOfInterest sphinx = new PointOfInterest("Sphinx");
            PointOfInterest fakeChamberTorch1 = new PointOfInterest("Left Torch");
            PointOfInterest fakeChamberTorch2 = new PointOfInterest("Center Torch");
            PointOfInterest fakeChamberTorch3 = new PointOfInterest("Right Torch");
            Entry chamberStairs = new Entry("Chamber Stairs with markings on the side", false, true, false, false, "Observatory");
            Entry doorFromSphinxChamberToBanquetHall = new Entry("Door to Banquet Hall", true, true, false, false, "Banquet Hall");
            var sphinxChamberPOI = new List<PointOfInterest>() { chamberWindow, sphinx, fakeChamberTorch1, fakeChamberTorch2, fakeChamberTorch3,
                chamberStairs, doorFromSphinxChamberToBanquetHall };

            RoomName = "Sphinx's Chamber";
            FirstEntry = "In this room, there appears to be a sphinx. It looks at you indifferently.";
            SubsequentEntry = "The sphinx rests quietly in the room.";
            PointsOfInterest = sphinxChamberPOI;

            GameFunctions.RoomEnteredEvent += (roomName) => { if (roomName == RoomName) RiddleAsked = false; RiddleAnswered = false; };
        }

        static void PreRiddleMessage()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nAs you moved, the sphinx spoke, \n'Who are you? No one may pass without my permission. " + // initial sphinx dialogue
                "To pass, you must answer a riddle!'");
        }

        static void PostRiddleMessage()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n'Dost thou know? Bring me the answer to my riddle and I shall let thee pass.'"); // post riddle dialogue
        }
        
        public static void SphinxTeleportsYou(string roomName)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\nSuddenly, the room begins to fade! It seems that the sphinx's magic has taken you to the {roomName}!");
            Entry.ChangeRoomEvent?.Invoke(roomName, false);
        }

        public void UseItemOnSphinx(string objectName)
        {
            if (objectName is not null)
            {
                if (!RiddleAsked)
                    SphinxTeleportsYou("Troll Bridge");
                else if (RiddleAsked && objectName != AnswerForRiddle)
                {
                    switch (AnswerForRiddle)
                    {
                        case "Mirror":
                            SphinxTeleportsYou("Tomb");
                            break;
                        case "Broom":
                            SphinxTeleportsYou("Troll Bridge");
                            break;
                        case "Horseshoe":
                            SphinxTeleportsYou("Laboratory");
                            break;
                        case "Map":
                            SphinxTeleportsYou("Study");
                            break;
                        case "Skull":
                            SphinxTeleportsYou("Drafty Hallway");
                            break;
                        case "Bellows":
                            SphinxTeleportsYou("Library");
                            break;
                    }
                }
                else if (RiddleAsked && objectName == AnswerForRiddle)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\nThe sphinx nods its head. 'You have correctly answered my riddle, warrior. Thou may now pass.'"); // altered line
                    RiddleAnswered = true;
                }
            }
            GameFunctions.ReduceTorchFire();
        }
        
        public override void MoveTo(string objectName)
        {
            switch(objectName)
            {
                case "Chamber Stairs with markings on the side":
                    
                    if (!RiddleAsked && !RiddleAnswered) // if riddle hasn't been asked since entering room, choose a random riddle
                    {
                        PreRiddleMessage();
                        _riddleIndex = rnd.Next(riddles.Count); // randomly choose a riddle from dictionary
                        Console.WriteLine(riddles.Values.ElementAt(_riddleIndex)); // show riddle at given index (which is also used to determine item to use)
                        AnswerForRiddle = ItemsForRiddle[_riddleIndex]; 
                        RiddleAsked = true;
                        PostRiddleMessage();
                        GameFunctions.ReduceTorchFire();
                    }
                    else if (RiddleAsked && !RiddleAnswered) // if riddle has already been asked since entering room, ask same riddle
                    {
                        PreRiddleMessage();
                        Console.WriteLine(riddles.Values.ElementAt(_riddleIndex)); // show riddle at given index 
                        PostRiddleMessage();
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
                case "Sphinx":
                    Console.WriteLine("\nYou have sumbled upon a sphinx. It has the body of a lion and the head of a man.");
                    break;
                case "Left Torch":
                case "Center Torch":
                case "Right Torch":
                    Console.WriteLine("\nThe strange, eerie flame burns silently.");
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
                case "Left Torch":
                case "Middle Torch":
                case "Right Torch":
                    GameFunctions.FindObject(objectName, PointsOfInterest).ThatSmartsMessage();
                    break;
                case "Sphinx":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    SphinxTeleportsYou("Troll Bridge");
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
                case "Sphinx":
                    GameFunctions.FindObject(objectName, PointsOfInterest).DoesNotUnderstandMessage();
                    break;
                default:
                    base.SpeakTo(objectName);
                    break;
            }
        }
    }
}
