using CharacterGen.Common.Abilities.Skills;
using CharacterGen.Generators.Abilities;
using CharacterGen.Generators.Randomizers.Stats;
using Ninject;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Tests.Integration.Stress.Abilities
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

        private IEnumerable<String> trainedSkills;

        [SetUp]
        public void Setup()
        {
            trainedSkills = new[]
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
        }

        [TestCase("SkillsGenerator")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            var race = GetNewRace(alignment, characterClass);
            var stats = StatsGenerator.GenerateWith(StatsRandomizer, characterClass, race);

            var skills = SkillsGenerator.GenerateWith(characterClass, race, stats);

            foreach (var skill in trainedSkills)
                if (skills.Keys.Contains(skill))
                    Assert.That(skills[skill].ClassSkill, Is.True);

            foreach (var skill in skills.Values)
            {
                Assert.That(skill.BaseStat, Is.Not.Null);
                Assert.That(stats.Values, Contains.Item(skill.BaseStat));
                Assert.That(skill.Bonus, Is.Not.Negative);
            }

            var sum = skills.Values.Sum(s => s.Ranks);
            Assert.That(sum, Is.AtLeast(characterClass.Level));
        }
    }
}