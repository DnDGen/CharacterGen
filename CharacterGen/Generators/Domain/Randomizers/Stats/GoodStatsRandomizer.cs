using CharacterGen.Common.Abilities.Stats;
using RollGen;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Generators.Domain.Randomizers.Stats
{
    public class GoodStatsRandomizer : BaseStatsRandomizer
    {
        protected override int defaultValue
        {
            get
            {
                return 13;
            }
        }

        private Dice dice;

        public GoodStatsRandomizer(Dice dice, Generator generator)
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
            return average >= 13 && average <= 15;
        }
    }
}