using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.Races;
using System;
using System.Collections.Generic;

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
        public String RequiredStat { get; set; }
        public Int32 RequiredStatMinimumValue { get; set; }

        public RacialFeatSelection()
        {
            Feat = String.Empty;
            SizeRequirement = String.Empty;
            Frequency = new Frequency();
            FocusType = String.Empty;
            RequiredStat = String.Empty;
        }

        public Boolean RequirementsMet(Race race, Int32 monsterHitDice, Dictionary<String, Stat> stats)
        {
            if (String.IsNullOrEmpty(SizeRequirement) == false && SizeRequirement != race.Size)
                return false;

            if (MaximumHitDieRequirement > 0 && monsterHitDice > MaximumHitDieRequirement)
                return false;

            if (String.IsNullOrEmpty(RequiredStat) == false && RequiredStatMinimumValue > stats[RequiredStat].Value)
                return false;

            return monsterHitDice >= MinimumHitDieRequirement;
        }
    }
}