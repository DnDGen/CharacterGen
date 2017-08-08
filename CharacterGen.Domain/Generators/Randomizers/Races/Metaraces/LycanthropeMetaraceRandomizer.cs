using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Tables;
using DnDGen.Core.Generators;
using DnDGen.Core.Selectors.Collections;
using DnDGen.Core.Selectors.Percentiles;
using System.Linq;

namespace CharacterGen.Domain.Generators.Randomizers.Races.Metaraces
{
    internal class LycanthropeMetaraceRandomizer : ForcableMetaraceBase
    {
        private readonly ICollectionsSelector collectionsSelector;

        public LycanthropeMetaraceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector adjustmentSelector, ICollectionsSelector collectionsSelector, Generator generator)
            : base(percentileResultSelector, adjustmentSelector, generator, collectionsSelector)
        {
            this.collectionsSelector = collectionsSelector;
        }

        protected override bool MetaraceIsAllowed(string metarace)
        {
            var metaraces = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, GroupConstants.Lycanthrope);
            return metaraces.Contains(metarace);
        }
    }
}