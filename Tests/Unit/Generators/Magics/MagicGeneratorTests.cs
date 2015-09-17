using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Items;
using CharacterGen.Common.Magics;
using CharacterGen.Common.Races;
using CharacterGen.Generators.Domain.Magics;
using CharacterGen.Generators.Magics;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CharacterGen.Tests.Unit.Generators.Magics
{
    [TestFixture]
    public class MagicGeneratorTests
    {
        private Mock<ISpellsGenerator> mockSpellsGenerator;
        private Mock<IAnimalGenerator> mockAnimalGenerator;
        private IMagicGenerator magicGenerator;
        private CharacterClass characterClass;
        private List<Feat> feats;
        private Equipment equipment;
        private Alignment alignment;
        private Race race;

        [SetUp]
        public void Setup()
        {
            mockSpellsGenerator = new Mock<ISpellsGenerator>();
            mockAnimalGenerator = new Mock<IAnimalGenerator>();
            magicGenerator = new MagicGenerator(mockSpellsGenerator.Object, mockAnimalGenerator.Object);
            characterClass = new CharacterClass();
            feats = new List<Feat>();
            equipment = new Equipment();
            alignment = new Alignment();
            race = new Race();
        }

        [Test]
        public void GenerateMagic()
        {
            var magic = magicGenerator.GenerateWith(alignment, characterClass, race, feats, equipment);
            Assert.That(magic, Is.Not.Null);
        }

        [Test]
        public void GenerateSpells()
        {
            var spells = new Dictionary<Int32, IEnumerable<String>>();
            mockSpellsGenerator.Setup(g => g.GenerateFrom(characterClass, feats, equipment)).Returns(spells);

            var magic = magicGenerator.GenerateWith(alignment, characterClass, race, feats, equipment);
            Assert.That(magic.Spells, Is.EqualTo(spells));
        }

        [Test]
        public void DoNotGenerateAnimal()
        {
            Animal animal = null;
            mockAnimalGenerator.Setup(g => g.GenerateFrom(alignment, characterClass, race, feats)).Returns(animal);

            var magic = magicGenerator.GenerateWith(alignment, characterClass, race, feats, equipment);
            Assert.That(magic.Animal, Is.Null);
        }

        [Test]
        public void GenerateAnimal()
        {
            Animal animal = new Animal();
            mockAnimalGenerator.Setup(g => g.GenerateFrom(alignment, characterClass, race, feats)).Returns(animal);

            var magic = magicGenerator.GenerateWith(alignment, characterClass, race, feats, equipment);
            Assert.That(magic.Animal, Is.EqualTo(animal));
        }
    }
}
