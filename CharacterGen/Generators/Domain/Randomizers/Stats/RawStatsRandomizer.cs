using CharacterGen.Common.Abilities.Stats;
using RollGen;
using System;
using System.Collections.Generic;

namespace CharacterGen.Generators.Domain.Randomizers.Stats
{
    public class RawStatsRandomizer : BaseStatsRandomizer
    {
        protected override Int32 defaultValue
        {
            get
            {
                return 10;
            }
        }

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