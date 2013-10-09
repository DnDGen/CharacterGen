using System;
using Moq;
using NPCGen.Core.Generation.Factories;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Factories
{
    [TestFixture]
    public class RaceFactoryTests
    {
        private Mock<IBaseRaceRandomizer> mockBaseRaceRandomizer;
        private Mock<IMetaraceRandomizer> mockMetaraceRandomizer;

        [SetUp]
        public void Setup()
        {
            mockBaseRaceRandomizer = new Mock<IBaseRaceRandomizer>();
            mockMetaraceRandomizer = new Mock<IMetaraceRandomizer>();
        }

        [Test]
        public void FactoryReturnsRandomizedBaseRace()
        {
            mockBaseRaceRandomizer.Setup(r => r.Randomize(It.IsAny<String>(), It.IsAny<String>())).Returns("base race");

            var race = RaceFactory.CreateUsing(String.Empty, String.Empty, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.BaseRace, Is.EqualTo("base race"));
        }

        [Test]
        public void FactoryReturnsRandomizedMetarace()
        {
            mockMetaraceRandomizer.Setup(r => r.Randomize(It.IsAny<String>(), It.IsAny<String>())).Returns("metarace");

            var race = RaceFactory.CreateUsing(String.Empty, String.Empty, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Metarace, Is.EqualTo("metarace"));
        }
    }
}