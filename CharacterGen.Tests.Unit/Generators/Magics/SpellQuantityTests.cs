using CharacterGen.Magics;
using NUnit.Framework;

namespace CharacterGen.Tests.Unit.Common.Magics
{
    [TestFixture]
    public class SpellQuantityTests
    {
        private SpellQuantity spellQuantity;

        [SetUp]
        public void Setup()
        {
            spellQuantity = new SpellQuantity();
        }

        [Test]
        public void SpellsInitialized()
        {
            Assert.That(spellQuantity.Level, Is.EqualTo(0));
            Assert.That(spellQuantity.Quantity, Is.EqualTo(0));
            Assert.That(spellQuantity.HasDomainSpell, Is.False);
        }
    }
}
