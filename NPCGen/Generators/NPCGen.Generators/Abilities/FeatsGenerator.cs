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

namespace NPCGen.Generators.Abilities
{
    public class FeatsGenerator : IFeatsGenerator
    {
        private ICollectionsSelector collectionsSelector;
        private IAdjustmentsSelector adjustmentsSelector;
        private IFeatsSelector featsSelector;
        private IDice dice;

        public FeatsGenerator(ICollectionsSelector collectionsSelector, IAdjustmentsSelector adjustmentsSelector,
            IFeatsSelector featsSelector, IDice dice)
        {
            this.collectionsSelector = collectionsSelector;
            this.adjustmentsSelector = adjustmentsSelector;
            this.featsSelector = featsSelector;
            this.dice = dice;
        }

        public IEnumerable<Feat> GenerateWith(CharacterClass characterClass, Race race, Dictionary<String, Stat> stats,
            Dictionary<String, Skill> skills, BaseAttack baseAttack)
        {
            var automaticFeats = GetAutomaticFeats(characterClass, race, skills);
            var additionalFeats = GetAdditionalFeats(characterClass, race, stats, skills, baseAttack, automaticFeats);

            var feats = automaticFeats.Union(additionalFeats);
            var bonusFeats = Enumerable.Empty<Feat>();

            if (characterClass.ClassName == CharacterClassConstants.Fighter)
                bonusFeats = GetFighterFeats(characterClass, race, stats, skills, baseAttack, feats);
            else if (characterClass.ClassName == CharacterClassConstants.Wizard)
                bonusFeats = GetWizardBonusFeats(characterClass, race, stats, skills, baseAttack, feats);

            return feats.Union(bonusFeats);
        }

        private IEnumerable<Feat> GetAutomaticFeats(CharacterClass characterClass, Race race, Dictionary<String, Skill> skills)
        {
            var racialFeats = GetRacialFeats(race);
            var classFeats = GetClassFeats(characterClass);
            var skillSynergyFeats = GetSkillSynergyFeats(skills);

            return racialFeats.Union(classFeats).Union(skillSynergyFeats);
        }

        private IEnumerable<Feat> GetRacialFeats(Race race)
        {
            var racialFeats = featsSelector.SelectRacial();
            var monsterHitDice = GetMonsterHitDice(race.BaseRace);

            var applicableRacialFeats = racialFeats.Where(f => f.RequirementsMet(race, monsterHitDice));
            applicableRacialFeats = OverwriteOverwritableStrengths(applicableRacialFeats);
            applicableRacialFeats = AddCumulativeStrengths(applicableRacialFeats);

            var feats = new HashSet<Feat>();
            foreach (var racialFeat in applicableRacialFeats)
            {
                var feat = new Feat();
                feat.Name = racialFeat.FeatName;
                feat.SpecificApplication = GetSpecificApplicationFromStrength(racialFeat.FeatStrength);

                feats.Add(feat);
            }

            return feats;
        }

        private Int32 GetMonsterHitDice(String baseRace)
        {
            var monsters = collectionsSelector.SelectFrom("BaseRaceGroups", "Monsters");
            if (!monsters.Contains(baseRace))
                return 1;

            var hitDice = adjustmentsSelector.SelectFrom("MonsterHitDice");
            return hitDice[baseRace];
        }

        private IEnumerable<RacialFeatSelection> OverwriteOverwritableStrengths(IEnumerable<RacialFeatSelection> racialFeats)
        {
            var overwritableStrengthNames = collectionsSelector.SelectFrom("FeatGroups", "Overwritten Strengths");
            var featToOverwrite = racialFeats.Where(f => overwritableStrengthNames.Contains(f.FeatName));
            var notOverwrittenStrengths = racialFeats.Except(featToOverwrite);

            var overwrittenStrengths = new List<RacialFeatSelection>();
            foreach (var feat in featToOverwrite)
            {
                var overwritableFeats = featToOverwrite.Where(f => f.FeatName == feat.FeatName);
                var max = overwritableFeats.Max(f => f.FeatStrength);

                if (feat.FeatStrength == max)
                    overwrittenStrengths.Add(feat);
            }

            return overwrittenStrengths.Union(notOverwrittenStrengths);
        }

