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
            Assert.That(race.BaseRace, Is.Not.Null);
            Assert.That(race.Metarace, Is.Not.Null);
            Assert.That(race.IsMale, Is.False);
            Assert.That(race.HasWings, Is.False);
            Assert.That(race.Size, Is.Empty);
            Assert.That(race.AerialSpeed, Is.EqualTo(0));
            Assert.That(race.LandSpeed, Is.EqualTo(0));
            Assert.That(race.MetaraceSpecies, Is.Empty);
            Assert.That(race.HeightInInches, Is.EqualTo(0));
            Assert.That(race.WeightInPounds, Is.EqualTo(0));
            Assert.That(race.Age, Is.Not.Null);
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