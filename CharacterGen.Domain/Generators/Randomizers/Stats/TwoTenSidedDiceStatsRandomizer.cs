using CharacterGen.Abilities.Stats;
using RollGen;
using System;
using System.Collections.Generic;

namespace CharacterGen.Domain.Generators.Randomizers.Stats
{
    internal class TwoTenSidedDiceStatsRandomizer : BaseStatsRandomizer
    {
        protected override Int32 defaultValue
        {
            get
            {
                return 10;
            }
        }

        private Dice dice;

        public TwoTenSidedDiceStatsRandomizer(Dice dice, Generator generator)
            : base(generator)
        {
            this.dice = dice;
        }

        protected override int RollStat()
        {
            return dice.Roll(2).d10();
        }

        protected override bool StatsAreAllowed(IEnumerable<Stat> stats)
        {
            return true;
        }
    }
}