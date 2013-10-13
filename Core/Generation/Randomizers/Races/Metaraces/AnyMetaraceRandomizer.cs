using System;
using NPCGen.Core.Generation.Providers.Interfaces;

namespace NPCGen.Core.Generation.Randomizers.Races.Metaraces
{
    public class AnyMetaraceRandomizer : BaseMetarace
    {
        public AnyMetaraceRandomizer(IPercentileResultProvider provider) : base(provider) { }

        protected override Boolean MetaraceIsAllowed(String metarace)
        {
            return true;
        }
    }
}