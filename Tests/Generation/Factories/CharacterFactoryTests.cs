using System;
using System.Collections.Generic;
using Moq;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Data.Stats;
using NPCGen.Core.Generation.Factories;
using NPCGen.Core.Generation.Factories.Interfaces;
using NPCGen.Core.Generation.Randomizers.Alignments.Interfaces;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;
using NPCGen.Core.Generation.Verifiers.Exceptions;
using NPCGen.Core.Generation.Verifiers.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Factories
{
    [TestFixture]
    public class CharacterFactoryTests
    {
        private ICharacterFactory characterFactory;

        private Mock<IStatsFactory> mockStatsFactory;
        private Mock<IAlignmentFactory> mockAlignmentFactory;
        private Mock<ICharacterClassFactory> mockCharacterClassFactory;
        private Mock<IRaceFactory> mockRaceFactory;

        private Mock<IRandomizerVerifier> mockRandomizerVerifier;
        private Mock<IClassNameVerifier> mockCharacterClassVerifier;
        private Mock<IBaseRaceVerifier> mockBaseRaceVerifier;

        private Dictionary<String, Stat> stats;
        private Alignment alignment;
        private CharacterClass characterClass;
        private Race race;

        [SetUp]
        public void Setup()
        {
            mockStatsFactory = new Mock<IStatsFactory>();
            stats = new Dictionary<String, Stat>();
            stats.Add(StatConstants.Constitution, new Stat());
            mockStatsFactory.Setup(f => f.Generate()).Returns(stats);

            mockAlignmentFactory = new Mock<IAlignmentFactory>();
            alignment = new Alignment();
            mockAlignmentFactory.Setup(f => f.Generate()).Returns(alignment);
            
            mockCharacterClassFactory = new Mock<ICharacterClassFactory>();
            characterClass = new CharacterClass();
            mockCharacterClassFactory.Setup(f => f.Generate(alignment, It.IsAny<Int32>())).Returns(characterClass);

            mockRaceFactory = new Mock<IRaceFactory>();
            race = new Race();
            mockRaceFactory.Setup(f => f.Generate(It.IsAny<String>(), It.IsAny<String>())).Returns(race);

            mockRandomizerVerifier = new Mock<IRandomizerVerifier>();
            mockRandomizerVerifier.Setup(v => v.VerifyCompatibility(It.IsAny<IAlignmentRandomizer>(),
                                                                    It.IsAny<IClassNameRandomizer>(),
                                                                    It.IsAny<IBaseRaceRandomizer>(),
                                                                    It.IsAny<IMetaraceRandomizer>())).Returns(true);

            mockCharacterClassVerifier = new Mock<IClassNameVerifier>();
            mockCharacterClassVerifier.Setup(v => v.VerifyCompatibility(alignment)).Returns(true);

            mockBaseRaceVerifier = new Mock<IBaseRaceVerifier>();
            mockBaseRaceVerifier.Setup(v => v.VerifyCompatibility(alignment)).Returns(true);

            characterFactory = new CharacterFactory(mockCharacterClassFactory.Object, mockAlignmentFactory.Object, mockRaceFactory.Object,
                mockRandomizerVerifier.Object, mockStatsFactory.Object, mockCharacterClassVerifier.Object, mockBaseRaceVerifier.Object);
        }

        [Test, ExpectedException(typeof(IncompatibleRandomizersException))]
        public void InvalidRandomizersThrowsException()
        {
            mockRandomizerVerifier.Setup(v => v.VerifyCompatibility(It.IsAny<IAlignmentRandomizer>(), 
                                                                    It.IsAny<IClassNameRandomizer>(), 
                                                                    It.IsAny<IBaseRaceRandomizer>(), 
                                                                    It.IsAny<IMetaraceRandomizer>())).Returns(false);

            characterFactory.Generate();
        }

        [Test]
        public void GeneratesStatsFromStatsFactory()
        {
            var character = characterFactory.Generate();
            Assert.That(character.Stats, Is.EqualTo(stats));
        }

        [Test]
        public void GeneratesAlignmentFromAlignmentFactory()
        {
            var character = characterFactory.Generate();
            Assert.That(character.Alignment, Is.EqualTo(alignment));
        }

        [Test]
        public void GeneratesCharacterClassFromCharacterClassFactory()
        {
            var character = characterFactory.Generate();
            Assert.That(character.Class, Is.EqualTo(characterClass));
        }

        [Test]
        public void GeneratesRaceFromRaceFactory()
        {
            var character = characterFactory.Generate();
            Assert.That(character.Race, Is.EqualTo(race));
        }

        [Test]
        public void AlignmentIncompatibleWithCharacterClassIsRegenerated()
        {
            mockCharacterClassVerifier.SetupSequence(v => v.VerifyCompatibility(alignment)).Returns(false).Returns(true);
            characterFactory.Generate();
            mockAlignmentFactory.Verify(f => f.Generate(), Times.Exactly(2));
        }

        [Test]
        public void AlignmentIncompatibleWithBaseRaceIsRegenerated()
        {
            mockBaseRaceVerifier.SetupSequence(v => v.VerifyCompatibility(alignment)).Returns(false).Returns(true);
            characterFactory.Generate();
            mockAlignmentFactory.Verify(f => f.Generate(), Times.Exactly(2));
        }
    }
}