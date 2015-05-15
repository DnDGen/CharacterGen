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
using NPCGen.Selectors.Interfaces;
using NPCGen.Selectors.Interfaces.Objects;
using NPCGen.Tables.Interfaces;

namespace NPCGen.Generators.Abilities.Feats
{
    public class FeatsGenerator : IFeatsGenerator
    {
        private ICollectionsSelector collectionsSelector;
        private IAdjustmentsSelector adjustmentsSelector;
        private IFeatsSelector featsSelector;
        private IDice dice;
        private INameSelector nameSelector;

        public FeatsGenerator(ICollectionsSelector collectionsSelector, IAdjustmentsSelector adjustmentsSelector,
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
            var automaticFeats = GetAutomaticFeats(characterClass, race, skills, stats);
            var additionalFeats = GetAdditionalFeats(characterClass, race, stats, skills, baseAttack, automaticFeats);

            var feats = automaticFeats.Union(additionalFeats);
            var bonusFeats = Enumerable.Empty<Feat>();

            if (characterClass.ClassName == CharacterClassConstants.Fighter)
                bonusFeats = GetFighterFeats(characterClass, race, stats, skills, baseAttack, feats);
            else if (characterClass.ClassName == CharacterClassConstants.Wizard)
                bonusFeats = GetWizardBonusFeats(characterClass, race, stats, skills, baseAttack, feats);

            return feats.Union(bonusFeats);
        }

        private IEnumerable<Feat> GetAutomaticFeats(CharacterClass characterClass, Race race, Dictionary<String, Skill> skills, Dictionary<String, Stat> stats)
        {
            var racialFeats = GetRacialFeats(race);
            var classFeats = GetClassFeats(characterClass, stats);
            var skillSynergyFeats = GetSkillSynergyFeats(skills);

            return racialFeats.Union(classFeats).Union(skillSynergyFeats);
        }

        private IEnumerable<Feat> GetRacialFeats(Race race)
        {
            var racialFeats = featsSelector.SelectRacial();
            var monsterHitDice = GetMonsterHitDice(race.BaseRace.Id);

            var applicableRacialFeats = racialFeats.Where(f => f.RequirementsMet(race, monsterHitDice));
            applicableRacialFeats = OverwriteOverwritableStrengths(applicableRacialFeats);
            applicableRacialFeats = AddCumulativeStrengths(applicableRacialFeats);

            var feats = new HashSet<Feat>();
            foreach (var racialFeat in applicableRacialFeats)
            {
                var feat = new Feat();
                feat.Name = racialFeat.Name;
                feat.Focus = GetSpecificApplicationFromStrength(racialFeat.FeatStrength);

                feats.Add(feat);
            }

            return feats;
        }

        private Int32 GetMonsterHitDice(String baseRace)
        {
            var monsters = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.Names, TableNameConstants.Set.Collection.Groups.Monsters);
            if (!monsters.Contains(baseRace))
                return 1;

