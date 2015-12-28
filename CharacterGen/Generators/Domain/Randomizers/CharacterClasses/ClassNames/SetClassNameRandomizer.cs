using CharacterGen.Common.Alignments;
using CharacterGen.Generators.Randomizers.CharacterClasses;
using CharacterGen.Generators.Verifiers.Exceptions;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Generators.Domain.Randomizers.CharacterClasses.ClassNames
{
    public class SetClassNameRandomizer : ISetClassNameRandomizer
    {
        public string SetClassName { get; set; }

        private ICollectionsSelector collectionsSelector;

        public SetClassNameRandomizer(ICollectionsSelector collectionsSelector)
        {
            this.collectionsSelector = collectionsSelector;
        }

        public string Randomize(Alignment alignment)
        {
            var classes = GetAllPossibleResults(alignment);

            if (classes.Any() == false)
                throw new IncompatibleRandomizersException();

            return classes.Single();
        }

        public IEnumerable<string> GetAllPossibleResults(Alignment alignment)
        {
            var alignmentClasses = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, alignment.Full);
            return alignmentClasses.Intersect(new[] { SetClassName });
        }
    }
}