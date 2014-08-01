using Moq;
using NPCGen.Common.Alignments;
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
            mockClassNameRandomizer.Setup(r => r.Randomize(alignment)).Returns("class name");

            var characterClass = characterClassGenerator.GenerateWith(alignment, mockLevelRandomizer.Object, mockClassNameRandomizer.Object);
            Assert.That(characterClass.ClassName, Is.EqualTo("class name"));
        }
    }
}