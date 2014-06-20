using System;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Generators.Randomizers.Races.Metaraces
{
    public class AnyMetaraceRandomizer : BaseMetarace
    {
        protected override Boolean allowNoMetarace
        {
            get { return true; }
        }

        public AnyMetaraceRandomizer(IPercentileSelector percentileResultSelector, ILevelAdjustmentsSelector levelAdjustmentsSelector)
            : base(percentileResultSelector, levelAdjustmentsSelector) { }

        protected override Boolean MetaraceIsAllowed(String metarace)
        {
            return true;
        }
    }
}