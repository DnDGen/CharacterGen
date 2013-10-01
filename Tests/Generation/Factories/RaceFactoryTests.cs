using System;
using Moq;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Factories;
using NPCGen.Core.Generation.Factories.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Factories
{
    [TestFixture]
    public class RaceFactoryTests
    {
        private IRaceFactory raceFactory;
        private Mock<IBaseRaceRandomizer> mockBaseRaceRandomizer;
        private Mock<IMetaraceRandomizer> mockMetaraceRandomizer;
        private Alignment alignment;
        private CharacterClass characterClass;

        [SetUp]
        public void Setup()
        {
            mockBaseRaceRandomizer = new Mock<IBaseRaceRandomizer>();
            mockMetaraceRandomizer = new Mock<IMetaraceRandomizer>();

            raceFactory = new RaceFactory(mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
        }

        [Test]
        public void RandomizersProperlySet()
        {
            Assert.That(raceFactory.BaseRaceRandomizer, Is.EqualTo(mockBaseRaceRandomizer.Object));
            Assert.That(raceFactory.MetaraceRandomizer, Is.EqualTo(mockMetaraceRandomizer.Object));
        }

        [Test]
        public void FactoryReturnsRandomizedBaseRace()
        {
            mockBaseRaceRandomizer.Setup(r => r.Randomize(alignment, It.IsAny<String>())).Returns("base race");

            var race = raceFactory.Generate(alignment, characterClass);
            Assert.That(race.BaseRace, Is.EqualTo("base race"));
        }

        [Test]
        public void ChangeBaseRaceRandomizer()
        {
            var differentRandomizer = new Mock<IBaseRaceRandomizer>();
            differentRandomizer.Setup(r => r.Randomize(alignment, It.IsAny<String>())).Returns("different base race");
            raceFactory.BaseRaceRandomizer = differentRandomizer.Object;

            var race = raceFactory.Generate(alignment, characterClass);
            Assert.That(race.BaseRace, Is.EqualTo("different base race"));
        }

        [Test]
        public void FactoryReturnsRandomizedMetarace()
        {
            mockMetaraceRandomizer.Setup(r => r.Randomize(alignment, It.IsAny<String>())).Returns("metarace");

            var race = raceFactory.Generate(alignment, characterClass);
            Assert.That(race.Metarace, Is.EqualTo("metarace"));
        }

        [Test]
        public void ChangeMetaraceRandomizer()
        {
            var differentRandomizer = new Mock<IMetaraceRandomizer>();
            differentRandomizer.Setup(r => r.Randomize(alignment, It.IsAny<String>())).Returns("different metarace");
            raceFactory.MetaraceRandomizer = differentRandomizer.Object;

            var race = raceFactory.Generate(alignment, characterClass);
            Assert.That(race.Metarace, Is.EqualTo("different metarace"));
        }
    }
}