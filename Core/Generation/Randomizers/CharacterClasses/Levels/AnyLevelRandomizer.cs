using System;
using D20Dice.Dice;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Interfaces;

namespace NPCGen.Core.Generation.Randomizers.Levels
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