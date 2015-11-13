using CharacterGen.Common.Alignments;
using CharacterGen.Selectors;
using System;

namespace CharacterGen.Generators.Domain.Randomizers.Alignments
{
    public class NonChaoticAlignmentRandomizer : BaseAlignmentRandomizer
    {
        public NonChaoticAlignmentRandomizer(IPercentileSelector innerSelector, Generator generator)
            : base(innerSelector, generator)
        { }

        protected override Boolean AlignmentIsAllowed(Alignment alignment)
        {
            return alignment.Lawfulness != AlignmentConstants.Chaotic;
        }
    }
}