using CharacterGen.Abilities.Stats;
using RollGen;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Domain.Generators.Randomizers.Stats
{
    internal class HeroicStatsRandomizer : BaseStatsRandomizer
    {
        protected override int defaultValue
        {
            get
            {
                return 16;
            }
        }

        private Dice dice;

        public HeroicStatsRandomizer(Dice dice, Generator generator)
            : base(generator)
        {
            this.dice = dice;
        }

        protected override int RollStat()
        {
            var rawRolls = dice.Roll(3).d(6).AsIndividualRolls();
            var validRolls = rawRolls.Where(r => r > 1);

            var rolls = new List<int>();
            rolls.AddRange(validRolls);

            while (rolls.Count < 3)
                rolls.Add(6);

            return rolls.Sum();
        }

        protected override bool StatsAreAllowed(IEnumerable<Stat> stats)
        {
            var average = stats.Average(s => s.Value);
            return average >= 16;
        }
    }
}