using System;
using D20Dice.Dice;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Interfaces;

namespace NPCGen.Core.Generation.Randomizers.Levels
{
    public abstract class RangedLevelRandomizer : ILevelRandomizer
    {
        protected Int32 rollBonus;
        private IDice dice;

        public RangedLevelRandomizer(IDice dice)
        {
            this.dice = dice;
        }

        public Int32 Randomize()
        {
            var roll = dice.d6();

            while (roll == 6)
                roll = dice.d6();

            return roll + rollBonus;
        }
    }
}