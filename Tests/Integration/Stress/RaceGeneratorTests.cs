using System;
using System.Collections.Generic;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress
{
    [TestFixture]
    public class RaceGeneratorTests : StressTests
    {
        private IEnumerable<String> baseRaces;
        private IEnumerable<String> metaraces;
        private IEnumerable<String> sizes;

        [SetUp]
        public void Setup()
        {
            baseRaces = RaceConstants.BaseRaces.GetBaseRaces();
            metaraces = RaceConstants.Metaraces.GetAllMetaraces();
            sizes = RaceConstants.Sizes.GetSizes();
        }

        protected override void MakeAssertions()
        {
            var race = GenerateRace();
            Assert.That(baseRaces, Contains.Item(race.BaseRace));
            Assert.That(metaraces, Contains.Item(race.Metarace));
            Assert.That(sizes, Contains.Item(race.Size));
        }

        private Race GenerateRace()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);

            return RaceGenerator.GenerateWith(alignment.Goodness, characterClass, BaseRaceRandomizer, MetaraceRandomizer);
        }

        [Test]
        public void MetaraceHappens()
        {
            var race = new Race();

            do race = GenerateRace();
            while (TestShouldKeepRunning() && race.Metarace == RaceConstants.Metaraces.None);

            Assert.That(race.Metarace, Is.Not.EqualTo(RaceConstants.Metaraces.None));
            AssertIterations();
        }

        [Test]
        public void MetaraceDoesNotHappen()
        {
            var race = new Race();

            do race = GenerateRace();
            while (TestShouldKeepRunning() && race.Metarace != RaceConstants.Metaraces.None);

            Assert.That(race.Metarace, Is.EqualTo(RaceConstants.Metaraces.None));
            AssertIterations();
        }

        [Test]
        public void WingsHappen()
        {
            var race = new Race();

            do race = GenerateRace();
            while (TestShouldKeepRunning() && !race.HasWings);

            Assert.That(race.HasWings, Is.True);
            AssertIterations();
        }

        [Test]
        public void WingsDoNotHappen()
        {
            var race = new Race();

            do race = GenerateRace();
            while (TestShouldKeepRunning() && race.HasWings);

            Assert.That(race.HasWings, Is.False);
            AssertIterations();
        }

        [Test]
        public void MaleHappens()
        {
            var race = new Race();

            do race = GenerateRace();
            while (TestShouldKeepRunning() && !race.Male);

            Assert.That(race.Male, Is.True);
            AssertIterations();
        }

        [Test]
        public void FemaleHappens()
        {
            var race = new Race();

            do race = GenerateRace();
            while (TestShouldKeepRunning() && race.Male);

            Assert.That(race.Male, Is.False);
            AssertIterations();
        }
    }
}