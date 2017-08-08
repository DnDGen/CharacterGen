using CharacterGen.Abilities;
using DnDGen.Core.Generators;
using RollGen;
using System.Collections.Generic;

namespace CharacterGen.Domain.Generators.Randomizers.Abilities
{
    internal class RawAbilitiesRandomizer : BaseAbilitiesRandomizer
    {
        protected override int defaultValue
        {
            get
            {
                return 10;
            }
        }

        private readonly Dice dice;

        public RawAbilitiesRandomizer(Dice dice, Generator generator)
            : base(generator)
        {
            this.dice = dice;
        }

        protected override int RollAbility()
        {
            return dice.Roll(3).d6().AsSum();
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