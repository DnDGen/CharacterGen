using DnDGen.CharacterGen.Abilities;
using DnDGen.Infrastructure.Generators;
using DnDGen.RollGen;
using System.Collections.Generic;

namespace DnDGen.CharacterGen.Generators.Randomizers.Abilities
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