using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Generators.Randomizers.Races;
using CharacterGen.Generators.Verifiers.Exceptions;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Generators.Domain.Randomizers.Races.Metaraces
{
    public class SetMetaraceRandomizer : ISetMetaraceRandomizer
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
            var classMetaraces = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, characterClass.ClassName);

            return alignmentMetaraces.Intersect(classMetaraces).Intersect(new[] { SetMetarace });
        }
    }
}