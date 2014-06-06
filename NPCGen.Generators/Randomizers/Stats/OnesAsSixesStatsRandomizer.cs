using System;
using System.Collections.Generic;
using System.Linq;
using D20Dice;
using NPCGen.Core.Data.Stats;

namespace NPCGen.Core.Generation.Randomizers.Stats
{
    public class OnesAsSixesStatsRandomizer : BaseStatsRandomizer
    {
        private IDice dice;

        public OnesAsSixesStatsRandomizer(IDice dice)
        {
            this.dice = dice;
        }

        protected override Int32 RollStat()
        {
            var rolls = new List<Int32>();

            for (var i = 0; i < 3; i++)
                rolls.Add(dice.d6());

            return rolls.Sum(r => GetValueToAdd(r));
        }

        private Int32 GetValueToAdd(Int32 roll)
        {
            if (roll == 1)
                return 6;

            return roll;
        }

        protected override Boolean StatsAreAllowed(IEnumerable<Stat> stats)
        {
            return true;
        }
    }
}