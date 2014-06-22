using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress
{
    [TestFixture]
    public class RaceGeneratorTests : StressTests
    {
        protected override void MakeAssertions()
        {
            var alignment = GetNewAlignment();
            var prototype = GetNewCharacterClassPrototype(alignment);

            var race = RaceGenerator.GenerateWith(alignment.Goodness, prototype, BaseRaceRandomizer,
                MetaraceRandomizer);
            Assert.That(race.BaseRace, Is.Not.Empty);
            Assert.That(race.Metarace, Is.Not.Null);
        }

        [Test]
        public void MetaraceHappens()
        {
            var race = new Race();

            do
            {
                var alignment = GetNewAlignment();
                var prototype = GetNewCharacterClassPrototype(alignment);
                race = RaceGenerator.GenerateWith(alignment.Goodness, prototype, BaseRaceRandomizer, MetaraceRandomizer);
            } while (TestShouldKeepRunning() && String.IsNullOrEmpty(race.Metarace));

            Assert.That(race.Metarace, Is.Not.Empty);
            AssertIterations();
        }

        [Test]
        public void MetaraceDoesNotHappen()
        {
            var race = new Race();

            do
            {
                var alignment = GetNewAlignment();
                var prototype = GetNewCharacterClassPrototype(alignment);
                race = RaceGenerator.GenerateWith(alignment.Goodness, prototype, BaseRaceRandomizer, MetaraceRandomizer);
            } while (TestShouldKeepRunning() && !String.IsNullOrEmpty(race.Metarace));

            Assert.That(race.Metarace, Is.Empty);
            AssertIterations();
        }
    }
}