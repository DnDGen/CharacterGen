using System;
using System.Collections.Generic;
using D20Dice;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Interfaces;

namespace NPCGen.Core.Generation.Randomizers.CharacterClasses.Levels
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
            var roll = Roll1to5();
            return roll + rollBonus;
        }

        private Int32 Roll1to5()
        {
            Double roll = dice.d10();
            return Convert.ToInt32(Math.Ceiling(roll / 2));
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