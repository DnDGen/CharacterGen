using System;
using NPCGen.Core.Generation.Providers.Interfaces;

namespace NPCGen.Core.Generation.Randomizers.Races.Metaraces
{
    public class AnyMetaraceRandomizer : BaseMetarace
    {
        public AnyMetaraceRandomizer(IPercentileResultProvider percentileResultProvider, ILevelAdjustmentsProvider levelAdjustmentsProvider)
            : base(percentileResultProvider, levelAdjustmentsProvider) { }

        protected override Boolean MetaraceIsAllowed(String metarace)
        {
            return true;
        }
    }
}