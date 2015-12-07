using CharacterGen.Common.Abilities.Stats;
using System;

namespace CharacterGen.Common.Abilities.Skills
{
    public class Skill
    {
        public Int32 Ranks { get; set; }
        public Stat BaseStat { get; set; }
        public Int32 Bonus { get; set; }
        public Boolean ClassSkill { get; set; }
        public Int32 ArmorCheckPenalty { get; set; }
        public Boolean CircumstantialBonus { get; set; }
        public Double EffectiveRanks
        {
            get
            {
                if (ClassSkill)
                    return Ranks;

                return (Double)Ranks / 2;
            }
        }
    }
}