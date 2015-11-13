using CharacterGen.Common.Abilities.Stats;
using RollGen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Generators.Domain.Randomizers.Stats
{
    public class BestOfFourStatsRandomizer : BaseStatsRandomizer
    {
        protected override Int32 defaultValue
        {
            get
            {
                return 10;
            }
        }

        private IDice dice;

        public BestOfFourStatsRandomizer(IDice dice, Generator generator)
            : base (generator)
        {
            this.dice = dice;
        }

        protected override Int32 RollStat()
        {
            var rolls = new List<Int32>();

            for (var i = 0; i < 4; i++)
            {
                var roll = dice.Roll().d6();
                rolls.Add(roll);
            }

            var lowest = rolls.Min();
            rolls.Remove(lowest);

            return rolls.Sum();
        }

        protected override Boolean StatsAreAllowed(IEnumerable<Stat> stats)
        {
            return true;
        }
    }
}