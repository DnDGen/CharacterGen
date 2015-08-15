using CharacterGen.Common.Alignments;
using CharacterGen.Selectors;
using System;

namespace CharacterGen.Generators.Domain.Randomizers.Alignments
{
    public class AnyAlignmentRandomizer : BaseAlignmentRandomizer
    {
        public AnyAlignmentRandomizer(IPercentileSelector innerSelector)
            : base(innerSelector)
        { }

        protected override Boolean AlignmentIsAllowed(Alignment alignment)
        {
            return true;
        }
    }
}