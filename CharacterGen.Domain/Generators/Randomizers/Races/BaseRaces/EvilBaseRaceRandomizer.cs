using CharacterGen.Alignments;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;
using CharacterGen.Domain.Tables;
using System.Linq;

namespace CharacterGen.Domain.Generators.Randomizers.Races.BaseRaces
{
    internal class EvilBaseRaceRandomizer : BaseRaceRandomizer
    {
        private ICollectionsSelector collectionsSelector;

        public EvilBaseRaceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentSelector,
            ICollectionsSelector collectionsSelector, Generator generator)
            : base(percentileResultSelector, levelAdjustmentSelector, generator)
        {
            this.collectionsSelector = collectionsSelector;
        }

        protected override bool BaseRaceIsAllowed(string baseRace)
        {
            var baseRaces = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, AlignmentConstants.Evil);
            return baseRaces.Contains(baseRace);
        }
    }
}