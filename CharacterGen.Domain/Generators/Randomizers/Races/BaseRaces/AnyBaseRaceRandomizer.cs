using DnDGen.Core.Generators;
using DnDGen.Core.Selectors.Collections;
using DnDGen.Core.Selectors.Percentiles;

namespace CharacterGen.Domain.Generators.Randomizers.Races.BaseRaces
{
    internal class AnyBaseRaceRandomizer : BaseRaceRandomizerBase
    {
        public AnyBaseRaceRandomizer(IPercentileSelector percentileResultSelector, Generator generator, ICollectionSelector collectionSelector)
            : base(percentileResultSelector, generator, collectionSelector) { }

        protected override bool BaseRaceIsAllowedByRandomizer(string baseRace)
        {
            return true;
        }
    }
}