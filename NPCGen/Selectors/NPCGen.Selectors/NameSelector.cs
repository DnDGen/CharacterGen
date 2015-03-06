using System;
using System.Linq;
using NPCGen.Selectors.Interfaces;
using NPCGen.Tables.Interfaces;

namespace NPCGen.Selectors
{
    public class NameSelector : INameSelector
    {
        private ICollectionsSelector collectionsSelector;

        public NameSelector(ICollectionsSelector collectionsSelector)
        {
            this.collectionsSelector = collectionsSelector;
        }

        public String Select(String id)
        {
            return collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.Names, id).Single();
        }
    }
}