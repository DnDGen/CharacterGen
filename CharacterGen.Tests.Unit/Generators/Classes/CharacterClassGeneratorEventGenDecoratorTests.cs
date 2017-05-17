using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Generators.Classes;
using CharacterGen.Races;
using CharacterGen.Randomizers.CharacterClasses;
using EventGen;
using Moq;
using NUnit.Framework;

namespace CharacterGen.Tests.Unit.Generators.Classes
{
    [TestFixture]
    public class CharacterClassGeneratorEventGenDecoratorTests
    {
        private ICharacterClassGenerator decorator;
        private Mock<ICharacterClassGenerator> mockInnerGenerator;
        private Mock<GenEventQueue> mockEventQueue;
        private Alignment alignment;
        private Mock<ILevelRandomizer> mockLevelRandomizer;
        private Mock<IClassNameRandomizer> mockClassNameRandomizer;

        [SetUp]
        public void Setup()
        {
            mockInnerGenerator = new Mock<ICharacterClassGenerator>();
            mockEventQueue = new Mock<GenEventQueue>();
            decorator = new CharacterClassGeneratorEventGenDecorator(mockInnerGenerator.Object, mockEventQueue.Object);

            alignment = new Alignment();
            mockLevelRandomizer = new Mock<ILevelRandomizer>();
            mockClassNameRandomizer = new Mock<IClassNameRandomizer>();

            alignment.Goodness = "goodness";
            alignment.Lawfulness = "lawfulness";
        }

        [Test]
        public void ReturnInnerClass()
        {
            var characterClass = new CharacterClass();
            mockInnerGenerator.Setup(g => g.GenerateWith(alignment, mockLevelRandomizer.Object, mockClassNameRandomizer.Object)).Returns(characterClass);

            var generatedClass = decorator.GenerateWith(alignment, mockLevelRandomizer.Object, mockClassNameRandomizer.Object);
            Assert.That(generatedClass, Is.EqualTo(characterClass));
        }

        [Test]
        public void LogEventsForClassGeneration()
        {
            var characterClass = new CharacterClass();
            characterClass.Name = "class name";
            characterClass.Level = 9266;

            mockInnerGenerator.Setup(g => g.GenerateWith(alignment, mockLevelRandomizer.Object, mockClassNameRandomizer.Object)).Returns(characterClass);

            var generatedClass = decorator.GenerateWith(alignment, mockLevelRandomizer.Object, mockClassNameRandomizer.Object);
            Assert.That(generatedClass, Is.EqualTo(characterClass));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Beginning class generation for {alignment.Full}"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Completed generation of {characterClass.Summary}"), Times.Once);
        }

        [Test]
        public void ReturnInnerRegeneratedSpecialistFields()
        {
            var characterClass = new CharacterClass();
            var race = new Race();
            var fields = new[]
            {
                "field 1",
                "field 2"
            };

            mockInnerGenerator.Setup(g => g.RegenerateSpecialistFields(alignment, characterClass, race)).Returns(fields);

            var generatedFields = decorator.RegenerateSpecialistFields(alignment, characterClass, race);
            Assert.That(generatedFields, Is.EqualTo(fields));
        }

        [Test]
        public void LogEventsForRegenerationOfSpecialistFields()
        {
            var characterClass = new CharacterClass { Name = "class name", Level = 9266 };
            var race = new Race { BaseRace = "base race" };
            var fields = new[]
            {
                "field 1",
                "field 2"
            };

            mockInnerGenerator.Setup(g => g.RegenerateSpecialistFields(alignment, characterClass, race)).Returns(fields);

            var generatedFields = decorator.RegenerateSpecialistFields(alignment, characterClass, race);
            Assert.That(generatedFields, Is.EqualTo(fields));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Regenerating specialist fields for {alignment.Full} {characterClass.Summary} {race.Summary}"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Completed regeneration of specialist fields: {string.Join(", ", fields)}"), Times.Once);
        }
    }
}
