﻿using System;
using System.Collections.Generic;
using System.Linq;
using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Skills;
using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Tables;

namespace CharacterGen.Selectors.Objects
{
    public class AdditionalFeatSelection
    {
        public String Feat { get; set; }
        public Frequency Frequency { get; set; }
        public Int32 Strength { get; set; }
        public IEnumerable<RequiredFeat> RequiredFeats { get; set; }
        public Int32 RequiredBaseAttack { get; set; }
        public Dictionary<String, Int32> RequiredStats { get; set; }
        public Dictionary<String, Int32> RequiredSkillRanks { get; set; }
        public Dictionary<String, Int32> RequiredCharacterClasses { get; set; }
        public String FocusType { get; set; }

        public AdditionalFeatSelection()
        {
            Feat = String.Empty;
            RequiredFeats = Enumerable.Empty<RequiredFeat>();
            RequiredStats = new Dictionary<String, Int32>();
            RequiredSkillRanks = new Dictionary<String, Int32>();
            RequiredCharacterClasses = new Dictionary<String, Int32>();
            FocusType = String.Empty;
            Frequency = new Frequency();
        }

        public Boolean ImmutableRequirementsMet(Int32 baseAttack, Dictionary<String, Stat> stats, Dictionary<String, Skill> skills, CharacterClass characterClass)
        {
            foreach (var stat in RequiredStats)
                if (stats[stat.Key].Value < stat.Value)
                    return false;

            foreach (var skill in RequiredSkillRanks)
                if (skills[skill.Key].EffectiveRanks < skill.Value)
                    return false;

            if (RequiredCharacterClasses.Any() && (RequiredCharacterClasses.ContainsKey(characterClass.ClassName) == false || RequiredCharacterClasses[characterClass.ClassName] > characterClass.Level))
                return false;

            return baseAttack >= RequiredBaseAttack;
        }

        public Boolean MutableRequirementsMet(IEnumerable<Feat> feats)
        {
            var proficiencyRequirement = RequiredFeats.FirstOrDefault(f => f.Feat == GroupConstants.Proficiency);
            var requirementsWithoutProficiency = RequiredFeats.Except(new[] { proficiencyRequirement });

            return requirementsWithoutProficiency.All(f => f.RequirementMet(feats));
        }
    }
}