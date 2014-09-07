using System;
using D20Dice;
using NPCGen.Common.Alignments;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Generators.Randomizers.Alignments
{
    public class GoodAlignmentRandomizer : BaseAlignmentRandomizer
    {
        public GoodAlignmentRandomizer(IDice dice, IPercentileSelector Selector) : base(dice, Selector) { }

        protected override Boolean AlignmentIsAllowed(Alignment alignment)
        {
            return alignment.Goodness == AlignmentConstants.Good;
        }
    }
}