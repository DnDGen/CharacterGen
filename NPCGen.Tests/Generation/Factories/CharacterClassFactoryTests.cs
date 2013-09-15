using D20Dice.Dice;
using Moq;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Factories;
using NPCGen.Core.Generation.Factories.Interfaces;
using NPCGen.Core.Generation.Randomizers.CharacterClass;
using NPCGen.Core.Generation.Randomizers.Level;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Factories
{
    [TestFixture]
    public class CharacterClassFactoryTests
    {
        private Mock<IDice> mockDice;
        private Mock<ILevelRandomizer> mockLevelRandomizer;
        private Mock<IClassRandomizer> mockClassRandomizer;

        private ICharacterClassFactory characterClassFactory;
        private Alignment alignment;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();
            mockLevelRandomizer = new Mock<ILevelRandomizer>();
            mockClassRandomizer = new Mock<IClassRandomizer>();

            characterClassFactory = new CharacterClassFactory(mockDice.Object, mockLevelRandomizer.Object,
                mockClassRandomizer.Object);

            alignment = new Alignment();
        }

        [Test]
        public void FactoryReturnsRandomizedLevel()
        {
            mockLevelRandomizer.Setup(r => r.Randomize()).Returns(9266);

            var characterClass = characterClassFactory.Generate(alignment, 0);
            Assert.That(characterClass.Level, Is.EqualTo(9266));
        }

        [Test]
        public void ChangeLevelRandomizer()
        {
            var differentRandomizer = new Mock<ILevelRandomizer>();
            differentRandomizer.Setup(r => r.Randomize()).Returns(42);
            characterClassFactory.LevelRandomizer = differentRandomizer.Object;

            var characterClass = characterClassFactory.Generate(alignment, 0);
            Assert.That(characterClass.Level, Is.EqualTo(42));
        }

        [Test]
        public void FactoryReturnsRandomizedClass()
        {
            mockClassRandomizer.Setup(r => r.Randomize(alignment)).Returns("my class");

            var characterClass = characterClassFactory.Generate(alignment, 0);
            Assert.That(characterClass.ClassName, Is.EqualTo("my class"));
        }

        [Test]
        public void ChangeClassRandomizer()
        {
            var differentRandomizer = new Mock<IClassRandomizer>();
            differentRandomizer.Setup(r => r.Randomize(alignment)).Returns("my new class");
            characterClassFactory.ClassRandomizer = differentRandomizer.Object;

            var characterClass = characterClassFactory.Generate(alignment, 0);
            Assert.That(characterClass.ClassName, Is.EqualTo("my new class"));
        }
    }
}