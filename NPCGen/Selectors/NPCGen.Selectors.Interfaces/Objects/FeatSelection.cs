using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Common.Abilities.Skills;
using NPCGen.Common.Abilities.Stats;

namespace NPCGen.Selectors.Interfaces.Objects
{
    public class FeatSelection
    {
        public String FeatName { get; set; }
        public List<String> RequiredFeats { get; set; }
        public Int32 RequiredBaseAttack { get; set; }
        public Dictionary<String, Int32> RequiredStats { get; set; }
        public Dictionary<String, Int32> RequiredSkillRanks { get; set; }
        public Boolean IsFighterFeat { get; set; }
        public List<String> RequiredClassNames { get; set; }

        public FeatSelection()
        {
            FeatName = String.Empty;
            RequiredFeats = new List<String>();
            RequiredStats = new Dictionary<String, Int32>();
            RequiredSkillRanks = new Dictionary<String, Int32>();
            RequiredClassNames = new List<String>();
        }

        public Boolean RequirementsMet(IEnumerable<String> feats, Int32 baseAttack, Dictionary<String, Stat> stats,
            Dictionary<String, Skill> skills, String className)
        {
            if (baseAttack < RequiredBaseAttack)
                return false;

            foreach (var stat in RequiredStats)
                if (stats[stat.Key].Value < stat.Value)
                    return false;

            foreach (var skill in RequiredSkillRanks)
                if (skills[skill.Key].EffectiveRanks < skill.Value)
                    return false;

            if (RequiredClassNames.Any() && !RequiredClassNames.Contains(className))
                return false;

            return RequiredFeats.All(f => feats.Contains(f));
        }
    }
}