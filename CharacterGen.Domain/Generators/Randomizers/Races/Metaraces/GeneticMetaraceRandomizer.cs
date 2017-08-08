using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Tables;
using DnDGen.Core.Generators;
using DnDGen.Core.Selectors.Collections;
using DnDGen.Core.Selectors.Percentiles;
using System.Linq;

namespace CharacterGen.Domain.Generators.Randomizers.Races.Metaraces
{
    internal class GeneticMetaraceRandomizer : ForcableMetaraceBase
    {
        private readonly ICollectionsSelector collectionsSelector;

        public GeneticMetaraceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentSelector, ICollectionsSelector collectionsSelector, Generator generator)
            : base(percentileResultSelector, levelAdjustmentSelector, generator, collectionsSelector)
        {
            this.collectionsSelector = collectionsSelector;
        }

        protected override bool MetaraceIsAllowed(string metarace)
        {
            var geneticMetaraces = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, GroupConstants.Genetic);
            return geneticMetaraces.Contains(metarace);
        }
    }
}