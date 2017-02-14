using CharacterGen.Abilities.Stats;
using CharacterGen.Domain.Generators.Randomizers.Stats;
using NUnit.Framework;
using System.Collections.Generic;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.Stats
{
    [TestFixture]
    public class BaseStatsRandomizerTests
    {
        private TestStatRandomizer randomizer;

        [SetUp]
        public void Setup()
        {
            randomizer = new TestStatRandomizer();
        }

        [Test]
        public void StatsContainAllStats()
        {
            var stats = randomizer.Randomize();

            Assert.That(stats.Keys, Contains.Item(StatConstants.Strength));
            Assert.That(stats.Keys, Contains.Item(StatConstants.Constitution));
            Assert.That(stats.Keys, Contains.Item(StatConstants.Dexterity));
            Assert.That(stats.Keys, Contains.Item(StatConstants.Intelligence));
            Assert.That(stats.Keys, Contains.Item(StatConstants.Wisdom));
            Assert.That(stats.Keys, Contains.Item(StatConstants.Charisma));
            Assert.That(stats.Count, Is.EqualTo(6));
        }

        [Test]
        public void StatsRolledTheSamePerStat()
        {
            randomizer.Roll = 11;

            var stats = randomizer.Randomize();

            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(randomizer.Roll));
            Assert.That(stats[StatConstants.Constitution].Value, Is.EqualTo(randomizer.Roll));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(randomizer.Roll));
            Assert.That(stats[StatConstants.Intelligence].Value, Is.EqualTo(randomizer.Roll));
            Assert.That(stats[StatConstants.Wisdom].Value, Is.EqualTo(randomizer.Roll));
            Assert.That(stats[StatConstants.Charisma].Value, Is.EqualTo(randomizer.Roll));
        }

        [Test]
        public void StatsAreRolledIndividually()
        {
            randomizer.Roll = 11;
            randomizer.Reroll = 12;

            var stats = randomizer.Randomize();

            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(16));
            Assert.That(stats[StatConstants.Constitution].Value, Is.EqualTo(13));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(14));
            Assert.That(stats[StatConstants.Intelligence].Value, Is.EqualTo(15));
            Assert.That(stats[StatConstants.Wisdom].Value, Is.EqualTo(17));
            Assert.That(stats[StatConstants.Charisma].Value, Is.EqualTo(12));
        }

        [Test]
        public void LoopUntilStatsAreAllowed()
        {
            randomizer.Roll = 11;
            randomizer.Reroll = 12;
            randomizer.AllowedOnRandomize = 10;

            var stats = randomizer.Randomize();

            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(16));
            Assert.That(stats[StatConstants.Constitution].Value, Is.EqualTo(13));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(14));
            Assert.That(stats[StatConstants.Intelligence].Value, Is.EqualTo(15));
            Assert.That(stats[StatConstants.Wisdom].Value, Is.EqualTo(17));
            Assert.That(stats[StatConstants.Charisma].Value, Is.EqualTo(12));
        }

        [Test]
        public void IfStatsNeverAllowed_ReturnDefaultValues()
        {
            randomizer.Roll = 11;
            randomizer.Reroll = 12;
            randomizer.AllowedOnRandomize = int.MaxValue;

            var stats = randomizer.Randomize();

            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(9266));
            Assert.That(stats[StatConstants.Constitution].Value, Is.EqualTo(9266));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(9266));
            Assert.That(stats[StatConstants.Intelligence].Value, Is.EqualTo(9266));
            Assert.That(stats[StatConstants.Wisdom].Value, Is.EqualTo(9266));
            Assert.That(stats[StatConstants.Charisma].Value, Is.EqualTo(9266));
        }

        private class TestStatRandomizer : BaseStatsRandomizer
        {
            public int Roll { get; set; }
            public int Reroll { get; set; }
            public int AllowedOnRandomize { get; set; }

            protected override int defaultValue
            {
                get
                {
                    return 9266;
                }
            }

            private int randomizeCount;

            public TestStatRandomizer()
                : base(new ConfigurableIterationGenerator(10))
            {
                randomizeCount = 0;
                AllowedOnRandomize = 1;
            }

            protected override int RollStat()
            {
                if (randomizeCount + 1 < AllowedOnRandomize || Reroll == 0)
                    return Roll;

                return Reroll++;
            }

            protected override bool StatsAreAllowed(IEnumerable<Stat> stats)
            {
                randomizeCount++;
                return randomizeCount >= AllowedOnRandomize;
            }
        }
    }
}