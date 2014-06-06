using System;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Providers.Interfaces;

namespace NPCGen.Core.Generation.Randomizers.Races.Metaraces
{
    public class NonEvilMetaraceRandomizer : BaseMetarace
    {
        public NonEvilMetaraceRandomizer(IPercentileResultProvider percentileResultProvider, ILevelAdjustmentsProvider levelAdjustmentsProvider)
            : base(percentileResultProvider, levelAdjustmentsProvider) { }

        protected override Boolean MetaraceIsAllowed(String metarace)
        {
            switch (metarace)
            {
                case RaceConstants.Metaraces.Wererat:
                case RaceConstants.Metaraces.Werewolf:
                case RaceConstants.Metaraces.HalfFiend: return false;
                case RaceConstants.Metaraces.Werebear:
                case RaceConstants.Metaraces.HalfDragon:
                case RaceConstants.Metaraces.HalfCelestial:
                case RaceConstants.Metaraces.Wereboar:
                case RaceConstants.Metaraces.Weretiger: return true;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}