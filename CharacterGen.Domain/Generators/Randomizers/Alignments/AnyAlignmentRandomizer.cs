using CharacterGen.Alignments;
using DnDGen.Core.Generators;
using DnDGen.Core.Selectors.Collections;
using DnDGen.Core.Selectors.Percentiles;

namespace CharacterGen.Domain.Generators.Randomizers.Alignments
{
    internal class AnyAlignmentRandomizer : BaseAlignmentRandomizer
    {
        public AnyAlignmentRandomizer(IPercentileSelector innerSelector, Generator generator, ICollectionsSelector collectionsSelector)
            : base(innerSelector, generator, collectionsSelector)
        { }

        protected override bool AlignmentIsAllowed(Alignment alignment)
        {
            return true;
        }
    }
}