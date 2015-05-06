using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Abilities.Skills;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.Races;

namespace NPCGen.Selectors.Interfaces.Objects
{
    public class AdditionalFeatSelection
    {
        public NameModel Name { get; set; }
        public IEnumerable<String> RequiredFeatIds { get; set; }
        public Int32 RequiredBaseAttack { get; set; }
        public Dictionary<String, Int32> RequiredStats { get; set; }
        public Dictionary<String, Int32> RequiredSkillRanks { get; set; }
        public Boolean IsFighterFeat { get; set; }
        public Boolean IsWizardFeat { get; set; }
        public IEnumerable<String> RequiredClassNames { get; set; }
        public String FocusType { get; set; }

        public AdditionalFeatSelection()
        {
            Name = new NameModel();
            RequiredFeatIds = Enumerable.Empty<String>();
            RequiredStats = new Dictionary<String, Int32>();
            RequiredSkillRanks = new Dictionary<String, Int32>();
            RequiredClassNames = Enumerable.Empty<String>();
            FocusType = String.Empty;
        }

        public Boolean ImmutableRequirementsMet(Int32 baseAttack, Dictionary<String, Stat> stats,
            Dictionary<String, Skill> skills, String className)
        {
            foreach (var stat in RequiredStats)
                if (stats[stat.Key].Value < stat.Value)
                    return false;

            foreach (var skill in RequiredSkillRanks)
                if (skills[skill.Key].EffectiveRanks < skill.Value)
                    return false;

            if (RequiredClassNames.Any() && !RequiredClassNames.Contains(className))
                return false;

            return baseAttack >= RequiredBaseAttack;
        }

        public Boolean MutableRequirementsMet(IEnumerable<Feat> feats)
        {
            var featIds = feats.Select(f => f.Name.Id);
            var missedRequirements = RequiredFeatIds.Except(featIds);

            return missedRequirements.Any() == false;
        }
    }
}