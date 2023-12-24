using DnDGen.CharacterGen.Abilities;
using DnDGen.Infrastructure.Generators;
using DnDGen.RollGen;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CharacterGen.Generators.Randomizers.Abilities
{
    internal class HeroicAbilitiesRandomizer : BaseAbilitiesRandomizer
    {
        protected override int defaultValue
        {
            get
            {
                return 16;
            }
        }

        private readonly Dice dice;

        public HeroicAbilitiesRandomizer(Dice dice, Generator generator)
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
            var average = stats.Average(s => s.Value);
            return average >= 16;
        }

        protected override string AbilitiesInvalidMessage(IEnumerable<Ability> abilities)
        {
            var average = abilities.Average(s => s.Value);
            return $"Average ability score of {average} is not at least 16";
        }
    }
}