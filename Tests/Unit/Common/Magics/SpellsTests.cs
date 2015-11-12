using CharacterGen.Common.Magics;
using NUnit.Framework;

namespace CharacterGen.Tests.Unit.Common.Magics
{
    [TestFixture]
    public class SpellsTests
    {
        private Spells spells;

        [SetUp]
        public void Setup()
        {
            spells = new Spells();
        }

        [Test]
        public void SpellsInitialized()
        {
            Assert.That(spells.Level, Is.EqualTo(0));
            Assert.That(spells.Quantity, Is.EqualTo(0));
            Assert.That(spells.HasDomainSpell, Is.False);
        }
    }
}
