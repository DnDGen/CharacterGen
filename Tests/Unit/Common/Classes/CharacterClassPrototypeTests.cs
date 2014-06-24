using NPCGen.Common.CharacterClasses;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Common.Classes
{
    [TestFixture]
    public class CharacterClassPrototypeTests
    {
        private CharacterClassPrototype characterClassPrototype;

        [SetUp]
        public void Setup()
        {
            characterClassPrototype = new CharacterClassPrototype();
        }

        [Test]
        public void CharacterClassPrototypeInitialized()
        {
            Assert.That(characterClassPrototype.ClassName, Is.Empty);
            Assert.That(characterClassPrototype.Level, Is.EqualTo(0));
        }
    }
}