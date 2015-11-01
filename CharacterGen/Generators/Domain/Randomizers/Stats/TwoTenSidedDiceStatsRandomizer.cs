using CharacterGen.Common.Abilities.Stats;
using RollGen;
using System;
using System.Collections.Generic;

namespace CharacterGen.Generators.Domain.Randomizers.Stats
{
    public class TwoTenSidedDiceStatsRandomizer : BaseStatsRandomizer
    {
        protected override Int32 defaultValue
        {
            get
            {
                return 10;
            }
        }

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