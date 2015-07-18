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
        private IFeatFocusGenerator featFocusGenerator;

        public AdditionalFeatsGenerator(ICollectionsSelector collectionsSelector, IFeatsSelector featsSelector, IDice dice, IFeatFocusGenerator featFocusGenerator)
        {
            this.collectionsSelector = collectionsSelector;
            this.featsSelector = featsSelector;
            this.dice = dice;
            this.featFocusGenerator = featFocusGenerator;
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
            var availableFeats = additionalFeats.Where(f => f.ImmutableRequirementsMet(baseAttack.Bonus, stats, skills, characterClass));

            var numberOfAdditionalFeats = characterClass.Level / 3 + 1;
            if (race.BaseRace == RaceConstants.BaseRaces.Human)
                numberOfAdditionalFeats++;
            if (characterClass.ClassName == CharacterClassConstants.Rogue && characterClass.Level >= 10)
                numberOfAdditionalFeats += (characterClass.Level - 10) / 3 + 1;

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

        private List<Feat> PopulateFeatsFrom(CharacterClass characterClass, Dictionary<String, Stat> stats, Dictionary<String, Skill> skills, BaseAttack baseAttack, IEnumerable<Feat> preselectedFeats, IEnumerable<AdditionalFeatSelection> sourceFeats, Int32 quantity)
        {
            var feats = new List<Feat>();
            var chosenFeats = preselectedFeats;
            var availableFeats = GetAvailableFeats(sourceFeats, chosenFeats);

            while (quantity-- > 0 && availableFeats.Any())
            {
                var index = GetRandomIndexOf(availableFeats);
                var featSelection = availableFeats.ElementAt(index);

                var preliminaryFocus = featFocusGenerator.GenerateFrom(featSelection.Feat, featSelection.FocusType, skills, featSelection.RequiredFeats, chosenFeats, characterClass);
                if (preliminaryFocus == ProficiencyConstants.All)
                {
                    quantity++;
                    sourceFeats = sourceFeats.Except(new[] { featSelection });
                    availableFeats = GetAvailableFeats(sourceFeats, chosenFeats);

                    continue;
                }

                var featInstanceQuantity = 1;
                if (featSelection.Feat == FeatConstants.SkillMastery)
                    featInstanceQuantity = stats[StatConstants.Intelligence].Bonus + featSelection.Strength;

                while (featInstanceQuantity-- > 0 && preliminaryFocus != ProficiencyConstants.All)
                {
                    var feat = new Feat();
                    feat.Name = featSelection.Feat;
                    feat.Focus = preliminaryFocus;
                    feat.Frequency = featSelection.Frequency;

                    if (featSelection.Feat == FeatConstants.SpellMastery)
                        feat.Strength = stats[StatConstants.Intelligence].Bonus;

                    feats.Add(feat);

                    chosenFeats = preselectedFeats.Union(feats);
                    availableFeats = GetAvailableFeats(sourceFeats, chosenFeats);

                    if (featInstanceQuantity > 0)
                        preliminaryFocus = featFocusGenerator.GenerateFrom(featSelection.Feat, featSelection.FocusType, skills, featSelection.RequiredFeats, chosenFeats, characterClass);
                }
            }

            return feats;
        }

        private IEnumerable<AdditionalFeatSelection> GetAvailableFeats(IEnumerable<AdditionalFeatSelection> sourceFeats, IEnumerable<Feat> chosenFeats)
        {
            var chosenFeatIds = chosenFeats.Select(f => f.Name);
            var featsWithRequirementsMet = sourceFeats.Where(f => f.MutableRequirementsMet(chosenFeats));
            var alreadyChosenFeats = sourceFeats.Where(f => f.FocusType == String.Empty && chosenFeatIds.Contains(f.Feat));

            var featIdsAllowingMultipleTakes = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.TakenMultipleTimes);
            var featsAllowingMultipleTakes = alreadyChosenFeats.Where(f => featIdsAllowingMultipleTakes.Contains(f.Feat));
            var excludedFeats = alreadyChosenFeats.Except(featsAllowingMultipleTakes);

            return featsWithRequirementsMet.Except(excludedFeats);
        }

        private Int32 GetRandomIndexOf(IEnumerable<Object> collection)
        {
            var die = collection.Count();
            return dice.Roll().d(die) - 1;
        }

        private IEnumerable<Feat> GetFighterFeats(CharacterClass characterClass, Race race, Dictionary<String, Stat> stats, Dictionary<String, Skill> skills,
            BaseAttack baseAttack, IEnumerable<Feat> selectedFeats)
        {
            var fighterFeatIds = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.FighterBonusFeats);
            var fighterFeats = featsSelector.SelectAdditional().Where(f => fighterFeatIds.Contains(f.Feat));
            var availableFeats = fighterFeats.Where(f => f.ImmutableRequirementsMet(baseAttack.Bonus, stats, skills, characterClass));

            var numberOfFighterFeats = characterClass.Level / 2 + 1;
            var feats = PopulateFeatsFrom(characterClass, stats, skills, baseAttack, selectedFeats, availableFeats, numberOfFighterFeats);

            return feats;
        }

        private IEnumerable<Feat> GetWizardBonusFeats(CharacterClass characterClass, Race race, Dictionary<string, Stat> stats, Dictionary<string, Skill> skills,
            BaseAttack baseAttack, IEnumerable<Feat> selectedFeats)
        {
            var wizardFeatIds = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.WizardBonusFeats);
            var wizardFeats = featsSelector.SelectAdditional().Where(f => wizardFeatIds.Contains(f.Feat));
            var availableFeats = wizardFeats.Where(f => f.ImmutableRequirementsMet(baseAttack.Bonus, stats, skills, characterClass));

            var numberOfWizardFeats = characterClass.Level / 5;
            var feats = PopulateFeatsFrom(characterClass, stats, skills, baseAttack, selectedFeats, availableFeats, numberOfWizardFeats);

            return feats;
        }
    }
}