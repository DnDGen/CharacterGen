using System;
using System.Collections.Generic;
using System.Linq;
using D20Dice;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Abilities.Skills;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Combats;
using NPCGen.Common.Items;
using NPCGen.Common.Races;
using NPCGen.Generators.Interfaces.Abilities.Feats;
using NPCGen.Selectors.Interfaces;
using NPCGen.Selectors.Interfaces.Objects;
using NPCGen.Tables.Interfaces;

namespace NPCGen.Generators.Abilities.Feats
{
    public class AdditionalFeatsGenerator : IAdditionalFeatsGenerator
    {
        private ICollectionsSelector collectionsSelector;
        private IFeatsSelector featsSelector;
        private IDice dice;

        public AdditionalFeatsGenerator(ICollectionsSelector collectionsSelector, IFeatsSelector featsSelector, IDice dice)
        {
            this.collectionsSelector = collectionsSelector;
            this.featsSelector = featsSelector;
            this.dice = dice;
        }

        public IEnumerable<Feat> GenerateWith(CharacterClass characterClass, Race race, Dictionary<String, Stat> stats,
            Dictionary<String, Skill> skills, BaseAttack baseAttack, IEnumerable<Feat> preselectedFeats)
        {
            var additionalFeats = GetAdditionalFeats(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var allButBonusFeats = preselectedFeats.Union(additionalFeats);
            var bonusFeats = GetBonusFeats(characterClass, race, stats, skills, baseAttack, allButBonusFeats);

            return additionalFeats.Union(bonusFeats);
        }

        private IEnumerable<Feat> GetAdditionalFeats(CharacterClass characterClass, Race race, Dictionary<String, Stat> stats, Dictionary<String, Skill> skills,
            BaseAttack baseAttack, IEnumerable<Feat> preselectedFeats)
        {
            var additionalFeats = featsSelector.SelectAdditional();
            var availableFeats = additionalFeats.Where(f => f.ImmutableRequirementsMet(baseAttack.Bonus, stats, skills, characterClass.ClassName));

            var numberOfAdditionalFeats = characterClass.Level / 3 + 1;
            if (race.BaseRace.Id == RaceConstants.BaseRaces.HumanId)
                numberOfAdditionalFeats++;

            var feats = PopulateFeatsFrom(characterClass, stats, skills, baseAttack, preselectedFeats, availableFeats, numberOfAdditionalFeats);

            return feats;
        }

        private IEnumerable<Feat> GetBonusFeats(CharacterClass characterClass, Race race, Dictionary<String, Stat> stats,
            Dictionary<String, Skill> skills, BaseAttack baseAttack, IEnumerable<Feat> preselectedFeats)
        {
            if (characterClass.ClassName == CharacterClassConstants.Fighter)
                return GetFighterFeats(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            else if (characterClass.ClassName == CharacterClassConstants.Wizard)
                return GetWizardBonusFeats(characterClass, race, stats, skills, baseAttack, preselectedFeats);

            return Enumerable.Empty<Feat>();
        }

        private String GetFocusOf(AdditionalFeatSelection featSelection, IEnumerable<Feat> feats, CharacterClass characterClass)
        {
            if (String.IsNullOrEmpty(featSelection.FocusType))
                return String.Empty;

            var foci = GetFoci(feats, featSelection);
            var usedFoci = feats.Where(f => f.Name.Id == featSelection.FeatId).Select(f => f.Focus);
            foci = foci.Except(usedFoci);

            if (featSelection.FocusType == TableNameConstants.Set.Collection.Groups.SchoolsOfMagic)
                foci = foci.Except(characterClass.ProhibitedFields);

            var spellcasters = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, TableNameConstants.Set.Collection.Groups.Spellcasters);
            if (spellcasters.Contains(characterClass.ClassName) == false)
                foci = foci.Except(new[] { WeaponProficiencyConstants.Ray });

            var index = GetRandomIndexOf(foci);
            var focus = foci.ElementAt(index);

            return focus;
        }

        private IEnumerable<String> GetFoci(IEnumerable<Feat> otherFeats, AdditionalFeatSelection sourceFeat)
        {
            if (otherFeats.Any(f => RequirementsHaveFoci(sourceFeat, f)))
                return otherFeats.Where(f => RequirementsHaveFoci(sourceFeat, f)).Select(f => f.Focus);

            return collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, sourceFeat.FocusType);
        }

