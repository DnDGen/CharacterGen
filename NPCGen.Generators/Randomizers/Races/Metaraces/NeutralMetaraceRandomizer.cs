using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Providers.Interfaces;
using System;

namespace NPCGen.Core.Generation.Randomizers.Races.Metaraces
{
    public class NeutralMetaraceRandomizer : BaseMetarace
    {
        public NeutralMetaraceRandomizer(IPercentileResultProvider percentileResultProvider, ILevelAdjustmentsProvider levelAdjustmentsProvider)
            : base(percentileResultProvider, levelAdjustmentsProvider) { }

        protected override Boolean MetaraceIsAllowed(String metarace)
        {
            switch (metarace)
            {
                case RaceConstants.Metaraces.Wereboar:
                case RaceConstants.Metaraces.Weretiger: return true;
                case RaceConstants.Metaraces.HalfFiend:
                case RaceConstants.Metaraces.HalfDragon:
                case RaceConstants.Metaraces.Werebear:
                case RaceConstants.Metaraces.HalfCelestial:
                case RaceConstants.Metaraces.Wererat:
                case RaceConstants.Metaraces.Werewolf: return false;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}