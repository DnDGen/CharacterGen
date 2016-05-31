using CharacterGen.CharacterClasses;
using NUnit.Framework;

namespace CharacterGen.Tests.Unit.Common.Classes
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
            Assert.That(characterClass.ClassName, Is.Empty);
            Assert.That(characterClass.Level, Is.EqualTo(0));
            Assert.That(characterClass.SpecialistFields, Is.Empty);
            Assert.That(characterClass.ProhibitedFields, Is.Empty);
        }
    }
}