        private List<Feat> PopulateFeatsFrom(CharacterClass characterClass, Dictionary<String, Stat> stats, Dictionary<String, Skill> skills, BaseAttack baseAttack, IEnumerable<Feat> preselectedFeats, IEnumerable<AdditionalFeatSelection> sourceFeats, Int32 quantity)
        {
            var feats = new List<Feat>();
            var chosenFeats = preselectedFeats;
            var availableFeats = GetAvailableFeats(sourceFeats, chosenFeats);

            while (quantity-- > 0 && availableFeats.Any())
            {
                var index = GetRandomIndexOf(availableFeats);
                var featSelection = availableFeats.ElementAt(index);

                var feat = new Feat();
                feat.Name.Id = featSelection.FeatId;
                feat.Focus = GetFocusOf(featSelection, chosenFeats, characterClass);
                feat.Frequency = featSelection.Frequency;

                if (featSelection.FeatId == FeatConstants.SpellMasteryId)
                    feat.Strength = stats[StatConstants.Intelligence].Bonus;

                feats.Add(feat);

                chosenFeats = preselectedFeats.Union(feats);
                availableFeats = GetAvailableFeats(sourceFeats, chosenFeats);
            }

            return feats;
        }

        private IEnumerable<AdditionalFeatSelection> GetAvailableFeats(IEnumerable<AdditionalFeatSelection> sourceFeats, IEnumerable<Feat> chosenFeats)
        {
            var chosenFeatIds = chosenFeats.Select(f => f.Name.Id);
            var featsWithRequirementsMet = sourceFeats.Where(f => f.MutableRequirementsMet(chosenFeatIds));
            var alreadyChosenFeats = sourceFeats.Where(f => f.FocusType == String.Empty && chosenFeatIds.Contains(f.FeatId));

            var featIdsAllowingMultipleTakes = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, TableNameConstants.Set.Collection.Groups.TakenMultipleTimes);
            var featsAllowingMultipleTakes = alreadyChosenFeats.Where(f => featIdsAllowingMultipleTakes.Contains(f.FeatId));
            var excludedFeats = alreadyChosenFeats.Except(featsAllowingMultipleTakes);

            return featsWithRequirementsMet.Except(excludedFeats);
        }

        private Int32 GetRandomIndexOf(IEnumerable<Object> collection)
        {
            var die = collection.Count();
            return dice.Roll().d(die) - 1;
        }

        private Boolean RequirementsHaveFoci(AdditionalFeatSelection sourceFeat, Feat feat)
        {
            return sourceFeat.RequiredFeatIds.Contains(feat.Name.Id) && !String.IsNullOrEmpty(feat.Focus);
        }

        private IEnumerable<Feat> GetFighterFeats(CharacterClass characterClass, Race race, Dictionary<String, Stat> stats, Dictionary<String, Skill> skills,
            BaseAttack baseAttack, IEnumerable<Feat> selectedFeats)
        {
            var fighterFeats = featsSelector.SelectAdditional().Where(f => f.IsFighterFeat);
            var availableFeats = fighterFeats.Where(f => f.ImmutableRequirementsMet(baseAttack.Bonus, stats, skills, characterClass.ClassName));

            var numberOfFighterFeats = characterClass.Level / 2 + 1;
            var feats = PopulateFeatsFrom(characterClass, stats, skills, baseAttack, selectedFeats, availableFeats, numberOfFighterFeats);

            return feats;
        }

        private IEnumerable<Feat> GetWizardBonusFeats(CharacterClass characterClass, Race race, Dictionary<string, Stat> stats, Dictionary<string, Skill> skills,
            BaseAttack baseAttack, IEnumerable<Feat> selectedFeats)
        {
            var wizardFeats = featsSelector.SelectAdditional().Where(f => f.IsWizardFeat);
            var availableFeats = wizardFeats.Where(f => f.ImmutableRequirementsMet(baseAttack.Bonus, stats, skills, characterClass.ClassName));

            var numberOfWizardFeats = characterClass.Level / 5;
            var feats = PopulateFeatsFrom(characterClass, stats, skills, baseAttack, selectedFeats, availableFeats, numberOfWizardFeats);

            return feats;
        }
    }
}