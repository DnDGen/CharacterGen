using System;
using System.Collections.Generic;
using D20Dice.Dice;
using NPCGen.Core.Data.Stats;

namespace NPCGen.Core.Generation.Randomizers.Stats
{
    public class RawStatsRandomizer : BaseStatsRandomizer
    {
        private IDice dice;

        public RawStatsRandomizer(IDice dice)
        {
            this.dice = dice;
        }

        protected override Int32 RollStat()
        {
            return dice.d6(3, 0);
        }

        protected override Boolean StatsAreAllowed(IEnumerable<Stat> stats)
        {
            return true;
        }
    }
}