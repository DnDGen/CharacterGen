using CharacterGen.Common.Abilities.Stats;
using RollGen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Generators.Domain.Randomizers.Stats
{
    public class GoodStatsRandomizer : BaseStatsRandomizer
    {
        protected override Int32 defaultValue
        {
            get
            {
                return 13;
            }
        }

        private IDice dice;

        public GoodStatsRandomizer(IDice dice)
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
            return average >= 13 && average <= 15;
        }
    }
}