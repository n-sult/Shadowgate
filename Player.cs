﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowgate
{
    public class Player : PointOfInterest
    {
        public bool IsCloakEquipped;
        public bool IsGauntletEquipped;
        public bool IsGlassesEquipped;
        public bool IsPlayerDead = false;
        public int HowManyTimesPlayerIsBurned;
        public List<Spell> Spellbook;
        public List<Item> PlayerInventory;

        public Player(string objectName, bool isCloakEquipped, bool isGauntletEquipped, bool isGlassesEquipped)
        {
            ObjectName = objectName;
            IsCloakEquipped = isCloakEquipped;
            IsGauntletEquipped = isGauntletEquipped;
            IsGlassesEquipped = isGlassesEquipped;
            Spellbook = new List<Spell>();
            PlayerInventory = new List<Item>();
        }

        void OddBehaviorMessage()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nWhat odd behavior for such a brave warrior!");
        }

        public override void Look()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nThou art truly a brave knight!");
            if (IsCloakEquipped)
                Console.WriteLine("You are wearing the cloak.");
            if (IsGlassesEquipped)
                Console.WriteLine("You are wearing the glasses.");
            if (IsGauntletEquipped)
                Console.WriteLine("You are wearing the silver gauntlets.");
        }

        public override void Use()
        {
            OddBehaviorMessage();
        }

        public override void Hit()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nSmash!! Now you see stars!");
            GameFunctions.ReduceTorchFire();
        }

        public override void Speak()
        {
            OddBehaviorMessage();
        }
    }
}