using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using NPCGen.Generators.Interfaces;
using NPCGen.Selectors.Interfaces;
using NPCGen.Generators.Interfaces.Verifiers;
using NPCGen.Generators.Interfaces.Randomizers.Alignments;
using NPCGen.Generators.Interfaces.Randomizers.CharacterClasses;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NPCGen.Generators.Interfaces.Randomizers.Stats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Common.Alignments;
using NPCGen.Generators;
using NPCGen.Common.Stats;
using NPCGen.Generators.Interfaces.Verifiers.Exceptions;

namespace NPCGen.Tests.Unit.Generators
{
    [TestFixture]
    public class CharacterGeneratorTests
    {
        private Mock<IAlignmentGenerator> mockAlignmentFactory;
        private Mock<ICharacterClassGenerator> mockCharacterClassFactory;
        private Mock<IRaceGenerator> mockRaceFactory;
        private Mock<IStatsGenerator> mockStatsFactory;
        private Mock<ILanguageGenerator> mockLanguageFactory;
        private Mock<IHitPointsGenerator> mockHitPointsFactory;
        private Mock<ILevelAdjustmentsSelector> mockLevelAdjustmentsProvider;
        private Mock<IRandomizerVerifier> mockRandomizerVerifier;
        private Mock<IPercentileSelector> mockPercentileResultProvider;
        private ICharacterGenerator characterFactory;

        private Mock<IAlignmentRandomizer> mockAlignmentRandomizer;
        private Mock<IClassNameRandomizer> mockClassNameRandomizer;
        private Mock<ILevelRandomizer> mockLevelRandomizer;
        private Mock<IBaseRaceRandomizer> mockBaseRaceRandomizer;
        private Mock<IMetaraceRandomizer> mockMetaraceRandomizer;
        private Mock<IStatsRandomizer> mockStatsRandomizer;

        private CharacterClassPrototype characterClassPrototype;
        private Race race;
        private Dictionary<String, Int32> adjustments;
        private const String BaseRace = "base race";
        private const String BaseRacePlusOne = "base race +1";
        private const String Metarace = "metarace";

        [SetUp]
        public void Setup()
        {
            SetUpMockRandomizers();
            SetUpFactories();

            adjustments = new Dictionary<String, Int32>();
            adjustments.Add(BaseRace, 0);
            adjustments.Add(String.Empty, 0);
            adjustments.Add(BaseRacePlusOne, 1);
            adjustments.Add(Metarace, 1);
            mockLevelAdjustmentsProvider = new Mock<ILevelAdjustmentsSelector>();
            mockLevelAdjustmentsProvider.Setup(p => p.GetLevelAdjustments()).Returns(adjustments);

            mockRandomizerVerifier = new Mock<IRandomizerVerifier>();
            mockRandomizerVerifier.Setup(v => v.VerifyCompatibility(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object,
                mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object)).Returns(true);
            mockRandomizerVerifier.Setup(v => v.VerifyAlignmentCompatibility(It.IsAny<Alignment>(), mockClassNameRandomizer.Object,
                mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object)).Returns(true);
            mockRandomizerVerifier.Setup(v => v.VerifyCharacterClassCompatibility(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>(),
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object)).Returns(true);

            mockPercentileResultProvider = new Mock<IPercentileSelector>();

            characterFactory = new CharacterGenerator(mockAlignmentFactory.Object, mockCharacterClassFactory.Object, mockRaceFactory.Object, mockStatsFactory.Object, mockLanguageFactory.Object, mockHitPointsFactory.Object, mockLevelAdjustmentsProvider.Object, mockRandomizerVerifier.Object, mockPercentileResultProvider.Object);
        }

        private void SetUpMockRandomizers()
        {
            mockAlignmentRandomizer = new Mock<IAlignmentRandomizer>();
            mockClassNameRandomizer = new Mock<IClassNameRandomizer>();
            mockLevelRandomizer = new Mock<ILevelRandomizer>();
            mockStatsRandomizer = new Mock<IStatsRandomizer>();
            mockBaseRaceRandomizer = new Mock<IBaseRaceRandomizer>();
            mockMetaraceRandomizer = new Mock<IMetaraceRandomizer>();
        }

        private void SetUpFactories()
        {
            mockAlignmentFactory = new Mock<IAlignmentGenerator>();
            mockAlignmentFactory.Setup(f => f.CreateWith(mockAlignmentRandomizer.Object)).Returns(new Alignment());

            characterClassPrototype = new CharacterClassPrototype();
            characterClassPrototype.Level = 1;
            mockCharacterClassFactory = new Mock<ICharacterClassGenerator>();
            mockCharacterClassFactory.Setup(f => f.CreatePrototypeWith(It.IsAny<Alignment>(), mockLevelRandomizer.Object,
                mockClassNameRandomizer.Object)).Returns(characterClassPrototype);
            mockCharacterClassFactory.Setup(f => f.CreateWith(characterClassPrototype)).Returns(new CharacterClass());

            race = new Race();
            race.BaseRace = BaseRace;
            race.Metarace = String.Empty;
            mockRaceFactory = new Mock<IRaceGenerator>();
            mockRaceFactory.Setup(f => f.CreateWith(It.IsAny<String>(), characterClassPrototype, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object)).Returns(race);

            var stats = new Dictionary<String, Stat>();
            foreach (var stat in StatConstants.GetStats())
                stats.Add(stat, new Stat());
            mockStatsFactory = new Mock<IStatsGenerator>();
            mockStatsFactory.Setup(f => f.CreateWith(mockStatsRandomizer.Object, It.IsAny<CharacterClass>(), It.IsAny<Race>())).Returns(stats);

            mockHitPointsFactory = new Mock<IHitPointsGenerator>();
            mockLanguageFactory = new Mock<ILanguageGenerator>();
        }

