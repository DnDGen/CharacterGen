using System;
using System.Collections.Generic;
using D20Dice;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Interfaces;

namespace NPCGen.Core.Generation.Randomizers.CharacterClasses.Levels
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
            return dice.d20();
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