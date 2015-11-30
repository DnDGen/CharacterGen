using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
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
        private Alignment alignment;
        private Race race;
        private Dictionary<String, Stat> stats;

        [SetUp]
        public void Setup()
        {
            mockSpellsGenerator = new Mock<ISpellsGenerator>();
            mockAnimalGenerator = new Mock<IAnimalGenerator>();
            magicGenerator = new MagicGenerator(mockSpellsGenerator.Object, mockAnimalGenerator.Object);
            characterClass = new CharacterClass();
            feats = new List<Feat>();
            alignment = new Alignment();
            race = new Race();
            stats = new Dictionary<String, Stat>();
        }

        [Test]
        public void GenerateMagic()
        {
            var magic = magicGenerator.GenerateWith(alignment, characterClass, race, stats, feats);
            Assert.That(magic, Is.Not.Null);
        }

        [Test]
        public void GenerateSpells()
        {
            var spells = new List<Spells>();
            mockSpellsGenerator.Setup(g => g.GenerateFrom(characterClass, stats)).Returns(spells);

            var magic = magicGenerator.GenerateWith(alignment, characterClass, race, stats, feats);
            Assert.That(magic.SpellsPerDay, Is.EqualTo(spells));
        }

        [Test]
        public void DoNotGenerateAnimal()
        {
            mockAnimalGenerator.Setup(g => g.GenerateFrom(alignment, characterClass, race, feats)).Returns(String.Empty);

            var magic = magicGenerator.GenerateWith(alignment, characterClass, race, stats, feats);
            Assert.That(magic.Animal, Is.Empty);
        }

        [Test]
        public void GenerateAnimal()
        {
            mockAnimalGenerator.Setup(g => g.GenerateFrom(alignment, characterClass, race, feats)).Returns("animal");

            var magic = magicGenerator.GenerateWith(alignment, characterClass, race, stats, feats);
            Assert.That(magic.Animal, Is.EqualTo("animal"));
        }
    }
}
