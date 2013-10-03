using NPCGen.Core.Generation.Providers.Interfaces;

namespace NPCGen.Core.Generation.Randomizers.Races.Metaraces
{
    public class AllowedAnyMetaraceRandomizer : AnyMetarace
    {
        public AllowedAnyMetaraceRandomizer(IPercentileResultProvider provider) : base(provider) 
        {
            forcedMetarace = false;
        }
    }
}