using CharacterGen.Abilities.Skills;
using CharacterGen.Abilities.Stats;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Generators.Abilities;
using CharacterGen.Races;
using EventGen;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CharacterGen.Tests.Unit.Generators.Abilities.Skills
{
    [TestFixture]
    public class SkillsGeneratorEventGenDecoratorTests
    {
        private ISkillsGenerator decorator;
        private Mock<ISkillsGenerator> mockInnerGenerator;
        private Mock<GenEventQueue> mockEventQueue;
        private CharacterClass characterClass;
        private Race race;
        private Dictionary<string, Stat> stats;

        [SetUp]
        public void Setup()
        {
            mockInnerGenerator = new Mock<ISkillsGenerator>();
            mockEventQueue = new Mock<GenEventQueue>();
            decorator = new SkillsGeneratorEventGenDecorator(mockInnerGenerator.Object, mockEventQueue.Object);

            characterClass = new CharacterClass();
            race = new Race();
            stats = new Dictionary<string, Stat>();

            characterClass.Name = Guid.NewGuid().ToString();
            characterClass.Level = 9266;

            race.BaseRace = Guid.NewGuid().ToString();
            stats["stat"] = new Stat("stat");
        }

        [Test]
        public void ReturnInnerSkills()
        {
            var skills = new[]
            {
                new Skill("skill 1", stats["stat"], 9266),
                new Skill("skill 2", stats["stat"], 90210),
            };

            mockInnerGenerator.Setup(g => g.GenerateWith(characterClass, race, stats)).Returns(skills);

            var generatedSkills = decorator.GenerateWith(characterClass, race, stats);
            Assert.That(generatedSkills, Is.EqualTo(skills));
        }

        [Test]
        public void LogEventsForSkillsGeneration()
        {
            var skills = new[]
            {
                new Skill("skill 1", stats["stat"], 9266),
                new Skill("skill 2", stats["stat"], 90210),
            };

            mockInnerGenerator.Setup(g => g.GenerateWith(characterClass, race, stats)).Returns(skills);

            var generatedSkills = decorator.GenerateWith(characterClass, race, stats);
            Assert.That(generatedSkills, Is.EqualTo(skills));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Beginning skills generation for {characterClass.Summary} {race.Summary}"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Completed generation of skills"), Times.Once);
        }
    }
}
