using System;
using System.Collections.Generic;
using System.Linq;
using D20Dice;
using NPCGen.Common.Abilities.Stats;

namespace NPCGen.Generators.Randomizers.Stats
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
            {
                var roll = dice.Roll().d6();
                rolls.Add(roll);
            }

            var lowest = rolls.Min();
            rolls.Remove(lowest);

            return rolls.Sum();
        }

        protected override Boolean StatsAreAllowed(IEnumerable<Stat> stats)
        {
            return true;
        }
    }
}