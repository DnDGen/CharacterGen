using CharacterGen.Abilities.Feats;
using CharacterGen.Abilities.Skills;
using CharacterGen.Abilities.Stats;
using CharacterGen.CharacterClasses;
using CharacterGen.Combats;
using CharacterGen.Domain.Generators.Abilities.Feats;
using CharacterGen.Races;
using EventGen;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CharacterGen.Tests.Unit.Generators.Abilities.Feats
{
    [TestFixture]
    public class ClassFeatsGeneratorEventGenDecoratorTests
    {
        private IClassFeatsGenerator decorator;
        private Mock<IClassFeatsGenerator> mockInnerGenerator;
        private Mock<GenEventQueue> mockEventQueue;
        private CharacterClass characterClass;
        private Race race;
        private Dictionary<string, Stat> stats;
        private List<Skill> skills;
        private BaseAttack baseAttack;
        private List<Feat> racialFeats;

        [SetUp]
        public void Setup()
        {
            mockInnerGenerator = new Mock<IClassFeatsGenerator>();
            mockEventQueue = new Mock<GenEventQueue>();
            decorator = new ClassFeatsGeneratorEventGenDecorator(mockInnerGenerator.Object, mockEventQueue.Object);

            characterClass = new CharacterClass();
            race = new Race();
            stats = new Dictionary<string, Stat>();
            skills = new List<Skill>();
            baseAttack = new BaseAttack();
            racialFeats = new List<Feat>();

            characterClass.Name = Guid.NewGuid().ToString();
            characterClass.Level = 9266;

            race.BaseRace = Guid.NewGuid().ToString();
        }

        [Test]
        public void ReturnInnerFeats()
        {
            var feats = new[]
            {
                new Feat(),
                new Feat(),
            };

            mockInnerGenerator.Setup(g => g.GenerateWith(characterClass, race, stats, racialFeats, skills)).Returns(feats);

            var generatedFeats = decorator.GenerateWith(characterClass, race, stats, racialFeats, skills);
            Assert.That(generatedFeats, Is.EqualTo(feats));
        }

        [Test]
        public void LogEventsForFeatsGeneration()
        {
            var feats = new[]
            {
                new Feat(),
                new Feat(),
            };

            mockInnerGenerator.Setup(g => g.GenerateWith(characterClass, race, stats, racialFeats, skills)).Returns(feats);

            var generatedFeats = decorator.GenerateWith(characterClass, race, stats, racialFeats, skills);
            Assert.That(generatedFeats, Is.EqualTo(feats));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Beginning class feats generation for {characterClass.Summary} {race.Summary}"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Completed generation of class feats"), Times.Once);
        }
    }
}
