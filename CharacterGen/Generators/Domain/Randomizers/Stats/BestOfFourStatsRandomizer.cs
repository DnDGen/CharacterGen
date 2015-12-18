using CharacterGen.Common.Abilities.Stats;
using RollGen;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Generators.Domain.Randomizers.Stats
{
    public class BestOfFourStatsRandomizer : BaseStatsRandomizer
    {
        protected override int defaultValue
        {
            get
            {
                return 10;
            }
        }

        private Dice dice;

        public BestOfFourStatsRandomizer(Dice dice, Generator generator)
            : base(generator)
        {
            this.dice = dice;
        }

        protected override int RollStat()
        {
            var rolls = new List<int>();

            for (var i = 0; i < 4; i++)
            {
                var roll = dice.Roll().d6();
                rolls.Add(roll);
            }

            var lowest = rolls.Min();
            rolls.Remove(lowest);

            return rolls.Sum();
        }

        protected override bool StatsAreAllowed(IEnumerable<Stat> stats)
        {
            return true;
        }
    }
}