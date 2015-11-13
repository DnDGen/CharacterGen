using CharacterGen.Common.Alignments;
using CharacterGen.Selectors;
using System;

namespace CharacterGen.Generators.Domain.Randomizers.Alignments
{
    public class NonEvilAlignmentRandomizer : BaseAlignmentRandomizer
    {
        public NonEvilAlignmentRandomizer(IPercentileSelector innerSelector, Generator generator)
            : base(innerSelector, generator)
        { }

        protected override Boolean AlignmentIsAllowed(Alignment alignment)
        {
            return alignment.Goodness != AlignmentConstants.Evil;
        }
    }
}