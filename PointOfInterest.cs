using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate
{
    public class PointOfInterest
    {
        public string ObjectName;
        public bool IsHidden;
        public bool CanBeDiscarded;

        public static string LookResult = "\nYou seem to be wasting your time.";
        public static string _openResult = "\nIt won't open!";
        public static string _useResult = "\nYou seem to be wasting your time.";
        public static string _leaveResult = "\nYou can't drop it here.";
        public static string _takeResult = "\nYou can't take it!";
        public static string _closeResult = "\nNothing happened.";
        public static string _hitResult = "\nNothing happened.";
        public static string _speakResult = "\nYou seem to be wasting your time.";
        public static string _moveResult = "\nYou can't do that.";
        public static string _doNoDoThatMessage = "\nNo! Wait a minute! It's best if you don't do that!";
        public static string _whatDidYouExpectMessage = "\nWhat you expected hasn't happened.";
        public static string _ouchMessage = "\nOuch! That smarts!";
        public static string _bumpedHeadMessage = "\n'Ouch!' You bumped it with your head!";
        public static string _thumpMessage = "\nThump! The sound echoes in the room!";
        public static string _doesNotUnderstandMessage = "\nIt doesn't seem to understand what you say.";
        public static string _cannotReachMessage = "\nHm! It's too high for you to reach.";
        public static string _afraidToGetNearMessage = "\nYou're afraid to get near it.";

        public PointOfInterest() // remove?
        {

        }

        public PointOfInterest(string objectName) 
        {
            ObjectName = objectName;
        }

        public PointOfInterest(string objectName, bool isHidden)
        {
            ObjectName = objectName;
            IsHidden = isHidden;
        }

        public PointOfInterest(string objectName, bool isHidden, bool canBeDiscarded)
        {
            ObjectName = objectName;
            IsHidden = isHidden;
            CanBeDiscarded = canBeDiscarded;
        }

        public virtual void Move()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(_moveResult);
        }

        public virtual void Look()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(LookResult);
        }

        public virtual bool Take()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(_takeResult);
            return false;
        }

        public virtual void Open()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(_openResult);
        }

        public virtual void Close()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(_closeResult);
        }

        public virtual void Use()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(_useResult);
        }

        public virtual void Hit()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(_hitResult);
        }

        public virtual void Leave()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(_leaveResult);
        }

        public virtual void Speak()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(_speakResult);
        }

        public virtual void DoNotDoThatMessage()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(_doNoDoThatMessage);
        }

        public virtual void WhatDidYouExpectMessage()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(_whatDidYouExpectMessage);
            GameFunctions.ReduceTorchFire();
        }

        public virtual void ThatSmartsMessage()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(_ouchMessage);
            GameFunctions.ReduceTorchFire();
        }

        public virtual void BumpedHeadMessage()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(_bumpedHeadMessage);
            GameFunctions.ReduceTorchFire();
        }
        
        public virtual void ThumpMessage()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(_thumpMessage);
            GameFunctions.ReduceTorchFire();
        }

        public virtual void DoesNotUnderstandMessage()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(_doesNotUnderstandMessage);
            GameFunctions.ReduceTorchFire();
        }
        
        public virtual void CannotReachMessage()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(_cannotReachMessage);
        }

        public virtual void AfraidToGetNearMessage()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(_afraidToGetNearMessage);
        }

        public static void LeaveInFountainMessage(string objectName)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\nThe {objectName} was dropped into the fountain and immediately melted in the acidic liquid.");
            var itemToRemove = GameFunctions.FindObject(objectName, null, Globals.currentPlayer.PlayerInventory);
            Globals.currentPlayer.PlayerInventory.Remove((Item)itemToRemove);
            GameFunctions.ReduceTorchFire();
        }
    }
}
