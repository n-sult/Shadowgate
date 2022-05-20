using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate
{
    public class ActiveTorch : Item
    {
        public int FireRemaining;
        public ActiveTorch(string objectName, int fireRemaining)
        {
            ObjectName = objectName;
            FireRemaining = fireRemaining;
        }

        public void TorchInWater()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nYou stick the burning torch in the water. \"Ssss\"...the flame just went out!");
            FireRemaining = 0;
        }

        public override void Look()
        {
            if (FireRemaining == 0)
                Console.WriteLine("\nThe flame from the torch has gone out.");
            else
                Console.WriteLine("\nThis torch throws dancing shadows about the room.");
        }

        public override void Use()
        {
            if (FireRemaining == 0) // if activetorch is out, allow player to attempt to use, but always give base response
            {
                if (GameFunctions.UseOn(ObjectName) is not null)
                    base.Use();
            }
            else
            {
                string result = GameFunctions.UseOn(ObjectName);
                if (result is not null)
                {
                    var activeObject = GameFunctions.FindObject(result, GameFunctions.PutPOIsItemsAndSelfInOneList());
                    if (activeObject is Entry || activeObject is ActiveTorch) // if trying to use activetorch on entry or other activetorch, alert player they can't
                        base.DoNotDoThatMessage();
                    else
                    {
                        switch (result)
                        {
                            case "Red Rug": // if using on rug, burn rug and remove from POIs
                            case "Rug expanding the hallway":
                            case "Blue Rug":
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine("\nThe rug quickly catches on fire and burns away.");
                                Globals.clonedRoom.PointsOfInterest.Remove(activeObject);
                                GameFunctions.ReduceTorchFire();
                                break;
                            case "Banquet Rug":
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine("\nThe rug quickly catches on fire and burns away. \nA key can be seen underneath!");
                                Globals.clonedRoom.PointsOfInterest.Remove(activeObject);
                                
                                GameFunctions.FindObject("Key 4", Globals.clonedRoom.PointsOfInterest).IsHidden = false;
                                GameFunctions.ReduceTorchFire();
                                break;
                            case "Tapestry":
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine("\nYou torched the Tapestry.");
                                Globals.clonedRoom.PointsOfInterest.Remove(activeObject);
                                GameFunctions.ReduceTorchFire();
                                break;
                            case "Pond": // if using on the pond in the shark pond room...
                                bool isSphereInPond = Globals.clonedRoom.PointsOfInterest.Contains(activeObject);
                                if (isSphereInPond) // if the sphere is currently in the pond, melt and re-freeze pond to make sphere takeable
                                {
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine("\nYou put the burning torch close to it. \nThe torch melts away the ice over the sphere, " +
                                        "allowing it to float to the surface. \nNot surprisingly, the lake quickly refreezes.");
                                    (Globals.clonedRoom as Rooms.SharkPond).UsedTorchOnPond = true;
                                    GameFunctions.ReduceTorchFire();
                                }
                                else if (!isSphereInPond && (Globals.clonedRoom as Rooms.SharkPond).UsedTorchOnPond)
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow; // if sphere has been used and taken and torch has been used on lake, give this message
                                    Console.WriteLine("\nThe lake has become a solid sheet of ice.");
                                }
                                else // otherwise, douse the fire on the torch
                                    TorchInWater();
                                break;
                            case "Waterfall": // if using on the waterfall, douse the fire on the torch
                                TorchInWater();
                                break;
                            case "Mummy": // if using torch on the mummy...
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine("\nThe mummy bursts into flames, leaving behind a scepter among the ashes."); // show message of mummy burning away
                                Globals.clonedRoom.PointsOfInterest.Remove(GameFunctions.FindObject("Mummy", Globals.clonedRoom.PointsOfInterest)); // permanently remove mummy
                                GameFunctions.FindObject("Scepter", Globals.clonedRoom.PointsOfInterest).IsHidden = false; // unhide the scepter
                                GameFunctions.ReduceTorchFire();
                                break;
                            case "Holy Torch":
                                if (Globals.clonedRoom.RoomName == "Wraith Room") // if you're in the wraith room and the wraith is alive...
                                //if (Globals.clonedRoom.RoomName == "Wraith Room" && !Rooms.WraithRoom.IsWraithDead) // if you're in the wraith room and the wraith is alive...
                                {
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine("\nThe torch burns with a strange white flame. \nWith a shout, you throw the flaming torch at it. " +
                                        "\nWith a blinding flash, the white flame engulfs the undead apparition! \nWhen you open your eyes again, the wraith is gone.");
                                    Globals.currentPlayer.PlayerInventory.Remove((Item)activeObject); // remove the holy torch from inventory
                                    Globals.clonedRoom.PointsOfInterest.Remove(GameFunctions.FindObject("Wraith", Globals.clonedRoom.PointsOfInterest)); // remove the wraith from the room POI
                                    GameFunctions.ReduceTorchFire();
                                }
                                else
                                    this.DoNotDoThatMessage();
                                break;
                            case "Woodpile":
                                if (!(Globals.clonedRoom as Rooms.Study).WoodpileLit)
                                {
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine("\nYou torched the firewood. \nThe fire starts burning, adding warmth to the room.");
                                    (Globals.clonedRoom as Rooms.Study).WoodpileLit = true;
                                }
                                else
                                    base.Use();
                                break;
                            case "Self":
                                if (Globals.currentPlayer.HowManyTimesPlayerIsBurned == 0)
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("\nYou now have terrific second-degree burns on your hands.");
                                    Globals.currentPlayer.HowManyTimesPlayerIsBurned++;
                                }
                                else if (Globals.currentPlayer.HowManyTimesPlayerIsBurned == 1)
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("\nYou hold the flame close enough to your skin to cause second and third-degree burns.");
                                    Globals.currentPlayer.HowManyTimesPlayerIsBurned++;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\nYou finally set your hair on fire. The rest of your body soon follows!");
                                    GameFunctions.GameOver();
                                }
                                GameFunctions.ReduceTorchFire();
                                break;
                            default:
                                base.Use();
                                break;
                        }
                    }
                }
            }
        }

        public override void Leave()
        {
            DoNotDoThatMessage();
        }
    }
}
