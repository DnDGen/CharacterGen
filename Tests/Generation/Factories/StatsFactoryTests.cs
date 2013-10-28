using System;
using System.Collections.Generic;
using Moq;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Data.Stats;
using NPCGen.Core.Generation.Factories;
using NPCGen.Core.Generation.Randomizers.Stats.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Factories
{
    [TestFixture]
    public class StatsFactoryTests
    {
        private Mock<IStatsRandomizer> mockStatRandomizer;
        private Dictionary<String, Stat> expectedStats;
        private Race race;
        private const Int32 baseStat = 10;

        [SetUp]
        public void Setup()
        {
            expectedStats = new Dictionary<String, Stat>();
            foreach (var stat in StatConstants.GetStats())
                expectedStats.Add(stat, new Stat() { Value = baseStat });

            mockStatRandomizer = new Mock<IStatsRandomizer>();
            mockStatRandomizer.Setup(r => r.Randomize()).Returns(expectedStats);

            race = new Race();
            race.BaseRace = RaceConstants.BaseRaces.Human;
            race.Metarace = String.Empty;
        }

        [Test]
        public void RandomizesStatValues()
        {
            StatsFactory.CreateUsing(mockStatRandomizer.Object, race);
            mockStatRandomizer.Verify(r => r.Randomize(), Times.Once);
        }

        [Test]
        public void AdjustsStatsByBaseRace()
        {
            race.BaseRace = RaceConstants.BaseRaces.Aasimar;

            var stats = StatsFactory.CreateUsing(mockStatRandomizer.Object, race);
            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(baseStat), StatConstants.Strength);
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(baseStat), StatConstants.Dexterity);
            Assert.That(stats[StatConstants.Constitution].Value, Is.EqualTo(baseStat), StatConstants.Constitution);
            Assert.That(stats[StatConstants.Intelligence].Value, Is.EqualTo(baseStat), StatConstants.Intelligence);
            Assert.That(stats[StatConstants.Wisdom].Value, Is.EqualTo(baseStat + 2), StatConstants.Wisdom);
            Assert.That(stats[StatConstants.Charisma].Value, Is.EqualTo(baseStat + 2), StatConstants.Charisma);
        }

        [Test]
        public void AdjustsStatsByMetarace()
        {
            race.Metarace = RaceConstants.Metaraces.HalfCelestial;

            var stats = StatsFactory.CreateUsing(mockStatRandomizer.Object, race);
            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(baseStat + 4), StatConstants.Strength);
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(baseStat + 2), StatConstants.Dexterity);
            Assert.That(stats[StatConstants.Constitution].Value, Is.EqualTo(baseStat + 4), StatConstants.Constitution);
            Assert.That(stats[StatConstants.Intelligence].Value, Is.EqualTo(baseStat + 2), StatConstants.Intelligence);
            Assert.That(stats[StatConstants.Wisdom].Value, Is.EqualTo(baseStat + 4), StatConstants.Wisdom);
            Assert.That(stats[StatConstants.Charisma].Value, Is.EqualTo(baseStat + 4), StatConstants.Charisma);
        }

        [Test]
        public void AdjustsStatsBySumOfBaseRaceAndMetarace()
        {
            race.BaseRace = RaceConstants.BaseRaces.Aasimar;
            race.Metarace = RaceConstants.Metaraces.HalfCelestial;

            var stats = StatsFactory.CreateUsing(mockStatRandomizer.Object, race);
            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(baseStat + 4), StatConstants.Strength);
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(baseStat + 2), StatConstants.Dexterity);
            Assert.That(stats[StatConstants.Constitution].Value, Is.EqualTo(baseStat + 4), StatConstants.Constitution);
            Assert.That(stats[StatConstants.Intelligence].Value, Is.EqualTo(baseStat + 2), StatConstants.Intelligence);
            Assert.That(stats[StatConstants.Wisdom].Value, Is.EqualTo(baseStat + 6), StatConstants.Wisdom);
            Assert.That(stats[StatConstants.Charisma].Value, Is.EqualTo(baseStat + 6), StatConstants.Charisma);
        }
    }
}