using System;
using RollGen;
using CharacterGen.Common.Alignments;
using CharacterGen.Selectors;

namespace CharacterGen.Generators.Domain.Randomizers.Alignments
{
    public class NonNeutralAlignmentRandomizer : BaseAlignmentRandomizer
    {
        public NonNeutralAlignmentRandomizer(IDice dice, IPercentileSelector Selector) : base(dice, Selector) { }

        protected override Boolean AlignmentIsAllowed(Alignment alignment)
        {
            return alignment.Goodness != AlignmentConstants.Neutral && alignment.Lawfulness != AlignmentConstants.Neutral;
        }
    }
}