using CharacterGen.Common.Magics;
using NUnit.Framework;

namespace CharacterGen.Tests.Unit.Common.Magics
{
    [TestFixture]
    public class FamiliarTests
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
            Assert.That(animal.Type, Is.Empty);
            Assert.That(animal.ArmorClass, Is.EqualTo(0));
            Assert.That(animal.HitPoints, Is.EqualTo(0));
            Assert.That(animal.Tricks, Is.EqualTo(0));
            Assert.That(animal.Feats, Is.Empty);
        }
    }
}