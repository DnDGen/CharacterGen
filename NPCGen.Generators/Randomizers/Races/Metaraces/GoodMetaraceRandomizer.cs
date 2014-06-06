using NPCGen.Common.Races;
using NPCGen.Generators.Providers.Interfaces;
using System;

namespace NPCGen.Generators.Randomizers.Races.Metaraces
{
    public class GoodMetaraceRandomizer : BaseMetarace
    {
        public GoodMetaraceRandomizer(IPercentileResultProvider percentileResultProvider, ILevelAdjustmentsProvider levelAdjustmentsProvider)
            : base(percentileResultProvider, levelAdjustmentsProvider) { }

        protected override Boolean MetaraceIsAllowed(String metarace)
        {
            switch (metarace)
            {
                case RaceConstants.Metaraces.HalfDragon:
                case RaceConstants.Metaraces.Werebear:
                case RaceConstants.Metaraces.HalfCelestial: return true;
                case RaceConstants.Metaraces.Wererat:
                case RaceConstants.Metaraces.Werewolf:
                case RaceConstants.Metaraces.HalfFiend:
                case RaceConstants.Metaraces.Wereboar:
                case RaceConstants.Metaraces.Weretiger: return false;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}