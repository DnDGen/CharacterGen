using NPCGen.Core.Generation.Providers.Interfaces;

namespace NPCGen.Core.Generation.Randomizers.Races.Metaraces
{
    public class ForcedGoodMetaraceRandomizer : GoodMetarace
    {
        public ForcedGoodMetaraceRandomizer(IPercentileResultProvider provider)
            : base(provider)
        {
            forcedMetarace = true;
        }
    }
}