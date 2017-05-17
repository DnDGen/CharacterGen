﻿using CharacterGen.Abilities.Feats;
using CharacterGen.Abilities.Stats;
using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Generators.Magics;
using CharacterGen.Items;
using CharacterGen.Magics;
using CharacterGen.Races;
using EventGen;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CharacterGen.Tests.Unit.Generators.Magics
{
    [TestFixture]
    public class MagicGeneratorEventGenDecoratorTests
    {
        private IMagicGenerator decorator;
        private Mock<IMagicGenerator> mockInnerGenerator;
        private Mock<GenEventQueue> mockEventQueue;
        private Alignment alignment;
        private CharacterClass characterClass;
        private Race race;
        private Dictionary<string, Stat> stats;
        private List<Feat> feats;
        private Equipment equipment;

        [SetUp]
        public void Setup()
        {
            mockInnerGenerator = new Mock<IMagicGenerator>();
            mockEventQueue = new Mock<GenEventQueue>();
            decorator = new MagicGeneratorEventGenDecorator(mockInnerGenerator.Object, mockEventQueue.Object);

            alignment = new Alignment();
            characterClass = new CharacterClass();
            race = new Race();
            stats = new Dictionary<string, Stat>();
            feats = new List<Feat>();
            equipment = new Equipment();

            alignment.Goodness = Guid.NewGuid().ToString();
            alignment.Lawfulness = Guid.NewGuid().ToString();

            characterClass.Name = Guid.NewGuid().ToString();
            characterClass.Level = 9266;

            race.BaseRace = Guid.NewGuid().ToString();
        }

        [Test]
        public void ReturnInnerMagic()
        {
            var magic = new Magic();
            mockInnerGenerator.Setup(g => g.GenerateWith(alignment, characterClass, race, stats, feats, equipment)).Returns(magic);

            var generatedMagic = decorator.GenerateWith(alignment, characterClass, race, stats, feats, equipment);
            Assert.That(generatedMagic, Is.EqualTo(magic));
        }

        [Test]
        public void LogEventsForMagicGeneration()
        {
            var magic = new Magic();
            mockInnerGenerator.Setup(g => g.GenerateWith(alignment, characterClass, race, stats, feats, equipment)).Returns(magic);

            var generatedMagic = decorator.GenerateWith(alignment, characterClass, race, stats, feats, equipment);
            Assert.That(generatedMagic, Is.EqualTo(magic));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Beginning magic generation for {alignment.Full} {characterClass.Summary} {race.Summary}"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Completed generation of magic"), Times.Once);
        }
    }
}
