using CharacterGen.Abilities;
using RollGen;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Domain.Generators.Randomizers.Abilities
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

        private Dice dice;

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
    }
}