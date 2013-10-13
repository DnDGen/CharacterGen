using NPCGen.Core.Generation.Providers.Interfaces;

namespace NPCGen.Core.Generation.Randomizers.Races.Metaraces
{
    public class ForcedLycanthropeMetaraceRandomizer : LycanthropeMetarace
    {
        public ForcedLycanthropeMetaraceRandomizer(IPercentileResultProvider provider)
            : base(provider)
        {
            forcedMetarace = true;
        }
    }
}