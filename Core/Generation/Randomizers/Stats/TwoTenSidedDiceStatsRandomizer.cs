using System;
using System.Collections.Generic;
using D20Dice;
using NPCGen.Core.Data.Stats;

namespace NPCGen.Core.Generation.Randomizers.Stats
{
    public class TwoTenSidedDiceStatsRandomizer : BaseStatsRandomizer
    {
        private IDice dice;

        public TwoTenSidedDiceStatsRandomizer(IDice dice)
        {
            this.dice = dice;
        }

        protected override Int32 RollStat()
        {
            return dice.d10(2);
        }

        protected override Boolean StatsAreAllowed(IEnumerable<Stat> stats)
        {
            return true;
        }
    }
}