        private IEnumerable<RacialFeatSelection> AddCumulativeStrengths(IEnumerable<RacialFeatSelection> racialFeats)
        {
            var cumulativeStrengthNames = collectionsSelector.SelectFrom("FeatGroups", "Cumulative Strengths");
            var featToSum = racialFeats.Where(f => cumulativeStrengthNames.Contains(f.FeatName));
            var unsummedStrengths = racialFeats.Except(featToSum);
            var featNamesToSum = featToSum.Select(f => f.FeatName).Distinct();

            var summedStrengths = new List<RacialFeatSelection>();
            foreach (var featName in featNamesToSum)
            {
                var summableFeats = featToSum.Where(f => f.FeatName == featName);

                var selection = new RacialFeatSelection();
                selection.FeatName = featName;
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

        private IEnumerable<Feat> GetClassFeats(CharacterClass characterClass)
        {
            var allClassFeats = collectionsSelector.SelectFrom("ClassFeats", characterClass.ClassName);
            var tableName = String.Format("{0}FeatLevelRequirements", characterClass.ClassName);
            var levelRequirements = adjustmentsSelector.SelectFrom(tableName);

            return allClassFeats.Where(f => levelRequirements[f] <= characterClass.Level)
                                .Select(f => new Feat { Name = f });
        }

        private IEnumerable<Feat> GetSkillSynergyFeats(Dictionary<String, Skill> skills)
        {
            var synergyFeats = new List<String>();
            var synergyQualifyingSkills = skills.Where(kvp => kvp.Value.EffectiveRanks >= 5).Select(kvp => kvp.Key);

            foreach (var skill in synergyQualifyingSkills)
            {
                var synergy = collectionsSelector.SelectFrom("SkillSynergyFeats", skill);
                synergyFeats.AddRange(synergy);
            }

            return synergyFeats.Select(f => new Feat { Name = f });
        }

        private IEnumerable<Feat> GetAdditionalFeats(CharacterClass characterClass, Race race, Dictionary<String, Stat> stats, Dictionary<String, Skill> skills,
            BaseAttack baseAttack, IEnumerable<Feat> automaticFeats)
        {
            var additionalFeats = featsSelector.SelectAdditional();
            var availableFeats = additionalFeats.Where(f => f.ImmutableRequirementsMet(baseAttack.Bonus, stats, skills, characterClass.ClassName));

            var numberOfAdditionalFeats = characterClass.Level / 3 + 1;
            if (race.BaseRace == RaceConstants.BaseRaces.Human)
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
                                                .Select(f => new Feat { Name = f.FeatName, SpecificApplication = f.SpecificApplicationType })
                                                .Except(feats)
                                                .Except(preselectedFeats);

                if (!availableFeats.Any())
                    break;

                var index = GetRandomIndexOf(availableFeats);
                var feat = availableFeats.ElementAt(index);
                var sourceFeat = sourceFeats.First(f => f.FeatName == feat.Name);

                feat.SpecificApplication = GetSpecificApplicationOf(feat, sourceFeat, feats, characterClass.ClassName, stats[StatConstants.Intelligence].Bonus);

                if (feat.Name == FeatConstants.SpellMastery && feats.Any(f => f.Name == FeatConstants.SpellMastery))
                    feats.Remove(feats.First(f => f.Name == FeatConstants.SpellMastery));

                feats.Add(feat);
            }

            return feats;
        }

        private Int32 GetRandomIndexOf(IEnumerable<Object> collection)
        {
            var die = collection.Count();
            return dice.Roll().d(die) - 1;
        }

        private String GetSpecificApplicationOf(Feat feat, AdditionalFeatSelection sourceFeat, IEnumerable<Feat> feats, String className, Int32 intelligenceBonus)
        {
            if (feat.Name == FeatConstants.SpellMastery)
            {
                if (!feats.Any(f => f.Name == FeatConstants.SpellMastery))
                    return Convert.ToString(intelligenceBonus);

                var previousSpellMastery = feats.First(f => f.Name == FeatConstants.SpellMastery);
                var previousSpellCount = Convert.ToInt32(previousSpellMastery.SpecificApplication);
                var newSpellCount = previousSpellCount + intelligenceBonus;

                return Convert.ToString(newSpellCount);
            }

            if (String.IsNullOrEmpty(feat.SpecificApplication))
                return String.Empty;

            IEnumerable<String> specificApplications;
            var usedSpecificApplications = feats.Where(f => f.Name == feat.Name).Select(f => f.SpecificApplication);

            if (feats.Any(f => RequirementsHaveSpecificApplications(sourceFeat, f)))
                specificApplications = feats.Where(f => RequirementsHaveSpecificApplications(sourceFeat, f))
                                            .Select(f => f.SpecificApplication);
            else
                specificApplications = collectionsSelector.SelectFrom("FeatSpecificApplications", feat.SpecificApplication);

            specificApplications = specificApplications.Except(usedSpecificApplications);

            var spellcasters = collectionsSelector.SelectFrom("ClassNameGroups", "Spellcasters");
            if (!spellcasters.Contains(className))
                specificApplications = specificApplications.Except(new[] { WeaponProficiencyConstants.Ray });

            var index = GetRandomIndexOf(specificApplications);
            var specificApplication = specificApplications.ElementAt(index);

            return specificApplication;
        }

        private Boolean RequirementsHaveSpecificApplications(AdditionalFeatSelection sourceFeat, Feat feat)
        {
            return sourceFeat.RequiredFeats.Contains(feat.Name) && !String.IsNullOrEmpty(feat.SpecificApplication);
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