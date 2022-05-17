using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate.SwitchCave
{
    public class Switch : PointOfInterest
    {
        public bool IsLowered;
        public static List<string> SwitchSequence = new List<string>() { };
        List<string> _switchSequenceAnswer = new List<string> { "Right Switch", "Middle Switch", "Right Switch"};

        public Switch(string objectName, bool isLowered)
        {
            ObjectName = objectName;
            IsLowered = isLowered;
        }

        public override void Look()
        {
            Console.WriteLine("\nIt's a finely-crafted wooden handle. \nThere are three handles here, side by side.");
        }

        public override void Use()
        {
            if (!IsLowered) // first, check if the switch is lowered/raised. then show the correct message based on that, and set it's status to lowered/raised
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"\nThe {ObjectName} was lowered.");
                this.IsLowered = true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"\nThe {ObjectName} was raised.");
                this.IsLowered = false;
            }
                
            SwitchSequence.Add(ObjectName); // add the name of the switch to a list

            if (SwitchSequence.Count == 3) // if 3 switches have been used...
            {
                if (SwitchSequence.SequenceEqual(_switchSequenceAnswer)) // check if the switches used match the names in the correct answer
                {
                    if (!Rooms.SwitchCave.CylinderOpen && !Rooms.SwitchCave.OrbTaken) // if cylinder's closed and orb isn't taken, show this message and reveal orb
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\nScree! The top half of the cylinder lifts with a shuddering sound. " +
                            "\nYou're momentarily dazzled as the darkness is lit by a blinding flash! \nThe silver orb is revealed inside!");
                        Rooms.SwitchCave.CylinderOpen = true;
                        GameFunctions.FindObject("Orb", Globals.currentRoom.PointsOfInterest).IsHidden = false;
                    }
                    else if (Rooms.SwitchCave.CylinderOpen && !Rooms.SwitchCave.OrbTaken) // if cylinder's open and orb isn't taken, hide the cylinder
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\nThe top half of the cylinder descends. It is now closed."); // altered line
                        Rooms.SwitchCave.CylinderOpen = false;
                        GameFunctions.FindObject("Orb", Globals.currentRoom.PointsOfInterest).IsHidden = true;
                    }
                    else if (!Rooms.SwitchCave.CylinderOpen && Rooms.SwitchCave.OrbTaken) // if cylinder's closed but orb is taken, just show this message
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\nThe top half of the cylinder rises!"); // altered line
                        Rooms.SwitchCave.CylinderOpen = true;
                    }
                    else // and if cylinder's open and orb is taken, just show this message
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\nThe top half of the cylinder descends. It is now closed."); // altered line
                        Rooms.SwitchCave.CylinderOpen = false;
                    }
                }
                else // if incorrect sequence was used...
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nNothing happened. \nThe switches returned to their original positions."); // altered line
                }
                
                foreach (PointOfInterest POI in Globals.currentRoom.PointsOfInterest)
                    if (POI is Switch)
                        (POI as Switch).IsLowered = false;

                SwitchSequence.Clear();
            }
            GameFunctions.ReduceTorchFire();
        }
    }
}
