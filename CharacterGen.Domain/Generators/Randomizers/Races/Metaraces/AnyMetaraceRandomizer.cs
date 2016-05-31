using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;

namespace CharacterGen.Domain.Generators.Randomizers.Races.Metaraces
{
    internal class AnyMetaraceRandomizer : BaseForcableMetarace
    {
        public AnyMetaraceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentSelector, Generator generator)
            : base(percentileResultSelector, levelAdjustmentSelector, generator)
        { }

        protected override bool MetaraceIsAllowed(string metarace)
        {
            return true;
        }
    }
}