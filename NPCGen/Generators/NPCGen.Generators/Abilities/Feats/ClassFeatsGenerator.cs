using System;
using System.Collections.Generic;
using System.Linq;
using D20Dice;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Items;
using NPCGen.Generators.Interfaces.Abilities.Feats;
using NPCGen.Selectors.Interfaces;
using NPCGen.Selectors.Interfaces.Objects;
using NPCGen.Tables.Interfaces;

namespace NPCGen.Generators.Abilities.Feats
{
    public class ClassFeatsGenerator : IClassFeatsGenerator
    {
        private ICollectionsSelector collectionsSelector;
        private IFeatsSelector featsSelector;
        private IDice dice;

        public ClassFeatsGenerator(ICollectionsSelector collectionsSelector, IFeatsSelector featsSelector, IDice dice)
        {
            this.collectionsSelector = collectionsSelector;
            this.featsSelector = featsSelector;
            this.dice = dice;
        }

        public IEnumerable<Feat> GenerateWith(CharacterClass characterClass, Dictionary<String, Stat> stats)
        {
            var characterClassFeatSelections = featsSelector.SelectClass(characterClass.ClassName).ToList();

            foreach (var specialistField in characterClass.SpecialistFields)
            {
                var specialistFeatSelections = featsSelector.SelectClass(specialistField);
                characterClassFeatSelections.AddRange(specialistFeatSelections);
            }

            var relevantClassFeatSelections = characterClassFeatSelections.Where(f => f.MinimumLevel <= characterClass.Level);
            var classFeats = new List<Feat>();

            foreach (var classFeatSelection in relevantClassFeatSelections)
            {
                var classFeat = new Feat();
                classFeat.Name.Id = classFeatSelection.FeatId;
                classFeat.Focus = GetFocus(classFeatSelection, classFeats, characterClass, stats[StatConstants.Intelligence].Bonus);
                classFeat.Frequency = classFeatSelection.Frequency;
                classFeat.Strength = classFeatSelection.Strength;

                classFeats.Add(classFeat);
            }

            return CombineSpellMasteryFeats(classFeats);
        }

        private String GetFocus(CharacterClassFeatSelection featSelection, IEnumerable<Feat> feats, CharacterClass characterClass, Int32 intelligenceBonus)
        {
            if (featSelection.FeatId == FeatConstants.SpellMasteryId)
                return Convert.ToString(intelligenceBonus);

            if (String.IsNullOrEmpty(featSelection.FocusType))
                return String.Empty;

            var availableFoci = GetAvailableFoci(featSelection.FocusType, feats, featSelection.RequiredFeatIds);
            var usedFoci = feats.Where(f => f.Name.Id == featSelection.FeatId).Select(f => f.Focus);
            availableFoci = availableFoci.Except(usedFoci);

            if (featSelection.FocusType == TableNameConstants.Set.Collection.Groups.SchoolsOfMagic)
                availableFoci = availableFoci.Except(characterClass.ProhibitedFields);

            var spellcasters = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, TableNameConstants.Set.Collection.Groups.Spellcasters);
            if (!spellcasters.Contains(characterClass.ClassName))
                availableFoci = availableFoci.Except(new[] { WeaponProficiencyConstants.Ray });

            var index = GetRandomIndexOf(availableFoci);
            var focus = availableFoci.ElementAt(index);

            return focus;
        }

        private IEnumerable<String> GetAvailableFoci(String focusType, IEnumerable<Feat> otherFeats, IEnumerable<String> requiredFeatIds)
        {
            var requiredFeats = otherFeats.Where(f => requiredFeatIds.Contains(f.Name.Id));

            if (requiredFeats.Any(f => !String.IsNullOrEmpty(f.Focus)))
                return requiredFeats.Select(f => f.Focus);

            return collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, focusType);
        }

        private Int32 GetRandomIndexOf(IEnumerable<Object> collection)
        {
            var die = collection.Count();
            return dice.Roll().d(die) - 1;
        }

        private IEnumerable<Feat> CombineSpellMasteryFeats(IEnumerable<Feat> classFeats)
        {
            if (!classFeats.Any(f => f.Name.Id == FeatConstants.SpellMasteryId))
                return classFeats;

            var spellMasteryFeats = classFeats.Where(f => f.Name.Id == FeatConstants.SpellMasteryId);
            var spellMasteryFociTotal = spellMasteryFeats.Select(f => f.Focus).Cast<Int32>().Sum();
            var templateSpellMasteryFeat = spellMasteryFeats.First();

            var newSpellMasteryFeat = new Feat();
            newSpellMasteryFeat.Name.Id = templateSpellMasteryFeat.Name.Id;
            newSpellMasteryFeat.Frequency = templateSpellMasteryFeat.Frequency;
            newSpellMasteryFeat.Strength = templateSpellMasteryFeat.Strength;
            newSpellMasteryFeat.Focus = Convert.ToString(spellMasteryFociTotal);

            var classFeatsWithoutSpellMastery = classFeats.Except(spellMasteryFeats);
            return classFeatsWithoutSpellMastery.Union(new[] { newSpellMasteryFeat });
        }
    }
}