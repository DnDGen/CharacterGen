using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Tables;
using CharacterGen.Randomizers.Races;
using CharacterGen.Verifiers.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Domain.Generators.Randomizers.Races.Metaraces
{
    internal class SetMetaraceRandomizer : ISetMetaraceRandomizer
    {
        public string SetMetarace { get; set; }

        private ICollectionsSelector collectionsSelector;

        public SetMetaraceRandomizer(ICollectionsSelector collectionsSelector)
        {
            this.collectionsSelector = collectionsSelector;
        }

        public string Randomize(Alignment alignment, CharacterClass characterClass)
        {
            var metaraces = GetAllPossible(alignment, characterClass);

            if (metaraces.Any() == false)
                throw new IncompatibleRandomizersException();

            return metaraces.Single();
        }

        public IEnumerable<string> GetAllPossible(Alignment alignment, CharacterClass characterClass)
        {
            var alignmentMetaraces = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, alignment.Goodness);
            var classMetaraces = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, characterClass.Name);

            return alignmentMetaraces.Intersect(classMetaraces).Intersect(new[] { SetMetarace });
        }
    }
}