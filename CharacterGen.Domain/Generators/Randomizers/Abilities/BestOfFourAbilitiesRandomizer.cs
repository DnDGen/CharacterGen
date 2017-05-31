using CharacterGen.Abilities;
using RollGen;
using System.Collections.Generic;

namespace CharacterGen.Domain.Generators.Randomizers.Abilities
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

        private Dice dice;

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
    }
}