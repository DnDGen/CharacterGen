using System;
using NPCGen.Common.Stats;

namespace NPCGen.Common.Skills
{
    public class Skill
    {
        public Int32 Ranks { get; set; }
        public Stat BaseStat { get; set; }
        public Int32 Bonus { get; set; }
        public Boolean ClassSkill { get; set; }
        public Boolean ArmorCheckPenalty { get; set; }
        public Int32 TotalSkillBonus
        {
            get
            {
                var total = Bonus + BaseStat.Bonus;

                if (ClassSkill)
                    return total + Ranks;

                return total + Ranks / 2;
            }
        }

        public Skill()
        {
            BaseStat = new Stat();
        }
    }
}