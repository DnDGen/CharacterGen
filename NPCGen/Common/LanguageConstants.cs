using System;
using System.Collections.Generic;

namespace NPCGen.Common
{
    public static class LanguageConstants
    {
        public const String Abyssal = "Abyssal";
        public const String Aquan = "Aquan";
        public const String Auran = "Auran";
        public const String Celestial = "Celestial";
        public const String Common = "Common";
        public const String Draconic = "Draconic";
        public const String Druidic = "Druidic";
        public const String Dwarven = "Dwarven";
        public const String Elven = "Elven";
        public const String Giant = "Giant";
        public const String Gnoll = "Gnoll";
        public const String Gnome = "Gnome";
        public const String Goblin = "Goblin";
        public const String Halfling = "Halfling";
        public const String Ignan = "Ignan";
        public const String Infernal = "Infernal";
        public const String Orc = "Orc";
        public const String Sylvan = "Sylvan";
        public const String Terran = "Terran";
        public const String Undercommon = "Undercommon";

        public static IEnumerable<String> GetLanguages()
        {
            return new[]
            {
                Common,
                Celestial,
                Goblin,
                Druidic,
                Dwarven,
                Undercommon,
                Elven,
                Gnome,
                Sylvan,
                Gnoll,
                Orc,
                Draconic,
                Halfling,
                Giant,
                Infernal,
                Abyssal,
                Aquan,
                Auran,
                Ignan,
                Terran
            };
        }
    }
}