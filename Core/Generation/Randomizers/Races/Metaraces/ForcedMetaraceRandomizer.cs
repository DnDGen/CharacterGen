using System;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Providers.Interfaces;

namespace NPCGen.Core.Generation.Randomizers.Races.Metaraces
{
    public abstract class ForcedMetaraceRandomizer : BaseMetaraceRandomizer
    {
        public ForcedMetaraceRandomizer(IPercentileResultProvider percentileResultProvider) : base(percentileResultProvider) { }

        protected override Boolean RaceIsAllowed(String metarace, Alignment alignment)
        {
            return !String.IsNullOrEmpty(metarace) && MetaraceIsAllowed(metarace, alignment);
        }

        protected abstract Boolean MetaraceIsAllowed(String metarace, Alignment alignment);
    }
}
