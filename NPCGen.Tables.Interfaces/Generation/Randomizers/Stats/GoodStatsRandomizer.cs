using System;
using System.Collections.Generic;
using System.Linq;
using D20Dice;
using NPCGen.Core.Data.Stats;

namespace NPCGen.Core.Generation.Randomizers.Stats
{
    public class GoodStatsRandomizer : BaseStatsRandomizer
    {
        private IDice dice;

        public GoodStatsRandomizer(IDice dice)
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
            return average >= 13 && average <= 15;
        }
    }
}