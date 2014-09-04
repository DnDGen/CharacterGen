using System;
using System.Collections.Generic;

namespace NPCGen.Common.Abilities.Feats
{
    public static class FeatConstants
    {
        public const String AasimarDaylight = "Aasimar Special Attack: Daylight";
        public const String Ambidexterity = "Ambidexterity";
        public const String Darkvision = "Darkvision";
        public const String LightArmorProficiency = "Light Armor Proficiency";
        public const String ResistanceToAcid = "Resistance to Acid";
        public const String ResistanceToCold = "Resistance to Cold";
        public const String ResistanceToElectricity = "Resistance to Electricity";
        public const String Scent = "Scent";
        public const String ShieldProficiency = "Shield Proficiency";
        public const String SkillFocus = "Skill Focus";
        public const String SpellMastery = "Spell Mastery";
        public const String Stability = "Stability";
        public const String Stonecunning = "Stonecunning";
        public const String WeaponFamiliarity = "Weapon Familiarity";

        public static IEnumerable<String> GetAllFeats()
        {
            return new[] 
            {
                AasimarDaylight,
                Ambidexterity,
                Darkvision,
                LightArmorProficiency,
                ResistanceToAcid,
                ResistanceToCold,
                ResistanceToElectricity,
                Scent,
                ShieldProficiency,
                SkillFocus,
                SpellMastery,
                Stability,
                Stonecunning,
                WeaponFamiliarity
            };
        }
    }
}