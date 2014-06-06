using System;
using System.Collections.Generic;

namespace NPCGen.Common.Alignments
{
    public class AlignmentConstants
    {
        public const String Lawful = "Lawful";
        public const String Good = "Good";
        public const String Neutral = "Neutral";
        public const String Chaotic = "Chaotic";
        public const String Evil = "Evil";

        public static IEnumerable<String> GetLawfulnesses()
        {
            return new[] { Lawful, Neutral, Chaotic };
        }

        public static IEnumerable<String> GetGoodnesses()
        {
            return new[] { Good, Neutral, Evil };
        }
    }
}