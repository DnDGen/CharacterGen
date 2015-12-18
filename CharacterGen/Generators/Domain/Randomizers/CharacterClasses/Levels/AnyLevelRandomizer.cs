using CharacterGen.Generators.Randomizers.CharacterClasses;
using RollGen;
using System.Collections.Generic;

namespace CharacterGen.Generators.Domain.Randomizers.CharacterClasses.Levels
{
    public class AnyLevelRandomizer : ILevelRandomizer
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
            var levels = new List<int>();

            for (var level = 1; level <= 20; level++)
                levels.Add(level);

            return levels;
        }
    }
}