using System;
using Moq;
using NPCGen.Core.Data;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Factories;
using NPCGen.Core.Generation.Randomizers.Alignments.Interfaces;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;
using NPCGen.Core.Generation.Randomizers.Stats.Interfaces;
using NPCGen.Core.Generation.Verifiers.Exceptions;
using NPCGen.Core.Generation.Verifiers.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Factories
{
    [TestFixture]
    public class CharacterFactoryTests
    {
        private Mock<IRandomizerVerifier> mockRandomizerVerifier;
        private Mock<IAlignmentVerifier> mockAlignmentVerifier;
        private Mock<IBaseRaceVerifier> mockBaseRaceVerifier;
        private Mock<IClassNameVerifier> mockClassNameVerifier;
        private Mock<IMetaraceVerifier> mockMetaraceVerifier;
        private VerifierCollection verifierCollection;
        private Mock<IAlignmentRandomizer> mockAlignmentRandomizer;
        private Mock<IClassNameRandomizer> mockClassNameRandomizer;
        private Mock<ILevelRandomizer> mockLevelRandomizer;
        private Mock<IBaseRaceRandomizer> mockBaseRaceRandomizer;
        private Mock<IMetaraceRandomizer> mockMetaraceRandomizer;
        private Mock<IStatsRandomizer> mockStatsRandomizer;

        [SetUp]
        public void Setup()
        {
            SetupMockRandomizers();
            SetupMockVerifiers();
        }

        private void SetupMockRandomizers()
        {
            mockAlignmentRandomizer = new Mock<IAlignmentRandomizer>();
            mockAlignmentRandomizer.Setup(r => r.Randomize()).Returns(new Alignment());

            mockClassNameRandomizer = new Mock<IClassNameRandomizer>();
            mockClassNameRandomizer.Setup(r => r.Randomize(It.IsAny<Alignment>())).Returns(CharacterClassConstants.Barbarian);

            mockLevelRandomizer = new Mock<ILevelRandomizer>();
            mockBaseRaceRandomizer = new Mock<IBaseRaceRandomizer>();
            mockMetaraceRandomizer = new Mock<IMetaraceRandomizer>();
            mockStatsRandomizer = new Mock<IStatsRandomizer>();
        }

        private void SetupMockVerifiers()
        {
            mockRandomizerVerifier = new Mock<IRandomizerVerifier>();
            mockRandomizerVerifier.Setup(v => v.VerifyCompatibility(It.IsAny<IAlignmentRandomizer>(), It.IsAny<IClassNameRandomizer>(),
                It.IsAny<IBaseRaceRandomizer>(), It.IsAny<IMetaraceRandomizer>())).Returns(true);

            mockAlignmentVerifier = new Mock<IAlignmentVerifier>();
            mockAlignmentVerifier.Setup(v => v.VerifyCompatibility(It.IsAny<IBaseRaceRandomizer>())).Returns(true);
            mockAlignmentVerifier.Setup(v => v.VerifyCompatibility(It.IsAny<IClassNameRandomizer>())).Returns(true);
            mockAlignmentVerifier.Setup(v => v.VerifyCompatibility(It.IsAny<IMetaraceRandomizer>())).Returns(true);

            mockBaseRaceVerifier = new Mock<IBaseRaceVerifier>();
            mockBaseRaceVerifier.Setup(v => v.VerifyCompatibility(It.IsAny<Alignment>())).Returns(true);
            mockBaseRaceVerifier.Setup(v => v.VerifyCompatibility(It.IsAny<String>())).Returns(true);
            mockBaseRaceVerifier.Setup(v => v.VerifyCompatibility(It.IsAny<IMetaraceRandomizer>())).Returns(true);

            mockClassNameVerifier = new Mock<IClassNameVerifier>();
            mockClassNameVerifier.Setup(v => v.VerifyCompatibility(It.IsAny<IBaseRaceRandomizer>())).Returns(true);
            mockClassNameVerifier.Setup(v => v.VerifyCompatibility(It.IsAny<Alignment>())).Returns(true);
            mockClassNameVerifier.Setup(v => v.VerifyCompatibility(It.IsAny<IMetaraceRandomizer>())).Returns(true);

            mockMetaraceVerifier = new Mock<IMetaraceVerifier>();
            mockMetaraceVerifier.Setup(v => v.VerifyCompatibility(It.IsAny<Alignment>())).Returns(true);
            mockMetaraceVerifier.Setup(v => v.VerifyClassNameCompatibility(It.IsAny<String>())).Returns(true);
            mockMetaraceVerifier.Setup(v => v.VerifyBaseRaceCompatibility(It.IsAny<String>())).Returns(true);

            verifierCollection = new VerifierCollection();
            verifierCollection.AlignmentVerifier = mockAlignmentVerifier.Object;
            verifierCollection.RandomizerVerifier = mockRandomizerVerifier.Object;
            verifierCollection.BaseRaceVerifier = mockBaseRaceVerifier.Object;
            verifierCollection.ClassNameVerifier = mockClassNameVerifier.Object;
            verifierCollection.MetaraceVerifier = mockMetaraceVerifier.Object;
        }

        [Test, ExpectedException(typeof(IncompatibleRandomizersException))]
        public void InvalidRandomizersThrowsException()
        {
            mockRandomizerVerifier.Setup(v => v.VerifyCompatibility(It.IsAny<IAlignmentRandomizer>(), It.IsAny<IClassNameRandomizer>(),
                It.IsAny<IBaseRaceRandomizer>(), It.IsAny<IMetaraceRandomizer>())).Returns(false);
            
            CreateCharacter();
        }

        [Test]
        public void GeneratesStatsFromStatsFactory()
        {
            var stats = StatsFactory.CreateUsing(mockStatsRandomizer.Object);
            
            var character = CreateCharacter();
            Assert.That(character.Stats, Is.EqualTo(stats));
        }

        [Test]
        public void GeneratesAlignmentFromAlignmentFactory()
        {
            var alignment = AlignmentFactory.CreateUsing(mockAlignmentRandomizer.Object);
            
            var character = CreateCharacter();
            Assert.That(character.Alignment, Is.EqualTo(alignment));
        }

        [Test]
        public void GeneratesCharacterClassFromCharacterClassFactory()
        {
            var alignment = AlignmentFactory.CreateUsing(mockAlignmentRandomizer.Object);
            var characterClass = CharacterClassFactory.CreateUsing(alignment, mockLevelRandomizer.Object, mockClassNameRandomizer.Object);
            
            var character = CreateCharacter();
            Assert.That(character.Class, Is.EqualTo(characterClass));
        }

        [Test]
        public void GeneratesRaceFromRaceFactory()
        {
            var alignment = AlignmentFactory.CreateUsing(mockAlignmentRandomizer.Object);
            var characterClass = CharacterClassFactory.CreateUsing(alignment, mockLevelRandomizer.Object, mockClassNameRandomizer.Object);
            var race = RaceFactory.CreateUsing(alignment.GetGoodnessString(), characterClass.ClassName, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object);
            
            var character = CreateCharacter();
            Assert.That(character.Race, Is.EqualTo(race));
        }

        [Test]
        public void AlignmentIncompatibleWithCharacterClassIsRegenerated()
        {
            mockClassNameVerifier.SetupSequence(v => v.VerifyCompatibility(It.IsAny<Alignment>())).Returns(false).Returns(true);

            CreateCharacter();
            mockClassNameVerifier.Verify(v => v.VerifyCompatibility(It.IsAny<Alignment>()), Times.Exactly(2));
        }

        [Test]
        public void AlignmentIncompatibleWithBaseRaceIsRegenerated()
        {
            mockBaseRaceVerifier.SetupSequence(v => v.VerifyCompatibility(It.IsAny<Alignment>())).Returns(false).Returns(true);

            CreateCharacter();
            mockClassNameVerifier.Verify(v => v.VerifyCompatibility(It.IsAny<Alignment>()), Times.Exactly(2));
        }

        [Test]
        public void AlignmentIncompatibleWithMetaraceIsRegnerated()
        {
            mockMetaraceVerifier.SetupSequence(v => v.VerifyCompatibility(It.IsAny<Alignment>())).Returns(false).Returns(true);

            CreateCharacter();
            mockClassNameVerifier.Verify(v => v.VerifyCompatibility(It.IsAny<Alignment>()), Times.Exactly(2));
        }

        private Character CreateCharacter()
        {
            return CharacterFactory.CreateUsing(verifierCollection, mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object,
                mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockStatsRandomizer.Object);
        }
    }
}