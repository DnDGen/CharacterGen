using System;
using System.Linq;
using CharacterGen.Common.Alignments;
using CharacterGen.Selectors;
using CharacterGen.Tables;

namespace CharacterGen.Generators.Domain.Randomizers.Races.Metaraces
{
    public class NonEvilMetaraceRandomizer : BaseForcableMetarace
    {
        private ICollectionsSelector collectionsSelector;

        public NonEvilMetaraceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentSelector,
            ICollectionsSelector collectionsSelector, Generator generator)
            : base(percentileResultSelector, levelAdjustmentSelector, generator)
        {
            this.collectionsSelector = collectionsSelector;
        }

        protected override Boolean MetaraceIsAllowed(String metarace)
        {
            var evilMetaraces = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, AlignmentConstants.Evil);
            var neutralMetaraces = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, AlignmentConstants.Neutral);
            var goodMetaraces = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, AlignmentConstants.Good);
            var forbiddenMetaraces = evilMetaraces.Except(neutralMetaraces).Except(goodMetaraces);

            return !forbiddenMetaraces.Contains(metarace);
        }
    }
}