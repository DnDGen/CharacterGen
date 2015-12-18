using CharacterGen.Generators.Randomizers.CharacterClasses;
using RollGen;
using System;
using System.Collections.Generic;

namespace CharacterGen.Generators.Domain.Randomizers.CharacterClasses.Levels
{
    public abstract class RangedLevelRandomizer : ILevelRandomizer
    {
        protected Int32 rollBonus;
        private Dice dice;

        public RangedLevelRandomizer(Dice dice)
        {
            this.dice = dice;
        }

        public int Randomize()
        {
            var roll = dice.Roll().d(5);
            return roll + rollBonus;
        }

        public IEnumerable<int> GetAllPossibleResults()
        {
            var levels = new List<int>();

            for (var roll = 1; roll <= 5; roll++)
                levels.Add(roll + rollBonus);

            return levels;
        }
    }
}