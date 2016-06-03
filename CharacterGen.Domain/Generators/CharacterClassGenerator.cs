using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using CharacterGen.Randomizers.CharacterClasses;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Domain.Generators
{
    internal class CharacterClassGenerator : ICharacterClassGenerator
    {
        private IAdjustmentsSelector adjustmentsSelector;
        private ICollectionsSelector collectionsSelector;
        private IBooleanPercentileSelector booleanPercentileSelector;

        public CharacterClassGenerator(IAdjustmentsSelector adjustmentsSelector, ICollectionsSelector collectionsSelector, IBooleanPercentileSelector booleanPercentileSelector)
        {
            this.adjustmentsSelector = adjustmentsSelector;
            this.booleanPercentileSelector = booleanPercentileSelector;
            this.collectionsSelector = collectionsSelector;
        }

        public CharacterClass GenerateWith(Alignment alignment, ILevelRandomizer levelRandomizer, IClassNameRandomizer classNameRandomizer)
        {
            var characterClass = new CharacterClass();

            characterClass.Level = levelRandomizer.Randomize();
            characterClass.Name = classNameRandomizer.Randomize(alignment);

            var tableName = string.Format(TableNameConstants.Formattable.TrueOrFalse.CLASSHasSpecialistFields, characterClass.Name);
            var isSpecialist = booleanPercentileSelector.SelectFrom(tableName);

            if (!isSpecialist)
                return characterClass;

            characterClass.SpecialistFields = GenerateSpecialistFields(characterClass, alignment);
            characterClass.ProhibitedFields = GenerateProhibitedFields(characterClass);

            return characterClass;
        }

        private IEnumerable<string> GenerateSpecialistFields(CharacterClass characterClass, Alignment alignment)
        {
            var allSpecialistFields = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.SpecialistFields, characterClass.Name);
            var specialistFieldQuantities = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.SpecialistFieldQuantities);
            var nonAlignmentFields = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ProhibitedFields, alignment.ToString());
            var possibleSpecialistFields = allSpecialistFields.Except(nonAlignmentFields);

            return PopulateFields(specialistFieldQuantities[characterClass.Name], possibleSpecialistFields);
        }

        private IEnumerable<string> PopulateFields(int quantity, IEnumerable<string> possibleFields)
        {
            var fields = new HashSet<string>();

            if (!possibleFields.Any())
                return fields;

            while (fields.Count < quantity)
            {
                var field = collectionsSelector.SelectRandomFrom(possibleFields);
                fields.Add(field);
            }

            return fields;
        }

        private IEnumerable<string> GenerateProhibitedFields(CharacterClass characterClass)
        {
            if (!characterClass.SpecialistFields.Any())
                return Enumerable.Empty<string>();

            var allProhibitedFields = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ProhibitedFields, characterClass.Name);
            var possibleProhibitedFields = allProhibitedFields.Except(characterClass.SpecialistFields);
            var prohibitedFieldQuantities = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.ProhibitedFieldQuantities);

            var prohibitedfieldQuantity = 0;
            foreach (var specialistField in characterClass.SpecialistFields)
                prohibitedfieldQuantity += prohibitedFieldQuantities[specialistField];

            return PopulateFields(prohibitedfieldQuantity, possibleProhibitedFields);
        }

        public IEnumerable<string> RegenerateSpecialistFields(Alignment alignment, CharacterClass characterClass, Race race)
        {
            if (characterClass.SpecialistFields.Any() == false || race.Metarace == RaceConstants.Metaraces.None)
                return characterClass.SpecialistFields;

            var allClassSpecialistFields = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.SpecialistFields, characterClass.Name);
            var allMetaraceSpecialistFields = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.SpecialistFields, race.Metarace);
            var applicableFields = allClassSpecialistFields.Intersect(allMetaraceSpecialistFields);

            if (applicableFields.Any() == false)
                return characterClass.SpecialistFields;

            var nonAlignmentFields = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ProhibitedFields, alignment.ToString());
            var possibleSpecialistFields = applicableFields.Except(nonAlignmentFields);

            return PopulateFields(characterClass.SpecialistFields.Count(), possibleSpecialistFields);
        }
    }
}