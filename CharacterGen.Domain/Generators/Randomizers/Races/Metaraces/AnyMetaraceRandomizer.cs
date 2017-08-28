using DnDGen.Core.Generators;
using DnDGen.Core.Selectors.Collections;
using DnDGen.Core.Selectors.Percentiles;

namespace CharacterGen.Domain.Generators.Randomizers.Races.Metaraces
{
    internal class AnyMetaraceRandomizer : ForcableMetaraceBase
    {
        public AnyMetaraceRandomizer(IPercentileSelector percentileResultSelector, Generator generator, ICollectionsSelector collectionSelector)
            : base(percentileResultSelector, generator, collectionSelector)
        { }

        protected override bool MetaraceIsAllowed(string metarace)
        {
            return true;
        }
    }
}