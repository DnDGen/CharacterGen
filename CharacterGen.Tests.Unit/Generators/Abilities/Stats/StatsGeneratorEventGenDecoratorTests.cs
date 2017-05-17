using CharacterGen.Abilities.Stats;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Generators.Abilities;
using CharacterGen.Races;
using CharacterGen.Randomizers.Stats;
using EventGen;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CharacterGen.Tests.Unit.Generators.Abilities.Stats
{
    [TestFixture]
    public class StatsGeneratorEventGenDecoratorTests
    {
        private IStatsGenerator decorator;
        private Mock<IStatsGenerator> mockInnerGenerator;
        private Mock<GenEventQueue> mockEventQueue;
        private Mock<IStatsRandomizer> mockStatsRandomizer;
        private CharacterClass characterClass;
        private Race race;

        [SetUp]
        public void Setup()
        {
            mockInnerGenerator = new Mock<IStatsGenerator>();
            mockEventQueue = new Mock<GenEventQueue>();
            decorator = new StatsGeneratorEventGenDecorator(mockInnerGenerator.Object, mockEventQueue.Object);

            mockStatsRandomizer = new Mock<IStatsRandomizer>();

            characterClass = new CharacterClass();
            race = new Race();

            characterClass.Name = Guid.NewGuid().ToString();
            characterClass.Level = 9266;

            race.BaseRace = Guid.NewGuid().ToString();
        }

        [Test]
        public void ReturnInnerStats()
        {
            var stats = new Dictionary<string, Stat>();
            stats["stat 1"] = new Stat("stat 1");
            stats["stat 2"] = new Stat("stat 2");

            mockInnerGenerator.Setup(g => g.GenerateWith(mockStatsRandomizer.Object, characterClass, race)).Returns(stats);

            var generatedStats = decorator.GenerateWith(mockStatsRandomizer.Object, characterClass, race);
            Assert.That(generatedStats, Is.EqualTo(stats));
        }

        [Test]
        public void LogEventsForStatsGeneration()
        {
            var stats = new Dictionary<string, Stat>();
            stats["stat 1"] = new Stat("stat 1");
            stats["stat 2"] = new Stat("stat 2");

            mockInnerGenerator.Setup(g => g.GenerateWith(mockStatsRandomizer.Object, characterClass, race)).Returns(stats);

            var generatedStats = decorator.GenerateWith(mockStatsRandomizer.Object, characterClass, race);
            Assert.That(generatedStats, Is.EqualTo(stats));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Beginning stats generation for {characterClass.Summary} {race.Summary}"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Completed generation of stats"), Times.Once);
        }
    }
}
