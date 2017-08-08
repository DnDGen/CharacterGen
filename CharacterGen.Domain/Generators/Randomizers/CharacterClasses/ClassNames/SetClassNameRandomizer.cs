using CharacterGen.Alignments;
using CharacterGen.Domain.Tables;
using CharacterGen.Randomizers.CharacterClasses;
using CharacterGen.Verifiers.Exceptions;
using DnDGen.Core.Selectors.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Domain.Generators.Randomizers.CharacterClasses.ClassNames
{
    internal class SetClassNameRandomizer : ISetClassNameRandomizer
    {
        public string SetClassName { get; set; }

        private readonly ICollectionsSelector collectionsSelector;

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