using CharacterGen.Abilities.Skills;
using CharacterGen.Abilities.Stats;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Domain.Generators.Abilities
{
    internal class SkillsGenerator : ISkillsGenerator
    {
        private ISkillSelector skillSelector;
        private ICollectionsSelector collectionsSelector;
        private IAdjustmentsSelector adjustmentsSelector;
        private IBooleanPercentileSelector booleanPercentileSelector;

        public SkillsGenerator(ISkillSelector skillSelector, ICollectionsSelector collectionsSelector, IAdjustmentsSelector adjustmentsSelector,
            IBooleanPercentileSelector booleanPercentileSelector)
        {
            this.skillSelector = skillSelector;
            this.collectionsSelector = collectionsSelector;
            this.adjustmentsSelector = adjustmentsSelector;
            this.booleanPercentileSelector = booleanPercentileSelector;
        }

        public Dictionary<string, Skill> GenerateWith(CharacterClass characterClass, Race race, Dictionary<string, Stat> stats)
        {
            var classSkills = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassSkills, characterClass.Name);
            var crossClassSkills = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.CrossClassSkills, characterClass.Name);

            var specialistSkills = Enumerable.Empty<string>();
            foreach (var specialistField in characterClass.SpecialistFields)
            {
                var newSpecialistSkills = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, specialistField);
                specialistSkills = specialistSkills.Union(newSpecialistSkills);
            }

            classSkills = classSkills.Union(specialistSkills);

            if (characterClass.Name == CharacterClassConstants.Expert)
                classSkills = GetRandomClassSkills();

            var skills = InitializeSkills(stats, classSkills, crossClassSkills, characterClass);

            skills = AddRanks(characterClass, race, stats, classSkills, crossClassSkills, skills);

            var monsters = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters);
            if (monsters.Contains(race.BaseRace))
                skills = AddMonsterSkillRanks(race, stats, skills);

            skills = ApplySkillSynergies(skills);

            return skills;
        }

        private IEnumerable<string> GetRandomClassSkills()
        {
            var allSkills = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, GroupConstants.Skills);
            var randomSkills = new HashSet<string>();

            while (randomSkills.Count < 10)
            {
                var skill = collectionsSelector.SelectRandomFrom(allSkills);
                randomSkills.Add(skill);
            }

            return randomSkills;
        }

        private Dictionary<string, Skill> AddMonsterSkillRanks(Race race, Dictionary<string, Stat> stats, Dictionary<string, Skill> skills)
        {
            var monsterSkills = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassSkills, race.BaseRace);
            var monsterHitDice = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.MonsterHitDice);

            foreach (var monsterSkill in monsterSkills)
            {
                if (skills.ContainsKey(monsterSkill) == false)
                {
                    var selection = skillSelector.SelectFor(monsterSkill);

                    if (stats.ContainsKey(selection.BaseStatName) == false)
                        continue;

                    skills[monsterSkill] = new Skill(monsterSkill, stats[selection.BaseStatName], 0);
                }

                skills[monsterSkill].RankCap += monsterHitDice[race.BaseRace] + 3;
                skills[monsterSkill].ClassSkill = true;
            }

            var intelligenceSkillBonus = Math.Max(1, 2 + stats[StatConstants.Intelligence].Bonus);
            var points = (monsterHitDice[race.BaseRace] + 3) * intelligenceSkillBonus;
            var validMonsterSkills = FilterOutInvalidSkills(monsterSkills, skills);

            while (points-- > 0 && validMonsterSkills.Any())
            {
                var skill = collectionsSelector.SelectRandomFrom(validMonsterSkills);
                skills[skill].Ranks++;
                validMonsterSkills = FilterOutInvalidSkills(monsterSkills, skills);
            }

            return skills;
        }

        private Dictionary<string, Skill> InitializeSkills(Dictionary<string, Stat> stats, IEnumerable<string> classSkills, IEnumerable<string> crossClassSkills, CharacterClass characterClass)
        {
            var skills = new Dictionary<string, Skill>();
            var allSkillNames = classSkills.Union(crossClassSkills);

            foreach (var skillName in allSkillNames)
            {
                var skillSelection = skillSelector.SelectFor(skillName);
                if (stats.ContainsKey(skillSelection.BaseStatName) == false)
                    continue;

                skills[skillName] = new Skill(skillName, stats[skillSelection.BaseStatName], characterClass.Level + 3);
                skills[skillName].ClassSkill = classSkills.Contains(skillName);
            }

            return skills;
        }

        private Dictionary<string, Skill> AddRanks(CharacterClass characterClass, Race race, Dictionary<string, Stat> stats, IEnumerable<string> classSkills, IEnumerable<string> crossClassSkills, Dictionary<string, Skill> skills)
        {
            var points = GetTotalSkillPoints(characterClass, stats[StatConstants.Intelligence], race);
            var validSkills = FilterOutInvalidSkills(skills.Keys, skills);

            while (points > 0 && validSkills.Any())
            {
                var skillCollection = GetSkillCollection(skills, classSkills, crossClassSkills);
                var skill = collectionsSelector.SelectRandomFrom(skillCollection);

                skills[skill].Ranks++;
                points--;
                validSkills = FilterOutInvalidSkills(skills.Keys, skills);
            }

            return skills;
        }

        private int GetTotalSkillPoints(CharacterClass characterClass, Stat intelligence, Race race)
        {
            var pointsTable = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.SkillPointsForClasses);
            var perLevel = pointsTable[characterClass.Name] + intelligence.Bonus;
            var multiplier = characterClass.Level;

            var monsters = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters);
            if (monsters.Contains(race.BaseRace) == false)
                multiplier += 3;

            if (race.BaseRace == RaceConstants.BaseRaces.Human)
                perLevel++;

            return Math.Max(perLevel * multiplier, characterClass.Level);
        }

        private IEnumerable<string> GetSkillCollection(Dictionary<string, Skill> skills, IEnumerable<string> classSkills, IEnumerable<string> crossClassSkills)
        {
            var validClassSkills = FilterOutInvalidSkills(classSkills, skills);
            var validCrossClassSkills = FilterOutInvalidSkills(crossClassSkills, skills);

            if (validClassSkills.Any() == false)
                return validCrossClassSkills;

            if (validCrossClassSkills.Any() == false)
                return validClassSkills;

            var shouldAddPointToCrossClassSkill = booleanPercentileSelector.SelectFrom(TableNameConstants.Set.TrueOrFalse.AssignPointToCrossClassSkill);
            if (shouldAddPointToCrossClassSkill)
                return validCrossClassSkills;

            return validClassSkills;
        }

        private IEnumerable<string> FilterOutInvalidSkills(IEnumerable<string> skillNameCollection, Dictionary<string, Skill> skills)
        {
            return skillNameCollection.Where(s => skills.ContainsKey(s) && skills[s].RanksMaxedOut == false);
        }

        private Dictionary<string, Skill> ApplySkillSynergies(Dictionary<string, Skill> skills)
        {
            var skillsWarrantingSynergy = skills.Where(s => s.Value.QualifiesForSkillSynergy);

            foreach (var skill in skillsWarrantingSynergy)
            {
                var synergySkills = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.SkillSynergy, skill.Key);

                foreach (var synergySkill in synergySkills)
                    if (skills.ContainsKey(synergySkill))
                        skills[synergySkill].Bonus += 2;
            }

            return skills;
        }
    }
}