using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Generators.Randomizers.Races;
using CharacterGen.Generators.Verifiers.Exceptions;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Generators.Domain.Randomizers.Races.BaseRaces
{
    public class SetBaseRaceRandomizer : ISetBaseRaceRandomizer
    {
        public string SetBaseRace { get; set; }

        private ICollectionsSelector collectionsSelector;

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
            var alignmentBaseRaces = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, alignment.Goodness);
            var classBaseRaces = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, characterClass.ClassName);

            return alignmentBaseRaces.Intersect(classBaseRaces).Intersect(new[] { SetBaseRace });
        }
    }
}