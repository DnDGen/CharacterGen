using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Unit.Common.Races
{
    [TestFixture]
    public class AgeTests
    {
        private Age age;

        [SetUp]
        public void Setup()
        {
            age = new Age();
        }

        [Test]
        public void AgeInitialized()
        {
            Assert.That(age.Years, Is.EqualTo(0));
            Assert.That(age.Stage, Is.Empty);
        }
    }
}
