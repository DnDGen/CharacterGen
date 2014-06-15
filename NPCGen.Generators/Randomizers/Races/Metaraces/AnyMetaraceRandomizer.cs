using System;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Generators.Randomizers.Races.Metaraces
{
    public class AnyMetaraceRandomizer : BaseMetarace
    {
        public AnyMetaraceRandomizer(IPercentileSelector percentileResultProvider, ILevelAdjustmentsSelector levelAdjustmentsProvider)
            : base(percentileResultProvider, levelAdjustmentsProvider) { }

        protected override Boolean MetaraceIsAllowed(String metarace)
        {
            return true;
        }
    }
}