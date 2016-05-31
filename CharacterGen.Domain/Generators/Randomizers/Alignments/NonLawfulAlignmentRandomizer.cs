using CharacterGen.Alignments;
using CharacterGen.Domain.Selectors.Percentiles;

namespace CharacterGen.Domain.Generators.Randomizers.Alignments
{
    internal class NonLawfulAlignmentRandomizer : BaseAlignmentRandomizer
    {
        public NonLawfulAlignmentRandomizer(IPercentileSelector innerSelector, Generator generator)
            : base(innerSelector, generator)
        { }

        protected override bool AlignmentIsAllowed(Alignment alignment)
        {
            return alignment.Lawfulness != AlignmentConstants.Lawful;
        }
    }
}