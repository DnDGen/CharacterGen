using System;
using System.Collections.Generic;
using D20Dice;
using Moq;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Data.Stats;
using NPCGen.Core.Generation.Factories;
using NPCGen.Core.Generation.Factories.Interfaces;
using NPCGen.Core.Generation.Randomizers.Stats.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generation.Factories
{
    [TestFixture]
    public class StatsFactoryTests
    {
        private Mock<IStatsRandomizer> mockStatRandomizer;
        private Mock<IDice> mockDice;
        private IStatsFactory statsFactory;

        private Dictionary<String, Stat> expectedStats;
        private Race race;
        private CharacterClass characterClass;
        private const Int32 baseStat = 10;

        [SetUp]
        public void Setup()
        {
            expectedStats = new Dictionary<String, Stat>();
            foreach (var stat in StatConstants.GetStats())
                expectedStats.Add(stat, new Stat() { Value = baseStat });

            mockStatRandomizer = new Mock<IStatsRandomizer>();
            mockStatRandomizer.Setup(r => r.Randomize()).Returns(expectedStats);

            mockDice = new Mock<IDice>();
            statsFactory = new StatsFactory(mockDice.Object);

            race = new Race();
            race.BaseRace = RaceConstants.BaseRaces.Human;
            race.Metarace = String.Empty;

            characterClass = new CharacterClass();
            characterClass.ClassName = CharacterClassConstants.Fighter;
        }

        [Test]
        public void RandomizesStatValues()
        {
            statsFactory.CreateWith(mockStatRandomizer.Object, characterClass, race);
            mockStatRandomizer.Verify(r => r.Randomize(), Times.Once);
        }

        [Test]
        public void StatsContainAllStats()
        {
            var stats = statsFactory.CreateWith(mockStatRandomizer.Object, characterClass, race);
            foreach (var stat in StatConstants.GetStats())
                Assert.That(stats.ContainsKey(stat), Is.True);

            Assert.That(stats.Count, Is.EqualTo(6));
        }

        [Test]
        public void PrioritizesStatsByClass()
        {
            characterClass.ClassName = CharacterClassConstants.Barbarian;
            expectedStats[StatConstants.Charisma].Value = 18;
            expectedStats[StatConstants.Intelligence].Value = 16;

            var stats = statsFactory.CreateWith(mockStatRandomizer.Object, characterClass, race);
            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(18));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(16));
            Assert.That(stats[StatConstants.Constitution].Value, Is.EqualTo(baseStat));
            Assert.That(stats[StatConstants.Intelligence].Value, Is.EqualTo(baseStat));
            Assert.That(stats[StatConstants.Wisdom].Value, Is.EqualTo(baseStat));
            Assert.That(stats[StatConstants.Charisma].Value, Is.EqualTo(baseStat));
        }

        [Test]
        public void AdjustsStatsByBaseRace()
        {
            race.BaseRace = RaceConstants.BaseRaces.Aasimar;

            var stats = statsFactory.CreateWith(mockStatRandomizer.Object, characterClass, race);
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

            var stats = statsFactory.CreateWith(mockStatRandomizer.Object, characterClass, race);
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

            var stats = statsFactory.CreateWith(mockStatRandomizer.Object, characterClass, race);
            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(baseStat + 4), StatConstants.Strength);
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(baseStat + 2), StatConstants.Dexterity);
            Assert.That(stats[StatConstants.Constitution].Value, Is.EqualTo(baseStat + 4), StatConstants.Constitution);
            Assert.That(stats[StatConstants.Intelligence].Value, Is.EqualTo(baseStat + 2), StatConstants.Intelligence);
            Assert.That(stats[StatConstants.Wisdom].Value, Is.EqualTo(baseStat + 6), StatConstants.Wisdom);
            Assert.That(stats[StatConstants.Charisma].Value, Is.EqualTo(baseStat + 6), StatConstants.Charisma);
        }

        [Test]
        public void LowOnD2IncreasesFirstPriorityStat()
        {
            characterClass.Level = 4;
            mockDice.Setup(d => d.d2(1)).Returns(1);

            var stats = statsFactory.CreateWith(mockStatRandomizer.Object, characterClass, race);
            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(baseStat + 1), StatConstants.Strength);
            Assert.That(stats[StatConstants.Constitution].Value, Is.EqualTo(baseStat), StatConstants.Constitution);
        }

        [Test]
        public void HighOnD2IncreasesSecondPriorityStat()
        {
            characterClass.Level = 4;
            mockDice.Setup(d => d.d2(1)).Returns(2);

            var stats = statsFactory.CreateWith(mockStatRandomizer.Object, characterClass, race);
            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(baseStat), StatConstants.Strength);
            Assert.That(stats[StatConstants.Constitution].Value, Is.EqualTo(baseStat + 1), StatConstants.Constitution);
        }

        [Test]
        public void DoNotIncreaseStat()
        {
            mockDice.Setup(d => d.d2(1)).Returns(1);
            for (var level = 1; level < 4; level++)
            {
                foreach (var stat in expectedStats.Values)
                    stat.Value = baseStat;

                characterClass.Level = level;

                var stats = statsFactory.CreateWith(mockStatRandomizer.Object, characterClass, race);
                Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(baseStat), level.ToString());
            }
        }

        [Test]
        public void IncreaseStatByOne()
        {
            mockDice.Setup(d => d.d2(1)).Returns(1);
            for (var level = 4; level < 8; level++)
            {
                foreach (var stat in expectedStats.Values)
                    stat.Value = baseStat;

                characterClass.Level = level;

                var stats = statsFactory.CreateWith(mockStatRandomizer.Object, characterClass, race);
                Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(baseStat + 1), level.ToString());
            }
        }

        [Test]
        public void IncreaseStatByTwo()
        {
            mockDice.Setup(d => d.d2(1)).Returns(1);
            for (var level = 8; level < 12; level++)
            {
                foreach (var stat in expectedStats.Values)
                    stat.Value = baseStat;

                characterClass.Level = level;

                var stats = statsFactory.CreateWith(mockStatRandomizer.Object, characterClass, race);
                Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(baseStat + 2), level.ToString());
            }
        }

        [Test]
        public void IncreaseStatByThree()
        {
            mockDice.Setup(d => d.d2(1)).Returns(1);
            for (var level = 12; level < 16; level++)
            {
                foreach (var stat in expectedStats.Values)
                    stat.Value = baseStat;

                characterClass.Level = level;

                var stats = statsFactory.CreateWith(mockStatRandomizer.Object, characterClass, race);
                Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(baseStat + 3), level.ToString());
            }
        }

        [Test]
        public void IncreaseStatByFour()
        {
            mockDice.Setup(d => d.d2(1)).Returns(1);
            for (var level = 16; level < 20; level++)
            {
                foreach (var stat in expectedStats.Values)
                    stat.Value = baseStat;

                characterClass.Level = level;

                var stats = statsFactory.CreateWith(mockStatRandomizer.Object, characterClass, race);
                Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(baseStat + 4), level.ToString());
            }
        }

        [Test]
        public void IncreaseStatByFive()
        {
            mockDice.Setup(d => d.d2(1)).Returns(1);
            characterClass.Level = 20;

            var stats = statsFactory.CreateWith(mockStatRandomizer.Object, characterClass, race);
            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(baseStat + 5));
        }
    }
}