using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;
using CharacterGen.Domain.Tables;
using System.Linq;

namespace CharacterGen.Domain.Generators.Randomizers.Races.Metaraces
{
    internal class GeneticMetaraceRandomizer : ForcableMetaraceBase
    {
        public GeneticMetaraceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector levelAdjustmentSelector, ICollectionsSelector collectionsSelector, Generator generator)
            : base(percentileResultSelector, levelAdjustmentSelector, generator, collectionsSelector)
        { }

        protected override bool MetaraceIsAllowed(string metarace)
        {
            var geneticMetaraces = collectionSelector.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, GroupConstants.Genetic);
            return geneticMetaraces.Contains(metarace);
        }
    }
}