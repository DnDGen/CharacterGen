using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using System.Collections.Generic;

namespace CharacterGen.Domain.Selectors.Collections
{
    internal class AbilityAdjustmentsSelector : IAbilityAdjustmentsSelector
    {
        private IAdjustmentsSelector innerSelector;
        private ICollectionsSelector collectionsSelector;

        public AbilityAdjustmentsSelector(IAdjustmentsSelector innerSelector, ICollectionsSelector collectionsSelector)
        {
            this.innerSelector = innerSelector;
            this.collectionsSelector = collectionsSelector;
        }

        public Dictionary<string, int> SelectFor(Race race)
        {
            var adjustments = new Dictionary<string, int>();
            var abilities = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.AbilityGroups, GroupConstants.All);

            var tableName = string.Format(TableNameConstants.Formattable.Adjustments.AGEAbilityAdjustments, race.Age.Description);
            var agingEffects = innerSelector.SelectAllFrom(tableName);

            tableName = string.Format(TableNameConstants.Formattable.Adjustments.RACEAbilityAdjustments, race.BaseRace);
            var baseRaceAdjustments = innerSelector.SelectAllFrom(tableName);

            tableName = string.Format(TableNameConstants.Formattable.Adjustments.RACEAbilityAdjustments, race.Metarace);
            var metaraceAdjustments = innerSelector.SelectAllFrom(tableName);

            foreach (var ability in abilities)
            {
                adjustments[ability] = baseRaceAdjustments[ability];
                adjustments[ability] += metaraceAdjustments[ability];
                adjustments[ability] += agingEffects[ability];
            }

            return adjustments;
        }
    }
}