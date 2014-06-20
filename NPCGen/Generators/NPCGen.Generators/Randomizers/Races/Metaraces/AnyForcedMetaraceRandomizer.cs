using System;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Generators.Randomizers.Races.Metaraces
{
    public class AnyForcedMetaraceRandomizer : BaseMetarace
    {
        protected override Boolean allowNoMetarace
        {
            get { return false; }
        }

        public AnyForcedMetaraceRandomizer(IPercentileSelector percentileResultSelector, ILevelAdjustmentsSelector levelAdjustmentsSelector)
            : base(percentileResultSelector, levelAdjustmentsSelector) { }

        protected override Boolean MetaraceIsAllowed(String metarace)
        {
            return true;
        }
    }
}