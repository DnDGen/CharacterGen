using System;
using System.Collections.Generic;
using D20Dice;
using NPCGen.Generators.Interfaces.Randomizers.CharacterClasses;

namespace NPCGen.Generators.Randomizers.CharacterClasses.Levels
{
    public abstract class RangedLevel : ILevelRandomizer
    {
        protected Int32 rollBonus;
        private IDice dice;

        public RangedLevel(IDice dice)
        {
            this.dice = dice;
        }

        public Int32 Randomize()
        {
            var roll = dice.Roll().d(5);
            return roll + rollBonus;
        }

        public IEnumerable<Int32> GetAllPossibleResults()
        {
            var levels = new List<Int32>();

            for (var roll = 1; roll <= 5; roll++)
                levels.Add(roll + rollBonus);

            return levels;
        }
    }
}