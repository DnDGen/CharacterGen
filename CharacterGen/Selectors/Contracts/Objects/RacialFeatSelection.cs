using System;
using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Races;

namespace CharacterGen.Selectors.Objects
{
    public class RacialFeatSelection
    {
        public String Feat { get; set; }
        public Int32 MinimumHitDieRequirement { get; set; }
        public String SizeRequirement { get; set; }
        public Frequency Frequency { get; set; }
        public String FocusType { get; set; }
        public Int32 Strength { get; set; }

        public RacialFeatSelection()
        {
            Feat = String.Empty;
            SizeRequirement = String.Empty;
            Frequency = new Frequency();
            FocusType = String.Empty;
        }

        public Boolean RequirementsMet(Race race, Int32 monsterHitDice)
        {
            if (String.IsNullOrEmpty(SizeRequirement) == false && SizeRequirement != race.Size)
                return false;

            return monsterHitDice >= MinimumHitDieRequirement;
        }
    }
}