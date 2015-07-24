using System;
using D20Dice;
using NPCGen.Common.Alignments;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Generators.Randomizers.Alignments
{
    public class AnyAlignmentRandomizer : BaseAlignmentRandomizer
    {
        public AnyAlignmentRandomizer(IDice dice, IPercentileSelector Selector) : base(dice, Selector) { }

        protected override Boolean AlignmentIsAllowed(Alignment alignment)
        {
            return true;
        }
    }
}