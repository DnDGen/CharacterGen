using NPCGen.Core.Generation.Providers.Interfaces;

namespace NPCGen.Core.Generation.Randomizers.Races.Metaraces
{
    public class ForcedGeneticMetaraceRandomizer : GeneticMetarace
    {
        public ForcedGeneticMetaraceRandomizer(IPercentileResultProvider provider)
            : base(provider)
        {
            forcedMetarace = true;
        }
    }
}
