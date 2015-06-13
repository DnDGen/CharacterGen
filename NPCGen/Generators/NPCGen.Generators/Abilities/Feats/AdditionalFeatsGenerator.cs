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
using NPCGen.Generators.Interfaces.Abilities;
using NPCGen.Generators.Interfaces.Abilities.Feats;
using NPCGen.Selectors.Interfaces;
using NPCGen.Selectors.Interfaces.Objects;
using NPCGen.Tables.Interfaces;

namespace NPCGen.Generators.Abilities.Feats
{
    public class AdditionalFeatsGenerator : IAdditionalFeatsGenerator
    {
        private ICollectionsSelector collectionsSelector;
        private IAdjustmentsSelector adjustmentsSelector;
        private IFeatsSelector featsSelector;
        private IDice dice;
        private INameSelector nameSelector;

        public AdditionalFeatsGenerator(ICollectionsSelector collectionsSelector, IAdjustmentsSelector adjustmentsSelector,
            IFeatsSelector featsSelector, IDice dice, INameSelector nameSelector)
        {
            this.collectionsSelector = collectionsSelector;
            this.adjustmentsSelector = adjustmentsSelector;
            this.featsSelector = featsSelector;
            this.dice = dice;
            this.nameSelector = nameSelector;
        }

        public IEnumerable<Feat> GenerateWith(CharacterClass characterClass, Race race, Dictionary<String, Stat> stats,
            Dictionary<String, Skill> skills, BaseAttack baseAttack)
        {
            var additionalFeats = GetAdditionalFeats(characterClass, race, stats, skills, baseAttack, automaticFeats);
            var bonusFeats = Enumerable.Empty<Feat>();

            if (characterClass.ClassName == CharacterClassConstants.Fighter)
                bonusFeats = GetFighterFeats(characterClass, race, stats, skills, baseAttack, feats);
            else if (characterClass.ClassName == CharacterClassConstants.Wizard)
                bonusFeats = GetWizardBonusFeats(characterClass, race, stats, skills, baseAttack, feats);

            return additionalFeats.Union(bonusFeats);
        }

        private IEnumerable<Feat> GetAdditionalFeats(CharacterClass characterClass, Race race, Dictionary<String, Stat> stats, Dictionary<String, Skill> skills,
            BaseAttack baseAttack, IEnumerable<Feat> automaticFeats)
        {
            var additionalFeats = featsSelector.SelectAdditional();
            var availableFeats = additionalFeats.Where(f => f.ImmutableRequirementsMet(baseAttack.Bonus, stats, skills, characterClass.ClassName));

            var numberOfAdditionalFeats = characterClass.Level / 3 + 1;
            if (race.BaseRace.Id == RaceConstants.BaseRaces.HumanId)
                numberOfAdditionalFeats++;

            var feats = PopulateFeatsFrom(characterClass, stats, skills, baseAttack, automaticFeats, availableFeats, numberOfAdditionalFeats);

            return feats;
        }

        private String GetFocusOf(Feat feat, AdditionalFeatSelection sourceFeat, IEnumerable<Feat> feats, CharacterClass characterClass, Int32 intelligenceBonus)
        {
            if (feat.Name.Id == FeatConstants.SpellMasteryId)
            {
                if (!feats.Any(f => f.Name.Id == FeatConstants.SpellMasteryId))
                    return Convert.ToString(intelligenceBonus);

                var previousSpellMastery = feats.First(f => f.Name.Id == FeatConstants.SpellMasteryId);
                var previousSpellCount = Convert.ToInt32(previousSpellMastery.Focus);
                var newSpellCount = previousSpellCount + intelligenceBonus;

                return Convert.ToString(newSpellCount);
            }

            if (String.IsNullOrEmpty(sourceFeat.FocusType))
                return String.Empty;

            var specificApplications = GetSpecificApplications(feats, sourceFeat);
            var usedSpecificApplications = feats.Where(f => f.Name == feat.Name).Select(f => f.Focus);
            specificApplications = specificApplications.Except(usedSpecificApplications);

            if (sourceFeat.FocusType == TableNameConstants.Set.Collection.Groups.SchoolsOfMagic)
                specificApplications = specificApplications.Except(characterClass.ProhibitedFields);

            var spellcasters = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, TableNameConstants.Set.Collection.Groups.Spellcasters);
            if (!spellcasters.Contains(characterClass.ClassName))
                specificApplications = specificApplications.Except(new[] { WeaponProficiencyConstants.Ray });

            var index = GetRandomIndexOf(specificApplications);
            var specificApplication = specificApplications.ElementAt(index);

            return specificApplication;
        }

        private IEnumerable<String> GetSpecificApplications(IEnumerable<Feat> otherFeats, AdditionalFeatSelection sourceFeat)
        {
            if (otherFeats.Any(f => RequirementsHaveFoci(sourceFeat, f)))
                return otherFeats.Where(f => RequirementsHaveFoci(sourceFeat, f)).Select(f => f.Focus);

            return collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, sourceFeat.FocusType);
        }

        private List<Feat> PopulateFeatsFrom(CharacterClass characterClass, Dictionary<String, Stat> stats, Dictionary<String, Skill> skills, BaseAttack baseAttack, IEnumerable<Feat> preselectedFeats, IEnumerable<AdditionalFeatSelection> sourceFeats, Int32 quantity)
        {
            var feats = new List<Feat>();

            while (quantity-- > 0)
            {
                var availableFeats = sourceFeats.Where(f => f.MutableRequirementsMet(feats))
                                                .Select(f => new Feat { Name = f.Name, Focus = f.FocusType })
                                                .Except(feats)
                                                .Except(preselectedFeats);

                if (!availableFeats.Any())
                    break;

                var index = GetRandomIndexOf(availableFeats);
                var feat = availableFeats.ElementAt(index);
                var sourceFeat = sourceFeats.First(f => f.Name == feat.Name);

                feat.Focus = GetFocusOf(feat, sourceFeat, feats, characterClass, stats[StatConstants.Intelligence].Bonus);

                if (feat.Name.Id == FeatConstants.SpellMasteryId && feats.Any(f => f.Name.Id == FeatConstants.SpellMasteryId))
                {
                    var spellMasteryFeat = feats.First(f => f.Name.Id == FeatConstants.SpellMasteryId);
                    feats.Remove(spellMasteryFeat);
                }

                feats.Add(feat);
            }

            return feats;
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