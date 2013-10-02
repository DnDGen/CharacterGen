using System;
using NPCGen.Core.Generation.Providers.Interfaces;

namespace NPCGen.Core.Generation.Randomizers.Races.Metaraces
{
    public abstract class ForcedMetaraceRandomizer : BaseMetaraceRandomizer
    {
        public ForcedMetaraceRandomizer(IPercentileResultProvider percentileResultProvider) : base(percentileResultProvider) { }

        protected override Boolean RaceIsAllowed(String metarace)
        {
            return !String.IsNullOrEmpty(metarace) && MetaraceIsAllowed(metarace);
        }

        protected abstract Boolean MetaraceIsAllowed(String metarace);
    }
}
