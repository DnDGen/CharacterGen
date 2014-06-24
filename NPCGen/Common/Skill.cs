using System;
using NPCGen.Common.Stats;

namespace NPCGen.Common
{
    public class Skill
    {
        public String Name { get; set; }
        public Int32 Ranks { get; set; }
        public Stat BaseStat { get; set; }
        public Int32 FeatBonus { get; set; }
        public Boolean CanLearn { get; set; }
        public Boolean ClassSkill { get; set; }

        public Skill()
        {
            Name = String.Empty;
            BaseStat = new Stat();
        }
    }
}