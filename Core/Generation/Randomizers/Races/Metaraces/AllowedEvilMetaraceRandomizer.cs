using NPCGen.Core.Generation.Providers.Interfaces;

namespace NPCGen.Core.Generation.Randomizers.Races.Metaraces
{
    public class AllowedEvilMetaraceRandomizer : EvilMetarace
    {
        public AllowedEvilMetaraceRandomizer(IPercentileResultProvider provider)
            : base(provider)
        {
            forcedMetarace = false;
        }
    }
}