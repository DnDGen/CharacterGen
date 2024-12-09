using DnDGen.CharacterGen.Magics;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Unit.Generators.Magics
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
            Assert.That(spell.Sources, Is.Empty);
            Assert.That(spell.Metamagic, Is.Empty);
            Assert.That(spell.Name, Is.Empty);
        }

        [Test]
        public void SummaryFromSingleSource()
        {
            spell.Name = "my spell";
            spell.Level = 9266;
            spell.Sources = ["my source"];

            Assert.That(spell.Summary, Is.EqualTo("my spell (my source/9266)"));
        }

        [Test]
        public void SummaryFromMultipleSources()
        {
            spell.Name = "my spell";
            spell.Level = 9266;
            spell.Sources = ["my source", "my other source"];

            Assert.That(spell.Summary, Is.EqualTo("my spell (my source/9266, my other source/9266)"));
        }
    }
}
