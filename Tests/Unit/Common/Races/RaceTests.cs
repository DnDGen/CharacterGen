using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Common.Races
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
            Assert.That(race.Male, Is.False);
            Assert.That(race.HasWings, Is.False);
            Assert.That(race.Size, Is.Empty);
            Assert.That(race.AerialSpeed, Is.EqualTo(0));
            Assert.That(race.LandSpeed, Is.EqualTo(0));
            Assert.That(race.MetaraceSpecies, Is.Empty);
        }
    }
}