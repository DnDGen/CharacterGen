using System;
using Moq;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Generators;
using NPCGen.Generators.Interfaces;
using NPCGen.Generators.Interfaces.Randomizers.CharacterClasses;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators
{
    [TestFixture]
    public class CharacterClassGeneratorTests
    {
        private Mock<ILevelRandomizer> mockLevelRandomizer;
        private Mock<IClassNameRandomizer> mockClassNameRandomizer;
        private ICharacterClassGenerator characterClassGenerator;

        private Alignment alignment;

        [SetUp]
        public void Setup()
        {
            characterClassGenerator = new CharacterClassGenerator();
            alignment = new Alignment();
            mockLevelRandomizer = new Mock<ILevelRandomizer>();
            mockClassNameRandomizer = new Mock<IClassNameRandomizer>();
        }

        [Test]
        public void GeneratorReturnsRandomizedLevel()
        {
            mockLevelRandomizer.Setup(r => r.Randomize()).Returns(9266);

            var characterClass = characterClassGenerator.GenerateWith(alignment, mockLevelRandomizer.Object, mockClassNameRandomizer.Object);
            Assert.That(characterClass.Level, Is.EqualTo(9266));
        }

        [Test]
        public void GeneratorReturnsRandomizedClass()
        {
            mockClassNameRandomizer.Setup(r => r.Randomize(alignment)).Returns(CharacterClassConstants.Barbarian);

            var characterClass = characterClassGenerator.GenerateWith(alignment, mockLevelRandomizer.Object, mockClassNameRandomizer.Object);
            Assert.That(characterClass.ClassName, Is.EqualTo(CharacterClassConstants.Barbarian));
        }

        //[TestCase(CharacterClassConstants.Fighter, 20)]
        //[TestCase(CharacterClassConstants.Paladin, 20)]
        //[TestCase(CharacterClassConstants.Ranger, 20)]
        //[TestCase(CharacterClassConstants.Barbarian, 20)]
        //[TestCase(CharacterClassConstants.Bard, 15)]
        //[TestCase(CharacterClassConstants.Cleric, 15)]
        //[TestCase(CharacterClassConstants.Monk, 15)]
        //[TestCase(CharacterClassConstants.Rogue, 15)]
        //[TestCase(CharacterClassConstants.Druid, 15)]
        //[TestCase(CharacterClassConstants.Sorcerer, 10)]
        //[TestCase(CharacterClassConstants.Wizard, 10)]
        //public void AttackBonus(String className, Int32 attackBonus)
        //{
        //    var prototype = new CharacterClassPrototype();
        //    prototype.ClassName = className;
        //    prototype.Level = 20;

        //    var characterClass = characterClassGenerator.GenerateWith(prototype);
        //    Assert.That(characterClass.BaseAttack.Bonus, Is.EqualTo(attackBonus));
        //}

        //[TestCase(1, 1)]
        //[TestCase(2, 2)]
        //[TestCase(3, 3)]
        //[TestCase(4, 4)]
        //[TestCase(5, 5)]
        //[TestCase(6, 6)]
        //[TestCase(7, 7)]
        //[TestCase(8, 8)]
        //[TestCase(9, 9)]
        //[TestCase(10, 10)]
        //[TestCase(11, 11)]
        //[TestCase(12, 12)]
        //[TestCase(13, 13)]
        //[TestCase(14, 14)]
        //[TestCase(15, 15)]
        //[TestCase(16, 16)]
        //[TestCase(17, 17)]
        //[TestCase(18, 18)]
        //[TestCase(19, 19)]
        //[TestCase(20, 20)]
        //public void GoodBaseAttackBonus(Int32 level, Int32 bonus)
        //{
        //    var prototype = new CharacterClassPrototype();
        //    prototype.ClassName = CharacterClassConstants.Fighter;
        //    prototype.Level = level;

        //    var characterClass = characterClassGenerator.GenerateWith(prototype);
        //    Assert.That(characterClass.BaseAttack.Bonus, Is.EqualTo(bonus));
        //}

        //[TestCase(1, 0)]
        //[TestCase(2, 1)]
        //[TestCase(3, 2)]
        //[TestCase(4, 3)]
        //[TestCase(5, 3)]
        //[TestCase(6, 4)]
        //[TestCase(7, 5)]
        //[TestCase(8, 6)]
        //[TestCase(9, 6)]
        //[TestCase(10, 7)]
        //[TestCase(11, 8)]
        //[TestCase(12, 9)]
        //[TestCase(13, 9)]
        //[TestCase(14, 10)]
        //[TestCase(15, 11)]
        //[TestCase(16, 12)]
        //[TestCase(17, 12)]
        //[TestCase(18, 13)]
        //[TestCase(19, 14)]
        //[TestCase(20, 15)]
        //public void AverageBaseAttackBonus(Int32 level, Int32 bonus)
        //{
        //    var prototype = new CharacterClassPrototype();
        //    prototype.ClassName = CharacterClassConstants.Cleric;
        //    prototype.Level = level;

        //    var characterClass = characterClassGenerator.GenerateWith(prototype);
        //    Assert.That(characterClass.BaseAttack.Bonus, Is.EqualTo(bonus));
        //}

        //[TestCase(1, 0)]
        //[TestCase(2, 1)]
        //[TestCase(3, 1)]
        //[TestCase(4, 2)]
        //[TestCase(5, 2)]
        //[TestCase(6, 3)]
        //[TestCase(7, 3)]
        //[TestCase(8, 4)]
        //[TestCase(9, 4)]
        //[TestCase(10, 5)]
        //[TestCase(11, 5)]
        //[TestCase(12, 6)]
        //[TestCase(13, 6)]
        //[TestCase(14, 7)]
        //[TestCase(15, 7)]
        //[TestCase(16, 8)]
        //[TestCase(17, 8)]
        //[TestCase(18, 9)]
        //[TestCase(19, 9)]
        //[TestCase(20, 10)]
        //public void PoorBaseAttackBonus(Int32 level, Int32 bonus)
        //{
        //    var prototype = new CharacterClassPrototype();
        //    prototype.ClassName = CharacterClassConstants.Wizard;
        //    prototype.Level = level;

        //    var characterClass = characterClassGenerator.GenerateWith(prototype);
        //    Assert.That(characterClass.BaseAttack.Bonus, Is.EqualTo(bonus));
        //}
    }
}