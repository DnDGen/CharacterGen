using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Generators.Randomizers.CharacterClasses;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Generators.Domain
{
    public class CharacterClassGenerator : ICharacterClassGenerator
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

        public CharacterClass GenerateWith(Alignment alignment, ILevelRandomizer levelRandomizer,
            IClassNameRandomizer classNameRandomizer)
        {
            var characterClass = new CharacterClass();

            characterClass.Level = levelRandomizer.Randomize();
            characterClass.ClassName = classNameRandomizer.Randomize(alignment);

            var tableName = String.Format(TableNameConstants.Formattable.TrueOrFalse.CLASSHasSpecialistFields, characterClass.ClassName);
            var isSpecialist = booleanPercentileSelector.SelectFrom(tableName);

            if (!isSpecialist)
                return characterClass;

            characterClass.SpecialistFields = GenerateSpecialistFields(characterClass, alignment);
            characterClass.ProhibitedFields = GenerateProhibitedFields(characterClass);

            return characterClass;
        }

        private IEnumerable<String> GenerateSpecialistFields(CharacterClass characterClass, Alignment alignment)
        {
            var allSpecialistFields = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.SpecialistFields, characterClass.ClassName);
            var specialistFieldQuantities = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.SpecialistFieldQuantities);
            var nonAlignmentFields = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ProhibitedFields, alignment.ToString());
            var possibleSpecialistFields = allSpecialistFields.Except(nonAlignmentFields);

            return PopulateFields(specialistFieldQuantities[characterClass.ClassName], possibleSpecialistFields);
        }

        private IEnumerable<String> PopulateFields(Int32 quantity, IEnumerable<String> possibleFields)
        {
            var fields = new HashSet<String>();

            if (!possibleFields.Any())
                return fields;

            while (fields.Count < quantity)
            {
                var field = collectionsSelector.SelectRandomFrom(possibleFields);
                fields.Add(field);
            }

            return fields;
        }

        private IEnumerable<String> GenerateProhibitedFields(CharacterClass characterClass)
        {
            if (!characterClass.SpecialistFields.Any())
                return Enumerable.Empty<String>();

            var allProhibitedFields = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ProhibitedFields, characterClass.ClassName);
            var possibleProhibitedFields = allProhibitedFields.Except(characterClass.SpecialistFields);
            var prohibitedFieldQuantities = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.ProhibitedFieldQuantities);

            var prohibitedfieldQuantity = 0;
            foreach (var specialistField in characterClass.SpecialistFields)
                prohibitedfieldQuantity += prohibitedFieldQuantities[specialistField];

            return PopulateFields(prohibitedfieldQuantity, possibleProhibitedFields);
        }
    }
}