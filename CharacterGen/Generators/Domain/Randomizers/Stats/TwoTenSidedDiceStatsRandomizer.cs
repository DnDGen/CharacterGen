using System;
using System.Collections.Generic;
using RollGen;
using CharacterGen.Common.Abilities.Stats;

namespace CharacterGen.Generators.Domain.Randomizers.Stats
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
            return dice.Roll(2).d10();
        }

        protected override Boolean StatsAreAllowed(IEnumerable<Stat> stats)
        {
            return true;
        }
    }
}