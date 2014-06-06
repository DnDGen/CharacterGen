using System;
using System.Collections.Generic;
using System.Linq;
using D20Dice;
using NPCGen.Core.Data.Stats;

namespace NPCGen.Core.Generation.Randomizers.Stats
{
    public class AverageStatsRandomizer : BaseStatsRandomizer
    {
        private IDice dice;

        public AverageStatsRandomizer(IDice dice)
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
            return average >= 10 && average <= 12;
        }
    }
}