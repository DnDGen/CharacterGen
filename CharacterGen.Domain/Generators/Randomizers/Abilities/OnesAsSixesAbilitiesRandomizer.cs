﻿using CharacterGen.Abilities;
using DnDGen.Core.Generators;
using RollGen;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Domain.Generators.Randomizers.Abilities
{
    internal class OnesAsSixesAbilitiesRandomizer : BaseAbilitiesRandomizer
    {
        protected override int defaultValue
        {
            get
            {
                return 10;
            }
        }

        private readonly Dice dice;

        public OnesAsSixesAbilitiesRandomizer(Dice dice, Generator generator)
            : base(generator)
        {
            this.dice = dice;
        }

        protected override int RollAbility()
        {
            var rawRolls = dice.Roll(3).d(6).AsIndividualRolls();
            var validRolls = rawRolls.Where(r => r > 1);

            var rolls = new List<int>();
            rolls.AddRange(validRolls);

            while (rolls.Count < 3)
                rolls.Add(6);

            return rolls.Sum();
        }

        protected override bool AbilitiesAreAllowed(IEnumerable<Ability> stats)
        {
            return true;
        }

        protected override string AbilitiesInvalidMessage(IEnumerable<Ability> abilities)
        {
            return string.Empty;
        }
    }
}