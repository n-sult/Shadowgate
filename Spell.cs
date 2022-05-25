using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate
{
    public class Spell
    {
        public string SpellName;
        
        public Spell(string spellName)
        {
            SpellName = spellName;
        }

        public static void InitialCastSpellMessage(string spellName)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            GameFunctions.WriteLine($"\nThe spell was chanted. \n\n'{spellName}'");
        }

        public static void WrongPlaceForSpellMessage()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            GameFunctions.WriteLine("\nWhat you expected hasn't happened.");
        }

        public static void HumanaNotWorkingMessage()
        {
            InitialCastSpellMessage("Humana");
            Console.ForegroundColor = ConsoleColor.Yellow;
            if (Globals.clonedRoom.RoomName == "Troll Bridge")
                GameFunctions.Write("\nUh oh, the wind has suddenly died down!");
            GameFunctions.WriteLine("\nNothing happens! There must be something missing!");
        }

        public static void MagicIsGoneMessage()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            GameFunctions.WriteLine("\nThe magic is already gone. You won't need that spell anymore.");
        }

        public static void CastSpell(string spellName)
        {
            switch (spellName)
            {
                case "Epor":
                    if (Globals.clonedRoom.RoomName == "Epor Room")
                    {
                        if ((Globals.clonedRoom as Rooms.EporRoom).IsEporActive)
                        {
                            InitialCastSpellMessage(spellName);
                            GameFunctions.WriteLine("\nYou return the outstretched rope to it's former place."); // altered line
                            (Globals.clonedRoom as Rooms.EporRoom).IsEporActive = false;
                        }
                        else
                        {
                            InitialCastSpellMessage(spellName);
                            GameFunctions.WriteLine("\nThere are many strange things in this world! When you said the magic spell, the rope moved. " +
                                "\nHaving stretched up to the hole, the rope stops moving.");
                            (Globals.clonedRoom as Rooms.EporRoom).IsEporActive = true;
                        }
                    }
                    else
                        WrongPlaceForSpellMessage();   
                    break;
                case "Humana":
                    if (Globals.clonedRoom.RoomName == "Troll Bridge")
                    {
                        if (!GameFunctions.FindObject("Troll", Globals.clonedRoom.PointsOfInterest).IsHidden && (Globals.clonedRoom as Rooms.TrollBridge).TrollHasSpear)
                        {
                            InitialCastSpellMessage(spellName);
                            GameFunctions.WriteLine("\nAs soon as the magic is invoked, you lose sight of yourself. You're as invisible as the wind!");
                            GameFunctions.MoveRooms("Courtyard");
                        }
                        else
                            HumanaNotWorkingMessage();
                    }
                    else
                        HumanaNotWorkingMessage();
                    break;
                case "Terrakk":
                    if (Globals.clonedRoom.RoomName != "Study")
                        WrongPlaceForSpellMessage();
                    else
                    {
                        if ((Globals.clonedRoom as Rooms.Study).TerrakkUsed)
                            MagicIsGoneMessage();
                        else
                        {
                            InitialCastSpellMessage(spellName);
                            GameFunctions.WriteLine("\nA large crack apears around the equator of the globe.");
                            (Globals.clonedRoom as Rooms.Study).TerrakkUsed = true;
                        }
                    }
                    break;
                case "Illumina":
                    if (Globals.clonedRoom.RoomName != "Gargoyle Cave")
                    {
                        InitialCastSpellMessage(spellName);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        GameFunctions.WriteLine("\nThe rooms suddenly seems to be lit by a million torches as the spell is released!");
                    }
                    else
                    {
                        InitialCastSpellMessage(spellName);
                        GameFunctions.WriteLine("\nSuddenly the cavern is so bright that you have to shade your eyes. " +
                            "\nIt takes you a few moments to regain your senses from the nova burst. " +
                            "\nIt seems the gargoyles were also affected and haven't yet recovered from the spell.");
                        (Globals.clonedRoom as Rooms.GargoyleCave).IlluminaUsed = true;
                    }
                    break;
                case "Motari":
                    if (Globals.clonedRoom.RoomName != "Lava Cave")
                        WrongPlaceForSpellMessage();
                    else
                    {
                        if (!(Globals.clonedRoom as Rooms.LavaCave).MotariUsed)
                        {
                            InitialCastSpellMessage(spellName);
                            GameFunctions.WriteLine("\nThe statue lowers and a large platform rises out of the lava! You now have a way across!");
                            (Globals.clonedRoom as Rooms.LavaCave).MotariUsed = true;
                        }
                        else
                            MagicIsGoneMessage();
                    }
                    break;
            }
            GameFunctions.ReduceTorchFire();
        }
    }
}
