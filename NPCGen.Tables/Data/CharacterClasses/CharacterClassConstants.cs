using System;
using System.Collections.Generic;

namespace NPCGen.Core.Data.CharacterClasses
{
    public static class CharacterClassConstants
    {
        public const String Barbarian = "Barbarian";
        public const String Bard = "Bard";
        public const String Cleric = "Cleric";
        public const String Druid = "Druid";
        public const String Fighter = "Fighter";
        public const String Monk = "Monk";
        public const String Paladin = "Paladin";
        public const String Ranger = "Ranger";
        public const String Rogue = "Rogue";
        public const String Sorcerer = "Sorcerer";
        public const String Wizard = "Wizard";

        public static IEnumerable<String> GetClassNames()
        {
            return new[]
            {
                Barbarian,
                Bard,
                Cleric,
                Druid,
                Fighter,
                Monk,
                Paladin,
                Ranger,
                Rogue,
                Sorcerer,
                Wizard
            };
        }
    }
}