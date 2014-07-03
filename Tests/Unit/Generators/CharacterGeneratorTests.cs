using System;
using System.Collections.Generic;
using EquipmentGen.Common.Items;
using Moq;
using NPCGen.Common;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Common.Abilities;
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
using NPCGen.Common.Abilities.Stats;
using NPCGen.Generators.Interfaces.Abilities;
using NPCGen.Generators.Interfaces.Combats;
using NPCGen.Common.Combats;
using NPCGen.Common.Abilities.Feats;

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
        private Mock<IAbilitiesGenerator> mockAbilitiesGenerator;
        private Mock<ICombatGenerator> mockCombatGenerator;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private Mock<IRandomizerVerifier> mockRandomizerVerifier;
        private Mock<IPercentileSelector> mockPercentileSelector;
        private ICharacterGenerator characterGenerator;
        private Mock<IAlignmentRandomizer> mockAlignmentRandomizer;
        private Mock<IClassNameRandomizer> mockClassNameRandomizer;
        private Mock<ILevelRandomizer> mockLevelRandomizer;
        private Mock<IBaseRaceRandomizer> mockBaseRaceRandomizer;
        private Mock<IMetaraceRandomizer> mockMetaraceRandomizer;
        private Mock<IStatsRandomizer> mockStatsRandomizer;

        private CharacterClass characterClass;
        private Race race;
        private Dictionary<String, Int32> adjustments;
        private Dictionary<String, Stat> stats;

        [SetUp]
        public void Setup()
        {
            SetUpMockRandomizers();
            SetUpGenerators();

            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            adjustments = new Dictionary<String, Int32>();
            mockRandomizerVerifier = new Mock<IRandomizerVerifier>();
            mockPercentileSelector = new Mock<IPercentileSelector>();

            adjustments.Add(BaseRace, 0);
            adjustments.Add(BaseRacePlusOne, 1);
            adjustments.Add(String.Empty, 0);
            adjustments.Add(Metarace, 1);
            mockAdjustmentsSelector.Setup(p => p.GetAdjustmentsFrom("LevelAdjustments")).Returns(adjustments);

            mockRandomizerVerifier.Setup(v => v.VerifyCompatibility(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object,
                mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object)).Returns(true);
            mockRandomizerVerifier.Setup(v => v.VerifyAlignmentCompatibility(It.IsAny<Alignment>(), mockClassNameRandomizer.Object,
                mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object)).Returns(true);
            mockRandomizerVerifier.Setup(v => v.VerifyCharacterClassCompatibility(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>(),
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object)).Returns(true);

            characterGenerator = new CharacterGenerator(mockAlignmentGenerator.Object, mockCharacterClassGenerator.Object, mockRaceGenerator.Object, mockStatsGenerator.Object, mockLanguageGenerator.Object, mockHitPointsGenerator.Object, mockAdjustmentsSelector.Object, mockRandomizerVerifier.Object, mockPercentileSelector.Object);
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
            characterClass = new CharacterClass();
            mockCharacterClassGenerator = new Mock<ICharacterClassGenerator>();
            race = new Race();
            mockRaceGenerator = new Mock<IRaceGenerator>();
            stats = new Dictionary<String, Stat>();
            mockAbilitiesGenerator = new Mock<IAbilitiesGenerator>();
            mockCombatGenerator = new Mock<ICombatGenerator>();

            mockAlignmentGenerator.Setup(f => f.GenerateWith(mockAlignmentRandomizer.Object)).Returns(new Alignment());

            characterClass.Level = 1;
            mockCharacterClassGenerator.Setup(f => f.GenerateWith(It.IsAny<Alignment>(), mockLevelRandomizer.Object,
                mockClassNameRandomizer.Object)).Returns(characterClass);

            race.BaseRace = BaseRace;
            race.Metarace = String.Empty;
            mockRaceGenerator.Setup(f => f.GenerateWith(It.IsAny<String>(), characterClass, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object)).Returns(race);
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
            mockRandomizerVerifier.SetupSequence(v => v.VerifyCharacterClassCompatibility(It.IsAny<String>(), It.IsAny<CharacterClass>(),
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object)).Returns(false).Returns(true);

            GenerateCharacter();
            mockCharacterClassGenerator.Verify(f => f.GenerateWith(It.IsAny<Alignment>(), mockLevelRandomizer.Object,
                mockClassNameRandomizer.Object), Times.Exactly(2));
        }

        [Test]
        public void IncompatibleBaseRaceIsRegenerated()
        {
            mockRaceGenerator.SetupSequence(f => f.GenerateWith(It.IsAny<String>(), characterClass, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object)).Returns(new Race() { BaseRace = BaseRacePlusOne, Metarace = String.Empty }).Returns(race);

            GenerateCharacter();
            mockRaceGenerator.Verify(f => f.GenerateWith(It.IsAny<String>(), characterClass, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object), Times.Exactly(2));
        }

        [Test]
        public void IncompatibleMetaraceIsRegenerated()
        {
            mockRaceGenerator.SetupSequence(f => f.GenerateWith(It.IsAny<String>(), characterClass, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object)).Returns(new Race() { BaseRace = BaseRace, Metarace = Metarace }).Returns(race);

            GenerateCharacter();
            mockRaceGenerator.Verify(f => f.GenerateWith(It.IsAny<String>(), characterClass, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object), Times.Exactly(2));
        }

        [Test]
        public void IncompatibleComboOfBaseRaceAndMetaraceIsRegeneratedWithCompatibleBaseRace()
        {
            var higherRace = new Race { BaseRace = BaseRacePlusOne, Metarace = Metarace };
            var normalRace = new Race { BaseRace = BaseRace, Metarace = Metarace };

            mockRaceGenerator.SetupSequence(f => f.GenerateWith(It.IsAny<String>(), characterClass, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object)).Returns(higherRace).Returns(normalRace);

            characterClass.Level = 2;

            GenerateCharacter();
            mockRaceGenerator.Verify(f => f.GenerateWith(It.IsAny<String>(), characterClass, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object), Times.Exactly(2));
        }

        [Test]
        public void IncompatibleComboOfBaseRaceAndMetaraceIsRegeneratedWithCompatibleMetarace()
        {
            var higherRace = new Race { BaseRace = BaseRacePlusOne, Metarace = Metarace };
            var normalRace = new Race { BaseRace = BaseRacePlusOne, Metarace = String.Empty };

            mockRaceGenerator.SetupSequence(f => f.GenerateWith(It.IsAny<String>(), characterClass, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object)).Returns(higherRace).Returns(normalRace);

            characterClass.Level = 2;

            GenerateCharacter();
            mockRaceGenerator.Verify(f => f.GenerateWith(It.IsAny<String>(), characterClass, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object), Times.Exactly(2));
        }

        [Test]
        public void AppliesBaseRaceLevelAdjustment()
        {
            characterClass.Level = 2;
            race.BaseRace = BaseRacePlusOne;

            GenerateCharacter();
            Assert.That(characterClass.Level, Is.EqualTo(1));
        }

        [Test]
        public void AppliesMetaraceLevelAdjustment()
        {
            characterClass.Level = 2;
            race.Metarace = Metarace;

            GenerateCharacter();
            Assert.That(characterClass.Level, Is.EqualTo(1));
        }

        [Test]
        public void ApplyBaseRaceAndMetaraceLevelAdjustments()
        {
            race.BaseRace = BaseRacePlusOne;
            race.Metarace = Metarace;
            characterClass.Level = 3;

            GenerateCharacter();
            Assert.That(characterClass.Level, Is.EqualTo(1));
        }

        [Test]
        public void GetsInterestingTraitFromPercentileResultSelector()
        {
            mockPercentileSelector.Setup(p => p.GetPercentileFrom("Traits")).Returns("interesting trait");
            var character = GenerateCharacter();
            Assert.That(character.InterestingTrait, Is.EqualTo("interesting trait"));
        }

        [Test]
        public void GetAbilityFromGenerator()
        {
            var ability = new Ability();
            mockAbilitiesGenerator.Setup(g => g.GenerateWith(characterClass, race, mockStatsRandomizer.Object)).Returns(ability);

            var character = GenerateCharacter();
            Assert.That(character.Ability, Is.EqualTo(ability));
        }

        [Test]
        public void GetCombatFromGenerator()
        {
            var ability = new Ability();
            mockAbilitiesGenerator.Setup(g => g.GenerateWith(characterClass, race, mockStatsRandomizer.Object)).Returns(ability);

            var combat = new Combat();
            mockCombatGenerator.Setup(g => g.GenerateWith(characterClass, ability.Feats, ability.Stats)).Returns(combat);
            var character = GenerateCharacter();
            Assert.That(character.Combat, Is.EqualTo(combat));
        }

        [Test]
        public void GetEquipmentFromGenerator()
        {
            var armor = new Item();
            mockArmorGenerator.Setup(g => g.GenerateAtLevel(It.IsAny<Int32>())).Returns(armor);

            var character = GenerateCharacter();
            Assert.That(character.Armor, Is.EqualTo(armor));
        }

        [Test]
        public void AdjustSkillsWithArmorCheckPenalty()
        {
            var skills = new Dictionary<String, Skill>();
            var skillWithPenalty = new Skill { ArmorCheckPenalty = true };
            var skill = new Skill { ArmorCheckPenalty = false };

            skills.Add("no penalty", skill);
            skills.Add("penalty", skillWithPenalty);

            mockSkillsGenerator.Setup(g => g.GenerateWith(It.IsAny<CharacterClass>(), race, stats)).Returns(skills);

            var armor = new Item { Name = "armor name" };
            mockArmorGenerator.Setup(g => g.GenerateAtLevel(It.IsAny<Int32>())).Returns(armor);

            var skillAdjustments = new Dictionary<String, Int32>();
            skillAdjustments.Add(armor.Name, 5);
            skillAdjustments.Add("other armor", 10);
            mockAdjustmentsSelector.Setup(s => s.GetAdjustmentsFrom("ArmorCheckPenalties")).Returns(skillAdjustments);

            var character = GenerateCharacter();
            Assert.That(character.Skills["no penalty"].Bonus, Is.EqualTo(0));
            Assert.That(character.Skills["penalty"].Bonus, Is.EqualTo(-5));
        }

        [Test]
        public void SwimmingTakesDoubleArmorCheckPenalty()
        {
            var skills = new Dictionary<String, Skill>();
            var swimming = new Skill { ArmorCheckPenalty = true };
            skills.Add(SkillConstants.Swim, swimming);

            mockSkillsGenerator.Setup(g => g.GenerateWith(It.IsAny<CharacterClass>(), race, stats)).Returns(skills);

            var armor = new Item { Name = "armor name" };
            mockArmorGenerator.Setup(g => g.GenerateAtLevel(It.IsAny<Int32>())).Returns(armor);

            var skillAdjustments = new Dictionary<String, Int32>();
            skillAdjustments.Add(armor.Name, 5);
            mockAdjustmentsSelector.Setup(s => s.GetAdjustmentsFrom("ArmorCheckPenalties")).Returns(skillAdjustments);

            var character = GenerateCharacter();
            Assert.That(character.Skills[SkillConstants.Swim].Bonus, Is.EqualTo(-10));
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