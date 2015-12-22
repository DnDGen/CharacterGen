using CharacterGen.Common.Alignments;
using CharacterGen.Generators.Randomizers.CharacterClasses;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using System.Collections.Generic;

namespace CharacterGen.Generators.Domain.Randomizers.CharacterClasses.ClassNames
{
    public class AnyNPCClassNameRandomizer : IClassNameRandomizer
    {
        private ICollectionsSelector collectionsSelector;

        public AnyNPCClassNameRandomizer(ICollectionsSelector collectionsSelector)
        {
            this.collectionsSelector = collectionsSelector;
        }

        public string Randomize(Alignment alignment)
        {
            var npcs = GetAllPossibleResults(alignment);
            return collectionsSelector.SelectRandomFrom(npcs);
        }

        public IEnumerable<string> GetAllPossibleResults(Alignment alignment)
        {
            return collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.NPCs);
        }
    }
}
