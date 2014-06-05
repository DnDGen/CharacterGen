using System;
using System.Collections.Generic;
using System.Linq;
using D20Dice;
using NPCGen.Core.Data.Stats;

namespace NPCGen.Core.Generation.Randomizers.Stats
{
    public class BestOfFourStatsRandomizer : BaseStatsRandomizer
    {
        private IDice dice;

        public BestOfFourStatsRandomizer(IDice dice)
        {
            this.dice = dice;
        }

        protected override Int32 RollStat()
        {
            var rolls = new List<Int32>();

            for (var i = 0; i < 4; i++)
                rolls.Add(dice.d6());

            var lowest = rolls.Min();
            var total = rolls.Sum();

            return total - lowest;
        }

        protected override Boolean StatsAreAllowed(IEnumerable<Stat> stats)
        {
            return true;
        }
    }
}