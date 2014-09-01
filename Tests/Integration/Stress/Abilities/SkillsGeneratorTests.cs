using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using NPCGen.Common.Abilities.Skills;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Generators.Interfaces.Abilities;
using NPCGen.Generators.Interfaces.Randomizers.Stats;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Abilities
{
    [TestFixture]
    public class SkillsGeneratorTests : StressTests
    {
        [Inject]
        public ISkillsGenerator SkillsGenerator { get; set; }
        [Inject]
        public IStatsGenerator StatsGenerator { get; set; }
        [Inject, Named(StatsRandomizerTypeConstants.Raw)]
        public IStatsRandomizer StatsRandomizer { get; set; }

        private IEnumerable<String> untrainedSkills;
        private IEnumerable<String> allSkills;

        [SetUp]
        public void Setup()
        {
            var trainedSkills = new[]
            {
                SkillConstants.DecipherScript,
                SkillConstants.DisableDevice,
                SkillConstants.HandleAnimal,
                SkillConstants.KnowledgeArcana,
                SkillConstants.KnowledgeArchitectureAndEngineering,
                SkillConstants.KnowledgeDungeoneering,
                SkillConstants.KnowledgeGeography,
                SkillConstants.KnowledgeHistory,
                SkillConstants.KnowledgeLocal,
                SkillConstants.KnowledgeNature,
                SkillConstants.KnowledgeNobilityAndRoyalty,
                SkillConstants.KnowledgeReligion,
                SkillConstants.KnowledgeThePlanes,
                SkillConstants.OpenLock,
                SkillConstants.SleightOfHand,
                SkillConstants.Spellcraft,
                SkillConstants.Tumble,
                SkillConstants.UseMagicDevice
            };

            allSkills = SkillConstants.GetSkills();
            untrainedSkills = allSkills.Except(trainedSkills);
        }

        protected override void MakeAssertions()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            var race = GetNewRace(alignment, characterClass);
            var stats = StatsGenerator.GenerateWith(StatsRandomizer, characterClass, race);

            var skills = SkillsGenerator.GenerateWith(characterClass, race, stats);

            foreach (var skill in untrainedSkills)
                Assert.That(skills.Keys, Contains.Item(skill));

            foreach (var skill in skills.Keys)
                Assert.That(allSkills, Contains.Item(skill));

            foreach (var skill in skills.Values)
            {
                Assert.That(stats.Values, Contains.Item(skill.BaseStat));
                Assert.That(skill.BaseStat, Is.Not.Null);
                Assert.That(skill.Bonus, Is.EqualTo(0));
            }

            var sum = skills.Values.Sum(s => s.Ranks);
            var minimumRanks = (2 + stats[StatConstants.Intelligence].Bonus) * (characterClass.Level + 3);
            Assert.That(sum, Is.AtLeast(minimumRanks));
        }
    }
}