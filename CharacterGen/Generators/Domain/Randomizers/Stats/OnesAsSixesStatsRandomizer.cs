using System;
using System.Collections.Generic;
using System.Linq;
using RollGen;
using CharacterGen.Common.Abilities.Stats;

namespace CharacterGen.Generators.Domain.Randomizers.Stats
{
    public class OnesAsSixesStatsRandomizer : BaseStatsRandomizer
    {
        private IDice dice;

        public OnesAsSixesStatsRandomizer(IDice dice)
        {
            this.dice = dice;
        }

        protected override Int32 RollStat()
        {
            var rolls = new List<Int32>();

            for (var i = 0; i < 3; i++)
            {
                var roll = GetRollToAdd();
                rolls.Add(roll);
            }

            return rolls.Sum();
        }

        private Int32 GetRollToAdd()
        {
            var roll = dice.Roll().d6();

            if (roll == 1)
                return 6;

            return roll;
        }

        protected override Boolean StatsAreAllowed(IEnumerable<Stat> stats)
        {
            return true;
        }
    }
}