using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Generators.Domain.Randomizers.Races.BaseRaces;
using CharacterGen.Generators.Randomizers.Races;
using CharacterGen.Generators.Verifiers.Exceptions;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class AnimalBaseRaceRandomizerTests
    {
        private RaceRandomizer animalBaseRaceRandomizer;
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private List<String> classAnimals;
        private List<String> alignmentAnimals;
        private Dictionary<String, Int32> animalLevelAdjustments;
        private CharacterClass characterClass;
        private Alignment alignment;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            animalBaseRaceRandomizer = new AnimalBaseRaceRandomizer(mockCollectionsSelector.Object, mockAdjustmentsSelector.Object);
            classAnimals = new List<String>();
            alignmentAnimals = new List<String>();
            animalLevelAdjustments = new Dictionary<String, Int32>();
            characterClass = new CharacterClass();
            alignment = new Alignment();

            characterClass.ClassName = "class name";
            characterClass.Level = 9266;
            alignment.Goodness = "goodness";
            alignment.Lawfulness = "lawfulness";
            classAnimals.Add("animal");
            alignmentAnimals.Add("animal");
            animalLevelAdjustments["animal"] = 42;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.Animals, characterClass.ClassName)).Returns(classAnimals);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.Animals, alignment.ToString())).Returns(alignmentAnimals);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments)).Returns(animalLevelAdjustments);
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<String>>())).Returns((IEnumerable<String> a) => a.Last());
        }

        [Test]
        public void GenerateAnimalBaseRace()
        {
            var animal = animalBaseRaceRandomizer.Randomize(alignment, characterClass);
            Assert.That(animal, Is.EqualTo("animal"));
        }

        [Test]
        public void GenerateAnimalBaseRaceThatMatchesAll()
        {
            classAnimals.Add("wrong animal");
            classAnimals.Add("class animal");
            alignmentAnimals.Add("alignment animal");
            alignmentAnimals.Add("wrong animal");
            animalLevelAdjustments["class animal"] = 600;
            animalLevelAdjustments["alignment animal"] = 600;
            animalLevelAdjustments["wrong animal"] = 90210;

            var animal = animalBaseRaceRandomizer.Randomize(alignment, characterClass);
            Assert.That(animal, Is.EqualTo("animal"));
        }

        [Test]
        public void GenerateRandomAnimalBaseRace()
        {
            classAnimals.Add("other animal");
            alignmentAnimals.Add("other animal");
            animalLevelAdjustments["other animal"] = 600;

            var animal = animalBaseRaceRandomizer.Randomize(alignment, characterClass);
            Assert.That(animal, Is.EqualTo("other animal"));
        }

        [Test]
        public void ThrowExceptionIfClassHasNoAnimals()
        {
            classAnimals.Clear();
            classAnimals.Add("class animal");

            Assert.That(() => animalBaseRaceRandomizer.Randomize(alignment, characterClass), Throws.InstanceOf<IncompatibleRandomizersException>());
        }

        [Test]
        public void ThrowExceptionIfAlignmentHasNoAnimals()
        {
            alignmentAnimals.Clear();
            alignmentAnimals.Add("alignment animal");

            Assert.That(() => animalBaseRaceRandomizer.Randomize(alignment, characterClass), Throws.InstanceOf<IncompatibleRandomizersException>());
        }

        [Test]
        public void ThrowExceptionIfLevelAdjustmentsPreventAnimal()
        {
            animalLevelAdjustments["animal"] = 90210;

            Assert.That(() => animalBaseRaceRandomizer.Randomize(alignment, characterClass), Throws.InstanceOf<IncompatibleRandomizersException>());
        }

        [Test]
        public void ReturnAllMatchingAnimals()
        {
            classAnimals.Add("other animal");
            classAnimals.Add("wrong animal");
            classAnimals.Add("class animal");
            alignmentAnimals.Add("alignment animal");
            alignmentAnimals.Add("wrong animal");
            alignmentAnimals.Add("other animal");
            animalLevelAdjustments["other animal"] = 600;
            animalLevelAdjustments["class animal"] = 600;
            animalLevelAdjustments["alignment animal"] = 600;
            animalLevelAdjustments["wrong animal"] = 90210;

            var animals = animalBaseRaceRandomizer.GetAllPossible(alignment, characterClass);
            Assert.That(animals, Contains.Item("animal"));
            Assert.That(animals, Contains.Item("other animal"));
            Assert.That(animals.Count(), Is.EqualTo(2));
        }
    }
}
