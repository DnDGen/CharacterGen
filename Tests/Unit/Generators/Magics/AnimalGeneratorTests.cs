using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;
using CharacterGen.Generators;
using CharacterGen.Generators.Abilities;
using CharacterGen.Generators.Domain.Magics;
using CharacterGen.Generators.Magics;
using CharacterGen.Generators.Randomizers.Races;
using CharacterGen.Generators.Randomizers.Stats;
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
        private Mock<IStatsGenerator> mockAnimalStatsGenerator;
        private Mock<ISetStatsRandomizer> mockSetStatsRandomizer;
        private IAnimalGenerator animalGenerator;
        private CharacterClass characterClass;
        private List<Feat> feats;
        private List<String> animals;
        private Race race;
        private Alignment alignment;
        private Dictionary<String, Int32> levelAdjustments;
        private Dictionary<String, Stat> stats;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            mockRaceGenerator = new Mock<IRaceGenerator>();
            mockAnimalBaseRaceRandomizer = new Mock<IBaseRaceRandomizer>();
            mockNoMetaraceRandomizer = new Mock<IMetaraceRandomizer>();
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockAnimalStatsGenerator = new Mock<IStatsGenerator>();
            mockSetStatsRandomizer = new Mock<ISetStatsRandomizer>();
            animalGenerator = new AnimalGenerator();
            characterClass = new CharacterClass();
            feats = new List<Feat>();
            animals = new List<String>();
            race = new Race();
            alignment = new Alignment();
            levelAdjustments = new Dictionary<String, Int32>();
            stats = new Dictionary<String, Stat>();

            characterClass.Level = 9266;
            characterClass.ClassName = "class name";
            race.BaseRace = "animal";
            animals.Add(race.BaseRace);
            levelAdjustments[race.BaseRace] = 0;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.Animals, characterClass.ClassName)).Returns(animals);
            mockRaceGenerator.Setup(g => g.GenerateWith(alignment, characterClass, mockAnimalBaseRaceRandomizer.Object, mockNoMetaraceRandomizer.Object)).Returns(race);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments)).Returns(levelAdjustments);
            mockAnimalStatsGenerator.Setup(s => s.GenerateWith(mockSetStatsRandomizer.Object, characterClass, race)).Returns(stats);
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
        public void GenerateAnimalStats()
        {
            var animal = animalGenerator.GenerateFrom(characterClass, feats);
            Assert.That(animal.Ability.Stats, Is.EqualTo(stats));
        }

        [Test]
        public void StatsRandomizerIsSetTo10ForAnimalStats()
        {
            var animal = animalGenerator.GenerateFrom(characterClass, feats);
            var statsRandomizer = mockSetStatsRandomizer.Object;

            Assert.That(statsRandomizer.SetCharisma, Is.EqualTo(10));
            Assert.That(statsRandomizer.SetConstitution, Is.EqualTo(10));
            Assert.That(statsRandomizer.SetDexterity, Is.EqualTo(10));
            Assert.That(statsRandomizer.SetIntelligence, Is.EqualTo(10));
            Assert.That(statsRandomizer.SetStrength, Is.EqualTo(10));
            Assert.That(statsRandomizer.SetWisdom, Is.EqualTo(10));
        }

        [Test]
        public void GenerateAnimalLanguages()
        {
            //Languages - racial languages, if any.  Animal class
            //regular language generator

            throw new NotImplementedException();
        }

        [Test]
        public void GenerateNoAnimalLanguages()
        {
            //Languages - racial languages, if any.  Animal class
            //regular language generator

            throw new NotImplementedException();
        }

        [Test]
        public void GenerateAnimalSkills()
        {
            //Skills - set
            //Animal skills generator (because no class/points per level)

            throw new NotImplementedException();
        }

        [Test]
        public void GenerateAnimalFeats()
        {
            //Feats - racial, ones from character class, no additional
            //Animal feats generator (because no additional)

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