        [Test, ExpectedException(typeof(IncompatibleRandomizersException))]
        public void InvalidRandomizersThrowsException()
        {
            mockRandomizerVerifier.Setup(v => v.VerifyCompatibility(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object,
                mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object)).Returns(false);

            characterFactory.CreateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockStatsRandomizer.Object);
        }

        [Test]
        public void IncompatibleAlignmentIsRegenerated()
        {
            mockRandomizerVerifier.SetupSequence(v => v.VerifyAlignmentCompatibility(It.IsAny<Alignment>(), mockClassNameRandomizer.Object,
                mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object)).Returns(false).Returns(true);

            characterFactory.CreateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockStatsRandomizer.Object);
            mockAlignmentFactory.Verify(f => f.CreateWith(mockAlignmentRandomizer.Object), Times.Exactly(2));
        }

        [Test]
        public void IncompatibleCharacterClassIsRegenerated()
        {
            mockRandomizerVerifier.SetupSequence(v => v.VerifyCharacterClassCompatibility(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>(),
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object)).Returns(false).Returns(true);

            characterFactory.CreateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockStatsRandomizer.Object);
            mockCharacterClassFactory.Verify(f => f.CreatePrototypeWith(It.IsAny<Alignment>(), mockLevelRandomizer.Object,
                mockClassNameRandomizer.Object), Times.Exactly(2));
        }

        [Test]
        public void IncompatibleBaseRaceIsRegenerated()
        {
            mockRaceFactory.SetupSequence(f => f.CreateWith(It.IsAny<String>(), characterClassPrototype, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object)).Returns(new Race() { BaseRace = BaseRacePlusOne, Metarace = String.Empty }).Returns(race);

            characterFactory.CreateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockStatsRandomizer.Object);
            mockRaceFactory.Verify(f => f.CreateWith(It.IsAny<String>(), characterClassPrototype, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object), Times.Exactly(2));
        }

        [Test]
        public void IncompatibleMetaraceIsRegenerated()
        {
            mockRaceFactory.SetupSequence(f => f.CreateWith(It.IsAny<String>(), characterClassPrototype, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object)).Returns(new Race() { BaseRace = BaseRace, Metarace = Metarace }).Returns(race);

            characterFactory.CreateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockStatsRandomizer.Object);
            mockRaceFactory.Verify(f => f.CreateWith(It.IsAny<String>(), characterClassPrototype, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object), Times.Exactly(2));
        }

        [Test]
        public void IncompatibleComboOfBaseRaceAndMetaraceIsRegeneratedWithCompatibleBaseRace()
        {
            mockRaceFactory.SetupSequence(f => f.CreateWith(It.IsAny<String>(), characterClassPrototype, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object)).Returns(new Race() { BaseRace = BaseRacePlusOne, Metarace = Metarace })
                .Returns(new Race() { BaseRace = BaseRace, Metarace = Metarace });

            characterClassPrototype.Level = 2;

            characterFactory.CreateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockStatsRandomizer.Object);
            mockRaceFactory.Verify(f => f.CreateWith(It.IsAny<String>(), characterClassPrototype, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object), Times.Exactly(2));
        }

        [Test]
        public void IncompatibleComboOfBaseRaceAndMetaraceIsRegeneratedWithCompatibleMetarace()
        {
            mockRaceFactory.SetupSequence(f => f.CreateWith(It.IsAny<String>(), characterClassPrototype, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object)).Returns(new Race() { BaseRace = BaseRacePlusOne, Metarace = Metarace })
                .Returns(new Race() { BaseRace = BaseRacePlusOne, Metarace = String.Empty });

            characterClassPrototype.Level = 2;

            characterFactory.CreateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockStatsRandomizer.Object);
            mockRaceFactory.Verify(f => f.CreateWith(It.IsAny<String>(), characterClassPrototype, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object), Times.Exactly(2));
        }

        [Test]
        public void AppliesBaseRaceLevelAdjustment()
        {
            characterClassPrototype.Level = 2;
            race.BaseRace = BaseRacePlusOne;

            characterFactory.CreateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockStatsRandomizer.Object);
            Assert.That(characterClassPrototype.Level, Is.EqualTo(1));
        }

        [Test]
        public void AppliesMetaraceLevelAdjustment()
        {
            characterClassPrototype.Level = 2;
            race.Metarace = Metarace;

            characterFactory.CreateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockStatsRandomizer.Object);
            Assert.That(characterClassPrototype.Level, Is.EqualTo(1));
        }

        [Test]
        public void ApplyBaseRaceAndMetaraceLevelAdjustments()
        {
            race.BaseRace = BaseRacePlusOne;
            race.Metarace = Metarace;
            characterClassPrototype.Level = 3;

            characterFactory.CreateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockStatsRandomizer.Object);
            Assert.That(characterClassPrototype.Level, Is.EqualTo(1));
        }

        [Test]
        public void GetsInterestingTraitFromPercentileResultProvider()
        {
            mockPercentileResultProvider.Setup(p => p.GetPercentileResult("Traits")).Returns("interesting trait");
            var character = characterFactory.CreateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockStatsRandomizer.Object);
            Assert.That(character.InterestingTrait, Is.EqualTo("interesting trait"));
        }
    }
}