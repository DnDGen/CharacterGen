using CharacterGen.Common.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Unit.Common.Races
{
    [TestFixture]
    public class RaceTests
    {
        private Race race;

        [SetUp]
        public void Setup()
        {
            race = new Race();
        }

        [Test]
        public void RaceInitialized()
        {
            Assert.That(race.BaseRace, Is.Not.Null);
            Assert.That(race.Metarace, Is.Not.Null);
            Assert.That(race.Male, Is.False);
            Assert.That(race.HasWings, Is.False);
            Assert.That(race.Size, Is.Empty);
            Assert.That(race.AerialSpeed, Is.EqualTo(0));
            Assert.That(race.LandSpeed, Is.EqualTo(0));
            Assert.That(race.MetaraceSpecies, Is.Empty);
            Assert.That(race.HeightInInches, Is.EqualTo(0));
            Assert.That(race.WeightInPounds, Is.EqualTo(0));
            Assert.That(race.AgeInYears, Is.EqualTo(0));
        }
    }
}