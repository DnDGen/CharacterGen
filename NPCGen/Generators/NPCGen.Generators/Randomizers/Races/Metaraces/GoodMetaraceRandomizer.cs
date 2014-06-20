using NPCGen.Common.Races;
using System;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Generators.Randomizers.Races.Metaraces
{
    public class GoodMetaraceRandomizer : BaseMetarace
    {
        protected override Boolean allowNoMetarace
        {
            get { return true; }
        }

        public GoodMetaraceRandomizer(IPercentileSelector percentileResultSelector, ILevelAdjustmentsSelector levelAdjustmentsSelector)
            : base(percentileResultSelector, levelAdjustmentsSelector) { }

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