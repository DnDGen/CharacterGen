using CharacterGen.Abilities.Feats;
using CharacterGen.Abilities.Stats;
using CharacterGen.CharacterClasses;
using CharacterGen.Combats;
using CharacterGen.Domain.Generators.Combats;
using CharacterGen.Items;
using CharacterGen.Races;
using EventGen;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CharacterGen.Tests.Unit.Generators.Combats
{
    [TestFixture]
    public class CombatGeneratorEventGenDecoratorTests
    {
        private ICombatGenerator decorator;
        private Mock<ICombatGenerator> mockInnerGenerator;
        private Mock<GenEventQueue> mockEventQueue;
        private CharacterClass characterClass;
        private Race race;
        private Dictionary<string, Stat> stats;
        private List<Feat> feats;
        private Equipment equipment;

        [SetUp]
        public void Setup()
        {
            mockInnerGenerator = new Mock<ICombatGenerator>();
            mockEventQueue = new Mock<GenEventQueue>();
            decorator = new CombatGeneratorEventGenDecorator(mockInnerGenerator.Object, mockEventQueue.Object);

            characterClass = new CharacterClass();
            race = new Race();
            stats = new Dictionary<string, Stat>();
            feats = new List<Feat>();
            equipment = new Equipment();

            characterClass.Name = Guid.NewGuid().ToString();
            characterClass.Level = 9266;

            race.BaseRace = Guid.NewGuid().ToString();
        }

        [Test]
        public void ReturnInnerCombat()
        {
            var baseAttack = new BaseAttack();
            var combat = new Combat();
            mockInnerGenerator.Setup(g => g.GenerateWith(baseAttack, characterClass, race, feats, stats, equipment)).Returns(combat);

            var generatedCombat = decorator.GenerateWith(baseAttack, characterClass, race, feats, stats, equipment);
            Assert.That(generatedCombat, Is.EqualTo(combat));
        }

        [Test]
        public void LogEventsForCombatGeneration()
        {
            var baseAttack = new BaseAttack();
            var combat = new Combat();
            mockInnerGenerator.Setup(g => g.GenerateWith(baseAttack, characterClass, race, feats, stats, equipment)).Returns(combat);

            var generatedCombat = decorator.GenerateWith(baseAttack, characterClass, race, feats, stats, equipment);
            Assert.That(generatedCombat, Is.EqualTo(combat));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Beginning combat statistic generation for {characterClass.Summary} {race.Summary}"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Completed generation of combat statistics"), Times.Once);
        }

        [Test]
        public void ReturnInnerBaseAttack()
        {
            var baseAttack = new BaseAttack();
            mockInnerGenerator.Setup(g => g.GenerateBaseAttackWith(characterClass, race, stats)).Returns(baseAttack);

            var generatedBaseAttack = decorator.GenerateBaseAttackWith(characterClass, race, stats);
            Assert.That(generatedBaseAttack, Is.EqualTo(baseAttack));
        }

        [Test]
        public void LogEventsForBaseAttackGeneration()
        {
            var baseAttack = new BaseAttack();
            mockInnerGenerator.Setup(g => g.GenerateBaseAttackWith(characterClass, race, stats)).Returns(baseAttack);

            var generatedBaseAttack = decorator.GenerateBaseAttackWith(characterClass, race, stats);
            Assert.That(generatedBaseAttack, Is.EqualTo(baseAttack));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Beginning base attack generation for {characterClass.Summary} {race.Summary}"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Completed generation of base attack"), Times.Once);
        }
    }
}
