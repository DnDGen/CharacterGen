using CharacterGen.Abilities.Stats;
using System;

namespace CharacterGen.Abilities.Skills
{
    public class Skill
    {
        public Stat BaseStat { get; set; }
        public int Bonus { get; set; }
        public bool ClassSkill { get; set; }
        public int ArmorCheckPenalty { get; set; }
        public bool CircumstantialBonus { get; set; }
        public int RankCap { get; set; }

        public double EffectiveRanks
        {
            get
            {
                if (ClassSkill)
                    return ranks;

                return (double)ranks / 2;
            }
        }

        public bool RanksMaxedOut
        {
            get
            {
                return Ranks == RankCap;
            }
        }

        private int ranks;

        public int Ranks
        {
            get
            {
                return ranks;
            }
            set
            {
                if (value > RankCap)
                    throw new InvalidOperationException("Ranks cannot exceed the Rank Cap");

                ranks = value;
            }
        }

        public bool QualifiesForSkillSynergy
        {
            get
            {
                return EffectiveRanks >= 5;
            }
        }

        public Skill(int rankCap)
        {
            RankCap = rankCap;
        }
    }
}