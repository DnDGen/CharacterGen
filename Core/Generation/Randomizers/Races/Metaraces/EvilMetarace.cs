
using System;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Providers.Interfaces;
namespace NPCGen.Core.Generation.Randomizers.Races.Metaraces
{
    public abstract class EvilMetarace : BaseMetarace
    {
        public EvilMetarace(IPercentileResultProvider provider) : base(provider) { }

        protected override Boolean MetaraceIsAllowed(String metarace)
        {
            switch (metarace)
            {
                case RaceConstants.Metaraces.HalfDragon:
                case RaceConstants.Metaraces.Wererat:
                case RaceConstants.Metaraces.Werewolf:
                case RaceConstants.Metaraces.HalfFiend: return true;
                case RaceConstants.Metaraces.Werebear:
                case RaceConstants.Metaraces.HalfCelestial:
                case RaceConstants.Metaraces.Wereboar:
                case RaceConstants.Metaraces.Weretiger: return false;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}