using System;
using System.Collections.Generic;
using System.Linq;
using D20Dice;
using NPCGen.Common.Abilities.Skills;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Generators.Interfaces.Abilities;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Generators.Abilities
{
    public class SkillsGenerator : ISkillsGenerator
    {
        private ISkillSelector skillSelector;
        private IDice dice;
        private ICollectionsSelector collectionsSelector;
        private IAdjustmentsSelector adjustmentsSelector;

        public SkillsGenerator(ISkillSelector skillSelector, IDice dice, ICollectionsSelector collectionsSelector, IAdjustmentsSelector adjustmentsSelector)
        {
            this.skillSelector = skillSelector;
            this.dice = dice;
            this.collectionsSelector = collectionsSelector;
            this.adjustmentsSelector = adjustmentsSelector;
        }

        public Dictionary<String, Skill> GenerateWith(CharacterClass characterClass, Dictionary<String, Stat> stats)
        {
            var classSkills = collectionsSelector.SelectFrom("ClassSkills", characterClass.ClassName);
            var crossClassSkills = collectionsSelector.SelectFrom("CrossClassSkills", characterClass.ClassName);

            var skills = new Dictionary<String, Skill>();

            foreach (var skill in classSkills)
                skills[skill] = new Skill { ClassSkill = true };

            foreach (var skill in crossClassSkills)
                skills[skill] = new Skill { ClassSkill = false };

            foreach (var skill in skills)
            {
                var selection = skillSelector.SelectFor(skill.Key);
                skill.Value.ArmorCheckPenalty = selection.ArmorCheckPenalty;
                skill.Value.BaseStat = stats[selection.BaseStatName];
            }

            var points = GetTotalSkillPoints(characterClass, stats[StatConstants.Intelligence]);
            var rankCap = characterClass.Level + 3;

            while (points > 0 && skills.Values.Any(s => s.Ranks < rankCap))
            {
                var skillCollection = GetSkillCollection(skills, rankCap, classSkills, crossClassSkills);
                var index = dice.RollIndex(skillCollection.Count());

                var skill = skillCollection.ElementAt(index);
                if (skills[skill].Ranks < characterClass.Level + 3)
                {
                    skills[skill].Ranks++;
                    points--;
                }
            }

            foreach (var skill in skills)
            {
                if (skill.Value.EffectiveRanks < 5)
                    continue;

                var synergySkills = collectionsSelector.SelectFrom("SkillSynergy", skill.Key);

                foreach (var synergySkill in synergySkills)
                    if (skills.ContainsKey(synergySkill))
                        skills[synergySkill].Bonus += 2;
            }

            return skills;
        }

        private Int32 GetTotalSkillPoints(CharacterClass characterClass, Stat intelligence)
        {
            var pointsTable = adjustmentsSelector.SelectAdjustmentsFrom("SkillPointsForClasses");
            var perLevel = pointsTable[characterClass.ClassName] + intelligence.Bonus;
            var multiplier = characterClass.Level + 3;

            return perLevel * multiplier;
        }

        private IEnumerable<String> GetSkillCollection(Dictionary<String, Skill> skills, Int32 rankCap, IEnumerable<String> classSkills, IEnumerable<String> crossClassSkills)
        {
            if (!skills.Any(s => classSkills.Contains(s.Key) && s.Value.Ranks < rankCap))
                return crossClassSkills;

            if (dice.d3() == 3)
                return crossClassSkills;

            return classSkills;
        }
    }
}