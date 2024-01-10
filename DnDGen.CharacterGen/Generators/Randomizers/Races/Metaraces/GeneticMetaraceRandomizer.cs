using DnDGen.CharacterGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.Infrastructure.Selectors.Percentiles;
using System.Linq;

namespace DnDGen.CharacterGen.Generators.Randomizers.Races.Metaraces
{
    internal class GeneticMetaraceRandomizer : ForcableMetaraceBase
    {
        private readonly ICollectionSelector collectionsSelector;

        public GeneticMetaraceRandomizer(IPercentileSelector percentileResultSelector, ICollectionSelector collectionsSelector)
            : base(percentileResultSelector, collectionsSelector)
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