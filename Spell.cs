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
            Console.WriteLine($"\nThe spell was chanted. \n\n'{spellName}'");
        }

        public static void WrongPlaceForSpellMessage()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nWhat you expected hasn't happened.");
        }

        public static void HumanaNotWorkingMessage()
        {
            InitialCastSpellMessage("Humana");
            Console.ForegroundColor = ConsoleColor.Yellow;
            if (Globals.currentRoom.RoomName == "Troll Bridge")
                Console.WriteLine("\nUh oh, the wind has suddenly died down!");
            Console.WriteLine("Nothing happens! There must be something missing!");
        }

        public static void MagicIsGoneMessage()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nThe magic is already gone. You won't need that spell anymore.");
        }

        public static void CastSpell(string spellName)
        {
            switch (spellName)
            {
                case "Epor":
                    if (Globals.currentRoom.RoomName == "Epor Room")
                    {
                        if (Rooms.EporRoom.IsEporActive)
                        {
                            InitialCastSpellMessage(spellName);
                            Console.WriteLine("\nYou return the outstretched rope to it's former place."); // altered line
                            Rooms.EporRoom.IsEporActive = false;
                        }
                        else
                        {
                            InitialCastSpellMessage(spellName);
                            Console.WriteLine("\nThere are many strange things in this world! When you said the magic spell, the rope moved. " +
                                "\nHaving stretched up to the hole, the rope stops moving.");
                            Rooms.EporRoom.IsEporActive = true;
                        }
                    }
                    else
                        WrongPlaceForSpellMessage();   
                    break;
                case "Humana":
                    if (Globals.currentRoom.RoomName != "Troll Bridge")
                        HumanaNotWorkingMessage();
                    else if (Globals.currentRoom.RoomName == "Troll Bridge" && !Rooms.TrollBridge.TrollReappeared)
                        HumanaNotWorkingMessage();
                    else
                    {
                        InitialCastSpellMessage(spellName);
                        Console.WriteLine("\nAs soon as the magic is invoked, you lose sight of yourself. You're as invisible as the wind!");
                        GameFunctions.MoveRooms("Courtyard");
                    }
                    break;
                case "Terrakk":
                    if (Globals.currentRoom.RoomName != "Study")
                        WrongPlaceForSpellMessage();
                    else
                    {
                        if (Rooms.Study.TerrakkUsed)
                            MagicIsGoneMessage();
                        else
                        {
                            InitialCastSpellMessage(spellName);
                            Console.WriteLine("\nA large crack apears around the equator of the globe.");
                            Rooms.Study.TerrakkUsed = true;
                        }
                    }
                    break;
                case "Illumina":
                    if (Globals.currentRoom.RoomName != "Gargoyle Cave")
                    {
                        InitialCastSpellMessage(spellName);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\nThe rooms suddenly seems to be lit by a million torches as the spell is released!");
                    }
                    else
                    {
                        InitialCastSpellMessage(spellName);
                        Console.WriteLine("\nSuddenly the cavern is so bright that you have to shade your eyes. " +
                            "\nIt takes you a few moments to regain your senses from the nova burst. " +
                            "\nIt seems the gargoyles were also affected and haven't yet recovered from the spell.");
                        Rooms.GargoyleCave.IlluminaUsed = true;
                    }
                    break;
                case "Motari":
                    if (Globals.currentRoom.RoomName != "Lava Cave")
                        WrongPlaceForSpellMessage();
                    else
                    {
                        if (!Rooms.LavaCave.MotariUsed)
                        {
                            InitialCastSpellMessage(spellName);
                            Console.WriteLine("\nThe statue lowers and a large platform rises out of the lava! You now have a way across!");
                            Rooms.LavaCave.MotariUsed = true;
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
