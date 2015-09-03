using CharacterGen.Common.Magics;
using NUnit.Framework;

namespace CharacterGen.Tests.Unit.Common.Magics
{
    [TestFixture]
    public class MagicTests
    {
        private Magic magic;

        [SetUp]
        public void Setup()
        {
            magic = new Magic();
        }

        [Test]
        public void MagicInitialized()
        {
            Assert.That(magic.Animals, Is.Empty);
            Assert.That(magic.Spells, Is.Empty);
        }
    }
}