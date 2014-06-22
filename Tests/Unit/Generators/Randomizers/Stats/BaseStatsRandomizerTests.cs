using System;
using System.Collections.Generic;
using NPCGen.Common.Stats;
using NPCGen.Generators.Randomizers.Stats;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.Stats
{
    [TestFixture]
    public class BaseStatsRandomizerTests
    {
        private TestStatRandomizer randomizer;

        [SetUp]
        public void Setup()
        {
            randomizer = new TestStatRandomizer();
            randomizer.Allowed = true;
            randomizer.SwitchAllowed = false;
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
            randomizer.Roll = 10;

            var stats = randomizer.Randomize();

            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(randomizer.Roll));
            Assert.That(stats[StatConstants.Constitution].Value, Is.EqualTo(randomizer.Roll));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(randomizer.Roll));
            Assert.That(stats[StatConstants.Intelligence].Value, Is.EqualTo(randomizer.Roll));
            Assert.That(stats[StatConstants.Wisdom].Value, Is.EqualTo(randomizer.Roll));
            Assert.That(stats[StatConstants.Charisma].Value, Is.EqualTo(randomizer.Roll));
        }

        [Test]
        public void LoopUntilStatsAreAllowed()
        {
            randomizer.Roll = 10;
            randomizer.Reroll = 12;
            randomizer.Allowed = false;
            randomizer.SwitchAllowed = true;

            var stats = randomizer.Randomize();

            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(randomizer.Reroll));
            Assert.That(stats[StatConstants.Constitution].Value, Is.EqualTo(randomizer.Reroll));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(randomizer.Reroll));
            Assert.That(stats[StatConstants.Intelligence].Value, Is.EqualTo(randomizer.Reroll));
            Assert.That(stats[StatConstants.Wisdom].Value, Is.EqualTo(randomizer.Reroll));
            Assert.That(stats[StatConstants.Charisma].Value, Is.EqualTo(randomizer.Reroll));
        }

        private class TestStatRandomizer : BaseStatsRandomizer
        {
            public Int32 Roll { get; set; }
            public Int32 Reroll { get; set; }
            public Boolean Allowed { get; set; }
            public Boolean SwitchAllowed { get; set; }

            private Boolean firstRoll;

            public TestStatRandomizer()
            {
                firstRoll = true;
            }

            protected override Int32 RollStat()
            {
                if (firstRoll || Reroll == 0)
                    return Roll;

                return Reroll;
            }

            protected override Boolean StatsAreAllowed(IEnumerable<Stat> stats)
            {
                var retunValue = Allowed;
                firstRoll &= !firstRoll;

                if (SwitchAllowed)
                    Allowed = !Allowed;

                return retunValue;
            }
        }
    }
}