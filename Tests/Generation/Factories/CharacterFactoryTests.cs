using System;
using D20Dice.Dice;
using Moq;
using NPCGen.Core.Data;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Data.Races;
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
        private Mock<IAlignmentRandomizer> mockAlignmentRandomizer;
        private Mock<IClassNameRandomizer> mockClassNameRandomizer;
        private Mock<ILevelRandomizer> mockLevelRandomizer;
        private Mock<IBaseRaceRandomizer> mockBaseRaceRandomizer;
        private Mock<IMetaraceRandomizer> mockMetaraceRandomizer;
        private Mock<IStatsRandomizer> mockStatsRandomizer;
        private Mock<IDice> mockDice;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();

            SetupMockRandomizers();
            SetupMockVerifiers();
        }

        private void SetupMockRandomizers()
        {
            mockAlignmentRandomizer = new Mock<IAlignmentRandomizer>();
            mockAlignmentRandomizer.Setup(r => r.Randomize()).Returns(new Alignment());
            mockAlignmentRandomizer.Setup(r => r.GetAllPossibleResults()).Returns(new[] { new Alignment() });

            mockClassNameRandomizer = new Mock<IClassNameRandomizer>();
            mockClassNameRandomizer.Setup(r => r.Randomize(It.IsAny<Alignment>())).Returns(CharacterClassConstants.Barbarian);
            mockClassNameRandomizer.Setup(r => r.GetAllPossibleResults(It.IsAny<Alignment>())).Returns(new[] { CharacterClassConstants.Barbarian });

            mockBaseRaceRandomizer = new Mock<IBaseRaceRandomizer>();
            mockBaseRaceRandomizer.Setup(r => r.GetAllPossibleResults(It.IsAny<String>(), It.IsAny<String>()))
                .Returns(new[] { RaceConstants.BaseRaces.Human });

            mockMetaraceRandomizer = new Mock<IMetaraceRandomizer>();
            mockMetaraceRandomizer.Setup(r => r.GetAllPossibleResults(It.IsAny<String>(), It.IsAny<String>()))
                .Returns(new[] { String.Empty });

            mockLevelRandomizer = new Mock<ILevelRandomizer>();
            mockStatsRandomizer = new Mock<IStatsRandomizer>();
        }

        private void SetupMockVerifiers()
        {
            mockRandomizerVerifier = new Mock<IRandomizerVerifier>();
            mockRandomizerVerifier.Setup(v => v.VerifyCompatibility()).Returns(true);
            mockRandomizerVerifier.Setup(v => v.VerifyAlignmentCompatibility(It.IsAny<Alignment>())).Returns(true);
            mockRandomizerVerifier.Setup(v => v.VerifyClassNameCompatibility(It.IsAny<String>(), It.IsAny<String>())).Returns(true);
        }

        [Test, ExpectedException(typeof(IncompatibleRandomizersException))]
        public void InvalidRandomizersThrowsException()
        {
            mockRandomizerVerifier.Setup(v => v.VerifyCompatibility()).Returns(false);
            
            CreateCharacter();
        }

        [Test]
        public void IncompatibleAlignmentIsRegenerated()
        {
            mockRandomizerVerifier.SetupSequence(v => v.VerifyAlignmentCompatibility(It.IsAny<Alignment>())).Returns(true).Returns(false)
                .Returns(true);

            CreateCharacter();
            mockAlignmentRandomizer.Verify(r => r.Randomize(), Times.Exactly(2));
        }

        [Test]
        public void IncompatibleCharacterClassIsRegenerated()
        {
            mockRandomizerVerifier.SetupSequence(v => v.VerifyClassNameCompatibility(It.IsAny<String>(), It.IsAny<String>())).Returns(false)
                .Returns(true);

            CreateCharacter();
            mockClassNameRandomizer.Verify(r => r.Randomize(It.IsAny<Alignment>()), Times.Exactly(2));
        }

        private Character CreateCharacter()
        {
            return CharacterFactory.CreateUsing(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockStatsRandomizer.Object, mockDice.Object);
        }
    }
}