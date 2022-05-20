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

            Rooms.SwitchCave switchRoom = Globals.clonedRoom as Rooms.SwitchCave;
            switchRoom.SwitchSequence.Add(ObjectName); // add the name of the switch to a list

            if (switchRoom.SwitchSequence.Count == 3) // if 3 switches have been used...
            {
                Rooms.SwitchCave switchCave = Globals.clonedRoom as Rooms.SwitchCave;
                if (switchRoom.SwitchSequence.SequenceEqual(switchRoom.SwitchSequenceAnswer)) // check if the switches used match the names in the correct answer
                {
                    SwitchCave.Orb theOrb = (SwitchCave.Orb)GameFunctions.FindObject("Orb", Globals.clonedRoom.PointsOfInterest);

                    if (!switchCave.CylinderOpen && theOrb is not null) // if cylinder's closed and orb isn't taken, show this message and reveal orb
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\nScree! The top half of the cylinder lifts with a shuddering sound. " +
                            "\nYou're momentarily dazzled as the darkness is lit by a blinding flash! \nThe silver orb is revealed inside!");
                        switchCave.CylinderOpen = true;
                        theOrb.IsHidden = false;
                    }
                    else if (switchCave.CylinderOpen && theOrb is not null) // if cylinder's open and orb isn't taken, hide the cylinder
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\nThe top half of the cylinder descends. It is now closed."); // altered line
                        switchCave.CylinderOpen = false;
                        theOrb.IsHidden = true;
                    }
                    else if (!switchCave.CylinderOpen && theOrb is null) // if cylinder's closed but orb is taken, just show this message
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\nThe top half of the cylinder rises!"); // altered line
                        switchCave.CylinderOpen = true;
                    }
                    else // and if cylinder's open and orb is taken, just show this message
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\nThe top half of the cylinder descends. It is now closed."); // altered line
                        switchCave.CylinderOpen = false;
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

                switchCave.SwitchSequence.Clear();
            }
            GameFunctions.ReduceTorchFire();
        }
    }
}
