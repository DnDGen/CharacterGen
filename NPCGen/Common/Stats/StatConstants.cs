using System;
using System.Collections.Generic;

namespace NPCGen.Common.Stats
{
    public static class StatConstants
    {
        public const String Strength = "Strength";
        public const String Constitution = "Constitution";
        public const String Dexterity = "Dexterity";
        public const String Intelligence = "Intelligence";
        public const String Wisdom = "Wisdom";
        public const String Charisma = "Charisma";

        public static IEnumerable<String> GetStats()
        {
            return new[]
            {
                Strength,
                Dexterity,
                Constitution,
                Intelligence,
                Wisdom,
                Charisma
            };
        }
    }
}