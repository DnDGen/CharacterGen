﻿using CharacterGen.Abilities;
using CharacterGen.CharacterClasses;
using CharacterGen.Combats;
using CharacterGen.Domain.Generators.Feats;
using CharacterGen.Feats;
using CharacterGen.Races;
using CharacterGen.Skills;
using EventGen;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CharacterGen.Tests.Unit.Generators.Feats
{
    [TestFixture]
    public class FeatsGeneratorEventGenDecoratorTests
    {
        private IFeatsGenerator decorator;
        private Mock<IFeatsGenerator> mockInnerGenerator;
        private Mock<GenEventQueue> mockEventQueue;
        private CharacterClass characterClass;
        private Race race;
        private Dictionary<string, Ability> stats;
        private List<Skill> skills;
        private BaseAttack baseAttack;

        [SetUp]
        public void Setup()
        {
            mockInnerGenerator = new Mock<IFeatsGenerator>();
            mockEventQueue = new Mock<GenEventQueue>();
            decorator = new FeatsGeneratorEventGenDecorator(mockInnerGenerator.Object, mockEventQueue.Object);

            characterClass = new CharacterClass();
            race = new Race();
            stats = new Dictionary<string, Ability>();
            skills = new List<Skill>();
            baseAttack = new BaseAttack();

            characterClass.Name = Guid.NewGuid().ToString();
            characterClass.Level = 9266;

            race.BaseRace = Guid.NewGuid().ToString();
        }

        [Test]
        public void ReturnInnerFeats()
        {
            var feats = new FeatCollections();
            mockInnerGenerator.Setup(g => g.GenerateWith(characterClass, race, stats, skills, baseAttack)).Returns(feats);

            var generatedFeats = decorator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            Assert.That(generatedFeats, Is.EqualTo(feats));
        }

        [Test]
        public void LogEventsForFeatsGeneration()
        {
            var feats = new FeatCollections();
            mockInnerGenerator.Setup(g => g.GenerateWith(characterClass, race, stats, skills, baseAttack)).Returns(feats);

            var generatedFeats = decorator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            Assert.That(generatedFeats, Is.EqualTo(feats));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Generating feats for {characterClass.Summary} {race.Summary}"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Generated feats"), Times.Once);
        }
    }
}
