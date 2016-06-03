using CharacterGen.Abilities.Stats;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;
using CharacterGen.Domain.Tables;
using CharacterGen.Magics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Domain.Generators.Magics
{
    internal class SpellsGenerator : ISpellsGenerator
    {
        private ICollectionsSelector collectionsSelector;
        private IAdjustmentsSelector adjustmentsSelector;
        private IBooleanPercentileSelector booleanPercentileSelector;

        public SpellsGenerator(ICollectionsSelector collectionsSelector, IAdjustmentsSelector adjustmentsSelector, IBooleanPercentileSelector booleanPercentileSelector)
        {
            this.collectionsSelector = collectionsSelector;
            this.adjustmentsSelector = adjustmentsSelector;
            this.booleanPercentileSelector = booleanPercentileSelector;
        }

        public IEnumerable<SpellQuantity> GeneratePerDay(CharacterClass characterClass, Dictionary<string, Stat> stats)
        {
            var spellcasters = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.Spellcasters);

            if (spellcasters.Contains(characterClass.Name) == false)
                return Enumerable.Empty<SpellQuantity>();

            var spellsPerDay = GetSpellsPerDay(characterClass, stats);
            return spellsPerDay.Where(s => s.Quantity > 0 || s.HasDomainSpell);
        }

        private IEnumerable<SpellQuantity> GetSpellsPerDay(CharacterClass characterClass, Dictionary<string, Stat> stats)
        {
            var tableName = string.Format(TableNameConstants.Formattable.Adjustments.LevelXCLASSSpellsPerDay, characterClass.Level, characterClass.Name);
            var spellsForClass = adjustmentsSelector.SelectFrom(tableName);

            var spellStat = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.StatGroups, characterClass.Name + GroupConstants.Spellcasters).Single();
            var maxSpellLevel = stats[spellStat].Value - 10;
            var spellsForCharacter = spellsForClass.Where(kvp => Convert.ToInt32(kvp.Key) <= maxSpellLevel);

            var spellsPerDay = new List<SpellQuantity>();

            foreach (var kvp in spellsForCharacter)
            {
                var spellQuantity = new SpellQuantity();
                spellQuantity.Level = Convert.ToInt32(kvp.Key);
                spellQuantity.Quantity = kvp.Value;
                spellQuantity.HasDomainSpell = characterClass.SpecialistFields.Any() && spellQuantity.Level > 0;

                if (spellQuantity.Level > 0 && spellQuantity.Level <= stats[spellStat].Bonus)
                {
                    var bonusSpells = (stats[spellStat].Bonus - spellQuantity.Level) / 4 + 1;
                    spellQuantity.Quantity += bonusSpells;
                }

                spellsPerDay.Add(spellQuantity);
            }

            return spellsPerDay;
        }

        public IEnumerable<Spell> GenerateKnown(CharacterClass characterClass, Dictionary<string, Stat> stats)
        {
            var spellcasters = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.Spellcasters);
            var spells = new List<Spell>();

            if (spellcasters.Contains(characterClass.Name) == false)
                return spells;

            var divineCasters = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, SpellConstants.Sources.Divine);

            if (divineCasters.Contains(characterClass.Name))
                return GetAllKnownSpells(characterClass, stats);

            var quantities = GetKnownSpellQuantities(characterClass, stats);

            foreach (var spellQuantity in quantities)
            {
                var levelSpells = GetRandomKnownSpellsForLevel(spellQuantity, characterClass);
                spells.AddRange(levelSpells);
            }

            return spells;
        }

        private IEnumerable<Spell> GetAllKnownSpells(CharacterClass characterClass, Dictionary<string, Stat> stats)
        {
            var tableName = string.Format(TableNameConstants.Formattable.Adjustments.LevelXCLASSKnownSpells, characterClass.Level, characterClass.Name);
            var spellsForClass = adjustmentsSelector.SelectFrom(tableName);

            var spellStat = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.StatGroups, characterClass.Name + GroupConstants.Spellcasters).Single();
            var maxSpellLevel = stats[spellStat].Value - 10;
            var spellsForCharacter = spellsForClass.Where(kvp => Convert.ToInt32(kvp.Key) <= maxSpellLevel);
            var spells = new List<Spell>();

            foreach (var kvp in spellsForCharacter)
            {
                var spellLevel = Convert.ToInt32(kvp.Key);
                var spellNames = GetSpellNames(characterClass, spellLevel);
                var specialistSpells = GetSpellNamesForFields(characterClass.SpecialistFields, spellLevel);

                spellNames = spellNames.Union(specialistSpells);

                foreach (var spellName in spellNames)
                {
                    var spell = BuildSpell(spellName, spellLevel);
                    spells.Add(spell);
                }
            }

            return spells;
        }

        private IEnumerable<string> GetSpellNames(CharacterClass characterClass)
        {
            var spellNames = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.SpellGroups, characterClass.Name);
            var forbiddenSpellNames = GetSpellNamesForFields(characterClass.ProhibitedFields);

            spellNames = spellNames.Except(forbiddenSpellNames);

            return spellNames;
        }

        private IEnumerable<string> GetSpellNames(CharacterClass characterClass, int level)
        {
            var tableName = string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, characterClass.Name);
            var spellLevels = adjustmentsSelector.SelectFrom(tableName);

            var spellNames = GetSpellNames(characterClass);
            spellNames = spellNames.Where(s => spellLevels[s] == level);

            return spellNames;
        }

        private Spell BuildSpell(string name, int level)
        {
            var spell = new Spell();
            spell.Name = name;
            spell.Level = level;

            return spell;
        }

        private IEnumerable<SpellQuantity> GetKnownSpellQuantities(CharacterClass characterClass, Dictionary<string, Stat> stats)
        {
            var tableName = string.Format(TableNameConstants.Formattable.Adjustments.LevelXCLASSKnownSpells, characterClass.Level, characterClass.Name);
            var spellsForClass = adjustmentsSelector.SelectFrom(tableName);

            var spellStat = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.StatGroups, characterClass.Name + GroupConstants.Spellcasters).Single();
            var maxSpellLevel = stats[spellStat].Value - 10;
            var spellQuantitiesForCharacter = spellsForClass.Where(kvp => Convert.ToInt32(kvp.Key) <= maxSpellLevel);

            var spellQuantities = new List<SpellQuantity>();
            var knowsMoreSpellsTableName = string.Format(TableNameConstants.Formattable.TrueOrFalse.CLASSKnowsAdditionalSpells, characterClass.Name);

            foreach (var kvp in spellQuantitiesForCharacter)
            {
                var spellQuantity = new SpellQuantity();
                spellQuantity.Level = Convert.ToInt32(kvp.Key);
                spellQuantity.HasDomainSpell = characterClass.SpecialistFields.Any() && spellQuantity.Level > 0;
                spellQuantity.Quantity = kvp.Value;

                while (booleanPercentileSelector.SelectFrom(knowsMoreSpellsTableName))
                    spellQuantity.Quantity++;

                spellQuantities.Add(spellQuantity);
            }

            return spellQuantities;
        }

        private IEnumerable<Spell> GetRandomKnownSpellsForLevel(SpellQuantity spellQuantity, CharacterClass characterClass)
        {
            var spellNames = GetSpellNames(characterClass, spellQuantity.Level);
            var knownSpells = new HashSet<Spell>();

            if (spellQuantity.Quantity >= spellNames.Count())
            {
                foreach (var spellName in spellNames)
                {
                    var spell = BuildSpell(spellName, spellQuantity.Level);
                    knownSpells.Add(spell);
                }

                return knownSpells;
            }

            while (spellQuantity.Quantity > knownSpells.Count)
            {
                var spellName = collectionsSelector.SelectRandomFrom(spellNames);
                var spell = BuildSpell(spellName, spellQuantity.Level);
                knownSpells.Add(spell);
            }

            var specialistSpellsForLevel = GetSpellNamesForFields(characterClass.SpecialistFields, spellQuantity.Level);
            var knownSpellNames = knownSpells.Select(s => s.Name);
            var unknownSpecialistSpells = specialistSpellsForLevel.Except(knownSpellNames);

            if (spellQuantity.HasDomainSpell && unknownSpecialistSpells.Any())
            {
                while (spellQuantity.Quantity + 1 > knownSpells.Count)
                {
                    var spellName = collectionsSelector.SelectRandomFrom(specialistSpellsForLevel);
                    var spell = BuildSpell(spellName, spellQuantity.Level);

                    knownSpells.Add(spell);
                }
            }
            else if (spellQuantity.HasDomainSpell)
            {
                while (spellQuantity.Quantity + 1 > knownSpells.Count)
                {
                    var spellName = collectionsSelector.SelectRandomFrom(spellNames);
                    var spell = BuildSpell(spellName, spellQuantity.Level);
                    knownSpells.Add(spell);
                }
            }


            return knownSpells;
        }

        public IEnumerable<Spell> GeneratePrepared(CharacterClass characterClass, IEnumerable<Spell> knownSpells, IEnumerable<SpellQuantity> spellsPerDay)
        {
            var spellcasters = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.Spellcasters);
            var spells = new List<Spell>();

            if (spellcasters.Contains(characterClass.Name) == false)
                return spells;

            foreach (var spellQuantity in spellsPerDay)
            {
                var levelSpells = GetPreparedSpellsForLevel(spellQuantity, knownSpells, characterClass);
                spells.AddRange(levelSpells);
            }

            return spells;
        }

        private IEnumerable<Spell> GetPreparedSpellsForLevel(SpellQuantity spellQuantity, IEnumerable<Spell> knownSpells, CharacterClass characterClass)
        {
            var preparedSpells = new List<Spell>();
            var knownSpellsForLevel = knownSpells.Where(s => s.Level == spellQuantity.Level);

            while (spellQuantity.Quantity > preparedSpells.Count)
            {
                var spell = collectionsSelector.SelectRandomFrom(knownSpellsForLevel);
                preparedSpells.Add(spell);
            }

            if (spellQuantity.HasDomainSpell)
            {
                var specialistSpells = GetSpellNamesForFields(characterClass.SpecialistFields);
                var specialistSpellsForLevel = knownSpellsForLevel.Where(s => specialistSpells.Contains(s.Name));
                var spell = collectionsSelector.SelectRandomFrom(specialistSpellsForLevel);

                preparedSpells.Add(spell);
            }

            return preparedSpells;
        }

        private IEnumerable<string> GetSpellNamesForFields(IEnumerable<string> fields)
        {
            var fieldSpellNames = new List<string>();

            foreach (var field in fields)
            {
                var fieldSpells = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.SpellGroups, field);
                fieldSpellNames.AddRange(fieldSpells);
            }

            return fieldSpellNames;
        }

        private IEnumerable<string> GetSpellNamesForFields(IEnumerable<string> fields, int spellLevel)
        {
            var fieldSpellNames = new List<string>();

            foreach (var field in fields)
            {
                var tableName = string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, field);
                var spellLevels = adjustmentsSelector.SelectFrom(tableName);

                var fieldSpells = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.SpellGroups, field);
                fieldSpells = fieldSpells.Where(s => spellLevels[s] == spellLevel);

                fieldSpellNames.AddRange(fieldSpells);
            }

            return fieldSpellNames;
        }
    }
}
