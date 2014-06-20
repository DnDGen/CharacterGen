using NPCGen.Common.Races;
using System;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Generators.Randomizers.Races.Metaraces
{
    public class NeutralMetaraceRandomizer : BaseMetarace
    {
        protected override Boolean allowNoMetarace
        {
            get { return true; }
        }

        public NeutralMetaraceRandomizer(IPercentileSelector percentileResultSelector, ILevelAdjustmentsSelector levelAdjustmentsSelector)
            : base(percentileResultSelector, levelAdjustmentsSelector) { }

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