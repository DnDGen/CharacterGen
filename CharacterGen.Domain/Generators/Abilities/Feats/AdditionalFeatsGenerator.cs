using CharacterGen.Abilities.Feats;
using CharacterGen.Abilities.Skills;
using CharacterGen.Abilities.Stats;
using CharacterGen.CharacterClasses;
using CharacterGen.Combats;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Selections;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Domain.Generators.Abilities.Feats
{
    internal class AdditionalFeatsGenerator : IAdditionalFeatsGenerator
    {
        private ICollectionsSelector collectionsSelector;
        private IFeatsSelector featsSelector;
        private IFeatFocusGenerator featFocusGenerator;
        private IAdjustmentsSelector adjustmentsSelector;

        public AdditionalFeatsGenerator(ICollectionsSelector collectionsSelector, IFeatsSelector featsSelector, IFeatFocusGenerator featFocusGenerator, IAdjustmentsSelector adjustmentsSelector)
        {
            this.collectionsSelector = collectionsSelector;
            this.featsSelector = featsSelector;
            this.featFocusGenerator = featFocusGenerator;
            this.adjustmentsSelector = adjustmentsSelector;
        }

        public IEnumerable<Feat> GenerateWith(CharacterClass characterClass, Race race, Dictionary<string, Stat> stats, IEnumerable<Skill> skills, BaseAttack baseAttack, IEnumerable<Feat> preselectedFeats)
        {
            var additionalFeats = GetAdditionalFeats(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var allButBonusFeats = preselectedFeats.Union(additionalFeats);
            var bonusFeats = GetBonusFeats(characterClass, race, stats, skills, baseAttack, allButBonusFeats);

            return additionalFeats.Union(bonusFeats);
        }

        private IEnumerable<Feat> GetAdditionalFeats(CharacterClass characterClass, Race race, Dictionary<string, Stat> stats, IEnumerable<Skill> skills, BaseAttack baseAttack, IEnumerable<Feat> preselectedFeats)
        {
            var additionalFeatSelections = featsSelector.SelectAdditional();
            var availableFeatSelections = additionalFeatSelections.Where(s => s.ImmutableRequirementsMet(baseAttack.RangedBonus, stats, skills, characterClass));

            var numberOfAdditionalFeats = characterClass.Level / 3 + 1;

            if (race.BaseRace == RaceConstants.BaseRaces.Human)
                numberOfAdditionalFeats++;

            if (characterClass.Name == CharacterClassConstants.Rogue && characterClass.Level >= 10)
                numberOfAdditionalFeats += (characterClass.Level - 10) / 3 + 1;

            var monsters = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters);
            if (monsters.Contains(race.BaseRace))
            {
                var monsterHitDice = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.MonsterHitDice, race.BaseRace);
                numberOfAdditionalFeats += monsterHitDice / 3 + 1;
            }

            var feats = PopulateFeatsFrom(characterClass, stats, skills, baseAttack, preselectedFeats, availableFeatSelections, numberOfAdditionalFeats);

            return feats;
        }

        private IEnumerable<Feat> GetBonusFeats(CharacterClass characterClass, Race race, Dictionary<string, Stat> stats, IEnumerable<Skill> skills, BaseAttack baseAttack, IEnumerable<Feat> preselectedFeats)
        {
            if (characterClass.Name == CharacterClassConstants.Fighter)
                return GetFighterFeats(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            else if (characterClass.Name == CharacterClassConstants.Wizard)
                return GetWizardBonusFeats(characterClass, race, stats, skills, baseAttack, preselectedFeats);

            return Enumerable.Empty<Feat>();
        }

        private List<Feat> PopulateFeatsFrom(CharacterClass characterClass, Dictionary<string, Stat> stats, IEnumerable<Skill> skills, BaseAttack baseAttack, IEnumerable<Feat> preselectedFeats, IEnumerable<AdditionalFeatSelection> sourceFeats, Int32 quantity)
        {
            var feats = new List<Feat>();
            var chosenFeats = preselectedFeats;
            var availableFeats = GetAvailableFeats(sourceFeats, chosenFeats);

            while (quantity-- > 0 && availableFeats.Any())
            {
                var featSelection = collectionsSelector.SelectRandomFrom(availableFeats);

                var preliminaryFocus = featFocusGenerator.GenerateFrom(featSelection.Feat, featSelection.FocusType, skills, featSelection.RequiredFeats, chosenFeats, characterClass);
                if (preliminaryFocus == FeatConstants.Foci.All)
                {
                    quantity++;
                    sourceFeats = sourceFeats.Except(new[] { featSelection });
                    availableFeats = GetAvailableFeats(sourceFeats, chosenFeats);

                    continue;
                }

                var feat = new Feat();
                var hasMatchingFeat = feats.Any(f => FeatsMatch(f, featSelection));

                if (hasMatchingFeat)
                {
                    feat = feats.First(f => FeatsMatch(f, featSelection));
                }
                else
                {
                    feat.Name = featSelection.Feat;
                    feat.Frequency = featSelection.Frequency;
                    feat.Power = featSelection.Power;

                    if (featSelection.Feat == FeatConstants.SpellMastery)
                        feat.Power = stats[StatConstants.Intelligence].Bonus;

                    feats.Add(feat);
                }

                chosenFeats = preselectedFeats.Union(feats);
                availableFeats = GetAvailableFeats(sourceFeats, chosenFeats);

                if (string.IsNullOrEmpty(preliminaryFocus) == false)
                    feat.Foci = feat.Foci.Union(new[] { preliminaryFocus });

                var featFociQuantity = 0;
                if (featSelection.Feat == FeatConstants.SkillMastery)
                    featFociQuantity = stats[StatConstants.Intelligence].Bonus + featSelection.Power - 1;

                while (featFociQuantity-- > 0 && preliminaryFocus != FeatConstants.Foci.All && string.IsNullOrEmpty(preliminaryFocus) == false)
                {
                    preliminaryFocus = featFocusGenerator.GenerateFrom(featSelection.Feat, featSelection.FocusType, skills, featSelection.RequiredFeats, chosenFeats, characterClass);
                    feat.Foci = feat.Foci.Union(new[] { preliminaryFocus });
                }
            }

            return feats;
        }

        private bool FeatsMatch(Feat feat, AdditionalFeatSelection additionalFeatSelection)
        {
            return feat.Frequency.TimePeriod == string.Empty && feat.Name == additionalFeatSelection.Feat
                   && feat.Power == additionalFeatSelection.Power
                   && feat.Foci.Any() && string.IsNullOrEmpty(additionalFeatSelection.FocusType) == false;
        }

        private IEnumerable<AdditionalFeatSelection> GetAvailableFeats(IEnumerable<AdditionalFeatSelection> sourceFeats, IEnumerable<Feat> chosenFeats)
        {
            var chosenFeatNames = chosenFeats.Select(f => f.Name);
            var featsWithRequirementsMet = sourceFeats.Where(f => f.MutableRequirementsMet(chosenFeats));
            var alreadyChosenFeats = sourceFeats.Where(f => f.FocusType == string.Empty && chosenFeatNames.Contains(f.Feat));

            var featIdsAllowingMultipleTakes = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.TakenMultipleTimes);
            var featsAllowingMultipleTakes = alreadyChosenFeats.Where(f => featIdsAllowingMultipleTakes.Contains(f.Feat));
            var excludedFeats = alreadyChosenFeats.Except(featsAllowingMultipleTakes);

            return featsWithRequirementsMet.Except(excludedFeats);
        }

        private IEnumerable<Feat> GetFighterFeats(CharacterClass characterClass, Race race, Dictionary<string, Stat> stats, IEnumerable<Skill> skills, BaseAttack baseAttack, IEnumerable<Feat> selectedFeats)
        {
            var fighterFeatIds = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.FighterBonusFeats);
            var fighterFeats = featsSelector.SelectAdditional().Where(f => fighterFeatIds.Contains(f.Feat));
            var availableFeats = fighterFeats.Where(f => f.ImmutableRequirementsMet(baseAttack.RangedBonus, stats, skills, characterClass));

            var numberOfFighterFeats = characterClass.Level / 2 + 1;
            var feats = PopulateFeatsFrom(characterClass, stats, skills, baseAttack, selectedFeats, availableFeats, numberOfFighterFeats);

            return feats;
        }

        private IEnumerable<Feat> GetWizardBonusFeats(CharacterClass characterClass, Race race, Dictionary<string, Stat> stats, IEnumerable<Skill> skills, BaseAttack baseAttack, IEnumerable<Feat> selectedFeats)
        {
            var wizardFeatIds = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.WizardBonusFeats);
            var wizardFeats = featsSelector.SelectAdditional().Where(f => wizardFeatIds.Contains(f.Feat));
            var availableFeats = wizardFeats.Where(f => f.ImmutableRequirementsMet(baseAttack.RangedBonus, stats, skills, characterClass));

            var numberOfWizardFeats = characterClass.Level / 5;
            var feats = PopulateFeatsFrom(characterClass, stats, skills, baseAttack, selectedFeats, availableFeats, numberOfWizardFeats);

            return feats;
        }
    }
}