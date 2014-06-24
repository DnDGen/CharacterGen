using NPCGen.Common.CharacterClasses;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Common.Classes
{
    [TestFixture]
    public class CharacterClassTests
    {
        private CharacterClass characterClass;

        [SetUp]
        public void Setup()
        {
            characterClass = new CharacterClass();
        }

        [Test]
        public void CharacterClassInitialized()
        {
            Assert.That(characterClass.BaseAttack, Is.Not.Null);
            Assert.That(characterClass.ClassName, Is.Empty);
            Assert.That(characterClass.Level, Is.EqualTo(0));
        }
    }
}