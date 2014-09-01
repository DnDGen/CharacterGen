using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Abilities.Skills;
using NPCGen.Common.Abilities.Stats;

namespace NPCGen.Selectors.Interfaces.Objects
{
    public class FeatSelection
    {
        public String FeatName { get; set; }
        public IEnumerable<String> RequiredFeats { get; set; }
        public Int32 RequiredBaseAttack { get; set; }
        public Dictionary<String, Int32> RequiredStats { get; set; }
        public Dictionary<String, Int32> RequiredSkillRanks { get; set; }
        public Boolean IsFighterFeat { get; set; }
        public IEnumerable<String> RequiredClassNames { get; set; }
        public String SpecificApplicationType { get; set; }

        public FeatSelection()
        {
            FeatName = String.Empty;
            RequiredFeats = Enumerable.Empty<String>();
            RequiredStats = new Dictionary<String, Int32>();
            RequiredSkillRanks = new Dictionary<String, Int32>();
            RequiredClassNames = Enumerable.Empty<String>();
            SpecificApplicationType = String.Empty;
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
            return RequiredFeats.All(rf => feats.Any(f => f.Name == rf));
        }
    }
}