using CharacterGen.Common.Alignments;
using CharacterGen.Selectors;
using System;

namespace CharacterGen.Generators.Domain.Randomizers.Alignments
{
    public class EvilAlignmentRandomizer : BaseAlignmentRandomizer
    {
        public EvilAlignmentRandomizer(IPercentileSelector innerSelector, Generator generator)
            : base(innerSelector, generator)
        { }

        protected override Boolean AlignmentIsAllowed(Alignment alignment)
        {
            return alignment.Goodness == AlignmentConstants.Evil;
        }
    }
}