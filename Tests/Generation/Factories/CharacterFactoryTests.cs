using System;
using System.Collections.Generic;
using Moq;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Data.Stats;
using NPCGen.Core.Generation.Factories;
using NPCGen.Core.Generation.Factories.Interfaces;
using NPCGen.Core.Generation.Randomizers.Alignments;
using NPCGen.Core.Generation.Randomizers.ClassNames;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;
using NPCGen.Core.Generation.Verifiers;
using NPCGen.Core.Generation.Verifiers.Exceptions;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Factories
{
    [TestFixture]
    public class CharacterFactoryTests
    {
        private ICharacterFactory characterFactory;
        private Mock<IRandomizerVerifier> mockRandomizerVerifier;
        private Mock<ICharacterClassFactory> mockCharacterClassFactory;
        private Mock<IAlignmentFactory> mockAlignmentFactory;
        private Mock<IRaceFactory> mockRaceFactory;
        private Mock<IStatsFactory> mockStatsFactory;

        private Dictionary<String, Stat> stats;
        private Alignment alignment;
        private CharacterClass characterClass;
        private Race race;

        [SetUp]
        public void Setup()
        {
            mockRandomizerVerifier = new Mock<IRandomizerVerifier>();
            mockRandomizerVerifier.Setup(v => v.VerifyCompatibility(It.IsAny<IAlignmentRandomizer>(),
                                                                    It.IsAny<IClassNameRandomizer>(),
                                                                    It.IsAny<IBaseRaceRandomizer>(),
                                                                    It.IsAny<IMetaraceRandomizer>())).Returns(true);


            mockAlignmentFactory = new Mock<IAlignmentFactory>();
            alignment = new Alignment();
            mockAlignmentFactory.Setup(f => f.Generate()).Returns(alignment);

            mockStatsFactory = new Mock<IStatsFactory>();
            stats = new Dictionary<String, Stat>();
            stats.Add(StatConstants.Constitution, new Stat());
            mockStatsFactory.Setup(f => f.Generate()).Returns(stats);
            
            mockCharacterClassFactory = new Mock<ICharacterClassFactory>();
            characterClass = new CharacterClass();
            mockCharacterClassFactory.Setup(f => f.Generate(alignment, It.IsAny<Int32>())).Returns(characterClass);

            mockRaceFactory = new Mock<IRaceFactory>();
            race = new Race();
            mockRaceFactory.Setup(f => f.Generate(alignment, characterClass)).Returns(race);

            characterFactory = new CharacterFactory(mockCharacterClassFactory.Object, mockAlignmentFactory.Object, mockRaceFactory.Object, mockRandomizerVerifier.Object, mockStatsFactory.Object);
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
        public void IncompatibleAlignmentIsRegenerated()
        {
            alignment.Goodness = AlignmentConstants.Evil;
            var acceptableAlignment = new Alignment();
            acceptableAlignment.Goodness = AlignmentConstants.Good;
            mockAlignmentFactory.SetupSequence(f => f.Generate()).Returns(alignment).Returns(acceptableAlignment);
        }
    }
}