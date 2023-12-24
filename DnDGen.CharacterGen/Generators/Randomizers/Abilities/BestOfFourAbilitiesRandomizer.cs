using DnDGen.CharacterGen.Abilities;
using DnDGen.Infrastructure.Generators;
using DnDGen.RollGen;
using System.Collections.Generic;

namespace DnDGen.CharacterGen.Generators.Randomizers.Abilities
{
    internal class BestOfFourAbilitiesRandomizer : BaseAbilitiesRandomizer
    {
        protected override int defaultValue
        {
            get
            {
                return 10;
            }
        }

        private readonly Dice dice;

        public BestOfFourAbilitiesRandomizer(Dice dice, Generator generator)
            : base(generator)
        {
            this.dice = dice;
        }

        protected override int RollAbility()
        {
            return dice.Roll("4d6k3").AsSum();
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