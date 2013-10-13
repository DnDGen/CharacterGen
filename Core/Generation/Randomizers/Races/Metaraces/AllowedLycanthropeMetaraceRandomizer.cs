using NPCGen.Core.Generation.Providers.Interfaces;

namespace NPCGen.Core.Generation.Randomizers.Races.Metaraces
{
    public class AllowedLycanthropeMetaraceRandomizer : LycanthropeMetarace
    {
        public AllowedLycanthropeMetaraceRandomizer(IPercentileResultProvider provider)
            : base(provider)
        {
            forcedMetarace = false;
        }
    }
}