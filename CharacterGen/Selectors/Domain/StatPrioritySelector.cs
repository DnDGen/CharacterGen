using System;
using System.Linq;
using CharacterGen.Selectors;
using CharacterGen.Selectors.Objects;
using CharacterGen.Tables;

namespace CharacterGen.Selectors.Domain
{
    public class StatPrioritySelector : IStatPrioritySelector
    {
        private ICollectionsSelector innerSelector;

        public StatPrioritySelector(ICollectionsSelector innerSelector)
        {
            this.innerSelector = innerSelector;
        }

        public StatPrioritySelection SelectFor(String className)
        {
            var priorities = innerSelector.SelectFrom(TableNameConstants.Set.Collection.StatPriorities, className);

            var statPriority = new StatPrioritySelection();
            statPriority.First = priorities.First();
            statPriority.Second = priorities.Last();

            return statPriority;
        }
    }
}