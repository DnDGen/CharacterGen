using CharacterGen.Alignments;
using CharacterGen.Domain.Selectors.Percentiles;

namespace CharacterGen.Domain.Generators.Randomizers.Alignments
{
    internal class ChaoticAlignmentRandomizer : BaseAlignmentRandomizer
    {
        public ChaoticAlignmentRandomizer(IPercentileSelector innerSelector, Generator generator)
            : base(innerSelector, generator)
        { }

        protected override bool AlignmentIsAllowed(Alignment alignment)
        {
            return alignment.Lawfulness == AlignmentConstants.Chaotic;
        }
    }
}