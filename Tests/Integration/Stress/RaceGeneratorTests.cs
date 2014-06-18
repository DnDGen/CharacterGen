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
            var dependentData = GetNewDependentData();

            var race = RaceGenerator.CreateWith(dependentData.Alignment.Goodness, dependentData.CharacterClassPrototype, BaseRaceRandomizer,
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
                var dependentData = GetNewDependentData();
                race = RaceGenerator.CreateWith(dependentData.Alignment.Goodness, dependentData.CharacterClassPrototype, BaseRaceRandomizer,
                    MetaraceRandomizer);
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
                var dependentData = GetNewDependentData();
                race = RaceGenerator.CreateWith(dependentData.Alignment.Goodness, dependentData.CharacterClassPrototype, BaseRaceRandomizer,
                    MetaraceRandomizer);
            } while (TestShouldKeepRunning() && !String.IsNullOrEmpty(race.Metarace));

            Assert.That(race.Metarace, Is.Empty);
            AssertIterations();
        }
    }
}