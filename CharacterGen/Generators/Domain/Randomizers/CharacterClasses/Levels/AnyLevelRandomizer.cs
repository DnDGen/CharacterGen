using System;
using System.Collections.Generic;
using RollGen;
using CharacterGen.Generators.Randomizers.CharacterClasses;

namespace CharacterGen.Generators.Domain.Randomizers.CharacterClasses.Levels
{
    public class AnyLevelRandomizer : ILevelRandomizer
    {
        private IDice dice;

        public AnyLevelRandomizer(IDice dice)
        {
            this.dice = dice;
        }

        public Int32 Randomize()
        {
            return dice.Roll().d20();
        }

        public IEnumerable<Int32> GetAllPossibleResults()
        {
            var levels = new List<Int32>();

            for (var level = 1; level <= 20; level++)
                levels.Add(level);

            return levels;
        }
    }
}