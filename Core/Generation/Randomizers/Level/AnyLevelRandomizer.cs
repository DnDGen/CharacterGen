using D20Dice.Dice;
using System;

namespace NPCGen.Core.Generation.Randomizers.Level
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
    }
}