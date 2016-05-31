using System.Collections.Generic;

namespace CharacterGen.Abilities
{
    public static class LanguageConstants
    {
        public const string Abyssal = "Abyssal";
        public const string Aquan = "Aquan";
        public const string Auran = "Auran";
        public const string Celestial = "Celestial";
        public const string Common = "Common";
        public const string Draconic = "Draconic";
        public const string Druidic = "Druidic";
        public const string Dwarven = "Dwarven";
        public const string Elven = "Elven";
        public const string Giant = "Giant";
        public const string Gnoll = "Gnoll";
        public const string Gnome = "Gnome";
        public const string Goblin = "Goblin";
        public const string Halfling = "Halfling";
        public const string Ignan = "Ignan";
        public const string Infernal = "Infernal";
        public const string Orc = "Orc";
        public const string Sylvan = "Sylvan";
        public const string Terran = "Terran";
        public const string Undercommon = "Undercommon";

        public static IEnumerable<string> GetLanguages()
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