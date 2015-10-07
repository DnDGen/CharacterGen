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
        private Mock<RaceRandomizer> mockAnimalBaseRaceRandomizer;
        private Mock<RaceRandomizer> mockNoMetaraceRandomizer;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private Mock<IAbilitiesGenerator> mockAnimalAbilitiesGenerator;
        private Mock<ISetStatsRandomizer> mockSetStatsRandomizer;
        private Mock<ICombatGenerator> mockAnimalCombatGenerator;
        private IAnimalGenerator animalGenerator;
        private CharacterClass characterClass;
        private List<Feat> feats;
        private List<String> animals;
        private List<String> druidAnimals;
        private Race characterRace;
        private Race animalRace;
        private Alignment alignment;
        private Dictionary<String, Int32> levelAdjustments;
        private Ability ability;
        private Combat combat;
        private BaseAttack baseAttack;
        private Dictionary<String, Int32> tricks;
        private List<String> animalsForSize;
        private List<String> improvedFamiliars;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            mockRaceGenerator = new Mock<IRaceGenerator>();
            mockAnimalBaseRaceRandomizer = new Mock<RaceRandomizer>();
            mockNoMetaraceRandomizer = new Mock<RaceRandomizer>();
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockAnimalAbilitiesGenerator = new Mock<IAbilitiesGenerator>();
            mockSetStatsRandomizer = new Mock<ISetStatsRandomizer>();
            mockAnimalCombatGenerator = new Mock<ICombatGenerator>();
            animalGenerator = new AnimalGenerator(mockCollectionsSelector.Object, mockRaceGenerator.Object, mockAnimalBaseRaceRandomizer.Object, mockNoMetaraceRandomizer.Object,
                mockAdjustmentsSelector.Object, mockAnimalAbilitiesGenerator.Object, mockSetStatsRandomizer.Object, mockAnimalCombatGenerator.Object);

            characterClass = new CharacterClass();
            characterRace = new Race();
            feats = new List<Feat>();
            animals = new List<String>();
            animalRace = new Race();
            alignment = new Alignment();
            levelAdjustments = new Dictionary<String, Int32>();
            ability = new Ability();
            combat = new Combat();
            baseAttack = new BaseAttack();
            tricks = new Dictionary<String, Int32>();
            animalsForSize = new List<String>();
            improvedFamiliars = new List<String>();
            druidAnimals = new List<String>();

            characterRace.BaseRace = "character race";
            characterRace.Size = "character size";
            characterClass.Level = 9266;
            characterClass.ClassName = "class name";
            animalRace.BaseRace = "animal";
            animals.Add(animalRace.BaseRace);
            animalsForSize.Add(animalRace.BaseRace);
            levelAdjustments[animalRace.BaseRace] = 0;
            tricks[characterClass.ClassName] = 42;

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<String>>())).Returns((IEnumerable<String> c) => c.Last());

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.Animals, characterClass.ClassName)).Returns(animals);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.Animals, CharacterClassConstants.Druid)).Returns(animals);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.Animals, characterRace.Size)).Returns(animalsForSize);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.Animals, FeatConstants.ImprovedFamiliar)).Returns(improvedFamiliars);
            mockRaceGenerator.Setup(g => g.GenerateWith(alignment, characterClass, mockAnimalBaseRaceRandomizer.Object, mockNoMetaraceRandomizer.Object)).Returns(animalRace);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments)).Returns(levelAdjustments);

            var tableName = String.Format(TableNameConstants.Formattable.Adjustments.LevelXAnimalTricks, 9266);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(tableName)).Returns(tricks);
            mockAnimalCombatGenerator.Setup(g => g.GenerateBaseAttackWith(characterClass, animalRace)).Returns(baseAttack);
            mockAnimalCombatGenerator.Setup(g => g.GenerateWith(baseAttack, characterClass, animalRace, ability.Feats, ability.Stats, It.IsAny<Equipment>())).Returns(combat);
            mockAnimalAbilitiesGenerator.Setup(g => g.GenerateWith(characterClass, animalRace, mockSetStatsRandomizer.Object, baseAttack)).Returns(ability);
        }

        [Test]
        public void GenerateNoAnimalIfNoneAvailable()
        {
            animals.Clear();
            var animal = animalGenerator.GenerateFrom(alignment, characterClass, characterRace, feats);
            Assert.That(animal, Is.Empty);
        }

        [Test]
        public void GenerateNoAnimalIfNoneAvailableWithinSize()
        {
            animals.Clear();
            animals.Add("other animal");
            levelAdjustments["other animal"] = 0;

            var animal = animalGenerator.GenerateFrom(alignment, characterClass, characterRace, feats);
            Assert.That(animal, Is.Empty);
        }

        [Test]
        public void GenerateNoAnimalIfNoneMatchSize()
        {
            animalsForSize.Clear();
            animalsForSize.Add("other animal");

            var animal = animalGenerator.GenerateFrom(alignment, characterClass, characterRace, feats);
            Assert.That(animal, Is.Empty);
        }

        [Test]
        public void GenerateNoAnimalIfNoneFitWithinLevel()
        {
            levelAdjustments[animalRace.BaseRace] = characterClass.Level * -1;
            var animal = animalGenerator.GenerateFrom(alignment, characterClass, characterRace, feats);
            Assert.That(animal, Is.Empty);
        }

        [Test]
        public void GenerateAnimal()
        {
            var animal = animalGenerator.GenerateFrom(alignment, characterClass, characterRace, feats);
            Assert.That(animal, Is.EqualTo("animal"));
        }

        //[Test]
        //public void GenerateAnimalRace()
        //{
        //    var animal = animalGenerator.GenerateFrom(alignment, characterClass, characterRace, feats);
        //    Assert.That(animal.Race, Is.EqualTo(animalRace));
        //}

        [Test]
        public void GenerateImprovedFamiliar()
        {
            var wrongRace = new Race { BaseRace = "other animal" };
            animals.Add(wrongRace.BaseRace);
            levelAdjustments[wrongRace.BaseRace] = 0;
            improvedFamiliars.Add(animalRace.BaseRace);

            feats.Add(new Feat { Name = FeatConstants.ImprovedFamiliar });

            mockRaceGenerator.SetupSequence(g => g.GenerateWith(alignment, characterClass, mockAnimalBaseRaceRandomizer.Object,
                mockNoMetaraceRandomizer.Object)).Returns(animalRace).Returns(wrongRace);

            var animal = animalGenerator.GenerateFrom(alignment, characterClass, characterRace, feats);
            Assert.That(animal, Is.EqualTo("animal"));
        }

        [Test]
        public void DoNotGenerateImprovedFamiliarBecauseFamiliarIsNotImproved()
        {
            var wrongRace = new Race { BaseRace = "other animal" };
            animals.Add(wrongRace.BaseRace);
            improvedFamiliars.Add(wrongRace.BaseRace);
            levelAdjustments[wrongRace.BaseRace] = 0;

            feats.Add(new Feat { Name = FeatConstants.ImprovedFamiliar });

            mockRaceGenerator.SetupSequence(g => g.GenerateWith(alignment, characterClass, mockAnimalBaseRaceRandomizer.Object,
                mockNoMetaraceRandomizer.Object)).Returns(animalRace).Returns(wrongRace);

            var animal = animalGenerator.GenerateFrom(alignment, characterClass, characterRace, feats);
            Assert.That(animal, Is.EqualTo("animal"));
        }

        [Test]
        public void DoNotGenerateImprovedFamiliarBecauseCharacterDoesNotHaveImprovedFamiliarFeat()
        {
            var wrongRace = new Race { BaseRace = "other animal" };
            animals.Add(wrongRace.BaseRace);
            improvedFamiliars.Add(wrongRace.BaseRace);
            levelAdjustments[wrongRace.BaseRace] = 0;

            mockRaceGenerator.SetupSequence(g => g.GenerateWith(alignment, characterClass, mockAnimalBaseRaceRandomizer.Object,
                mockNoMetaraceRandomizer.Object)).Returns(wrongRace).Returns(animalRace);

            var animal = animalGenerator.GenerateFrom(alignment, characterClass, characterRace, feats);
            Assert.That(animal, Is.EqualTo("animal"));
        }

        [Test]
        public void RangersUseHalfTheirLevelAsDruidLevel()
        {
            characterClass.ClassName = CharacterClassConstants.Ranger;

            var wrongRace = new Race { BaseRace = "other animal" };
            druidAnimals.Add(wrongRace.BaseRace);
            druidAnimals.Add(animalRace.BaseRace);
            levelAdjustments[wrongRace.BaseRace] = characterClass.Level / -2 - 1;

            var rangerAnimalBaseAttack = new BaseAttack();
            var rangerAnimalCombat = new Combat();
            var rangerAnimalAbility = new Ability();

            mockRaceGenerator.Setup(g => g.GenerateWith(alignment, It.Is<CharacterClass>(c => c.ClassName == CharacterClassConstants.Druid && c.Level == characterClass.Level / 2), mockAnimalBaseRaceRandomizer.Object,
                mockNoMetaraceRandomizer.Object)).Returns(animalRace);

            mockAnimalCombatGenerator.Setup(g => g.GenerateBaseAttackWith(It.Is<CharacterClass>(c => c.ClassName == CharacterClassConstants.Druid && c.Level == characterClass.Level / 2), animalRace)).Returns(rangerAnimalBaseAttack);
            mockAnimalCombatGenerator.Setup(g => g.GenerateWith(rangerAnimalBaseAttack, It.Is<CharacterClass>(c => c.ClassName == CharacterClassConstants.Druid && c.Level == characterClass.Level / 2), animalRace, rangerAnimalAbility.Feats, rangerAnimalAbility.Stats, It.IsAny<Equipment>())).Returns(rangerAnimalCombat);
            mockAnimalAbilitiesGenerator.Setup(g => g.GenerateWith(It.Is<CharacterClass>(c => c.ClassName == CharacterClassConstants.Druid && c.Level == characterClass.Level / 2), animalRace, mockSetStatsRandomizer.Object, rangerAnimalBaseAttack)).Returns(rangerAnimalAbility);

            mockRaceGenerator.SetupSequence(g => g.GenerateWith(alignment, It.Is<CharacterClass>(c => c.ClassName == CharacterClassConstants.Druid && c.Level == characterClass.Level / 2), mockAnimalBaseRaceRandomizer.Object,
                mockNoMetaraceRandomizer.Object)).Returns(wrongRace).Returns(animalRace);

            var rangerTricks = new Dictionary<String, Int32>();
            rangerTricks[CharacterClassConstants.Druid] = 600;

            var tableName = String.Format(TableNameConstants.Formattable.Adjustments.LevelXAnimalTricks, 9266 / 2);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(tableName)).Returns(rangerTricks);

            var animal = animalGenerator.GenerateFrom(alignment, characterClass, characterRace, feats);
            Assert.That(animal, Is.EqualTo("animal"));
        }

        [Test]
        public void OriginalClassNotModified()
        {
            characterClass.ClassName = CharacterClassConstants.Ranger;

            var wrongRace = new Race { BaseRace = "other animal" };
            druidAnimals.Add(wrongRace.BaseRace);
            druidAnimals.Add(animalRace.BaseRace);
            levelAdjustments[wrongRace.BaseRace] = characterClass.Level / -2 - 1;

            var rangerAnimalBaseAttack = new BaseAttack();
            var rangerAnimalCombat = new Combat();
            var rangerAnimalAbility = new Ability();

            mockRaceGenerator.Setup(g => g.GenerateWith(alignment, It.Is<CharacterClass>(c => c.ClassName == CharacterClassConstants.Druid && c.Level == characterClass.Level / 2), mockAnimalBaseRaceRandomizer.Object,
                mockNoMetaraceRandomizer.Object)).Returns(animalRace);

            mockAnimalCombatGenerator.Setup(g => g.GenerateBaseAttackWith(It.Is<CharacterClass>(c => c.ClassName == CharacterClassConstants.Druid && c.Level == characterClass.Level / 2), animalRace)).Returns(rangerAnimalBaseAttack);
            mockAnimalCombatGenerator.Setup(g => g.GenerateWith(rangerAnimalBaseAttack, It.Is<CharacterClass>(c => c.ClassName == CharacterClassConstants.Druid && c.Level == characterClass.Level / 2), animalRace, rangerAnimalAbility.Feats, rangerAnimalAbility.Stats, It.IsAny<Equipment>())).Returns(rangerAnimalCombat);
            mockAnimalAbilitiesGenerator.Setup(g => g.GenerateWith(It.Is<CharacterClass>(c => c.ClassName == CharacterClassConstants.Druid && c.Level == characterClass.Level / 2), animalRace, mockSetStatsRandomizer.Object, rangerAnimalBaseAttack)).Returns(rangerAnimalAbility);

            mockRaceGenerator.SetupSequence(g => g.GenerateWith(alignment, It.Is<CharacterClass>(c => c.ClassName == CharacterClassConstants.Druid && c.Level == characterClass.Level / 2), mockAnimalBaseRaceRandomizer.Object,
                mockNoMetaraceRandomizer.Object)).Returns(wrongRace).Returns(animalRace);

            var rangerTricks = new Dictionary<String, Int32>();
            rangerTricks[CharacterClassConstants.Druid] = 600;

            var tableName = String.Format(TableNameConstants.Formattable.Adjustments.LevelXAnimalTricks, 9266 / 2);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(tableName)).Returns(rangerTricks);

            var animal = animalGenerator.GenerateFrom(alignment, characterClass, characterRace, feats);
            Assert.That(characterClass.ClassName, Is.EqualTo(CharacterClassConstants.Ranger));
            Assert.That(characterClass.Level, Is.EqualTo(9266));
        }

        //[Test]
        //public void GenerateAnimalAbilities()
        //{
        //    var animal = animalGenerator.GenerateFrom(alignment, characterClass, characterRace, feats);
        //    Assert.That(animal.Ability, Is.EqualTo(ability));
        //}

        //[Test]
        //public void GenerateRangerAnimalAbilities()
        //{
        //    characterClass.ClassName = CharacterClassConstants.Ranger;
        //    druidAnimals.Add(animalRace.BaseRace);

        //    var rangerAnimalBaseAttack = new BaseAttack();
        //    var rangerAnimalCombat = new Combat();
        //    var rangerAnimalAbility = new Ability();

        //    mockRaceGenerator.Setup(g => g.GenerateWith(alignment, It.Is<CharacterClass>(c => c.ClassName == CharacterClassConstants.Druid && c.Level == characterClass.Level / 2), mockAnimalBaseRaceRandomizer.Object,
        //        mockNoMetaraceRandomizer.Object)).Returns(animalRace);

        //    mockAnimalCombatGenerator.Setup(g => g.GenerateBaseAttackWith(It.Is<CharacterClass>(c => c.ClassName == CharacterClassConstants.Druid && c.Level == characterClass.Level / 2), animalRace)).Returns(rangerAnimalBaseAttack);
        //    mockAnimalCombatGenerator.Setup(g => g.GenerateWith(rangerAnimalBaseAttack, It.Is<CharacterClass>(c => c.ClassName == CharacterClassConstants.Druid && c.Level == characterClass.Level / 2), animalRace, rangerAnimalAbility.Feats, rangerAnimalAbility.Stats, It.IsAny<Equipment>())).Returns(rangerAnimalCombat);
        //    mockAnimalAbilitiesGenerator.Setup(g => g.GenerateWith(It.Is<CharacterClass>(c => c.ClassName == CharacterClassConstants.Druid && c.Level == characterClass.Level / 2), animalRace, mockSetStatsRandomizer.Object, rangerAnimalBaseAttack)).Returns(rangerAnimalAbility);

        //    var rangerTricks = new Dictionary<String, Int32>();
        //    rangerTricks[CharacterClassConstants.Druid] = 600;

        //    var tableName = String.Format(TableNameConstants.Formattable.Adjustments.LevelXAnimalTricks, 9266 / 2);
        //    mockAdjustmentsSelector.Setup(s => s.SelectFrom(tableName)).Returns(rangerTricks);

        //    var animal = animalGenerator.GenerateFrom(alignment, characterClass, characterRace, feats);
        //    Assert.That(animal.Ability, Is.EqualTo(rangerAnimalAbility));
        //}

        //[Test]
        //public void GenerateAnimalCombat()
        //{
        //    var animal = animalGenerator.GenerateFrom(alignment, characterClass, characterRace, feats);
        //    Assert.That(animal.Combat, Is.EqualTo(combat));
        //}

        //[Test]
        //public void GenerateRangerAnimalCombat()
        //{
        //    characterClass.ClassName = CharacterClassConstants.Ranger;
        //    druidAnimals.Add(animalRace.BaseRace);

        //    var rangerAnimalBaseAttack = new BaseAttack();
        //    var rangerAnimalCombat = new Combat();
        //    var rangerAnimalAbility = new Ability();

        //    mockRaceGenerator.Setup(g => g.GenerateWith(alignment, It.Is<CharacterClass>(c => c.ClassName == CharacterClassConstants.Druid && c.Level == characterClass.Level / 2), mockAnimalBaseRaceRandomizer.Object,
        //        mockNoMetaraceRandomizer.Object)).Returns(animalRace);

        //    mockAnimalCombatGenerator.Setup(g => g.GenerateBaseAttackWith(It.Is<CharacterClass>(c => c.ClassName == CharacterClassConstants.Druid && c.Level == characterClass.Level / 2), animalRace)).Returns(rangerAnimalBaseAttack);
        //    mockAnimalCombatGenerator.Setup(g => g.GenerateWith(rangerAnimalBaseAttack, It.Is<CharacterClass>(c => c.ClassName == CharacterClassConstants.Druid && c.Level == characterClass.Level / 2), animalRace, rangerAnimalAbility.Feats, rangerAnimalAbility.Stats, It.IsAny<Equipment>())).Returns(rangerAnimalCombat);
        //    mockAnimalAbilitiesGenerator.Setup(g => g.GenerateWith(It.Is<CharacterClass>(c => c.ClassName == CharacterClassConstants.Druid && c.Level == characterClass.Level / 2), animalRace, mockSetStatsRandomizer.Object, rangerAnimalBaseAttack)).Returns(rangerAnimalAbility);

        //    var rangerTricks = new Dictionary<String, Int32>();
        //    rangerTricks[CharacterClassConstants.Druid] = 600;

        //    var tableName = String.Format(TableNameConstants.Formattable.Adjustments.LevelXAnimalTricks, 9266 / 2);
        //    mockAdjustmentsSelector.Setup(s => s.SelectFrom(tableName)).Returns(rangerTricks);

        //    var animal = animalGenerator.GenerateFrom(alignment, characterClass, characterRace, feats);
        //    Assert.That(animal.Combat, Is.EqualTo(rangerAnimalCombat));
        //}

        //[Test]
        //public void UseEmptyEquipmentToGenerateAnimalCombat()
        //{
        //    var animal = animalGenerator.GenerateFrom(alignment, characterClass, characterRace, feats);
        //    mockAnimalCombatGenerator.Verify(g => g.GenerateWith(baseAttack, characterClass, animalRace, ability.Feats, ability.Stats, It.IsAny<Equipment>()), Times.Once);
        //    mockAnimalCombatGenerator.Verify(g => g.GenerateWith(baseAttack, characterClass, animalRace, ability.Feats, ability.Stats, It.Is<Equipment>(e => e.Armor == null)), Times.Once);
        //    mockAnimalCombatGenerator.Verify(g => g.GenerateWith(baseAttack, characterClass, animalRace, ability.Feats, ability.Stats, It.Is<Equipment>(e => e.OffHand == null)), Times.Once);
        //    mockAnimalCombatGenerator.Verify(g => g.GenerateWith(baseAttack, characterClass, animalRace, ability.Feats, ability.Stats, It.Is<Equipment>(e => e.PrimaryHand == null)), Times.Once);
        //    mockAnimalCombatGenerator.Verify(g => g.GenerateWith(baseAttack, characterClass, animalRace, ability.Feats, ability.Stats, It.Is<Equipment>(e => e.Treasure.Coin.Quantity == 0)), Times.Once);
        //    mockAnimalCombatGenerator.Verify(g => g.GenerateWith(baseAttack, characterClass, animalRace, ability.Feats, ability.Stats, It.Is<Equipment>(e => e.Treasure.Goods.Any() == false)), Times.Once);
        //    mockAnimalCombatGenerator.Verify(g => g.GenerateWith(baseAttack, characterClass, animalRace, ability.Feats, ability.Stats, It.Is<Equipment>(e => e.Treasure.Items.Any() == false)), Times.Once);
        //}

        //[Test]
        //public void UseEmptyEquipmentToGenerateRangerAnimalCombat()
        //{
        //    characterClass.ClassName = CharacterClassConstants.Ranger;
        //    druidAnimals.Add(animalRace.BaseRace);

        //    var rangerAnimalBaseAttack = new BaseAttack();
        //    var rangerAnimalCombat = new Combat();
        //    var rangerAnimalAbility = new Ability();

        //    mockRaceGenerator.Setup(g => g.GenerateWith(alignment, It.Is<CharacterClass>(c => c.ClassName == CharacterClassConstants.Druid && c.Level == characterClass.Level / 2), mockAnimalBaseRaceRandomizer.Object,
        //        mockNoMetaraceRandomizer.Object)).Returns(animalRace);

        //    mockAnimalCombatGenerator.Setup(g => g.GenerateBaseAttackWith(It.Is<CharacterClass>(c => c.ClassName == CharacterClassConstants.Druid && c.Level == characterClass.Level / 2), animalRace)).Returns(rangerAnimalBaseAttack);
        //    mockAnimalCombatGenerator.Setup(g => g.GenerateWith(rangerAnimalBaseAttack, It.Is<CharacterClass>(c => c.ClassName == CharacterClassConstants.Druid && c.Level == characterClass.Level / 2), animalRace, rangerAnimalAbility.Feats, rangerAnimalAbility.Stats, It.IsAny<Equipment>())).Returns(rangerAnimalCombat);
        //    mockAnimalAbilitiesGenerator.Setup(g => g.GenerateWith(It.Is<CharacterClass>(c => c.ClassName == CharacterClassConstants.Druid && c.Level == characterClass.Level / 2), animalRace, mockSetStatsRandomizer.Object, rangerAnimalBaseAttack)).Returns(rangerAnimalAbility);

        //    var rangerTricks = new Dictionary<String, Int32>();
        //    rangerTricks[CharacterClassConstants.Druid] = 600;

        //    var tableName = String.Format(TableNameConstants.Formattable.Adjustments.LevelXAnimalTricks, 9266 / 2);
        //    mockAdjustmentsSelector.Setup(s => s.SelectFrom(tableName)).Returns(rangerTricks);

        //    var animal = animalGenerator.GenerateFrom(alignment, characterClass, characterRace, feats);
        //    mockAnimalCombatGenerator.Verify(g => g.GenerateWith(rangerAnimalBaseAttack, It.Is<CharacterClass>(c => c.ClassName == CharacterClassConstants.Druid && c.Level == characterClass.Level / 2), animalRace, rangerAnimalAbility.Feats, rangerAnimalAbility.Stats, It.IsAny<Equipment>()), Times.Once);
        //    mockAnimalCombatGenerator.Verify(g => g.GenerateWith(rangerAnimalBaseAttack, It.Is<CharacterClass>(c => c.ClassName == CharacterClassConstants.Druid && c.Level == characterClass.Level / 2), animalRace, rangerAnimalAbility.Feats, rangerAnimalAbility.Stats, It.Is<Equipment>(e => e.Armor == null)), Times.Once);
        //    mockAnimalCombatGenerator.Verify(g => g.GenerateWith(rangerAnimalBaseAttack, It.Is<CharacterClass>(c => c.ClassName == CharacterClassConstants.Druid && c.Level == characterClass.Level / 2), animalRace, rangerAnimalAbility.Feats, rangerAnimalAbility.Stats, It.Is<Equipment>(e => e.OffHand == null)), Times.Once);
        //    mockAnimalCombatGenerator.Verify(g => g.GenerateWith(rangerAnimalBaseAttack, It.Is<CharacterClass>(c => c.ClassName == CharacterClassConstants.Druid && c.Level == characterClass.Level / 2), animalRace, rangerAnimalAbility.Feats, rangerAnimalAbility.Stats, It.Is<Equipment>(e => e.PrimaryHand == null)), Times.Once);
        //    mockAnimalCombatGenerator.Verify(g => g.GenerateWith(rangerAnimalBaseAttack, It.Is<CharacterClass>(c => c.ClassName == CharacterClassConstants.Druid && c.Level == characterClass.Level / 2), animalRace, rangerAnimalAbility.Feats, rangerAnimalAbility.Stats, It.Is<Equipment>(e => e.Treasure.Coin.Quantity == 0)), Times.Once);
        //    mockAnimalCombatGenerator.Verify(g => g.GenerateWith(rangerAnimalBaseAttack, It.Is<CharacterClass>(c => c.ClassName == CharacterClassConstants.Druid && c.Level == characterClass.Level / 2), animalRace, rangerAnimalAbility.Feats, rangerAnimalAbility.Stats, It.Is<Equipment>(e => e.Treasure.Goods.Any() == false)), Times.Once);
        //    mockAnimalCombatGenerator.Verify(g => g.GenerateWith(rangerAnimalBaseAttack, It.Is<CharacterClass>(c => c.ClassName == CharacterClassConstants.Druid && c.Level == characterClass.Level / 2), animalRace, rangerAnimalAbility.Feats, rangerAnimalAbility.Stats, It.Is<Equipment>(e => e.Treasure.Items.Any() == false)), Times.Once);
        //}

        //[Test]
        //public void GenerateAnimalTricks()
        //{
        //    var animal = animalGenerator.GenerateFrom(alignment, characterClass, characterRace, feats);
        //    Assert.That(animal.Tricks, Is.EqualTo(42));
        //}

        //[Test]
        //public void GenerateRangerAnimalTricks()
        //{
        //    characterClass.ClassName = CharacterClassConstants.Ranger;
        //    druidAnimals.Add(animalRace.BaseRace);

        //    var rangerAnimalBaseAttack = new BaseAttack();
        //    var rangerAnimalCombat = new Combat();
        //    var rangerAnimalAbility = new Ability();

        //    mockRaceGenerator.Setup(g => g.GenerateWith(alignment, It.Is<CharacterClass>(c => c.ClassName == CharacterClassConstants.Druid && c.Level == characterClass.Level / 2), mockAnimalBaseRaceRandomizer.Object,
        //        mockNoMetaraceRandomizer.Object)).Returns(animalRace);

        //    mockAnimalCombatGenerator.Setup(g => g.GenerateBaseAttackWith(It.Is<CharacterClass>(c => c.ClassName == CharacterClassConstants.Druid && c.Level == characterClass.Level / 2), animalRace)).Returns(rangerAnimalBaseAttack);
        //    mockAnimalCombatGenerator.Setup(g => g.GenerateWith(rangerAnimalBaseAttack, It.Is<CharacterClass>(c => c.ClassName == CharacterClassConstants.Druid && c.Level == characterClass.Level / 2), animalRace, rangerAnimalAbility.Feats, rangerAnimalAbility.Stats, It.IsAny<Equipment>())).Returns(rangerAnimalCombat);
        //    mockAnimalAbilitiesGenerator.Setup(g => g.GenerateWith(It.Is<CharacterClass>(c => c.ClassName == CharacterClassConstants.Druid && c.Level == characterClass.Level / 2), animalRace, mockSetStatsRandomizer.Object, rangerAnimalBaseAttack)).Returns(rangerAnimalAbility);

        //    var rangerTricks = new Dictionary<String, Int32>();
        //    rangerTricks[CharacterClassConstants.Druid] = 600;

        //    var tableName = String.Format(TableNameConstants.Formattable.Adjustments.LevelXAnimalTricks, 9266 / 2);
        //    mockAdjustmentsSelector.Setup(s => s.SelectFrom(tableName)).Returns(rangerTricks);

        //    var animal = animalGenerator.GenerateFrom(alignment, characterClass, characterRace, feats);
        //    Assert.That(animal.Tricks, Is.EqualTo(600));
        //}
    }
}
