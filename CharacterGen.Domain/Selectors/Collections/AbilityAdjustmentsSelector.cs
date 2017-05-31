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

            foreach (var ability in abilities)
            {
                tableName = string.Format(TableNameConstants.Formattable.Adjustments.ABILITYAbilityAdjustments, ability);
                var abilityAdjustments = innerSelector.SelectAllFrom(tableName);

                adjustments[ability] = abilityAdjustments[race.BaseRace];
                adjustments[ability] += abilityAdjustments[race.Metarace];
                adjustments[ability] += agingEffects[ability];
            }

            return adjustments;
        }
    }
}