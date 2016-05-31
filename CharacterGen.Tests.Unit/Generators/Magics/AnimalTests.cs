using CharacterGen.Magics;
using NUnit.Framework;

namespace CharacterGen.Tests.Unit.Common.Magics
{
    [TestFixture]
    public class AnimalTests
    {
        private Animal animal;

        [SetUp]
        public void Setup()
        {
            animal = new Animal();
        }

        [Test]
        public void AnimalInitialized()
        {
            Assert.That(animal.Race, Is.Not.Null);
            Assert.That(animal.Ability, Is.Not.Null);
            Assert.That(animal.Combat, Is.Not.Null);
            Assert.That(animal.Tricks, Is.EqualTo(0));
        }
    }
}