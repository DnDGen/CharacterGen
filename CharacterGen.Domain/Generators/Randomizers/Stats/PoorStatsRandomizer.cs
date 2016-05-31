using CharacterGen.Abilities.Stats;
using RollGen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Domain.Generators.Randomizers.Stats
{
    internal class PoorStatsRandomizer : BaseStatsRandomizer
    {
        protected override Int32 defaultValue
        {
            get
            {
                return 9;
            }
        }

        private Dice dice;

        public PoorStatsRandomizer(Dice dice, Generator generator)
            : base(generator)
        {
            this.dice = dice;
        }

        protected override int RollStat()
        {
            return dice.Roll(3).d6();
        }

        protected override bool StatsAreAllowed(IEnumerable<Stat> stats)
        {
            var average = stats.Average(s => s.Value);
            return average < 10;
        }
    }
}