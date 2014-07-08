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

        public Int32 GetTotalSkillBonus()
        {
            var total = Bonus + BaseStat.Bonus;

            if (ClassSkill)
                return total + Ranks;

            return total + Ranks / 2;
        }
    }
}