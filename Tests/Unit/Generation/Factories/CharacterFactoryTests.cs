using Moq;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Data.Stats;
using NPCGen.Core.Generation.Factories;
using NPCGen.Core.Generation.Factories.Interfaces;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Randomizers.Alignments.Interfaces;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;
using NPCGen.Core.Generation.Randomizers.Stats.Interfaces;
using NPCGen.Core.Generation.Verifiers.Exceptions;
using NPCGen.Core.Generation.Verifiers.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace NPCGen.Tests.Unit.Generation.Factories
{
    [TestFixture]
    public class CharacterFactoryTests
    {
        private Mock<IAlignmentFactory> mockAlignmentFactory;
        private Mock<ICharacterClassFactory> mockCharacterClassFactory;
        private Mock<IRaceFactory> mockRaceFactory;
        private Mock<IStatsFactory> mockStatsFactory;
        private Mock<ILanguageFactory> mockLanguageFactory;
        private Mock<IHitPointsFactory> mockHitPointsFactory;
        private Mock<ILevelAdjustmentsProvider> mockLevelAdjustmentsProvider;
        private Mock<IRandomizerVerifier> mockRandomizerVerifier;
        private Mock<IPercentileResultProvider> mockPercentileResultProvider;
        private ICharacterFactory characterFactory;

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
            mockLevelAdjustmentsProvider = new Mock<ILevelAdjustmentsProvider>();
            mockLevelAdjustmentsProvider.Setup(p => p.GetLevelAdjustments()).Returns(adjustments);

            mockRandomizerVerifier = new Mock<IRandomizerVerifier>();
            mockRandomizerVerifier.Setup(v => v.VerifyCompatibility(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object,
                mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object)).Returns(true);
            mockRandomizerVerifier.Setup(v => v.VerifyAlignmentCompatibility(It.IsAny<Alignment>(), mockClassNameRandomizer.Object,
                mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object)).Returns(true);
            mockRandomizerVerifier.Setup(v => v.VerifyCharacterClassCompatibility(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>(),
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object)).Returns(true);

            mockPercentileResultProvider = new Mock<IPercentileResultProvider>();

            characterFactory = new CharacterFactory(mockAlignmentFactory.Object, mockCharacterClassFactory.Object, mockRaceFactory.Object, mockStatsFactory.Object, mockLanguageFactory.Object, mockHitPointsFactory.Object, mockLevelAdjustmentsProvider.Object, mockRandomizerVerifier.Object, mockPercentileResultProvider.Object);
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
            mockAlignmentFactory = new Mock<IAlignmentFactory>();
            mockAlignmentFactory.Setup(f => f.CreateWith(mockAlignmentRandomizer.Object)).Returns(new Alignment());

            characterClassPrototype = new CharacterClassPrototype();
            characterClassPrototype.Level = 1;
            mockCharacterClassFactory = new Mock<ICharacterClassFactory>();
            mockCharacterClassFactory.Setup(f => f.CreatePrototypeWith(It.IsAny<Alignment>(), mockLevelRandomizer.Object,
                mockClassNameRandomizer.Object)).Returns(characterClassPrototype);
            mockCharacterClassFactory.Setup(f => f.CreateWith(characterClassPrototype)).Returns(new CharacterClass());

            race = new Race();
            race.BaseRace = BaseRace;
            race.Metarace = String.Empty;
            mockRaceFactory = new Mock<IRaceFactory>();
            mockRaceFactory.Setup(f => f.CreateWith(It.IsAny<String>(), characterClassPrototype, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object)).Returns(race);

            var stats = new Dictionary<String, Stat>();
            foreach (var stat in StatConstants.GetStats())
                stats.Add(stat, new Stat());
            mockStatsFactory = new Mock<IStatsFactory>();
            mockStatsFactory.Setup(f => f.CreateWith(mockStatsRandomizer.Object, It.IsAny<CharacterClass>(), It.IsAny<Race>())).Returns(stats);

            mockHitPointsFactory = new Mock<IHitPointsFactory>();
            mockLanguageFactory = new Mock<ILanguageFactory>();
        }

        [Test, ExpectedException(typeof(IncompatibleRandomizersException))]
        public void InvalidRandomizersThrowsException()
        {
            mockRandomizerVerifier.Setup(v => v.VerifyCompatibility(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object,
                mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object)).Returns(false);

            characterFactory.CreateUsing(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockStatsRandomizer.Object);
        }

        [Test]
        public void IncompatibleAlignmentIsRegenerated()
        {
            mockRandomizerVerifier.SetupSequence(v => v.VerifyAlignmentCompatibility(It.IsAny<Alignment>(), mockClassNameRandomizer.Object,
                mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object)).Returns(false).Returns(true);

            characterFactory.CreateUsing(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockStatsRandomizer.Object);
            mockAlignmentFactory.Verify(f => f.CreateWith(mockAlignmentRandomizer.Object), Times.Exactly(2));
        }

        [Test]
        public void IncompatibleCharacterClassIsRegenerated()
        {
            mockRandomizerVerifier.SetupSequence(v => v.VerifyCharacterClassCompatibility(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>(),
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object)).Returns(false).Returns(true);

            characterFactory.CreateUsing(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockStatsRandomizer.Object);
            mockCharacterClassFactory.Verify(f => f.CreatePrototypeWith(It.IsAny<Alignment>(), mockLevelRandomizer.Object,
                mockClassNameRandomizer.Object), Times.Exactly(2));
        }

        [Test]
        public void IncompatibleBaseRaceIsRegenerated()
        {
            mockRaceFactory.SetupSequence(f => f.CreateWith(It.IsAny<String>(), characterClassPrototype, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object)).Returns(new Race() { BaseRace = BaseRacePlusOne, Metarace = String.Empty }).Returns(race);

            characterFactory.CreateUsing(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockStatsRandomizer.Object);
            mockRaceFactory.Verify(f => f.CreateWith(It.IsAny<String>(), characterClassPrototype, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object), Times.Exactly(2));
        }

        [Test]
        public void IncompatibleMetaraceIsRegenerated()
        {
            mockRaceFactory.SetupSequence(f => f.CreateWith(It.IsAny<String>(), characterClassPrototype, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object)).Returns(new Race() { BaseRace = BaseRace, Metarace = Metarace }).Returns(race);

            characterFactory.CreateUsing(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
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

            characterFactory.CreateUsing(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
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

            characterFactory.CreateUsing(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockStatsRandomizer.Object);
            mockRaceFactory.Verify(f => f.CreateWith(It.IsAny<String>(), characterClassPrototype, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object), Times.Exactly(2));
        }

        [Test]
        public void AppliesBaseRaceLevelAdjustment()
        {
            characterClassPrototype.Level = 2;
            race.BaseRace = BaseRacePlusOne;

            characterFactory.CreateUsing(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockStatsRandomizer.Object);
            Assert.That(characterClassPrototype.Level, Is.EqualTo(1));
        }

        [Test]
        public void AppliesMetaraceLevelAdjustment()
        {
            characterClassPrototype.Level = 2;
            race.Metarace = Metarace;

            characterFactory.CreateUsing(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockStatsRandomizer.Object);
            Assert.That(characterClassPrototype.Level, Is.EqualTo(1));
        }

        [Test]
        public void ApplyBaseRaceAndMetaraceLevelAdjustments()
        {
            race.BaseRace = BaseRacePlusOne;
            race.Metarace = Metarace;
            characterClassPrototype.Level = 3;

            characterFactory.CreateUsing(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockStatsRandomizer.Object);
            Assert.That(characterClassPrototype.Level, Is.EqualTo(1));
        }

        [Test]
        public void GetsInterestingTraitFromPercentileResultProvider()
        {
            mockPercentileResultProvider.Setup(p => p.GetPercentileResult("Traits")).Returns("interesting trait");
            var character = characterFactory.CreateUsing(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockStatsRandomizer.Object);
            Assert.That(character.InterestingTrait, Is.EqualTo("interesting trait"));
        }
    }
}