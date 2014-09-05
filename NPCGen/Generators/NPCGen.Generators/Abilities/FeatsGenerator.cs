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

            if (characterClass.ClassName != CharacterClassConstants.Fighter)
                return feats;

            var fighterFeats = GetFighterFeats(characterClass, race, stats, skills, baseAttack, feats);
            return feats.Union(fighterFeats);
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
            var baseRacialFeats = collectionsSelector.SelectFrom("RacialFeats", race.BaseRace).Select(f => new Feat { Name = f });
            var metaracialFeats = collectionsSelector.SelectFrom("RacialFeats", race.Metarace).Select(f => new Feat { Name = f });

            return baseRacialFeats.Union(metaracialFeats);
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
            var additionalFeats = featsSelector.SelectAll();
            var availableFeats = additionalFeats.Where(f => f.ImmutableRequirementsMet(baseAttack.Bonus, stats, skills, characterClass.ClassName));

            var numberOfAdditionalFeats = characterClass.Level / 3 + 1;
            if (race.BaseRace == RaceConstants.BaseRaces.Human)
                numberOfAdditionalFeats++;

            var feats = PopulateFeatsFrom(characterClass, stats, skills, baseAttack, automaticFeats, availableFeats, numberOfAdditionalFeats);

            return feats;
        }

        private List<Feat> PopulateFeatsFrom(CharacterClass characterClass, Dictionary<String, Stat> stats, Dictionary<String, Skill> skills, BaseAttack baseAttack, IEnumerable<Feat> preselectedFeats, IEnumerable<FeatSelection> sourceFeats, Int32 quantity)
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

        private String GetSpecificApplicationOf(Feat feat, FeatSelection sourceFeat, IEnumerable<Feat> feats, String className, Int32 intelligenceBonus)
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

        private Boolean RequirementsHaveSpecificApplications(FeatSelection sourceFeat, Feat feat)
        {
            return sourceFeat.RequiredFeats.Contains(feat.Name) && !String.IsNullOrEmpty(feat.SpecificApplication);
        }

        private IEnumerable<Feat> GetFighterFeats(CharacterClass characterClass, Race race, Dictionary<String, Stat> stats, Dictionary<String, Skill> skills,
            BaseAttack baseAttack, IEnumerable<Feat> selectedFeats)
        {
            var fighterFeats = featsSelector.SelectAll().Where(f => f.IsFighterFeat);
            var availableFeats = fighterFeats.Where(f => f.ImmutableRequirementsMet(baseAttack.Bonus, stats, skills, characterClass.ClassName));

            var numberOfFighterFeats = characterClass.Level / 2 + 1;
            var feats = PopulateFeatsFrom(characterClass, stats, skills, baseAttack, selectedFeats, availableFeats, numberOfFighterFeats);

            return feats;
        }
    }
}