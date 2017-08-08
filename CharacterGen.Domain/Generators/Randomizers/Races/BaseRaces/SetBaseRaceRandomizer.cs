using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Randomizers.Races;
using CharacterGen.Verifiers.Exceptions;
using DnDGen.Core.Selectors.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Domain.Generators.Randomizers.Races.BaseRaces
{
    internal class SetBaseRaceRandomizer : ISetBaseRaceRandomizer
    {
        public string SetBaseRace { get; set; }

        private readonly ICollectionsSelector collectionsSelector;

        public SetBaseRaceRandomizer(ICollectionsSelector collectionsSelector)
        {
            this.collectionsSelector = collectionsSelector;
        }

        public string Randomize(Alignment alignment, CharacterClass characterClass)
        {
            var baseRaces = GetAllPossible(alignment, characterClass);

            if (baseRaces.Any() == false)
                throw new IncompatibleRandomizersException();

            return baseRaces.Single();
        }

        public IEnumerable<string> GetAllPossible(Alignment alignment, CharacterClass characterClass)
        {
            var alignmentBaseRaces = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, alignment.Full);

            return alignmentBaseRaces.Intersect(new[] { SetBaseRace });
        }
    }
}