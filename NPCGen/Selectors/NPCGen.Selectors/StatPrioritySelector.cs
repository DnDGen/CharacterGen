using System;
using System.Linq;
using NPCGen.Selectors.Interfaces;
using NPCGen.Selectors.Interfaces.Objects;
using NPCGen.Tables.Interfaces;

namespace NPCGen.Selectors
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