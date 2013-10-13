using NPCGen.Core.Generation.Providers.Interfaces;

namespace NPCGen.Core.Generation.Randomizers.Races.Metaraces
{
    public class AllowedGoodMetaraceRandomizer : GoodMetarace
    {
        public AllowedGoodMetaraceRandomizer(IPercentileResultProvider provider)
            : base(provider)
        {
            forcedMetarace = false;
        }
    }
}
