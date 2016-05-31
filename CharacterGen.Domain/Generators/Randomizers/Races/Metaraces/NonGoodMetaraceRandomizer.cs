using CharacterGen.Alignments;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;
using CharacterGen.Domain.Tables;
using System.Linq;

namespace CharacterGen.Domain.Generators.Randomizers.Races.Metaraces
{
    internal class NonGoodMetaraceRandomizer : BaseForcableMetarace
    {
        private ICollectionsSelector collectionsSelector;

        public NonGoodMetaraceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentSelector,
            ICollectionsSelector collectionsSelector, Generator generator)
            : base(percentileResultSelector, levelAdjustmentSelector, generator)
        {
            this.collectionsSelector = collectionsSelector;
        }

        protected override bool MetaraceIsAllowed(string metarace)
        {
            var evilMetaraces = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, AlignmentConstants.Evil);
            var neutralMetaraces = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, AlignmentConstants.Neutral);
            var goodMetaraces = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, AlignmentConstants.Good);
            var forbiddenMetaraces = goodMetaraces.Except(neutralMetaraces).Except(evilMetaraces);

            return !forbiddenMetaraces.Contains(metarace);
        }
    }
}