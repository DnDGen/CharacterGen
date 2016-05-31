using CharacterGen.Randomizers.CharacterClasses;
using RollGen;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Domain.Generators.Randomizers.CharacterClasses.Levels
{
    internal class AnyLevelRandomizer : ILevelRandomizer
    {
        private Dice dice;

        public AnyLevelRandomizer(Dice dice)
        {
            this.dice = dice;
        }

        public int Randomize()
        {
            return dice.Roll().d20();
        }

        public IEnumerable<int> GetAllPossibleResults()
        {
            return Enumerable.Range(1, 20);
        }
    }
}