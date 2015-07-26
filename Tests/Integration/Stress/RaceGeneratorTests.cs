﻿using System;
using CharacterGen.Common.Races;
using CharacterGen.Generators.Randomizers.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Stress
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
            Assert.That(race.BaseRace, Is.Not.Empty);
            Assert.That(race.Metarace, Is.Not.Empty);
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
            while (TestShouldKeepRunning() && race.Metarace == RaceConstants.Metaraces.None);

            Assert.That(race.Metarace, Is.Not.EqualTo(RaceConstants.Metaraces.None));
        }

        [Test]
        public void MetaraceDoesNotHappen()
        {
            var race = new Race();

            do race = GenerateRace();
            while (TestShouldKeepRunning() && race.Metarace != RaceConstants.Metaraces.None);

            Assert.That(race.Metarace, Is.EqualTo(RaceConstants.Metaraces.None));
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
            var forcableRandomizer = MetaraceRandomizer as IForcableMetaraceRandomizer;
            forcableRandomizer.ForceMetarace = true;

            var race = new Race();

            do race = GenerateRace();
            while (TestShouldKeepRunning() && String.IsNullOrEmpty(race.MetaraceSpecies));

            Assert.That(race.MetaraceSpecies, Is.Not.Empty);
        }

        [Test]
        public void MetaraceSpeciesDoNotHappen()
        {
            var forcableRandomizer = MetaraceRandomizer as IForcableMetaraceRandomizer;
            forcableRandomizer.ForceMetarace = true;

            var race = new Race();

            do race = GenerateRace();
            while (TestShouldKeepRunning() && !String.IsNullOrEmpty(race.MetaraceSpecies));

            Assert.That(race.MetaraceSpecies, Is.Empty);
        }
    }
}