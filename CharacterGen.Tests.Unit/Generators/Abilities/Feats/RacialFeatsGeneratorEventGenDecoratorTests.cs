using CharacterGen.Abilities.Feats;
using CharacterGen.Abilities.Skills;
using CharacterGen.Abilities.Stats;
using CharacterGen.Domain.Generators.Abilities.Feats;
using CharacterGen.Races;
using EventGen;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CharacterGen.Tests.Unit.Generators.Abilities.Feats
{
    [TestFixture]
    public class RacialFeatsGeneratorEventGenDecoratorTests
    {
        private IRacialFeatsGenerator decorator;
        private Mock<IRacialFeatsGenerator> mockInnerGenerator;
        private Mock<GenEventQueue> mockEventQueue;
        private Race race;
        private Dictionary<string, Stat> stats;
        private List<Skill> skills;

        [SetUp]
        public void Setup()
        {
            mockInnerGenerator = new Mock<IRacialFeatsGenerator>();
            mockEventQueue = new Mock<GenEventQueue>();
            decorator = new RacialFeatsGeneratorEventGenDecorator(mockInnerGenerator.Object, mockEventQueue.Object);

            race = new Race();
            stats = new Dictionary<string, Stat>();
            skills = new List<Skill>();

            race.BaseRace = Guid.NewGuid().ToString();
        }

        [Test]
        public void ReturnInnerFeats()
        {
            var feats = new[]
            {
                new Feat(),
                new Feat(),
            };

            mockInnerGenerator.Setup(g => g.GenerateWith(race, skills, stats)).Returns(feats);

            var generatedFeats = decorator.GenerateWith(race, skills, stats);
            Assert.That(generatedFeats, Is.EqualTo(feats));
        }

        [Test]
        public void LogEventsForFeatsGeneration()
        {
            var feats = new[]
            {
                new Feat(),
                new Feat(),
            };

            mockInnerGenerator.Setup(g => g.GenerateWith(race, skills, stats)).Returns(feats);

            var generatedFeats = decorator.GenerateWith(race, skills, stats);
            Assert.That(generatedFeats, Is.EqualTo(feats));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Beginning racial feats generation for {race.Summary}"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Completed generation of racial feats"), Times.Once);
        }
    }
}
