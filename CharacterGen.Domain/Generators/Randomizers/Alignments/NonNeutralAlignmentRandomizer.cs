using CharacterGen.Alignments;
using CharacterGen.Domain.Selectors;
using CharacterGen.Domain.Selectors.Percentiles;
using System;

namespace CharacterGen.Domain.Generators.Randomizers.Alignments
{
    internal class NonNeutralAlignmentRandomizer : BaseAlignmentRandomizer
    {
        public NonNeutralAlignmentRandomizer(IPercentileSelector innerSelector, Generator generator)
            : base(innerSelector, generator)
        { }

        protected override bool AlignmentIsAllowed(Alignment alignment)
        {
            return alignment.Goodness != AlignmentConstants.Neutral && alignment.Lawfulness != AlignmentConstants.Neutral;
        }
    }
}