using System;
using System.Collections.Generic;
using NPCGen.Core.Data.Stats;
using NPCGen.Core.Generation.Randomizers.Stats;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.Stats
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

            Assert.That(stats.Count, Is.EqualTo(6));
            Assert.That(stats.ContainsKey(StatConstants.Strength), Is.True);
            Assert.That(stats.ContainsKey(StatConstants.Constitution), Is.True);
            Assert.That(stats.ContainsKey(StatConstants.Dexterity), Is.True);
            Assert.That(stats.ContainsKey(StatConstants.Intelligence), Is.True);
            Assert.That(stats.ContainsKey(StatConstants.Wisdom), Is.True);
            Assert.That(stats.ContainsKey(StatConstants.Charisma), Is.True);
        }

        [Test]
        public void StatsAreProperlyNamed()
        {
            var stats = randomizer.Randomize();

            Assert.That(stats[StatConstants.Strength].Name, Is.EqualTo(StatConstants.Strength));
            Assert.That(stats[StatConstants.Constitution].Name, Is.EqualTo(StatConstants.Constitution));
            Assert.That(stats[StatConstants.Dexterity].Name, Is.EqualTo(StatConstants.Dexterity));
            Assert.That(stats[StatConstants.Intelligence].Name, Is.EqualTo(StatConstants.Intelligence));
            Assert.That(stats[StatConstants.Wisdom].Name, Is.EqualTo(StatConstants.Wisdom));
            Assert.That(stats[StatConstants.Charisma].Name, Is.EqualTo(StatConstants.Charisma));
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