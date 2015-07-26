using System;
using CharacterGen.Selectors;

namespace CharacterGen.Generators.Domain.Randomizers.Races.Metaraces
{
    public class AnyMetaraceRandomizer : BaseForcableMetarace
    {
        public AnyMetaraceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentSelector)
            : base(percentileResultSelector, levelAdjustmentSelector) { }

        protected override Boolean MetaraceIsAllowed(String metarace)
        {
            return true;
        }
    }
}