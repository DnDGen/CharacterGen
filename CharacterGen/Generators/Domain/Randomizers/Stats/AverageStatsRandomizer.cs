using System;
using System.Collections.Generic;
using System.Linq;
using RollGen;
using CharacterGen.Common.Abilities.Stats;

namespace CharacterGen.Generators.Domain.Randomizers.Stats
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
            return dice.Roll(3).d6();
        }

        protected override Boolean StatsAreAllowed(IEnumerable<Stat> stats)
        {
            var average = stats.Average(s => s.Value);
            return average >= 10 && average <= 12;
        }
    }
}