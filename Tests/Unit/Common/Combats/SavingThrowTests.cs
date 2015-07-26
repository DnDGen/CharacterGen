using CharacterGen.Common.Combats;
using NUnit.Framework;

namespace CharacterGen.Tests.Unit.Common.Combats
{
    [TestFixture]
    public class SavingThrowTests
    {
        private SavingThrows savingThrows;

        [SetUp]
        public void Setup()
        {
            savingThrows = new SavingThrows();
        }

        [Test]
        public void SavingThrowsInitialized()
        {
            Assert.That(savingThrows.Fortitude, Is.EqualTo(0));
            Assert.That(savingThrows.Reflex, Is.EqualTo(0));
            Assert.That(savingThrows.Will, Is.EqualTo(0));
        }
    }
}