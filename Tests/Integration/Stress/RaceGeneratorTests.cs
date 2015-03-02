using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress
{
    [TestFixture]
    public class RaceGeneratorTests : StressTests
    {
        [TestCase("RaceGenerator")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var race = GenerateRace();
            Assert.That(race.BaseRace.Id, Is.Not.Empty);
            Assert.That(race.BaseRace.Name, Is.Not.Empty);
            Assert.That(race.Metarace.Id, Is.Not.Empty);
            Assert.That(race.Metarace.Name, Is.Not.Null);
            Assert.That(race.Size, Is.EqualTo(RaceConstants.Sizes.Large).Or.EqualTo(RaceConstants.Sizes.Medium).Or.EqualTo(RaceConstants.Sizes.Small));
            Assert.That(race.LandSpeed, Is.AtLeast(20));
            Assert.That(race.LandSpeed % 10, Is.EqualTo(0));
            Assert.That(race.AerialSpeed, Is.Not.Negative);
            Assert.That(race.MetaraceSpecies, Is.Not.Null);
        }

        private Race GenerateRace()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);

            return RaceGenerator.GenerateWith(alignment, characterClass, BaseRaceRandomizer, MetaraceRandomizer);
        }

        [Test]
        public void MetaraceHappens()
        {
            var race = new Race();

            do race = GenerateRace();
            while (TestShouldKeepRunning() && race.Metarace.Id == RaceConstants.Metaraces.NoneId);

            Assert.That(race.Metarace.Id, Is.Not.EqualTo(RaceConstants.Metaraces.NoneId));
        }

        [Test]
        public void MetaraceDoesNotHappen()
        {
            var race = new Race();

            do race = GenerateRace();
            while (TestShouldKeepRunning() && race.Metarace.Id != RaceConstants.Metaraces.NoneId);

            Assert.That(race.Metarace.Id, Is.EqualTo(RaceConstants.Metaraces.NoneId));
        }

        [Test]
        public void WingsHappen()
        {
            var race = new Race();

            do race = GenerateRace();
            while (TestShouldKeepRunning() && !race.HasWings);

            Assert.That(race.HasWings, Is.True);
            Assert.That(race.AerialSpeed, Is.Positive);
        }

        [Test]
        public void WingsDoNotHappen()
        {
            var race = new Race();

            do race = GenerateRace();
            while (TestShouldKeepRunning() && race.HasWings);

            Assert.That(race.HasWings, Is.False);
            Assert.That(race.AerialSpeed, Is.EqualTo(0));
        }

        [Test]
        public void MaleHappens()
        {
            var race = new Race();

            do race = GenerateRace();
            while (TestShouldKeepRunning() && !race.Male);

            Assert.That(race.Male, Is.True);
        }

        [Test]
        public void FemaleHappens()
        {
            var race = new Race();

            do race = GenerateRace();
            while (TestShouldKeepRunning() && race.Male);

            Assert.That(race.Male, Is.False);
        }

        [Test]
        public void MetaraceSpeciesHappen()
        {
            var race = new Race();

            do race = GenerateRace();
            while (TestShouldKeepRunning() && String.IsNullOrEmpty(race.MetaraceSpecies));

            Assert.That(race.MetaraceSpecies, Is.Not.Empty);
        }

        [Test]
        public void MetaraceSpeciesDoNotHappen()
        {
            var race = new Race();

            do race = GenerateRace();
            while (TestShouldKeepRunning() && !String.IsNullOrEmpty(race.MetaraceSpecies));

            Assert.That(race.MetaraceSpecies, Is.Empty);
        }
    }
}