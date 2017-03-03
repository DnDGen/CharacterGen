using CharacterGen.Races;
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
            Assert.That(race.BaseRace, Is.Empty);
            Assert.That(race.Metarace, Is.Empty);
            Assert.That(race.IsMale, Is.False);
            Assert.That(race.HasWings, Is.False);
            Assert.That(race.Size, Is.Empty);
            Assert.That(race.MetaraceSpecies, Is.Empty);
            Assert.That(race.Age, Is.Not.Null);
            Assert.That(race.Age.Unit, Is.EqualTo("Years"));
            Assert.That(race.MaximumAge, Is.Not.Null);
            Assert.That(race.MaximumAge.Unit, Is.EqualTo("Years"));
            Assert.That(race.Height, Is.Not.Null);
            Assert.That(race.Height.Unit, Is.EqualTo("Inches"));
            Assert.That(race.Weight, Is.Not.Null);
            Assert.That(race.Weight.Unit, Is.EqualTo("Pounds"));
            Assert.That(race.LandSpeed, Is.Not.Null);
            Assert.That(race.LandSpeed.Unit, Is.EqualTo("feet per round"));
            Assert.That(race.AerialSpeed, Is.Not.Null);
            Assert.That(race.AerialSpeed.Unit, Is.EqualTo("feet per round"));
            Assert.That(race.ChallengeRating, Is.EqualTo(0));
        }

        [Test]
        public void GenderIsFemale()
        {
            race.IsMale = false;
            Assert.That(race.Gender, Is.EqualTo("Female"));
        }

        [Test]
        public void GenderIsMale()
        {
            race.IsMale = true;
            Assert.That(race.Gender, Is.EqualTo("Male"));
        }
    }
}