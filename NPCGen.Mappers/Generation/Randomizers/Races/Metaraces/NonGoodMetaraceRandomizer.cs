using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Providers.Interfaces;
using System;

namespace NPCGen.Core.Generation.Randomizers.Races.Metaraces
{
    public class NonGoodMetaraceRandomizer : BaseMetarace
    {
        public NonGoodMetaraceRandomizer(IPercentileResultProvider percentileResultProvider, ILevelAdjustmentsProvider levelAdjustmentsProvider)
            : base(percentileResultProvider, levelAdjustmentsProvider) { }

        protected override Boolean MetaraceIsAllowed(String metarace)
        {
            switch (metarace)
            {
                case RaceConstants.Metaraces.HalfDragon:
                case RaceConstants.Metaraces.Wererat:
                case RaceConstants.Metaraces.Werewolf:
                case RaceConstants.Metaraces.HalfFiend:
                case RaceConstants.Metaraces.Wereboar:
                case RaceConstants.Metaraces.Weretiger: return true;
                case RaceConstants.Metaraces.Werebear:
                case RaceConstants.Metaraces.HalfCelestial: return false;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}