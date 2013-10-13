using NPCGen.Core.Generation.Providers.Interfaces;

namespace NPCGen.Core.Generation.Randomizers.Races.Metaraces
{
    public class AllowedGeneticMetaraceRandomizer : GeneticMetarace
    {
        public AllowedGeneticMetaraceRandomizer(IPercentileResultProvider provider)
            : base(provider)
        {
            forcedMetarace = false;
        }
    }
}