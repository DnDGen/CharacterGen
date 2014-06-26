using System;
using System.Collections.Generic;
using Moq;
using NPCGen.Common;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Feats;
using NPCGen.Common.Races;
using NPCGen.Common.Skills;
using NPCGen.Common.Stats;
using NPCGen.Generators;
using NPCGen.Generators.Interfaces;
using NPCGen.Generators.Interfaces.Randomizers.Alignments;
using NPCGen.Generators.Interfaces.Randomizers.CharacterClasses;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NPCGen.Generators.Interfaces.Randomizers.Stats;
using NPCGen.Generators.Interfaces.Verifiers;
using NPCGen.Generators.Interfaces.Verifiers.Exceptions;
using NPCGen.Selectors.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators
{
    [TestFixture]
    public class CharacterGeneratorTests
    {
        private const String BaseRace = "base race";
        private const String BaseRacePlusOne = "base race +1";
        private const String Metarace = "metarace";

        private Mock<IAlignmentGenerator> mockAlignmentGenerator;
        private Mock<ICharacterClassGenerator> mockCharacterClassGenerator;
        private Mock<IRaceGenerator> mockRaceGenerator;
        private Mock<IStatsGenerator> mockStatsGenerator;
        private Mock<ILanguageGenerator> mockLanguageGenerator;
        private Mock<IHitPointsGenerator> mockHitPointsGenerator;
        private Mock<ILevelAdjustmentsSelector> mockLevelAdjustmentsSelector;
        private Mock<IRandomizerVerifier> mockRandomizerVerifier;
        private Mock<IPercentileSelector> mockPercentileSelector;
        private ICharacterGenerator characterGenerator;
        private Mock<ISkillsGenerator> mockSkillsGenerator;
        private Mock<IFeatsGenerator> mockFeatsGenerator;

        private Mock<IAlignmentRandomizer> mockAlignmentRandomizer;
        private Mock<IClassNameRandomizer> mockClassNameRandomizer;
        private Mock<ILevelRandomizer> mockLevelRandomizer;
        private Mock<IBaseRaceRandomizer> mockBaseRaceRandomizer;
        private Mock<IMetaraceRandomizer> mockMetaraceRandomizer;
        private Mock<IStatsRandomizer> mockStatsRandomizer;

        private CharacterClassPrototype characterClassPrototype;
        private Race race;
        private Dictionary<String, Int32> adjustments;
        private Dictionary<String, Stat> stats;

        [SetUp]
        public void Setup()
        {
            SetUpMockRandomizers();
            SetUpGenerators();

            mockLevelAdjustmentsSelector = new Mock<ILevelAdjustmentsSelector>();
            adjustments = new Dictionary<String, Int32>();
            mockRandomizerVerifier = new Mock<IRandomizerVerifier>();
            mockPercentileSelector = new Mock<IPercentileSelector>();

            adjustments.Add(BaseRace, 0);
            adjustments.Add(BaseRacePlusOne, 1);
            adjustments.Add(String.Empty, 0);
            adjustments.Add(Metarace, 1);
            mockLevelAdjustmentsSelector.Setup(p => p.GetAdjustments()).Returns(adjustments);

            mockRandomizerVerifier.Setup(v => v.VerifyCompatibility(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object,
                mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object)).Returns(true);
            mockRandomizerVerifier.Setup(v => v.VerifyAlignmentCompatibility(It.IsAny<Alignment>(), mockClassNameRandomizer.Object,
                mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object)).Returns(true);
            mockRandomizerVerifier.Setup(v => v.VerifyCharacterClassCompatibility(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>(),
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object)).Returns(true);

            characterGenerator = new CharacterGenerator(mockAlignmentGenerator.Object, mockCharacterClassGenerator.Object, mockRaceGenerator.Object, mockStatsGenerator.Object, mockLanguageGenerator.Object, mockHitPointsGenerator.Object, mockLevelAdjustmentsSelector.Object, mockRandomizerVerifier.Object, mockPercentileSelector.Object);
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

        private void SetUpGenerators()
        {
            mockAlignmentGenerator = new Mock<IAlignmentGenerator>();
            characterClassPrototype = new CharacterClassPrototype();
            mockCharacterClassGenerator = new Mock<ICharacterClassGenerator>();
            race = new Race();
            mockRaceGenerator = new Mock<IRaceGenerator>();
            stats = new Dictionary<String, Stat>();
            mockStatsGenerator = new Mock<IStatsGenerator>();
            mockHitPointsGenerator = new Mock<IHitPointsGenerator>();
            mockLanguageGenerator = new Mock<ILanguageGenerator>();
            mockSkillsGenerator = new Mock<ISkillsGenerator>();
            mockFeatsGenerator = new Mock<IFeatsGenerator>();

            mockAlignmentGenerator.Setup(f => f.GenerateWith(mockAlignmentRandomizer.Object)).Returns(new Alignment());

            characterClassPrototype.Level = 1;
            mockCharacterClassGenerator.Setup(f => f.GeneratePrototypeWith(It.IsAny<Alignment>(), mockLevelRandomizer.Object,
                mockClassNameRandomizer.Object)).Returns(characterClassPrototype);
            mockCharacterClassGenerator.Setup(f => f.GenerateWith(characterClassPrototype)).Returns(new CharacterClass());

            race.BaseRace = BaseRace;
            race.Metarace = String.Empty;
            mockRaceGenerator.Setup(f => f.GenerateWith(It.IsAny<String>(), characterClassPrototype, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object)).Returns(race);

            foreach (var stat in StatConstants.GetStats())
                stats.Add(stat, new Stat());
            mockStatsGenerator.Setup(f => f.GenerateWith(mockStatsRandomizer.Object, It.IsAny<CharacterClass>(), race)).Returns(stats);
        }

        [Test]
        public void InvalidRandomizersThrowsException()
        {
            mockRandomizerVerifier.Setup(v => v.VerifyCompatibility(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object,
                mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object)).Returns(false);

            Assert.That(() => GenerateCharacter(), Throws.InstanceOf<IncompatibleRandomizersException>());
        }

        private Character GenerateCharacter()
        {
            return characterGenerator.GenerateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockStatsRandomizer.Object);
        }

        [Test]
        public void IncompatibleAlignmentIsRegenerated()
        {
            mockRandomizerVerifier.SetupSequence(v => v.VerifyAlignmentCompatibility(It.IsAny<Alignment>(), mockClassNameRandomizer.Object,
                mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object)).Returns(false).Returns(true);

            GenerateCharacter();
            mockAlignmentGenerator.Verify(f => f.GenerateWith(mockAlignmentRandomizer.Object), Times.Exactly(2));
        }

        [Test]
        public void IncompatibleCharacterClassIsRegenerated()
        {
            mockRandomizerVerifier.SetupSequence(v => v.VerifyCharacterClassCompatibility(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>(),
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object)).Returns(false).Returns(true);

            GenerateCharacter();
            mockCharacterClassGenerator.Verify(f => f.GeneratePrototypeWith(It.IsAny<Alignment>(), mockLevelRandomizer.Object,
                mockClassNameRandomizer.Object), Times.Exactly(2));
        }

        [Test]
        public void IncompatibleBaseRaceIsRegenerated()
        {
            mockRaceGenerator.SetupSequence(f => f.GenerateWith(It.IsAny<String>(), characterClassPrototype, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object)).Returns(new Race() { BaseRace = BaseRacePlusOne, Metarace = String.Empty }).Returns(race);

            GenerateCharacter();
            mockRaceGenerator.Verify(f => f.GenerateWith(It.IsAny<String>(), characterClassPrototype, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object), Times.Exactly(2));
        }

        [Test]
        public void IncompatibleMetaraceIsRegenerated()
        {
            mockRaceGenerator.SetupSequence(f => f.GenerateWith(It.IsAny<String>(), characterClassPrototype, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object)).Returns(new Race() { BaseRace = BaseRace, Metarace = Metarace }).Returns(race);

            GenerateCharacter();
            mockRaceGenerator.Verify(f => f.GenerateWith(It.IsAny<String>(), characterClassPrototype, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object), Times.Exactly(2));
        }

        [Test]
        public void IncompatibleComboOfBaseRaceAndMetaraceIsRegeneratedWithCompatibleBaseRace()
        {
            var higherRace = new Race { BaseRace = BaseRacePlusOne, Metarace = Metarace };
            var normalRace = new Race { BaseRace = BaseRace, Metarace = Metarace };

            mockRaceGenerator.SetupSequence(f => f.GenerateWith(It.IsAny<String>(), characterClassPrototype, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object)).Returns(higherRace).Returns(normalRace);

            characterClassPrototype.Level = 2;

            GenerateCharacter();
            mockRaceGenerator.Verify(f => f.GenerateWith(It.IsAny<String>(), characterClassPrototype, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object), Times.Exactly(2));
        }

        [Test]
        public void IncompatibleComboOfBaseRaceAndMetaraceIsRegeneratedWithCompatibleMetarace()
        {
            var higherRace = new Race { BaseRace = BaseRacePlusOne, Metarace = Metarace };
            var normalRace = new Race { BaseRace = BaseRacePlusOne, Metarace = String.Empty };

            mockRaceGenerator.SetupSequence(f => f.GenerateWith(It.IsAny<String>(), characterClassPrototype, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object)).Returns(higherRace).Returns(normalRace);

            characterClassPrototype.Level = 2;

            GenerateCharacter();
            mockRaceGenerator.Verify(f => f.GenerateWith(It.IsAny<String>(), characterClassPrototype, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object), Times.Exactly(2));
        }

        [Test]
        public void AppliesBaseRaceLevelAdjustment()
        {
            characterClassPrototype.Level = 2;
            race.BaseRace = BaseRacePlusOne;

            GenerateCharacter();
            Assert.That(characterClassPrototype.Level, Is.EqualTo(1));
        }

        [Test]
        public void AppliesMetaraceLevelAdjustment()
        {
            characterClassPrototype.Level = 2;
            race.Metarace = Metarace;

            GenerateCharacter();
            Assert.That(characterClassPrototype.Level, Is.EqualTo(1));
        }

        [Test]
        public void ApplyBaseRaceAndMetaraceLevelAdjustments()
        {
            race.BaseRace = BaseRacePlusOne;
            race.Metarace = Metarace;
            characterClassPrototype.Level = 3;

            GenerateCharacter();
            Assert.That(characterClassPrototype.Level, Is.EqualTo(1));
        }

        [Test]
        public void GetsInterestingTraitFromPercentileResultSelector()
        {
            mockPercentileSelector.Setup(p => p.GetPercentileFrom("Traits")).Returns("interesting trait");
            var character = GenerateCharacter();
            Assert.That(character.InterestingTrait, Is.EqualTo("interesting trait"));
        }

        [Test]
        public void GetStatsFromGenerator()
        {
            var character = GenerateCharacter();
            Assert.That(character.Stats, Is.EqualTo(stats));
        }

        [Test]
        public void GetLanguagesFromGenerator()
        {
            var languages = new List<String>();
            mockLanguageGenerator.Setup(g => g.GenerateWith(race, characterClassPrototype.ClassName, It.IsAny<Int32>())).Returns(languages);

            var character = GenerateCharacter();
            Assert.That(character.Languages, Is.EqualTo(languages));
        }

        [Test]
        public void GetHitPointsFromGenerator()
        {
            mockHitPointsGenerator.Setup(g => g.GenerateWith(It.IsAny<CharacterClass>(), It.IsAny<Int32>(), race)).Returns(9266);
            var character = GenerateCharacter();
            Assert.That(character.HitPoints, Is.EqualTo(9266));
        }

        [Test]
        public void GetSkillsFromSkillsGenerator()
        {
            var skills = new Dictionary<String, Skill>();
            mockSkillsGenerator.Setup(g => g.GenerateWith(It.IsAny<CharacterClass>(), race, stats)).Returns(skills);

            var character = GenerateCharacter();
            Assert.That(character.Skills, Is.EqualTo(skills));
        }

        [Test]
        public void GetFeatsFromFeatsGenerator()
        {
            var feats = new List<Feat>();
            mockFeatsGenerator.Setup(g => g.GenerateWith(It.IsAny<CharacterClass>(), race, stats)).Returns(feats);

            var character = GenerateCharacter();
            Assert.That(character.Feats, Is.EqualTo(feats));
        }

        [Test]
        public void AdjustSkillsByFeat()
        {
            Assert.Fail();
        }

        [Test]
        public void AdjustSavesByFeat()
        {
            Assert.Fail();
        }

        [Test]
        public void GetSavingThrowsFromSelector()
        {
            Assert.Fail();
        }

        [Test]
        public void GetArmorFromGenerator()
        {
            Assert.Fail();
        }

        [Test]
        public void AdjustSkillsWithArmorCheckPenalty()
        {
            Assert.Fail();
        }

        [Test]
        public void DoNotAdjustSkillsWithoutArmorCheckPenalty()
        {
            Assert.Fail();
        }

        [Test]
        public void GetArmorClassFromGenerator()
        {
            Assert.Fail();
        }

        [Test]
        public void GetPrimaryHandFromGenerator()
        {
            Assert.Fail();
        }

        [Test]
        public void NotHavingPrimaryHandIsNotAllowed()
        {
            Assert.Fail();
        }

        [Test]
        public void GetFamiliarFromGenerator()
        {
            Assert.Fail();
        }

        [Test]
        public void GetTreasureFromGenerator()
        {
            Assert.Fail();
        }

        [Test]
        public void IfPrimaryHandIsTwoHanded_OffHandIsEqualToPrimaryHand()
        {
            //set it up for both two-handed and shields

            var character = characterGenerator.GenerateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockStatsRandomizer.Object);
            Assert.That(character.OffHand, Is.EqualTo(character.PrimaryHand));
        }

        [Test]
        public void IfCharacterHasTwoWeaponFighting_GenerateSecondaryWeapon()
        {
            Assert.Fail();
        }

        [Test]
        public void IfCharacterHasTwoWeaponFighting_NotHavingASecondWeaponIsAllowed()
        {
            Assert.Fail();
        }

        [Test]
        public void IfCharacterDoesNotHaveTwoWeaponFighting_DoNotGenerateSecondaryWeapon()
        {
            Assert.Fail();
        }

        [Test]
        public void IfCharacterHasTwoWeaponFighting_SecondWeaponCannotBeTwoHanded()
        {
            Assert.Fail();
        }

        [Test]
        public void IfCharacterHasShieldProficiencyAndNoSecondWeapon_GenerateShield()
        {
            Assert.Fail();
        }

        [Test]
        public void IfCharacterHasShieldProficiencyAndSecondWeapon_DoNotGenerateShield()
        {
            Assert.Fail();
        }

        [Test]
        public void IfCharacterHasShieldProficiencyAndNoSecondWeapon_NotHavingAShieldIsAllowed()
        {
            Assert.Fail();
        }

        [Test]
        public void IfCharacterDoesNotHaveShieldProficiency_DoNotGenerateShield()
        {
            Assert.Fail();
        }

        [Test]
        public void GetSpellsFromGenerator()
        {
            Assert.Fail();
        }
    }
}