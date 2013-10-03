using NPCGen.Core.Generation.Providers.Interfaces;

namespace NPCGen.Core.Generation.Randomizers.Races.Metaraces
{
    public class ForcedAnyMetaraceRandomizer : AnyMetarace
    {
        public ForcedAnyMetaraceRandomizer(IPercentileResultProvider provider) : base(provider)
        {
            forcedMetarace = true;
        }
    }
}