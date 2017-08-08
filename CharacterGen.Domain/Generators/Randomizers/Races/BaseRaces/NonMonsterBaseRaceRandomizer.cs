using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Tables;
using DnDGen.Core.Generators;
using DnDGen.Core.Selectors.Collections;
using DnDGen.Core.Selectors.Percentiles;
using System.Linq;

namespace CharacterGen.Domain.Generators.Randomizers.Races.BaseRaces
{
    internal class NonMonsterBaseRaceRandomizer : BaseRaceRandomizerBase
    {
        private readonly ICollectionsSelector collectionsSelector;

        public NonMonsterBaseRaceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector adjustmentSelector, ICollectionsSelector collectionsSelector, Generator generator)
            : base(percentileResultSelector, adjustmentSelector, generator, collectionsSelector)
        {
            this.collectionsSelector = collectionsSelector;
        }

        protected override bool BaseRaceIsAllowedByRandomizer(string baseRace)
        {
            var monsters = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters);
            return !monsters.Contains(baseRace);
        }
    }
}
