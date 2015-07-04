using System;
using System.Collections.Generic;
using System.Linq;
using D20Dice;
using NPCGen.Common.Abilities.Skills;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Generators.Interfaces.Abilities;
using NPCGen.Selectors.Interfaces;
using NPCGen.Tables.Interfaces;

namespace NPCGen.Generators.Abilities
{
    public class SkillsGenerator : ISkillsGenerator
    {
        private ISkillSelector skillSelector;
        private IDice dice;
        private ICollectionsSelector collectionsSelector;
        private IAdjustmentsSelector adjustmentsSelector;
        private IBooleanPercentileSelector booleanPercentileSelector;

        public SkillsGenerator(ISkillSelector skillSelector, IDice dice, ICollectionsSelector collectionsSelector, IAdjustmentsSelector adjustmentsSelector,
            IBooleanPercentileSelector booleanPercentileSelector)
        {
            this.skillSelector = skillSelector;
            this.dice = dice;
            this.collectionsSelector = collectionsSelector;
            this.adjustmentsSelector = adjustmentsSelector;
            this.booleanPercentileSelector = booleanPercentileSelector;
        }

        public Dictionary<String, Skill> GenerateWith(CharacterClass characterClass, Race race, Dictionary<String, Stat> stats)
        {
            var classSkills = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassSkills, characterClass.ClassName);
            var crossClassSkills = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.CrossClassSkills, characterClass.ClassName);

            var specialistSkills = Enumerable.Empty<String>();
            foreach (var specialistField in characterClass.SpecialistFields)
            {
                var newSpecialistSkills = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, specialistField);
                specialistSkills = specialistSkills.Union(newSpecialistSkills);
            }

            classSkills = classSkills.Union(specialistSkills);

            var skills = InitializeSkills(stats, classSkills, crossClassSkills);
            skills = AddRanks(characterClass, race, stats, classSkills, crossClassSkills, skills);
            skills = ApplySkillSynergies(skills);

            return skills;
        }

        private Dictionary<String, Skill> InitializeSkills(Dictionary<String, Stat> stats, IEnumerable<String> classSkills, IEnumerable<String> crossClassSkills)
        {
            var skills = new Dictionary<String, Skill>();

            foreach (var skill in crossClassSkills)
                skills[skill] = new Skill { ClassSkill = false };

            foreach (var skill in classSkills)
                skills[skill] = new Skill { ClassSkill = true };

            foreach (var skill in skills)
            {
                var selection = skillSelector.SelectFor(skill.Key);
                skill.Value.ArmorCheckPenalty = selection.ArmorCheckPenalty;
                skill.Value.BaseStat = stats[selection.BaseStatName];
            }

            return skills;
        }

        private Dictionary<String, Skill> AddRanks(CharacterClass characterClass, Race race, Dictionary<String, Stat> stats, IEnumerable<String> classSkills, IEnumerable<String> crossClassSkills, Dictionary<String, Skill> skills)
        {
            var points = GetTotalSkillPoints(characterClass, stats[StatConstants.Intelligence], race);
            var rankCap = characterClass.Level + 3;

            while (points > 0 && skills.Values.Any(s => s.Ranks < rankCap))
            {
                var skillCollection = GetSkillCollection(skills, rankCap, classSkills, crossClassSkills);
                var index = dice.Roll().d(skillCollection.Count()) - 1;

                var skill = skillCollection.ElementAt(index);
                skills[skill].Ranks++;
                points--;
            }

            return skills;
        }

        private Int32 GetTotalSkillPoints(CharacterClass characterClass, Stat intelligence, Race race)
        {
            var pointsTable = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.SkillPointsForClasses);
            var perLevel = pointsTable[characterClass.ClassName] + intelligence.Bonus;
            var multiplier = characterClass.Level;

            var monsters = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters);
            if (monsters.Contains(race.BaseRace.Id) == false)
                multiplier += 3;

            if (race.BaseRace.Id == RaceConstants.BaseRaces.HumanId)
                perLevel++;

            return perLevel * multiplier;
        }

        private IEnumerable<String> GetSkillCollection(Dictionary<String, Skill> skills, Int32 rankCap, IEnumerable<String> classSkills, IEnumerable<String> crossClassSkills)
        {
            if (skills.Keys.Intersect(classSkills).Any(s => skills[s].Ranks < rankCap) == false)
                return crossClassSkills.Where(s => skills[s].Ranks < rankCap);

            if (skills.Keys.Intersect(crossClassSkills).Any(s => skills[s].Ranks < rankCap) == false)
                return classSkills.Where(s => skills[s].Ranks < rankCap);

            var shouldAddPointToCrossClassSkill = booleanPercentileSelector.SelectFrom(TableNameConstants.Set.TrueOrFalse.AssignPointToCrossClassSkill);
            if (shouldAddPointToCrossClassSkill)
                return crossClassSkills.Where(s => skills[s].Ranks < rankCap);

            return classSkills.Where(s => skills[s].Ranks < rankCap);
        }

        private Dictionary<String, Skill> ApplySkillSynergies(Dictionary<String, Skill> skills)
        {
            var skillsWarrantingSynergy = skills.Where(s => s.Value.EffectiveRanks >= 5);
            foreach (var skill in skillsWarrantingSynergy)
            {
                var synergySkills = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.SkillSynergy, skill.Key);

                foreach (var synergySkill in synergySkills)
                    if (skills.ContainsKey(synergySkill))
                        skills[synergySkill].Bonus += 2;
            }

            return skills;
        }

        //private Dictionary<String, Skill> ApplyRacialAdjustments(Dictionary<String, Skill> skills, Race race)
        //{
        //    if (race.BaseRace.Id != RaceConstants.BaseRaces.MindFlayerId)
        //        return skills;

        //    var allKnowledgeSkills = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, GroupConstants.Knowledge);
        //    var knowledgeSkills = allKnowledgeSkills.Intersect(skills.Keys);

        //    if (knowledgeSkills.Any())
        //        return ApplyMindFlayerKnowledgeBonus(skills, knowledgeSkills);

        //    return skills;
        //}

        //private Dictionary<String, Skill> ApplyMindFlayerKnowledgeBonus(Dictionary<String, Skill> skills, IEnumerable<String> knowledgeSkills)
        //{
        //    var index = dice.Roll().d(knowledgeSkills.Count()) - 1;
        //    var knowledgeSkill = knowledgeSkills.ElementAt(index);
        //    skills[knowledgeSkill].Bonus += 8;

        //    return skills;
        //}
    }
}