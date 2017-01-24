using CharacterGen.Magics;
using NUnit.Framework;

namespace CharacterGen.Tests.Unit.Generators.Magics
{
    [TestFixture]
    public class SpellTests
    {
        private Spell spell;

        [SetUp]
        public void Setup()
        {
            spell = new Spell();
        }

        [Test]
        public void SpellIsInitialized()
        {
            Assert.That(spell.Level, Is.EqualTo(0));
            Assert.That(spell.Source, Is.Empty);
            Assert.That(spell.Metamagic, Is.Empty);
            Assert.That(spell.Name, Is.Empty);
        }
    }
}
