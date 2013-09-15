using NPCGen.Core.Data.Stats;
using System;

namespace NPCGen.Core.Data
{
    public class Skill
    {
        public String Name { get; set; }
        public Int32 Ranks { get; set; }
        public Stat BaseStat { get; set; }
        public Int32 FeatBonus { get; set; }
        public Boolean CanLearn { get; set; }
        public Boolean ClassSkill { get; set; }
    }
}