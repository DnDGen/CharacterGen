using System;
using System.Collections.Generic;

namespace NPCGen.Common.Abilities.Feats
{
    public static class FeatConstants
    {
        public const String Ambidexterity = "Ambidexterity";
        public const String LightArmorProficiency = "Light Armor Proficiency";
        public const String ShieldProficiency = "Shield Proficiency";
        public const String SkillFocus = "Skill Focus";

        public static IEnumerable<String> GetAllFeats()
        {
            return new[] 
            {
                Ambidexterity,
                LightArmorProficiency,
                ShieldProficiency,
                SkillFocus
            };
        }
    }
}