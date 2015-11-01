﻿using CharacterGen.Common.Abilities.Stats;
using RollGen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Generators.Domain.Randomizers.Stats
{
    public class OnesAsSixesStatsRandomizer : BaseStatsRandomizer
    {
        protected override Int32 defaultValue
        {
            get
            {
                return 10;
            }
        }

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