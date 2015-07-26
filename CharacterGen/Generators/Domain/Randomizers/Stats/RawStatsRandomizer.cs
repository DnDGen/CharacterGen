using System;
using System.Collections.Generic;
using RollGen;
using CharacterGen.Common.Abilities.Stats;

namespace CharacterGen.Generators.Domain.Randomizers.Stats
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
            return dice.Roll(3).d6();
        }

        protected override Boolean StatsAreAllowed(IEnumerable<Stat> stats)
        {
            return true;
        }
    }
}