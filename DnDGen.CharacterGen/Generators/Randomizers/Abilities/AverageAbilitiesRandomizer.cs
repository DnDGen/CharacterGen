using DnDGen.CharacterGen.Abilities;
using DnDGen.Infrastructure.Generators;
using DnDGen.RollGen;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CharacterGen.Generators.Randomizers.Abilities
{
    internal class AverageAbilitiesRandomizer : BaseAbilitiesRandomizer
    {
        protected override int defaultValue
        {
            get
            {
                return 10;
            }
        }

        private readonly Dice dice;

        public AverageAbilitiesRandomizer(Dice dice, Generator generator)
            : base(generator)
        {
            this.dice = dice;
        }

        protected override int RollAbility()
        {
            return dice.Roll(3).d6().AsSum();
        }

        protected override bool AbilitiesAreAllowed(IEnumerable<Ability> abilities)
        {
            var average = abilities.Average(s => s.Value);
            return average >= 10 && average <= 12;
        }

        protected override string AbilitiesInvalidMessage(IEnumerable<Ability> abilities)
        {
            var average = abilities.Average(s => s.Value);
            return $"Average ability score of {average} is not between 10 and 12";
        }
    }
}