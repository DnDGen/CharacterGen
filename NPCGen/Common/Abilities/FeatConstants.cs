using System;
using System.Collections.Generic;

namespace NPCGen.Common.Abilities
{
    public static class FeatConstants
    {
        public const String Ambidexterity = "Ambidexterity";
        public const String ShieldProficiency = "Shield Proficiency";
        public const String LightArmorProficiency = "Light Armor Proficiency";

        public static IEnumerable<String> GetAllFeats()
        {
            return new[] 
            {
                Ambidexterity,
                LightArmorProficiency,
                ShieldProficiency
            };
        }
    }
}