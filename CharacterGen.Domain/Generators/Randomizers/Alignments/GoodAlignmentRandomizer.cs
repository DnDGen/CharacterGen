using CharacterGen.Alignments;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;

namespace CharacterGen.Domain.Generators.Randomizers.Alignments
{
    internal class GoodAlignmentRandomizer : BaseAlignmentRandomizer
    {
        public GoodAlignmentRandomizer(IPercentileSelector innerSelector, Generator generator, ICollectionsSelector collectionsSelector)
            : base(innerSelector, generator, collectionsSelector)
        { }

        protected override bool AlignmentIsAllowed(Alignment alignment)
        {
            return alignment.Goodness == AlignmentConstants.Good;
        }
    }
}