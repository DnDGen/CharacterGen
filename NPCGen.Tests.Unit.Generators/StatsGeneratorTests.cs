using System;
using System.Collections.Generic;
using D20Dice;
using Moq;
using NPCGen.Common;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Common.Stats;
using NPCGen.Generators;
using NPCGen.Generators.Interfaces;
using NPCGen.Generators.Interfaces.Randomizers.Stats;
using NPCGen.Selectors.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators
{
    [TestFixture]
    public class StatsGeneratorTests
    {
        private Mock<IDice> mockDice;
        private Mock<IStatPrioritySelector> mockStatPriorityProvider;
        private Mock<IStatAdjustmentsSelector> mockStatAdjustmentsProvider;
        private IStatsGenerator statsFactory;

        private Mock<IStatsRandomizer> mockStatRandomizer;
        private Dictionary<String, Stat> expectedStats;
        private Dictionary<String, Int32> adjustments;
        private Race race;
        private CharacterClass characterClass;
        private StatPriority statPriority;
        private const Int32 baseStat = 10;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();

            statPriority = new StatPriority();
            statPriority.FirstPriority = "first priority";
            statPriority.SecondPriority = "second priority";
            mockStatPriorityProvider = new Mock<IStatPrioritySelector>();
            mockStatPriorityProvider.Setup(p => p.GetStatPriorities(It.IsAny<String>())).Returns(statPriority);

            race = new Race();
            race.BaseRace = "base race";
            race.Metarace = String.Empty;
            adjustments = new Dictionary<String, Int32>();
            adjustments.Add(statPriority.FirstPriority, 0);
            adjustments.Add(statPriority.SecondPriority, 0);
            adjustments.Add("other stat", 0);
            mockStatAdjustmentsProvider = new Mock<IStatAdjustmentsSelector>();
            mockStatAdjustmentsProvider.Setup(p => p.GetAdjustments(race)).Returns(adjustments);

            statsFactory = new StatsGenerator(mockDice.Object, mockStatPriorityProvider.Object,
                mockStatAdjustmentsProvider.Object);

            expectedStats = new Dictionary<String, Stat>();
            expectedStats.Add(statPriority.FirstPriority, new Stat() { Value = baseStat });
            expectedStats.Add(statPriority.SecondPriority, new Stat() { Value = baseStat });
            expectedStats.Add("other stat", new Stat() { Value = baseStat });
            mockStatRandomizer = new Mock<IStatsRandomizer>();
            mockStatRandomizer.Setup(r => r.Randomize()).Returns(expectedStats);


            characterClass = new CharacterClass();
            characterClass.ClassName = "class name";
        }

        [Test]
        public void RandomizesStatValues()
        {
            statsFactory.CreateWith(mockStatRandomizer.Object, characterClass, race);
            mockStatRandomizer.Verify(r => r.Randomize(), Times.Once);
        }

        [Test]
        public void GetPrioritiesByClassName()
        {
            statsFactory.CreateWith(mockStatRandomizer.Object, characterClass, race);
            mockStatPriorityProvider.Verify(p => p.GetStatPriorities(characterClass.ClassName), Times.Once);
        }

        [Test]
        public void PrioritizesStatsByClass()
        {
            expectedStats["other stat"].Value = 18;
            expectedStats[statPriority.FirstPriority].Value = 16;

            var stats = statsFactory.CreateWith(mockStatRandomizer.Object, characterClass, race);
            Assert.That(stats[statPriority.FirstPriority].Value, Is.EqualTo(18));
            Assert.That(stats[statPriority.SecondPriority].Value, Is.EqualTo(16));
            Assert.That(stats["other stat"].Value, Is.EqualTo(baseStat));
        }

        [Test]
        public void AdjustsStatsByBaseRace()
        {
            adjustments["other stat"] = 1;
            adjustments[statPriority.FirstPriority] = -1;

            var stats = statsFactory.CreateWith(mockStatRandomizer.Object, characterClass, race);
            Assert.That(stats[statPriority.FirstPriority].Value, Is.EqualTo(baseStat - 1));
            Assert.That(stats[statPriority.SecondPriority].Value, Is.EqualTo(baseStat));
            Assert.That(stats["other stat"].Value, Is.EqualTo(baseStat + 1));
        }

        [Test]
        public void LowOnD2IncreasesFirstPriorityStat()
        {
            characterClass.Level = 4;
            mockDice.Setup(d => d.d2(1)).Returns(1);

            var stats = statsFactory.CreateWith(mockStatRandomizer.Object, characterClass, race);
            Assert.That(stats[statPriority.FirstPriority].Value, Is.EqualTo(baseStat + 1), StatConstants.Strength);
            Assert.That(stats[statPriority.SecondPriority].Value, Is.EqualTo(baseStat), StatConstants.Constitution);
        }

        [Test]
        public void HighOnD2IncreasesSecondPriorityStat()
        {
            characterClass.Level = 4;
            mockDice.Setup(d => d.d2(1)).Returns(2);

            var stats = statsFactory.CreateWith(mockStatRandomizer.Object, characterClass, race);
            Assert.That(stats[statPriority.FirstPriority].Value, Is.EqualTo(baseStat), StatConstants.Strength);
            Assert.That(stats[statPriority.SecondPriority].Value, Is.EqualTo(baseStat + 1), StatConstants.Constitution);
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
                Assert.That(stats[statPriority.FirstPriority].Value, Is.EqualTo(baseStat), level.ToString());
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
                Assert.That(stats[statPriority.FirstPriority].Value, Is.EqualTo(baseStat + 1), level.ToString());
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
                Assert.That(stats[statPriority.FirstPriority].Value, Is.EqualTo(baseStat + 2), level.ToString());
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
                Assert.That(stats[statPriority.FirstPriority].Value, Is.EqualTo(baseStat + 3), level.ToString());
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
                Assert.That(stats[statPriority.FirstPriority].Value, Is.EqualTo(baseStat + 4), level.ToString());
            }
        }

        [Test]
        public void IncreaseStatByFive()
        {
            mockDice.Setup(d => d.d2(1)).Returns(1);
            characterClass.Level = 20;

            var stats = statsFactory.CreateWith(mockStatRandomizer.Object, characterClass, race);
            Assert.That(stats[statPriority.FirstPriority].Value, Is.EqualTo(baseStat + 5));
        }

        [Test]
        public void CannotHaveStatLessThanOne()
        {
            adjustments[statPriority.FirstPriority] = -9266;

            var stats = statsFactory.CreateWith(mockStatRandomizer.Object, characterClass, race);
            Assert.That(stats[statPriority.FirstPriority].Value, Is.EqualTo(1));
        }
    }
}