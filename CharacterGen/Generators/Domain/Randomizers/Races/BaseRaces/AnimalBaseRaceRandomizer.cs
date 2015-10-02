using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Generators.Randomizers.Races;
using CharacterGen.Generators.Verifiers.Exceptions;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Generators.Domain.Randomizers.Races.BaseRaces
{
    public class AnimalBaseRaceRandomizer : RaceRandomizer
    {
        private ICollectionsSelector collectionsSelector;
        private IAdjustmentsSelector adjustmentsSelector;

        public AnimalBaseRaceRandomizer(ICollectionsSelector collectionsSelector, IAdjustmentsSelector adjustmentsSelector)
        {
            this.collectionsSelector = collectionsSelector;
            this.adjustmentsSelector = adjustmentsSelector;
        }

        public IEnumerable<String> GetAllPossible(Alignment alignment, CharacterClass characterClass)
        {
            var classAnimals = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.Animals, characterClass.ClassName);
            var alignmentAnimals = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.Animals, alignment.ToString());
            var levelAdjustments = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments);

            var animals = classAnimals.Intersect(alignmentAnimals);

            return animals.Where(a => levelAdjustments[a] < characterClass.Level);
        }

        public String Randomize(Alignment alignment, CharacterClass characterClass)
        {
            var animals = GetAllPossible(alignment, characterClass);

            if (animals.Any() == false)
                throw new IncompatibleRandomizersException();

            return collectionsSelector.SelectRandomFrom(animals);
        }
    }
}
