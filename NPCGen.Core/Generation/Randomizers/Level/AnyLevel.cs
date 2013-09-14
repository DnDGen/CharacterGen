using D20Dice.Dice;
using System;

namespace NPCGen.Core.Generation.Randomizers.Level
{
    public class AnyLevel : ILevelRandomizer
    {
        private IDice dice;

        public AnyLevel(IDice dice)
        {
            this.dice = dice;
        }

        public Int32 Randomize()
        {
            return dice.d20();
        }
    }
}