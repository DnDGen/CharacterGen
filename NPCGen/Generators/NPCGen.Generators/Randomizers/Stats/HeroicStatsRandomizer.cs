using System;
using System.Collections.Generic;
using System.Linq;
using D20Dice;
using NPCGen.Common.Abilities.Stats;

namespace NPCGen.Generators.Randomizers.Stats
{
    public class HeroicStatsRandomizer : BaseStatsRandomizer
    {
        private IDice dice;

        public HeroicStatsRandomizer(IDice dice)
        {
            this.dice = dice;
        }

        protected override Int32 RollStat()
        {
            return dice.d6(3);
        }

        protected override Boolean StatsAreAllowed(IEnumerable<Stat> stats)
        {
            var average = stats.Average(s => s.Value);
            return average >= 16;
        }
    }
}