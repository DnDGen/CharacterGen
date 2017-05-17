using CharacterGen.Abilities.Skills;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Generators.Abilities;
using CharacterGen.Races;
using EventGen;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CharacterGen.Tests.Unit.Generators.Abilities
{
    [TestFixture]
    public class LanguageGeneratorEventGenDecoratorTests
    {
        private ILanguageGenerator decorator;
        private Mock<ILanguageGenerator> mockInnerGenerator;
        private Mock<GenEventQueue> mockEventQueue;
        private CharacterClass characterClass;
        private Race race;
        private List<Skill> skills;

        [SetUp]
        public void Setup()
        {
            mockInnerGenerator = new Mock<ILanguageGenerator>();
            mockEventQueue = new Mock<GenEventQueue>();
            decorator = new LanguageGeneratorEventGenDecorator(mockInnerGenerator.Object, mockEventQueue.Object);

            characterClass = new CharacterClass();
            race = new Race();
            skills = new List<Skill>();

            characterClass.Name = Guid.NewGuid().ToString();
            characterClass.Level = 9266;

            race.BaseRace = Guid.NewGuid().ToString();
        }

        [Test]
        public void ReturnInnerLanguages()
        {
            var languages = new[]
            {
                "klingon",
                "German",
            };

            mockInnerGenerator.Setup(g => g.GenerateWith(race, characterClass.Name, 90210, skills)).Returns(languages);

            var generatedLanguages = decorator.GenerateWith(race, characterClass.Name, 90210, skills);
            Assert.That(generatedLanguages, Is.EqualTo(languages));
        }

        [Test]
        public void LogEventsForLanguagesGeneration()
        {
            var languages = new[]
            {
                "klingon",
                "German",
            };

            mockInnerGenerator.Setup(g => g.GenerateWith(race, characterClass.Name, 90210, skills)).Returns(languages);

            var generatedLanguages = decorator.GenerateWith(race, characterClass.Name, 90210, skills);
            Assert.That(generatedLanguages, Is.EqualTo(languages));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Beginning language generation for {characterClass.Name} {race.Summary}"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Completed generation of languages: klingon, German"), Times.Once);
        }
    }
}
