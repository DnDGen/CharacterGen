using System;
using System.Collections.Generic;
using System.Linq;
using D20Dice.Dice;
using Moq;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Data.Stats;
using NPCGen.Core.Generation.Factories;
using NPCGen.Core.Generation.Randomizers.Alignments.Interfaces;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;
using NPCGen.Core.Generation.Randomizers.Stats.Interfaces;
using NPCGen.Core.Generation.Verifiers.Exceptions;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Factories
{
    [TestFixture]
    public class CharacterFactoryTests
    {
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
        }

        private void SetupMockRandomizers()
        {
            mockAlignmentRandomizer = new Mock<IAlignmentRandomizer>();
            mockAlignmentRandomizer.Setup(r => r.Randomize()).Returns(new Alignment());
            mockAlignmentRandomizer.Setup(r => r.GetAllPossibleResults()).Returns(new[] { new Alignment() });

            mockClassNameRandomizer = new Mock<IClassNameRandomizer>();
            mockClassNameRandomizer.Setup(r => r.Randomize(It.IsAny<Alignment>())).Returns(CharacterClassConstants.Barbarian);
            mockClassNameRandomizer.Setup(r => r.GetAllPossibleResults(It.IsAny<Alignment>())).Returns(new[] { CharacterClassConstants.Barbarian });

            mockLevelRandomizer = new Mock<ILevelRandomizer>();
            mockLevelRandomizer.Setup(r => r.Randomize()).Returns(1);
            mockLevelRandomizer.Setup(r => r.GetAllPossibleResults()).Returns(new[] { 1 });

            mockBaseRaceRandomizer = new Mock<IBaseRaceRandomizer>();
            mockBaseRaceRandomizer.Setup(r => r.Randomize(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>())).Returns(RaceConstants.BaseRaces.Human);
            mockBaseRaceRandomizer.Setup(r => r.GetAllPossibleResults(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>()))
                .Returns(new[] { RaceConstants.BaseRaces.Human });

            mockMetaraceRandomizer = new Mock<IMetaraceRandomizer>();
            mockMetaraceRandomizer.Setup(r => r.Randomize(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>())).Returns(String.Empty);
            mockMetaraceRandomizer.Setup(r => r.GetAllPossibleResults(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>()))
                .Returns(new[] { String.Empty });

            var stats = new Dictionary<String, Stat>();
            foreach(var stat in StatConstants.GetStats())
                stats.Add(stat, new Stat());

            mockStatsRandomizer = new Mock<IStatsRandomizer>();
            mockStatsRandomizer.Setup(r => r.Randomize()).Returns(stats);
        }

        [Test, ExpectedException(typeof(IncompatibleRandomizersException))]
        public void InvalidRandomizersThrowsException()
        {
            mockAlignmentRandomizer.Setup(r => r.GetAllPossibleResults()).Returns(Enumerable.Empty<Alignment>());

            CharacterFactory.CreateUsing(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockStatsRandomizer.Object, mockDice.Object);
        }

        [Test]
        public void IncompatibleAlignmentIsRegenerated()
        {
            var possibleResults = new[] { CharacterClassConstants.Barbarian };
            mockClassNameRandomizer.SetupSequence(r => r.GetAllPossibleResults(It.IsAny<Alignment>()))
                .Returns(possibleResults) //initial verification
                .Returns(Enumerable.Empty<String>()) //incompatible alignment
                .Returns(possibleResults); //compatible alignment
            
            CharacterFactory.CreateUsing(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockStatsRandomizer.Object, mockDice.Object);
            mockAlignmentRandomizer.Verify(r => r.Randomize(), Times.Exactly(2));
        }

        [Test]
        public void IncompatibleCharacterClassIsRegenerated()
        {
            var possibleResults = new[] { RaceConstants.BaseRaces.Human };
            mockBaseRaceRandomizer.SetupSequence(r => r.GetAllPossibleResults(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>()))
                .Returns(possibleResults) //initial verification
                .Returns(possibleResults) //alignment verification
                .Returns(Enumerable.Empty<String>()) // incompatible character class
                .Returns(possibleResults); //compatible character class

            CharacterFactory.CreateUsing(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockStatsRandomizer.Object, mockDice.Object);
            mockClassNameRandomizer.Verify(r => r.Randomize(It.IsAny<Alignment>()), Times.Exactly(2));
        }

        [Test]
        public void IncompatibleBaseRaceIsRegenerated()
        {
            mockBaseRaceRandomizer.SetupSequence(r => r.Randomize(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>()))
                .Returns(RaceConstants.BaseRaces.Svirfneblin).Returns(RaceConstants.BaseRaces.Human);

            CharacterFactory.CreateUsing(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockStatsRandomizer.Object, mockDice.Object);
            mockBaseRaceRandomizer.Verify(r => r.Randomize(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>()), Times.Exactly(2));
            mockMetaraceRandomizer.Verify(r => r.Randomize(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>()), Times.Exactly(2));
        }

        [Test]
        public void IncompatibleMetaraceIsRegenerated()
        {
            mockMetaraceRandomizer.SetupSequence(r => r.Randomize(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>()))
                .Returns(RaceConstants.Metaraces.HalfDragon).Returns(String.Empty);

            CharacterFactory.CreateUsing(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockStatsRandomizer.Object, mockDice.Object);
            mockBaseRaceRandomizer.Verify(r => r.Randomize(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>()), Times.Exactly(2));
            mockMetaraceRandomizer.Verify(r => r.Randomize(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>()), Times.Exactly(2));
        }

        [Test]
        public void IncompatibleComboOfBaseRaceAndMetaraceIsRegeneratedWithCompatibleBaseRace()
        {
            mockBaseRaceRandomizer.SetupSequence(r => r.Randomize(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>()))
                .Returns(RaceConstants.BaseRaces.Svirfneblin).Returns(RaceConstants.BaseRaces.Human);

            mockMetaraceRandomizer.Setup(r => r.Randomize(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>()))
                .Returns(RaceConstants.Metaraces.HalfDragon);

            mockLevelRandomizer.Setup(r => r.Randomize()).Returns(3);

            CharacterFactory.CreateUsing(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockStatsRandomizer.Object, mockDice.Object);
            mockBaseRaceRandomizer.Verify(r => r.Randomize(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>()), Times.Exactly(2));
            mockMetaraceRandomizer.Verify(r => r.Randomize(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>()), Times.Exactly(2));
        }

        [Test]
        public void IncompatibleComboOfBaseRaceAndMetaraceIsRegeneratedWithCompatibleMetarace()
        {
            mockBaseRaceRandomizer.Setup(r => r.Randomize(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>()))
                .Returns(RaceConstants.BaseRaces.Svirfneblin);

            mockMetaraceRandomizer.SetupSequence(r => r.Randomize(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>()))
                .Returns(RaceConstants.Metaraces.HalfDragon).Returns(String.Empty);

            mockLevelRandomizer.Setup(r => r.Randomize()).Returns(3);

            CharacterFactory.CreateUsing(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockStatsRandomizer.Object, mockDice.Object);
            mockBaseRaceRandomizer.Verify(r => r.Randomize(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>()), Times.Exactly(2));
            mockMetaraceRandomizer.Verify(r => r.Randomize(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>()), Times.Exactly(2));
        }

        [Test]
        public void AppliesBaseRaceLevelAdjustment()
        {
            mockLevelRandomizer.Setup(r => r.Randomize()).Returns(2);
            mockBaseRaceRandomizer.Setup(r => r.Randomize(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>()))
                .Returns(RaceConstants.BaseRaces.Svirfneblin);

            var character = CharacterFactory.CreateUsing(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockStatsRandomizer.Object, mockDice.Object);
            Assert.That(character.Class.Level, Is.EqualTo(1));
        }

        [Test]
        public void AppliesMetaraceLevelAdjustment()
        {
            mockLevelRandomizer.Setup(r => r.Randomize()).Returns(3);
            mockMetaraceRandomizer.Setup(r => r.Randomize(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>()))
                .Returns(RaceConstants.Metaraces.HalfDragon);

            var character = CharacterFactory.CreateUsing(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockStatsRandomizer.Object, mockDice.Object);
            Assert.That(character.Class.Level, Is.EqualTo(1));
        }

        [Test]
        public void ApplyBaseRaceAndMetaraceLevelAdjustments()
        {
            mockBaseRaceRandomizer.Setup(r => r.Randomize(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>()))
                .Returns(RaceConstants.BaseRaces.Svirfneblin);

            mockMetaraceRandomizer.Setup(r => r.Randomize(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>()))
                .Returns(RaceConstants.Metaraces.HalfDragon);

            mockLevelRandomizer.Setup(r => r.Randomize()).Returns(4);

            var character = CharacterFactory.CreateUsing(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockStatsRandomizer.Object, mockDice.Object);
            Assert.That(character.Class.Level, Is.EqualTo(1));
        }

        [Test]
        public void ReturnsACharacter()
        {
            var character = CharacterFactory.CreateUsing(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockStatsRandomizer.Object, mockDice.Object);
            Assert.That(character, Is.Not.Null);
        }
    }
}