            var hitDice = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.MonsterHitDice);
            return hitDice[baseRace];
        }

        private IEnumerable<RacialFeatSelection> OverwriteOverwritableStrengths(IEnumerable<RacialFeatSelection> racialFeats)
        {
            var overwritableStrengthIds = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups,
                TableNameConstants.Set.Collection.Groups.OverwrittenStrengths);
            var featToOverwrite = racialFeats.Where(f => overwritableStrengthIds.Contains(f.Name.Id));
            var notOverwrittenStrengths = racialFeats.Except(featToOverwrite);

            var overwrittenStrengths = new List<RacialFeatSelection>();
            foreach (var feat in featToOverwrite)
            {
                var overwritableFeats = featToOverwrite.Where(f => f.Name.Id == feat.Name.Id);
                var max = overwritableFeats.Max(f => f.FeatStrength);

                if (feat.FeatStrength == max)
                    overwrittenStrengths.Add(feat);
            }

            return overwrittenStrengths.Union(notOverwrittenStrengths);
        }

        private IEnumerable<RacialFeatSelection> AddCumulativeStrengths(IEnumerable<RacialFeatSelection> racialFeats)
        {
            var cumulativeStrengthIds = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, TableNameConstants.Set.Collection.Groups.CumulativeStrengths);
            var featToSum = racialFeats.Where(f => cumulativeStrengthIds.Contains(f.Name.Id));
            var unsummedStrengths = racialFeats.Except(featToSum);
            var featIdsToSum = featToSum.Select(f => f.Name.Id).Distinct();

            var summedStrengths = new List<RacialFeatSelection>();
            foreach (var featId in featIdsToSum)
            {
                var summableFeats = featToSum.Where(f => f.Name.Id == featId);

                var selection = new RacialFeatSelection();
                selection.Name.Id = featId;
                selection.FeatStrength = summableFeats.Sum(f => f.FeatStrength);

                summedStrengths.Add(selection);
            }

            return summedStrengths.Union(unsummedStrengths);
        }

        private String GetSpecificApplicationFromStrength(Int32 strength)
        {
            if (strength == 0)
                return String.Empty;

            return Convert.ToString(strength);
        }

        private IEnumerable<Feat> GetClassFeats(CharacterClass characterClass, Dictionary<String, Stat> stats)
        {
            var allClassFeats = featsSelector.SelectClassFeats();
            var relevantClassFeats = allClassFeats.Where(f => f.RequirementsSatisfied(characterClass));
            var classFeats = new List<Feat>();

            foreach (var classFeatSelection in relevantClassFeats)
            {
                var classFeat = new Feat();
                classFeat.Name = classFeatSelection.Name;

                var sourceFeat = featsSelector.SelectAdditional(classFeatSelection.Name.Id);

                if (!String.IsNullOrEmpty(sourceFeat.FocusType))
                    classFeat.Focus = GetSpecificApplicationOf(classFeat, sourceFeat, classFeats, characterClass, stats[StatConstants.Intelligence].Bonus);
                else
                    classFeat.Focus = GetSpecificApplicationFromStrength(classFeatSelection.Strength);

                if (classFeats.Any(f => AlreadyHaveStrongerFeat(f, classFeatSelection)) == false)
                    classFeats.Add(classFeat);

                var weakerFeats = classFeats.Where(f => FeatIsWeaker(f, classFeatSelection));
                if (weakerFeats.Any())
                {
                    var newFeats = classFeats.Except(weakerFeats);
                    classFeats = new List<Feat>(newFeats);
                }
            }

            return classFeats;
        }

        private Boolean AlreadyHaveStrongerFeat(Feat feat, CharacterClassFeatSelection classFeatSelection)
        {
            if (feat.Name.Id != classFeatSelection.Name.Id)
                return false;

            var strength = 0;
            if (!Int32.TryParse(feat.Focus, out strength))
                return false;

            return strength >= classFeatSelection.Strength;
        }

        private Boolean FeatIsWeaker(Feat feat, CharacterClassFeatSelection classFeatSelection)
        {
            if (feat.Name.Id != classFeatSelection.Name.Id)
                return false;

            var strength = 0;
            if (!Int32.TryParse(feat.Focus, out strength))
                return false;

            return strength < classFeatSelection.Strength;
        }

        private String GetSpecificApplicationOf(Feat feat, AdditionalFeatSelection sourceFeat, IEnumerable<Feat> feats, CharacterClass characterClass, Int32 intelligenceBonus)
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

            if (sourceFeat.FocusType == AdditionalFeatSelectionConstants.SchoolsOfMagic)
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
            if (otherFeats.Any(f => RequirementsHaveSpecificApplications(sourceFeat, f)))
                return otherFeats.Where(f => RequirementsHaveSpecificApplications(sourceFeat, f)).Select(f => f.Focus);

            return collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, sourceFeat.FocusType);
        }

        private IEnumerable<Feat> GetSkillSynergyFeats(Dictionary<String, Skill> skills)
        {
            var synergyFeatIds = new List<String>();
            var synergyQualifyingSkills = skills.Where(kvp => kvp.Value.EffectiveRanks >= 5).Select(kvp => kvp.Key);

            foreach (var skill in synergyQualifyingSkills)
            {
                var synergy = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.SkillSynergyFeats, skill);
                synergyFeatIds.AddRange(synergy);
            }

            return synergyFeatIds.Select(f => CreateFeat(f));
        }

        private Feat CreateFeat(String id)
        {
            var feat = new Feat();
            feat.Name.Id = id;
            feat.Name.Name = nameSelector.Select(id);

            return feat;
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

                feat.Focus = GetSpecificApplicationOf(feat, sourceFeat, feats, characterClass, stats[StatConstants.Intelligence].Bonus);

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

        private Boolean RequirementsHaveSpecificApplications(AdditionalFeatSelection sourceFeat, Feat feat)
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