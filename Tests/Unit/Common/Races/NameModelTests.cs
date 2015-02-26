using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Common.Races
{
    [TestFixture]
    public class NameModelTests
    {
        private NameModel nameModel;

        [SetUp]
        public void Setup()
        {
            nameModel = new NameModel();
        }

        [Test]
        public void NameModelIsInitialized()
        {
            Assert.That(nameModel.Id, Is.Empty);
            Assert.That(nameModel.Name, Is.Empty);
        }
    }
}