using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;
using CharacterGen.Generators;
using CharacterGen.Generators.Domain.Magics;
using CharacterGen.Generators.Magics;
using CharacterGen.Generators.Randomizers.Races;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CharacterGen.Tests.Unit.Generators.Magics
{
    [TestFixture]
    public class AnimalGeneratorTests
    {
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private Mock<IRaceGenerator> mockRaceGenerator;
        private Mock<IBaseRaceRandomizer> mockAnimalBaseRaceRandomizer;
        private Mock<IMetaraceRandomizer> mockNoMetaraceRandomizer;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private IAnimalGenerator animalGenerator;
        private CharacterClass characterClass;
        private List<Feat> feats;
        private List<String> animals;
        private Race race;
        private Alignment alignment;
        private Dictionary<String, Int32> levelAdjustments;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            mockRaceGenerator = new Mock<IRaceGenerator>();
            mockAnimalBaseRaceRandomizer = new Mock<IBaseRaceRandomizer>();
            mockNoMetaraceRandomizer = new Mock<IMetaraceRandomizer>();
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            animalGenerator = new AnimalGenerator();
            characterClass = new CharacterClass();
            feats = new List<Feat>();
            animals = new List<String>();
            race = new Race();
            alignment = new Alignment();
            levelAdjustments = new Dictionary<String, Int32>();

            characterClass.Level = 9266;
            characterClass.ClassName = "class name";
            race.BaseRace = "animal";
            animals.Add(race.BaseRace);
            levelAdjustments[race.BaseRace] = 0;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.Animals, characterClass.ClassName))
                .Returns(animals);
            mockRaceGenerator.Setup(g => g.GenerateWith(alignment, characterClass, mockAnimalBaseRaceRandomizer.Object,
                mockNoMetaraceRandomizer.Object)).Returns(race);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments))
                .Returns(levelAdjustments);
        }

        [Test]
        public void GenerateNoAnimal()
        {
            animals.Clear();
            var animal = animalGenerator.GenerateFrom(characterClass, feats);
            Assert.That(animal, Is.Null);
        }

        [Test]
        public void GenerateAnimal()
        {
            var animal = animalGenerator.GenerateFrom(characterClass, feats);
            Assert.That(animal, Is.Not.Null);
        }

        [Test]
        public void GenerateAnimalRace()
        {
            var animal = animalGenerator.GenerateFrom(characterClass, feats);
            Assert.That(animal.Race, Is.EqualTo(race));
        }

        [Test]
        public void RegenerateAnimalRaceIfLevelAdjustmentIsTooMuch()
        {
            var wrongRace = new Race { BaseRace = "other animal" };
            animals.Add(wrongRace.BaseRace);
            levelAdjustments[wrongRace.BaseRace] = 90210;

            mockRaceGenerator.SetupSequence(g => g.GenerateWith(alignment, characterClass, mockAnimalBaseRaceRandomizer.Object,
                mockNoMetaraceRandomizer.Object)).Returns(wrongRace).Returns(race);

            var animal = animalGenerator.GenerateFrom(characterClass, feats);
            Assert.That(animal.Race, Is.EqualTo(race));
        }

        [Test]
        public void GenerateAnimalAbilities()
        {
            //Languages - racial languages, if any
            //Stats - set, adjusted (increased) for some character classes
            //Skills - set
            //Feats - racial, ones from character class, no additional

            throw new NotImplementedException();
        }

        [Test]
        public void GenerateAnimalCombat()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void GenerateAnimalTricks()
        {
            throw new NotImplementedException();
        }
    }
}
