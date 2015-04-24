using System;
using System.Linq;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Races;

namespace NPCGen.Selectors.Interfaces.Objects
{
    public class RacialFeatSelection
    {
        public String FeatId { get; set; }
        public Int32 MinimumHitDieRequirement { get; set; }
        public String SizeRequirement { get; set; }
        public Frequency Frequency { get; set; }
        public String Focus { get; set; }

        public RacialFeatSelection()
        {
            FeatId = String.Empty;
            SizeRequirement = String.Empty;
            Frequency = new Frequency();
            Focus = String.Empty;
        }

        public Boolean RequirementsMet(Race race, Int32 monsterHitDice)
        {
            if (monsterHitDice < MinimumHitDieRequirement)
                return false;

            if (SizeRequirement.Any() && SizeRequirement != race.Size)
                return false;

            return true;
        }
    }
}