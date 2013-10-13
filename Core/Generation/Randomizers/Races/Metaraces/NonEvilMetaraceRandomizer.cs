using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Providers.Interfaces;
using System;

namespace NPCGen.Core.Generation.Randomizers.Races.Metaraces
{
    public class NonEvilMetaraceRandomizer : BaseMetarace
    {
        public NonEvilMetaraceRandomizer(IPercentileResultProvider provider) : base(provider) { }

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