using CharacterGen.Abilities.Stats;
using RollGen;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Domain.Generators.Randomizers.Stats
{
    internal class OnesAsSixesStatsRandomizer : BaseStatsRandomizer
    {
        protected override int defaultValue
        {
            get
            {
                return 10;
            }
        }

        private Dice dice;

        public OnesAsSixesStatsRandomizer(Dice dice, Generator generator)
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
            return true;
        }
    }
}