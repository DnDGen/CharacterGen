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

        public override String ToString()
        {
            var rankBonus = GetBonus(Ranks, "Ranks");
            var baseStatBonus = GetBonus(BaseStat.Bonus, BaseStat.Name);
            var featBonus = GetBonus(FeatBonus, "Feat");

            return String.Format("{0}: {1} {2} {3}", Name, rankBonus, baseStatBonus, featBonus);
        }

        private String GetBonus(Int32 bonus, String title)
        {
            return String.Format("{0}{1} ({2})", ShowBonus(bonus), bonus, title);
        }

        private String ShowBonus(Int32 bonus)
        {
            if (bonus >= 0)
                return "+";

            return String.Empty;

        }
    }
}