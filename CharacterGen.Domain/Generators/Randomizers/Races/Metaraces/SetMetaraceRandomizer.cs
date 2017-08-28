using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Randomizers.Races;
using CharacterGen.Verifiers.Exceptions;
using DnDGen.Core.Selectors.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Domain.Generators.Randomizers.Races.Metaraces
{
    internal class SetMetaraceRandomizer : ISetMetaraceRandomizer
    {
        public string SetMetarace { get; set; }

        private readonly ICollectionsSelector collectionsSelector;

        public SetMetaraceRandomizer(ICollectionsSelector collectionsSelector)
        {
            this.collectionsSelector = collectionsSelector;
        }

        public string Randomize(Alignment alignment, CharacterClassPrototype characterClass)
        {
            var metaraces = GetAllPossible(alignment, characterClass);

            if (!metaraces.Any())
                throw new IncompatibleRandomizersException();

            return metaraces.Single();
        }

        public IEnumerable<string> GetAllPossible(Alignment alignment, CharacterClassPrototype characterClass)
        {
            var alignmentMetaraces = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, alignment.Full);
            var classMetaraces = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, characterClass.Name);

            return alignmentMetaraces.Intersect(classMetaraces).Intersect(new[] { SetMetarace });
        }
    }
}