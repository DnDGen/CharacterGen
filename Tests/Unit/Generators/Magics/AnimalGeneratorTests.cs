using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Generators.Domain.Magics;
using CharacterGen.Generators.Magics;
using NUnit.Framework;
using System.Collections.Generic;

namespace CharacterGen.Tests.Unit.Generators.Magics
{
    [TestFixture]
    public class AnimalGeneratorTests
    {
        private IAnimalGenerator animalGenerator;
        private CharacterClass characterClass;
        private List<Feat> feats;

        [SetUp]
        public void Setup()
        {
            animalGenerator = new AnimalGenerator();
            characterClass = new CharacterClass();
            feats = new List<Feat>();
        }

        [Test]
        public void GenerateAnimal()
        {
            var animal = animalGenerator.GenerateFrom(characterClass, feats);
            Assert.That(animal, Is.Not.Null);
        }

        [Test]
        public void GenerateNoAnimal()
        {
            var animal = animalGenerator.GenerateFrom(characterClass, feats);
            Assert.That(animal, Is.Null);
        }
    }
}
