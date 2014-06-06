using NPCGen.Common.Races;
using NPCGen.Generators.Providers.Interfaces;
using System;

namespace NPCGen.Generators.Randomizers.Races.Metaraces
{
    public class GeneticMetaraceRandomizer : BaseMetarace
    {
        public GeneticMetaraceRandomizer(IPercentileResultProvider percentileResultProvider, ILevelAdjustmentsProvider levelAdjustmentsProvider)
            : base(percentileResultProvider, levelAdjustmentsProvider) { }

        protected override Boolean MetaraceIsAllowed(String metarace)
        {
            switch (metarace)
            {
                case RaceConstants.Metaraces.HalfDragon:
                case RaceConstants.Metaraces.HalfCelestial:
                case RaceConstants.Metaraces.HalfFiend: return true;
                case RaceConstants.Metaraces.Wererat:
                case RaceConstants.Metaraces.Werewolf:
                case RaceConstants.Metaraces.Werebear:
                case RaceConstants.Metaraces.Wereboar:
                case RaceConstants.Metaraces.Weretiger: return false;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}