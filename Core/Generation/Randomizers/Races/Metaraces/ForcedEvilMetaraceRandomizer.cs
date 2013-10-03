using NPCGen.Core.Generation.Providers.Interfaces;

namespace NPCGen.Core.Generation.Randomizers.Races.Metaraces
{
    public class ForcedEvilMetaraceRandomizer : EvilMetarace
    {
        public ForcedEvilMetaraceRandomizer(IPercentileResultProvider provider) : base(provider)
        {
            forcedMetarace = true;
        }
    }
}