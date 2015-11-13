using CharacterGen.Common.Races;
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
            Assert.That(race.Metarace, Is.Not.Null);
            Assert.That(race.Size, Is.EqualTo(RaceConstants.Sizes.Large).Or.EqualTo(RaceConstants.Sizes.Medium).Or.EqualTo(RaceConstants.Sizes.Small));
            Assert.That(race.LandSpeed, Is.Positive);
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
    }
}