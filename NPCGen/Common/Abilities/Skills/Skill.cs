using System;
using NPCGen.Common.Abilities.Stats;

namespace NPCGen.Common.Abilities.Skills
{
    public class Skill
    {
        public Int32 Ranks { get; set; }
        public Stat BaseStat { get; set; }
        public Int32 Bonus { get; set; }
        public Boolean ClassSkill { get; set; }
        public Boolean ArmorCheckPenalty { get; set; }
        public Double EffectiveRanks
        {
            get
            {
                if (ClassSkill)
                    return Ranks;

                return (Double)Ranks / 2;
            }
        }

        public Int32 GetTotalSkillBonus()
        {
            var roundedEffectiveRanks = Math.Floor(EffectiveRanks);
            return Bonus + BaseStat.Bonus + Convert.ToInt32(roundedEffectiveRanks);
        }
    }
}