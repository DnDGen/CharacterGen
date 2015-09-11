using CharacterGen.Common.Abilities;
using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Combats;
using CharacterGen.Common.Items;
using CharacterGen.Common.Races;
using CharacterGen.Generators;
using CharacterGen.Generators.Abilities;
using CharacterGen.Generators.Combats;
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
using System.Linq;

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
        private Mock<IAbilitiesGenerator> mockAnimalAbilitiesGenerator;
        private Mock<ISetStatsRandomizer> mockSetStatsRandomizer;
        private Mock<ICombatGenerator> mockAnimalCombatGenerator;
        private IAnimalGenerator animalGenerator;
        private CharacterClass characterClass;
        private List<Feat> feats;
        private List<String> animals;
        private Race race;
        private Alignment alignment;
        private Dictionary<String, Int32> levelAdjustments;
        private Ability ability;
        private Combat combat;
        private BaseAttack baseAttack;
        private Dictionary<String, Int32> tricks;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            mockRaceGenerator = new Mock<IRaceGenerator>();
            mockAnimalBaseRaceRandomizer = new Mock<IBaseRaceRandomizer>();
            mockNoMetaraceRandomizer = new Mock<IMetaraceRandomizer>();
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockAnimalAbilitiesGenerator = new Mock<IAbilitiesGenerator>();
            mockSetStatsRandomizer = new Mock<ISetStatsRandomizer>();
            mockAnimalCombatGenerator = new Mock<ICombatGenerator>();
            animalGenerator = new AnimalGenerator();
            characterClass = new CharacterClass();
            feats = new List<Feat>();
            animals = new List<String>();
            race = new Race();
            alignment = new Alignment();
            levelAdjustments = new Dictionary<String, Int32>();
            ability = new Ability();
            combat = new Combat();
            baseAttack = new BaseAttack();

            characterClass.Level = 9266;
            characterClass.ClassName = "class name";
            race.BaseRace = "animal";
            animals.Add(race.BaseRace);
            levelAdjustments[race.BaseRace] = 0;
            tricks[characterClass.ClassName] = 42;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.Animals, characterClass.ClassName)).Returns(animals);
            mockRaceGenerator.Setup(g => g.GenerateWith(alignment, characterClass, mockAnimalBaseRaceRandomizer.Object, mockNoMetaraceRandomizer.Object)).Returns(race);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments)).Returns(levelAdjustments);

            var tableName = String.Format(TableNameConstants.Formattable.Adjustments.LevelXAnimalTricks, 9266);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(tableName)).Returns(tricks);
            mockAnimalCombatGenerator.Setup(g => g.GenerateBaseAttackWith(characterClass, race)).Returns(baseAttack);
            mockAnimalCombatGenerator.Setup(g => g.GenerateWith(baseAttack, characterClass, race, ability.Feats, ability.Stats, It.IsAny<Equipment>())).Returns(combat);
            mockAnimalAbilitiesGenerator.Setup(g => g.GenerateWith(characterClass, race, mockSetStatsRandomizer.Object, baseAttack)).Returns(ability);
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
            var animal = animalGenerator.GenerateFrom(characterClass, feats);
            Assert.That(animal.Ability, Is.EqualTo(ability));
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
        public void GenerateAnimalCombat()
        {
            var animal = animalGenerator.GenerateFrom(characterClass, feats);
            Assert.That(animal.Combat, Is.EqualTo(combat));
        }

        [Test]
        public void UseEmptyEquipmentToGenerateAnimalCombat()
        {
            var animal = animalGenerator.GenerateFrom(characterClass, feats);
            mockAnimalCombatGenerator.Verify(g => g.GenerateWith(baseAttack, characterClass, race, ability.Feats, ability.Stats, It.IsAny<Equipment>()), Times.Once);
            mockAnimalCombatGenerator.Verify(g => g.GenerateWith(baseAttack, characterClass, race, ability.Feats, ability.Stats, It.Is<Equipment>(e => e.Armor == null)), Times.Once);
            mockAnimalCombatGenerator.Verify(g => g.GenerateWith(baseAttack, characterClass, race, ability.Feats, ability.Stats, It.Is<Equipment>(e => e.OffHand == null)), Times.Once);
            mockAnimalCombatGenerator.Verify(g => g.GenerateWith(baseAttack, characterClass, race, ability.Feats, ability.Stats, It.Is<Equipment>(e => e.PrimaryHand == null)), Times.Once);
            mockAnimalCombatGenerator.Verify(g => g.GenerateWith(baseAttack, characterClass, race, ability.Feats, ability.Stats, It.Is<Equipment>(e => e.Treasure.Coin.Quantity == 0)), Times.Once);
            mockAnimalCombatGenerator.Verify(g => g.GenerateWith(baseAttack, characterClass, race, ability.Feats, ability.Stats, It.Is<Equipment>(e => e.Treasure.Goods.Any() == false)), Times.Once);
            mockAnimalCombatGenerator.Verify(g => g.GenerateWith(baseAttack, characterClass, race, ability.Feats, ability.Stats, It.Is<Equipment>(e => e.Treasure.Items.Any() == false)), Times.Once);
        }

        [Test]
        public void GenerateAnimalTricks()
        {
            var animal = animalGenerator.GenerateFrom(characterClass, feats);
            Assert.That(animal.Tricks, Is.EqualTo(42));
        }
    }
}
