using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;

namespace CharacterGen.Domain.Generators.Randomizers.Races.Metaraces
{
    internal class AnyMetaraceRandomizer : ForcableMetaraceBase
    {
        public AnyMetaraceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentSelector, Generator generator, ICollectionsSelector collectionSelector)
            : base(percentileResultSelector, levelAdjustmentSelector, generator, collectionSelector)
        { }

        protected override bool MetaraceIsAllowed(string metarace)
        {
            return true;
        }
    }
}