using DnDGen.CharacterGen.Alignments;
using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Races;
using DnDGen.CharacterGen.Randomizers.CharacterClasses;
using DnDGen.CharacterGen.Selectors.Collections;
using DnDGen.CharacterGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.Infrastructure.Selectors.Percentiles;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CharacterGen.Generators.Classes
{
    internal class CharacterClassGenerator : ICharacterClassGenerator
    {
        private readonly IAdjustmentsSelector adjustmentsSelector;
        private readonly ICollectionSelector collectionsSelector;
        private readonly IPercentileSelector percentileSelector;

        public CharacterClassGenerator(IAdjustmentsSelector adjustmentsSelector, ICollectionSelector collectionsSelector, IPercentileSelector percentileSelector)
        {
            this.adjustmentsSelector = adjustmentsSelector;
            this.percentileSelector = percentileSelector;
            this.collectionsSelector = collectionsSelector;
        }

        public CharacterClassPrototype GeneratePrototype(Alignment alignmentPrototype, IClassNameRandomizer classNameRandomizer, ILevelRandomizer levelRandomizer)
        {
            var npcs = collectionsSelector.SelectFrom(Config.Name, TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.NPCs);

            var prototype = new CharacterClassPrototype();
            prototype.Name = classNameRandomizer.Randomize(alignmentPrototype);
            prototype.Level = levelRandomizer.Randomize();
            prototype.IsNPC = npcs.Contains(prototype.Name);

            return prototype;
        }

        public CharacterClass GenerateWith(Alignment alignment, CharacterClassPrototype classPrototype)
        {
            var characterClass = new CharacterClass();

            characterClass.Level = classPrototype.Level;
            characterClass.Name = classPrototype.Name;
            characterClass.IsNPC = classPrototype.IsNPC;

            var tableName = string.Format(TableNameConstants.Formattable.TrueOrFalse.CLASSHasSpecialistFields, characterClass.Name);
            var isSpecialist = percentileSelector.SelectFrom<bool>(Config.Name, tableName);

            if (!isSpecialist)
                return characterClass;

            characterClass.SpecialistFields = GenerateSpecialistFields(characterClass, alignment);
            characterClass.ProhibitedFields = GenerateProhibitedFields(characterClass);

            return characterClass;
        }

        private IEnumerable<string> GenerateSpecialistFields(CharacterClass characterClass, Alignment alignment)
        {
            var allSpecialistFields = collectionsSelector.SelectFrom(Config.Name, TableNameConstants.Set.Collection.SpecialistFields, characterClass.Name);
            var specialistFieldQuantity = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.SpecialistFieldQuantities, characterClass.Name);
            var nonAlignmentFields = collectionsSelector.SelectFrom(Config.Name, TableNameConstants.Set.Collection.ProhibitedFields, alignment.ToString());
            var possibleSpecialistFields = allSpecialistFields.Except(nonAlignmentFields);

            return PopulateFields(specialistFieldQuantity, possibleSpecialistFields);
        }

        private IEnumerable<string> PopulateFields(int quantity, IEnumerable<string> possibleFields)
        {
            var fields = new HashSet<string>();

            if (possibleFields.Count() < quantity)
                return possibleFields;

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

            var allProhibitedFields = collectionsSelector.SelectFrom(Config.Name, TableNameConstants.Set.Collection.ProhibitedFields, characterClass.Name);
            var possibleProhibitedFields = allProhibitedFields.Except(characterClass.SpecialistFields);
            var prohibitedFieldQuantities = adjustmentsSelector.SelectAllFrom(TableNameConstants.Set.Adjustments.ProhibitedFieldQuantities);

            var prohibitedfieldQuantity = 0;
            foreach (var specialistField in characterClass.SpecialistFields)
                prohibitedfieldQuantity += prohibitedFieldQuantities[specialistField];

            return PopulateFields(prohibitedfieldQuantity, possibleProhibitedFields);
        }

        public IEnumerable<string> RegenerateSpecialistFields(Alignment alignment, CharacterClass characterClass, Race race)
        {
            if (!characterClass.SpecialistFields.Any())
                return characterClass.SpecialistFields;

            var allClassSpecialistFields = collectionsSelector.SelectFrom(Config.Name, TableNameConstants.Set.Collection.SpecialistFields, characterClass.Name);
            var allBaseRaceSpecialistFields = collectionsSelector.SelectFrom(Config.Name, TableNameConstants.Set.Collection.SpecialistFields, race.BaseRace);
            var allMetaraceSpecialistFields = collectionsSelector.SelectFrom(Config.Name, TableNameConstants.Set.Collection.SpecialistFields, race.Metarace);
            var nonAlignmentFields = collectionsSelector.SelectFrom(Config.Name, TableNameConstants.Set.Collection.ProhibitedFields, alignment.ToString());

            var applicableFields = allClassSpecialistFields
                .Intersect(allBaseRaceSpecialistFields)
                .Intersect(allMetaraceSpecialistFields)
                .Except(nonAlignmentFields);

            return PopulateFields(characterClass.SpecialistFields.Count(), applicableFields);
        }

        public IEnumerable<CharacterClassPrototype> GeneratePrototypes(
            Alignment alignmentPrototype,
            IClassNameRandomizer classNameRandomizer,
            ILevelRandomizer levelRandomizer)
        {
            var npcs = collectionsSelector.SelectFrom(Config.Name, TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.NPCs);
            var classNames = classNameRandomizer.GetAllPossibleResults(alignmentPrototype);
            var levels = levelRandomizer.GetAllPossibleResults();
            var prototypes = new List<CharacterClassPrototype>();

            foreach (var className in classNames)
            {
                foreach (var level in levels)
                {
                    var prototype = new CharacterClassPrototype();
                    prototype.Name = className;
                    prototype.Level = level;
                    prototype.IsNPC = npcs.Contains(className);

                    prototypes.Add(prototype);
                }
            }

            return prototypes;
        }
    }
}