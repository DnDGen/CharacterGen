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
using System.Linq;

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
        private Dictionary<String, Int32> spellsPerDayForClass;

        [SetUp]
        public void Setup()
        {
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            spellsGenerator = new SpellsGenerator(mockCollectionsSelector.Object, mockAdjustmentsSelector.Object);
            characterClass = new CharacterClass();
            spellcasters = new List<String>();
            stats = new Dictionary<String, Stat>();
            spellsPerDayForClass = new Dictionary<String, Int32>();

            characterClass.ClassName = "class name";
            characterClass.Level = 9266;
            spellcasters.Add(characterClass.ClassName);
            spellcasters.Add("other class");
            spellsPerDayForClass["0"] = 90210;
            spellsPerDayForClass["1"] = 42;
            stats["stat"] = new Stat { Value = 11 };
            stats["other stat"] = new Stat { Value = 11 };

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.Spellcasters)).Returns(spellcasters);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.StatGroups, characterClass.ClassName + GroupConstants.Spellcasters)).Returns(new[] { "stat" });

            var tableName = String.Format(TableNameConstants.Formattable.Adjustments.LevelXCLASSSpellsPerDay, characterClass.Level, characterClass.ClassName);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(tableName)).Returns(spellsPerDayForClass);
        }

        [Test]
        public void DoNotGenerateSpellsIfNotASpellcaster()
        {
            spellcasters.Remove(characterClass.ClassName);
            var spellsPerDay = spellsGenerator.GenerateFrom(characterClass, stats);
            Assert.That(spellsPerDay, Is.Empty);
        }

        [Test]
        public void GenerateSpellQuantities()
        {
            var spellsPerDay = spellsGenerator.GenerateFrom(characterClass, stats);

            var cantrips = spellsPerDay.First(s => s.Level == 0);
            Assert.That(cantrips.Quantity, Is.EqualTo(90210));

            var firstLevelSpells = spellsPerDay.First(s => s.Level == 1);
            Assert.That(firstLevelSpells.Quantity, Is.EqualTo(42));

            Assert.That(spellsPerDay.Count, Is.EqualTo(2));
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
            spellsPerDayForClass["0"] = 10;
            spellsPerDayForClass["1"] = 9;
            spellsPerDayForClass["2"] = 8;
            spellsPerDayForClass["3"] = 7;
            spellsPerDayForClass["4"] = 6;
            spellsPerDayForClass["5"] = 5;
            spellsPerDayForClass["6"] = 4;
            spellsPerDayForClass["7"] = 3;
            spellsPerDayForClass["8"] = 2;
            spellsPerDayForClass["9"] = 1;

            var spellsPerDay = spellsGenerator.GenerateFrom(characterClass, stats);

            for (var spellLevel = 0; spellLevel < levelBonuses.Length; spellLevel++)
            {
                var expectedQuantity = (10 - spellLevel) + levelBonuses[spellLevel];
                var spells = spellsPerDay.First(s => s.Level == spellLevel);
                Assert.That(spells.Quantity, Is.EqualTo(expectedQuantity), spellLevel.ToString());
            }

            Assert.That(spellsPerDay.Count, Is.EqualTo(levelBonuses.Length));
        }

        [Test]
        public void CannotGetBonusSpellsInLevelThatCharacterCannotCast()
        {
            stats["stat"].Value = 45;

            var spellsPerDay = spellsGenerator.GenerateFrom(characterClass, stats);

            var cantrips = spellsPerDay.First(s => s.Level == 0);
            Assert.That(cantrips.Quantity, Is.EqualTo(90210));

            var firstLevelSpells = spellsPerDay.First(s => s.Level == 1);
            Assert.That(firstLevelSpells.Quantity, Is.EqualTo(47));

            Assert.That(spellsPerDay.Count, Is.EqualTo(2));
        }

        [Test]
        public void CanGetBonusSpellsInLevelWithQuantityOf0()
        {
            stats["stat"].Value = 45;
            spellsPerDayForClass["2"] = 0;

            var spellsPerDay = spellsGenerator.GenerateFrom(characterClass, stats);

            var cantrips = spellsPerDay.First(s => s.Level == 0);
            Assert.That(cantrips.Quantity, Is.EqualTo(90210));

            var firstLevelSpells = spellsPerDay.First(s => s.Level == 1);
            Assert.That(firstLevelSpells.Quantity, Is.EqualTo(47));

            var secondLevelSpells = spellsPerDay.First(s => s.Level == 2);
            Assert.That(secondLevelSpells.Quantity, Is.EqualTo(4));

            Assert.That(spellsPerDay.Count, Is.EqualTo(3));
        }

        [Test]
        public void IfTotalSpellsPerDayIs0AndDoesNotHaveDomainSpell_RemoveSpellLevel()
        {
            spellsPerDayForClass["1"] = 0;

            var spellsPerDay = spellsGenerator.GenerateFrom(characterClass, stats);
            var cantrips = spellsPerDay.First(s => s.Level == 0);

            Assert.That(cantrips.Quantity, Is.EqualTo(90210));
            Assert.That(spellsPerDay.Count, Is.EqualTo(1));
        }

        [Test]
        public void IfTotalSpellsPerDayIs0AndHasDomainSpell_DoNotRemoveSpellLevel()
        {
            spellsPerDayForClass["1"] = 0;
            characterClass.SpecialistFields = new[] { "specialist" };

            var spellsPerDay = spellsGenerator.GenerateFrom(characterClass, stats);

            var cantrips = spellsPerDay.First(s => s.Level == 0);
            Assert.That(cantrips.Quantity, Is.EqualTo(90210));
            Assert.That(cantrips.HasDomainSpell, Is.False);

            var firstLevelSpells = spellsPerDay.First(s => s.Level == 1);
            Assert.That(firstLevelSpells.Quantity, Is.EqualTo(0));
            Assert.That(firstLevelSpells.HasDomainSpell, Is.True);

            Assert.That(spellsPerDay.Count, Is.EqualTo(2));
        }

        [Test]
        public void IfTotalSpellsPerDayIsGreatThan0AndDoesNotHaveDomainSpell_DoNotRemoveSpellLevel()
        {
            spellsPerDayForClass["1"] = 1;

            var spellsPerDay = spellsGenerator.GenerateFrom(characterClass, stats);

            var cantrips = spellsPerDay.First(s => s.Level == 0);
            Assert.That(cantrips.Quantity, Is.EqualTo(90210));
            Assert.That(cantrips.HasDomainSpell, Is.False);

            var firstLevelSpells = spellsPerDay.First(s => s.Level == 1);
            Assert.That(firstLevelSpells.Quantity, Is.EqualTo(1));
            Assert.That(firstLevelSpells.HasDomainSpell, Is.False);

            Assert.That(spellsPerDay.Count, Is.EqualTo(2));
        }

        [Test]
        public void AllSpellLevelsExcept0GetDomainSpellIfClassHasSpecialistFields()
        {
            characterClass.SpecialistFields = new[] { "specialist" };
            spellsPerDayForClass["0"] = 10;
            spellsPerDayForClass["1"] = 9;
            spellsPerDayForClass["2"] = 8;
            spellsPerDayForClass["3"] = 7;
            spellsPerDayForClass["4"] = 6;
            spellsPerDayForClass["5"] = 5;
            spellsPerDayForClass["6"] = 4;
            spellsPerDayForClass["7"] = 3;
            spellsPerDayForClass["8"] = 2;
            spellsPerDayForClass["9"] = 1;

            var spellsPerDay = spellsGenerator.GenerateFrom(characterClass, stats);

            var cantrips = spellsPerDay.First(s => s.Level == 0);
            Assert.That(cantrips.HasDomainSpell, Is.False);

            var nonCantrips = spellsPerDay.Except(new[] { cantrips });
            foreach (var nonCantrip in nonCantrips)
                Assert.That(nonCantrip.HasDomainSpell, Is.True);
        }

        [Test]
        public void AllSpellLevelsExcept0GetDomainSpellIfClassHasMultipleSpecialistFields()
        {
            characterClass.SpecialistFields = new[] { "specialist", "also specialist" };
            spellsPerDayForClass["0"] = 10;
            spellsPerDayForClass["1"] = 9;
            spellsPerDayForClass["2"] = 8;
            spellsPerDayForClass["3"] = 7;
            spellsPerDayForClass["4"] = 6;
            spellsPerDayForClass["5"] = 5;
            spellsPerDayForClass["6"] = 4;
            spellsPerDayForClass["7"] = 3;
            spellsPerDayForClass["8"] = 2;
            spellsPerDayForClass["9"] = 1;

            var spellsPerDay = spellsGenerator.GenerateFrom(characterClass, stats);

            var cantrips = spellsPerDay.First(s => s.Level == 0);
            Assert.That(cantrips.HasDomainSpell, Is.False);

            var nonCantrips = spellsPerDay.Except(new[] { cantrips });
            foreach (var nonCantrip in nonCantrips)
                Assert.That(nonCantrip.HasDomainSpell, Is.True, nonCantrip.Level.ToString());
        }

        [Test]
        public void NoSpellLevelsGetDomainSpellIfClassDoesNotHaveSpecialistFields()
        {
            spellsPerDayForClass["0"] = 10;
            spellsPerDayForClass["1"] = 9;
            spellsPerDayForClass["2"] = 8;
            spellsPerDayForClass["3"] = 7;
            spellsPerDayForClass["4"] = 6;
            spellsPerDayForClass["5"] = 5;
            spellsPerDayForClass["6"] = 4;
            spellsPerDayForClass["7"] = 3;
            spellsPerDayForClass["8"] = 2;
            spellsPerDayForClass["9"] = 1;

            var spellsPerDay = spellsGenerator.GenerateFrom(characterClass, stats);

            foreach (var spells in spellsPerDay)
                Assert.That(spells.HasDomainSpell, Is.False, spells.Level.ToString());
        }
    }
}
