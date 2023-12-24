using DnDGen.CharacterGen.Abilities;
using DnDGen.Infrastructure.Generators;
using DnDGen.RollGen;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CharacterGen.Generators.Randomizers.Abilities
{
    internal class PoorAbilitiesRandomizer : BaseAbilitiesRandomizer
    {
        protected override int defaultValue
        {
            get
            {
                return 9;
            }
        }

        private readonly Dice dice;

        public PoorAbilitiesRandomizer(Dice dice, Generator generator)
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
            var average = stats.Average(s => s.Value);
            return average < 10;
        }

        protected override string AbilitiesInvalidMessage(IEnumerable<Ability> abilities)
        {
            var average = abilities.Average(s => s.Value);
            return $"Average ability score of {average} is not less than 10";
        }
    }
}