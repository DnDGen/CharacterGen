using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Common.Races
{
    [TestFixture]
    public class RaceNameTests
    {
        private RaceName raceName;

        [SetUp]
        public void Setup()
        {
            raceName = new RaceName();
        }

        [Test]
        public void RaceNameIsInitialized()
        {
            Assert.That(raceName.Id, Is.Empty);
            Assert.That(raceName.Name, Is.Empty);
        }
    }
}
