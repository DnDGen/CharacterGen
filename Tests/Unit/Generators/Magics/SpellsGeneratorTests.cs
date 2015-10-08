using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Generators.Domain.Magics;
using CharacterGen.Generators.Magics;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CharacterGen.Tests.Unit.Generators.Magics
{
    [TestFixture]
    public class SpellsGeneratorTests
    {
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private ISpellsGenerator spellsGenerator;
        private CharacterClass characterClass;
        private Dictionary<String, Stat> stats;
        private List<String> spellcasters;
        private Dictionary<String, Int32> spellQuantitiesForClass;

        [SetUp]
        public void Setup()
        {
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            spellsGenerator = new SpellsGenerator(mockCollectionsSelector.Object, mockAdjustmentsSelector.Object);
            characterClass = new CharacterClass();
            spellcasters = new List<String>();
            stats = new Dictionary<String, Stat>();
            spellQuantitiesForClass = new Dictionary<String, Int32>();

            characterClass.ClassName = "class name";
            characterClass.Level = 9266;
            spellcasters.Add(characterClass.ClassName);
            spellcasters.Add("other class");
            spellQuantitiesForClass["0"] = 90210;
            spellQuantitiesForClass["1"] = 42;
            stats["stat"] = new Stat { Value = 11 };
            stats["other stat"] = new Stat { Value = 11 };

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.Spellcasters)).Returns(spellcasters);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.StatGroups, characterClass.ClassName + GroupConstants.Spellcasters)).Returns(new[] { "stat" });

            var tableName = String.Format(TableNameConstants.Formattable.Adjustments.LevelXCLASSSpellQuantities, characterClass.Level, characterClass.ClassName);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(tableName)).Returns(spellQuantitiesForClass);
        }

        [Test]
        public void DoNotGenerateSpellsIfNotASpellcaster()
        {
            spellcasters.Remove(characterClass.ClassName);
            var spellQuantities = spellsGenerator.GenerateFrom(characterClass, stats);
            Assert.That(spellQuantities, Is.Empty);
        }

        [Test]
        public void GenerateSpellQuantities()
        {
            var spellQuantities = spellsGenerator.GenerateFrom(characterClass, stats);
            Assert.That(spellQuantities[0], Is.EqualTo(90210));
            Assert.That(spellQuantities[1], Is.EqualTo(42));
            Assert.That(spellQuantities.Count, Is.EqualTo(2));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        [TestCase(10, 0)]
        [TestCase(11, 0, 0)]
        [TestCase(12, 0, 1, 0)]
        [TestCase(13, 0, 1, 0, 0)]
        [TestCase(14, 0, 1, 1, 0, 0)]
        [TestCase(15, 0, 1, 1, 0, 0, 0)]
        [TestCase(16, 0, 1, 1, 1, 0, 0, 0)]
        [TestCase(17, 0, 1, 1, 1, 0, 0, 0, 0)]
        [TestCase(18, 0, 1, 1, 1, 1, 0, 0, 0, 0)]
        [TestCase(19, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0)]
        [TestCase(20, 0, 2, 1, 1, 1, 1, 0, 0, 0, 0)]
        [TestCase(21, 0, 2, 1, 1, 1, 1, 0, 0, 0, 0)]
        [TestCase(22, 0, 2, 2, 1, 1, 1, 1, 0, 0, 0)]
        [TestCase(23, 0, 2, 2, 1, 1, 1, 1, 0, 0, 0)]
        [TestCase(24, 0, 2, 2, 2, 1, 1, 1, 1, 0, 0)]
        [TestCase(25, 0, 2, 2, 2, 1, 1, 1, 1, 0, 0)]
        [TestCase(26, 0, 2, 2, 2, 2, 1, 1, 1, 1, 0)]
        [TestCase(27, 0, 2, 2, 2, 2, 1, 1, 1, 1, 0)]
        [TestCase(28, 0, 3, 2, 2, 2, 2, 1, 1, 1, 1)]
        [TestCase(29, 0, 3, 2, 2, 2, 2, 1, 1, 1, 1)]
        [TestCase(30, 0, 3, 3, 2, 2, 2, 2, 1, 1, 1)]
        [TestCase(31, 0, 3, 3, 2, 2, 2, 2, 1, 1, 1)]
        [TestCase(32, 0, 3, 3, 3, 2, 2, 2, 2, 1, 1)]
        [TestCase(33, 0, 3, 3, 3, 2, 2, 2, 2, 1, 1)]
        [TestCase(34, 0, 3, 3, 3, 3, 2, 2, 2, 2, 1)]
        [TestCase(35, 0, 3, 3, 3, 3, 2, 2, 2, 2, 1)]
        [TestCase(36, 0, 4, 3, 3, 3, 3, 2, 2, 2, 2)]
        [TestCase(37, 0, 4, 3, 3, 3, 3, 2, 2, 2, 2)]
        [TestCase(38, 0, 4, 4, 3, 3, 3, 3, 2, 2, 2)]
        [TestCase(39, 0, 4, 4, 3, 3, 3, 3, 2, 2, 2)]
        [TestCase(40, 0, 4, 4, 4, 3, 3, 3, 3, 2, 2)]
        [TestCase(41, 0, 4, 4, 4, 3, 3, 3, 3, 2, 2)]
        [TestCase(42, 0, 4, 4, 4, 4, 3, 3, 3, 3, 2)]
        [TestCase(43, 0, 4, 4, 4, 4, 3, 3, 3, 3, 2)]
        [TestCase(44, 0, 5, 4, 4, 4, 4, 3, 3, 3, 3)]
        [TestCase(45, 0, 5, 4, 4, 4, 4, 3, 3, 3, 3)]
        public void AddBonusSpellsForStat(Int32 statValue, params Int32[] levelBonuses)
        {
            stats["stat"].Value = statValue;
            spellQuantitiesForClass["0"] = 10;
            spellQuantitiesForClass["1"] = 9;
            spellQuantitiesForClass["2"] = 8;
            spellQuantitiesForClass["3"] = 7;
            spellQuantitiesForClass["4"] = 6;
            spellQuantitiesForClass["5"] = 5;
            spellQuantitiesForClass["6"] = 4;
            spellQuantitiesForClass["7"] = 3;
            spellQuantitiesForClass["8"] = 2;
            spellQuantitiesForClass["9"] = 1;

            var spellQuantities = spellsGenerator.GenerateFrom(characterClass, stats);

            for (var spellLevel = 0; spellLevel < levelBonuses.Length; spellLevel++)
            {
                var expectedQuantity = (10 - spellLevel) + levelBonuses[spellLevel];
                Assert.That(spellQuantities[spellLevel], Is.EqualTo(expectedQuantity), spellLevel.ToString());
            }

            Assert.That(spellQuantities.Count, Is.EqualTo(levelBonuses.Length));
        }

        [Test]
        public void CannotGetBonusSpellsInLevelThatCharacterCannotCast()
        {
            stats["stat"].Value = 45;

            var spellQuantities = spellsGenerator.GenerateFrom(characterClass, stats);
            Assert.That(spellQuantities[0], Is.EqualTo(90210));
            Assert.That(spellQuantities[1], Is.EqualTo(47));
            Assert.That(spellQuantities.Count, Is.EqualTo(2));
        }

        [Test]
        public void CanGetBonusSpellsInLevelWithQuantityOf0()
        {
            stats["stat"].Value = 45;
            spellQuantitiesForClass["2"] = 0;

            var spellQuantities = spellsGenerator.GenerateFrom(characterClass, stats);
            Assert.That(spellQuantities[0], Is.EqualTo(90210));
            Assert.That(spellQuantities[1], Is.EqualTo(47));
            Assert.That(spellQuantities[2], Is.EqualTo(4));
            Assert.That(spellQuantities.Count, Is.EqualTo(3));
        }
    }
}
