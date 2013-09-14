using D20Dice.Dice;
using NPCGen.Core.Data.Alignments;
using System;

namespace NPCGen.Core.Generation.Randomizers.Alignments
{
    public abstract class BaseAlignmentRandomizer : IAlignmentRandomizer
    {
        private IDice dice;

        public BaseAlignmentRandomizer(IDice dice)
        {
            this.dice = dice;
        }

        public abstract Alignment Randomize();

        protected Int32 RollLawfulness()
        {
            var roll = dice.d3();

            if (roll == 1)
                return AlignmentConstants.Chaotic;
            else if (roll == 2)
                return AlignmentConstants.Neutral;

            return AlignmentConstants.Lawful;
        }

        protected Int32 RollGoodness()
        {
            var roll = dice.Percentile();

            if (roll <= 20)
                return AlignmentConstants.Good;
            else if (roll <= 50)
                return AlignmentConstants.Neutral;

            return AlignmentConstants.Evil;
        }
    }
}