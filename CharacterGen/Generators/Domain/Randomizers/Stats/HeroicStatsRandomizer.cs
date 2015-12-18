using CharacterGen.Common.Abilities.Stats;
using RollGen;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Generators.Domain.Randomizers.Stats
{
    public class HeroicStatsRandomizer : BaseStatsRandomizer
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
            var rolls = new List<int>();

            for (var i = 0; i < 3; i++)
            {
                var roll = GetRollToAdd();
                rolls.Add(roll);
            }

            return rolls.Sum();
        }

        private int GetRollToAdd()
        {
            var roll = dice.Roll().d6();

            if (roll == 1)
                return 6;

            return roll;
        }

        protected override bool StatsAreAllowed(IEnumerable<Stat> stats)
        {
            var average = stats.Average(s => s.Value);
            return average >= 16;
        }
    }
}