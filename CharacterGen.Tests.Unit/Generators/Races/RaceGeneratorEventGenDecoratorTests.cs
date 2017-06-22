using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Generators.Races;
using CharacterGen.Races;
using CharacterGen.Randomizers.Races;
using EventGen;
using Moq;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Unit.Generators.Races
{
    [TestFixture]
    public class RaceGeneratorEventGenDecoratorTests
    {
        private IRaceGenerator decorator;
        private Mock<IRaceGenerator> mockInnerGenerator;
        private Mock<GenEventQueue> mockEventQueue;
        private Alignment alignment;
        private CharacterClass characterClass;
        private Mock<RaceRandomizer> mockBaseRaceRandomizer;
        private Mock<RaceRandomizer> mockMetaraceRandomizer;

        [SetUp]
        public void Setup()
        {
            mockInnerGenerator = new Mock<IRaceGenerator>();
            mockEventQueue = new Mock<GenEventQueue>();
            decorator = new RaceGeneratorEventGenDecorator(mockInnerGenerator.Object, mockEventQueue.Object);

            mockBaseRaceRandomizer = new Mock<RaceRandomizer>();
            mockMetaraceRandomizer = new Mock<RaceRandomizer>();

            alignment = new Alignment();
            characterClass = new CharacterClass();

            alignment.Goodness = Guid.NewGuid().ToString();
            alignment.Lawfulness = Guid.NewGuid().ToString();

            characterClass.Name = Guid.NewGuid().ToString();
            characterClass.Level = 9266;
        }

        [Test]
        public void ReturnInnerRace()
        {
            var race = new Race();
            mockInnerGenerator.Setup(g => g.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object)).Returns(race);

            var generatedRace = decorator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(generatedRace, Is.EqualTo(race));
        }

        [Test]
        public void LogEventsForRaceGeneration()
        {
            var race = new Race();
            race.BaseRace = "base race";
            race.Metarace = "metarace";

            mockInnerGenerator.Setup(g => g.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object)).Returns(race);

            var generatedRace = decorator.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(generatedRace, Is.EqualTo(race));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Generating race for {alignment.Full} {characterClass.Summary}"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Generated {race.Summary}"), Times.Once);
        }
    }
}
