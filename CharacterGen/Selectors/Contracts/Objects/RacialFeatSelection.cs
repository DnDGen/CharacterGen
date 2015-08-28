using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.Races;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Selectors.Objects
{
    public class RacialFeatSelection
    {
        public String Feat { get; set; }
        public Int32 MinimumHitDieRequirement { get; set; }
        public Int32 MaximumHitDieRequirement { get; set; }
        public String SizeRequirement { get; set; }
        public Frequency Frequency { get; set; }
        public String FocusType { get; set; }
        public Int32 Strength { get; set; }
        public Dictionary<String, Int32> MinimumStats { get; set; }

        public RacialFeatSelection()
        {
            Feat = String.Empty;
            SizeRequirement = String.Empty;
            Frequency = new Frequency();
            FocusType = String.Empty;
            MinimumStats = new Dictionary<String, Int32>();
        }

        public Boolean RequirementsMet(Race race, Int32 monsterHitDice, Dictionary<String, Stat> stats)
        {
            if (String.IsNullOrEmpty(SizeRequirement) == false && SizeRequirement != race.Size)
                return false;

            if (MaximumHitDieRequirement > 0 && monsterHitDice > MaximumHitDieRequirement)
                return false;

            if (MinimumStatMet(stats) == false)
                return false;

            return monsterHitDice >= MinimumHitDieRequirement;
        }

        private Boolean MinimumStatMet(Dictionary<String, Stat> stats)
        {
            if (MinimumStats.Any() == false)
                return true;

            foreach (var stat in MinimumStats)
                if (stats[stat.Key].Value >= stat.Value)
                    return true;

            return false;
        }
    }
}