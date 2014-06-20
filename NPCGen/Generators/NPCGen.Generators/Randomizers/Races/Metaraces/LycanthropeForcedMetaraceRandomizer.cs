using NPCGen.Common.Races;
using System;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Generators.Randomizers.Races.Metaraces
{
    public class LycanthropeForcedMetaraceRandomizer : BaseMetarace
    {
        protected override Boolean allowNoMetarace
        {
            get { return false; }
        }

        public LycanthropeForcedMetaraceRandomizer(IPercentileSelector percentileResultSelector, ILevelAdjustmentsSelector levelAdjustmentsSelector)
            : base(percentileResultSelector, levelAdjustmentsSelector) { }

        protected override Boolean MetaraceIsAllowed(String metarace)
        {
            switch (metarace)
            {
                case RaceConstants.Metaraces.HalfDragon:
                case RaceConstants.Metaraces.HalfCelestial:
                case RaceConstants.Metaraces.HalfFiend: return false;
                case RaceConstants.Metaraces.Wererat:
                case RaceConstants.Metaraces.Werewolf:
                case RaceConstants.Metaraces.Werebear:
                case RaceConstants.Metaraces.Wereboar:
                case RaceConstants.Metaraces.Weretiger: return true;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}