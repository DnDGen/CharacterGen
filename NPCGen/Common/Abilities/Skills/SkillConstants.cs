using System;
using System.Collections.Generic;

namespace NPCGen.Common.Abilities.Skills
{
    public static class SkillConstants
    {
        public const String Swim = "Swim";

        public static IEnumerable<String> GetSkills()
        {
            return new[]
            {
                Swim
            };
        }
    }
}