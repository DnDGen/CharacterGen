using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;
using CharacterGen.Domain.Tables;
using System.Linq;

namespace CharacterGen.Domain.Generators.Randomizers.Races.BaseRaces
{
    internal class MonsterBaseRaceRandomizer : BaseRaceRandomizerBase
    {
        public MonsterBaseRaceRandomizer(IPercentileSelector percentileResultSelector, IAdjustmentsSelector adjustmentSelector, ICollectionsSelector collectionSelector, Generator generator)
            : base(percentileResultSelector, adjustmentSelector, generator, collectionSelector)
        { }

        protected override bool BaseRaceIsAllowed(string baseRace)
        {
            var monsters = collectionSelector.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters);
            return monsters.Contains(baseRace);
        }
    }
}
