using CharacterGen.Common.Races;
using CharacterGen.Generators.Randomizers.Races;
using NUnit.Framework;
using System;

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
            Assert.That(race.Age, Is.InRange<Int32>(15, 170), race.BaseRace);
            Assert.That(race.HeightInInches, Is.InRange<Int32>(32, 82), race.BaseRace);
            Assert.That(race.WeightInPounds, Is.InRange<Int32>(27, 438), race.BaseRace);
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
            var race = Generate<Race>(GenerateRace,
                r => r.Metarace != RaceConstants.Metaraces.None);

            Assert.That(race.Metarace, Is.Not.EqualTo(RaceConstants.Metaraces.None));
        }

        [Test]
        public void MetaraceDoesNotHappen()
        {
            var race = Generate<Race>(GenerateRace,
                r => r.Metarace == RaceConstants.Metaraces.None);

            Assert.That(race.Metarace, Is.EqualTo(RaceConstants.Metaraces.None));
        }

        [Test]
        public void WingsHappen()
        {
            var race = Generate<Race>(GenerateRace,
                r => r.HasWings);

            Assert.That(race.HasWings, Is.True);
            Assert.That(race.AerialSpeed, Is.Positive);
        }

        [Test]
        public void WingsDoNotHappen()
        {
            var race = Generate<Race>(GenerateRace,
                r => r.HasWings == false);

            Assert.That(race.HasWings, Is.False);
            Assert.That(race.AerialSpeed, Is.EqualTo(0));
        }

        [Test]
        public void MaleHappens()
        {
            var race = Generate<Race>(GenerateRace,
                r => r.Male);

            Assert.That(race.Male, Is.True);
        }

        [Test]
        public void FemaleHappens()
        {
            var race = Generate<Race>(GenerateRace,
                r => r.Male == false);

            Assert.That(race.Male, Is.False);
        }

        [Test]
        public void MetaraceSpeciesHappen()
        {
            var forcableRandomizer = MetaraceRandomizer as IForcableMetaraceRandomizer;
            forcableRandomizer.ForceMetarace = true;

            var race = Generate<Race>(GenerateRace,
                r => String.IsNullOrEmpty(r.MetaraceSpecies) == false);

            Assert.That(race.MetaraceSpecies, Is.Not.Empty);
        }

        [Test]
        public void MetaraceSpeciesDoNotHappen()
        {
            var forcableRandomizer = MetaraceRandomizer as IForcableMetaraceRandomizer;
            forcableRandomizer.ForceMetarace = true;

            var race = Generate<Race>(GenerateRace,
                r => String.IsNullOrEmpty(r.MetaraceSpecies));

            Assert.That(race.MetaraceSpecies, Is.Empty);
        }
    }
}