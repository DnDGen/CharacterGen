using System;
using System.Collections.Generic;
using D20Dice.Dice;
using Moq;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Data.Stats;
using NPCGen.Core.Generation.Factories;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Randomizers.Alignments;
using NPCGen.Core.Generation.Randomizers.Alignments.Interfaces;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.ClassNames;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Interfaces;
using NPCGen.Core.Generation.Randomizers.Levels;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;
using NPCGen.Core.Generation.Verifiers.Exceptions;
using NPCGen.Core.Generation.Verifiers.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Factories
{
    [TestFixture]
    public class CharacterFactoryTests
    {
        private Dictionary<String, Stat> stats;
        private Alignment alignment;
        private CharacterClass characterClass;
        private Race race;
        private Mock<IDice> mockDice;
        private Mock<IPercentileResultProvider> mockPercentileResultProvider;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();
            mockPercentileResultProvider = new Mock<IPercentileResultProvider>();
        }

        [Test, ExpectedException(typeof(IncompatibleRandomizersException))]
        public void InvalidRandomizersThrowsException()
        {
            CharacterFactory.CreateUsing(new GoodAlignmentRandomizer(mockDice.Object), 
                new AnyClassNameRandomizer(mockPercentileResultProvider.Object), new AnyLevelRandomizer(mockDice.Object),
                new AnyBaseRaceRandomizer(mockPercentileResultProvider.Object), 
                new ForcedEvilMetaraceRandomizer(mockPercentileResultProvider.Object), new 
        }

        [Test]
        public void GeneratesStatsFromStatsFactory()
        {
            var character = CharacterFactory.CreateUsing();
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

        [Test]
        public void AlignmentIncompatibleWithMetaraceIsRegnerated()
        {
            
        }
    }
}