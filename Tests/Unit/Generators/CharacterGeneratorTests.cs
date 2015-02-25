using System;
using System.Collections.Generic;
using Moq;
using NPCGen.Common;
using NPCGen.Common.Abilities;
using NPCGen.Common.Abilities.Skills;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Combats;
using NPCGen.Common.Items;
using NPCGen.Common.Races;
using NPCGen.Generators;
using NPCGen.Generators.Interfaces;
using NPCGen.Generators.Interfaces.Abilities;
using NPCGen.Generators.Interfaces.Combats;
using NPCGen.Generators.Interfaces.Items;
using NPCGen.Generators.Interfaces.Randomizers.Alignments;
using NPCGen.Generators.Interfaces.Randomizers.CharacterClasses;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NPCGen.Generators.Interfaces.Randomizers.Stats;
using NPCGen.Generators.Interfaces.Verifiers;
using NPCGen.Generators.Interfaces.Verifiers.Exceptions;
using NPCGen.Selectors.Interfaces;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators
{
    [TestFixture]
    public class CharacterGeneratorTests
    {
        private const String BaseRaceId = "baserace";
        private const String BaseRacePlusOneId = "baserace+1";
        private const String MetaraceId = "metarace";

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
        private Mock<IEquipmentGenerator> mockEquipmentGenerator;

        private CharacterClass characterClass;
        private Race race;
        private Dictionary<String, Int32> adjustments;
        private Alignment alignment;
        private Ability ability;
        private Combat combat;
        private Equipment equipment;
        private BaseAttack baseAttack;

        [SetUp]
        public void Setup()
        {
            SetUpMockRandomizers();
            SetUpGenerators();

            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            adjustments = new Dictionary<String, Int32>();
            mockRandomizerVerifier = new Mock<IRandomizerVerifier>();
            mockPercentileSelector = new Mock<IPercentileSelector>();

            adjustments.Add(BaseRaceId, 0);
            adjustments.Add(BaseRacePlusOneId, 1);
            adjustments.Add(String.Empty, 0);
            adjustments.Add(MetaraceId, 1);
            mockAdjustmentsSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments)).Returns(adjustments);

            mockRandomizerVerifier.Setup(v => v.VerifyCompatibility(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object,
                mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object)).Returns(true);
            mockRandomizerVerifier.Setup(v => v.VerifyAlignmentCompatibility(It.IsAny<Alignment>(), mockClassNameRandomizer.Object,
                mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object)).Returns(true);
            mockRandomizerVerifier.Setup(v => v.VerifyCharacterClassCompatibility(It.IsAny<String>(), It.IsAny<CharacterClass>(),
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object)).Returns(true);

            characterGenerator = new CharacterGenerator(mockAlignmentGenerator.Object, mockCharacterClassGenerator.Object,
                mockRaceGenerator.Object, mockAdjustmentsSelector.Object, mockRandomizerVerifier.Object, mockPercentileSelector.Object,
                mockAbilitiesGenerator.Object, mockCombatGenerator.Object, mockEquipmentGenerator.Object);
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
            mockAbilitiesGenerator = new Mock<IAbilitiesGenerator>();
            mockCombatGenerator = new Mock<ICombatGenerator>();
            mockEquipmentGenerator = new Mock<IEquipmentGenerator>();
            mockCharacterClassGenerator = new Mock<ICharacterClassGenerator>();
            mockRaceGenerator = new Mock<IRaceGenerator>();
            alignment = new Alignment();
            characterClass = new CharacterClass();
            race = new Race();
            ability = new Ability();
            equipment = new Equipment();
            combat = new Combat();
            baseAttack = new BaseAttack();

            alignment.Goodness = "goodness";
            alignment.Lawfulness = "lawfulness";
            characterClass.Level = 1;
            characterClass.ClassName = "class name";
            race.BaseRace.Id = BaseRaceId;
            race.Metarace.Id = RaceConstants.Metaraces.NoneId;

            mockAlignmentGenerator.Setup(f => f.GenerateWith(mockAlignmentRandomizer.Object)).Returns(alignment);
            mockCharacterClassGenerator.Setup(f => f.GenerateWith(alignment, mockLevelRandomizer.Object,
                mockClassNameRandomizer.Object)).Returns(characterClass);
            mockRaceGenerator.Setup(f => f.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object)).Returns(race);
            mockAbilitiesGenerator.Setup(g => g.GenerateWith(characterClass, race, mockStatsRandomizer.Object, baseAttack)).Returns(ability);
            mockEquipmentGenerator.Setup(g => g.GenerateWith(ability.Feats, characterClass)).Returns(equipment);
            mockCombatGenerator.Setup(g => g.GenerateWith(baseAttack, characterClass, race, ability.Feats, ability.Stats, equipment)).Returns(combat);
            mockCombatGenerator.Setup(g => g.GenerateBaseAttackWith(characterClass, race)).Returns(baseAttack);
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
            mockCharacterClassGenerator.Verify(f => f.GenerateWith(alignment, mockLevelRandomizer.Object,
                mockClassNameRandomizer.Object), Times.Exactly(2));
        }

        [Test]
        public void IncompatibleBaseRaceIsRegenerated()
        {
            race.BaseRace.Id = BaseRacePlusOneId;
            race.Metarace.Id = RaceConstants.Metaraces.NoneId;

            GenerateCharacter();
            mockRaceGenerator.Verify(f => f.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object), Times.Exactly(2));
        }

        [Test]
        public void IncompatibleMetaraceIsRegenerated()
        {
            race.BaseRace.Id = BaseRaceId;
            race.Metarace.Id = MetaraceId;

            GenerateCharacter();
            mockRaceGenerator.Verify(f => f.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object), Times.Exactly(2));
        }

        [Test]
        public void IncompatibleComboOfBaseRaceAndMetaraceIsRegeneratedWithCompatibleBaseRace()
        {
            var higherRace = new Race();
            higherRace.BaseRace.Id = BaseRacePlusOneId;
            higherRace.Metarace.Id = MetaraceId;
            race.BaseRace.Id = BaseRaceId;
            race.Metarace.Id = MetaraceId;

            mockRaceGenerator.SetupSequence(f => f.GenerateWith(It.IsAny<Alignment>(), It.IsAny<CharacterClass>(), mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object)).Returns(higherRace).Returns(race);

            characterClass.Level = 2;

            GenerateCharacter();
            mockRaceGenerator.Verify(f => f.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object), Times.Exactly(2));
        }

        [Test]
        public void IncompatibleComboOfBaseRaceAndMetaraceIsRegeneratedWithCompatibleMetarace()
        {
            var higherRace = new Race();
            higherRace.BaseRace.Id = BaseRacePlusOneId;
            higherRace.Metarace.Id = MetaraceId;
            race.BaseRace.Id = BaseRacePlusOneId;
            race.Metarace.Id = RaceConstants.Metaraces.NoneId;

            mockRaceGenerator.SetupSequence(f => f.GenerateWith(It.IsAny<Alignment>(), It.IsAny<CharacterClass>(), mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object)).Returns(higherRace).Returns(race);

            characterClass.Level = 2;

            GenerateCharacter();
            mockRaceGenerator.Verify(f => f.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object), Times.Exactly(2));
        }

        [Test]
        public void AppliesBaseRaceLevelAdjustment()
        {
            characterClass.Level = 2;
            race.BaseRace.Id = BaseRacePlusOneId;

            GenerateCharacter();
            Assert.That(characterClass.Level, Is.EqualTo(1));
        }

        [Test]
        public void AppliesMetaraceLevelAdjustment()
        {
            characterClass.Level = 2;
            race.Metarace.Id = MetaraceId;

            GenerateCharacter();
            Assert.That(characterClass.Level, Is.EqualTo(1));
        }

        [Test]
        public void ApplyBaseRaceAndMetaraceLevelAdjustments()
        {
            race.BaseRace.Id = BaseRacePlusOneId;
            race.Metarace.Id = MetaraceId;
            characterClass.Level = 3;

            GenerateCharacter();
            Assert.That(characterClass.Level, Is.EqualTo(1));
        }

        [Test]
        public void GetsInterestingTraitFromPercentileResultSelector()
        {
            mockPercentileSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Percentile.Traits)).Returns("interesting trait");
            var character = GenerateCharacter();
            Assert.That(character.InterestingTrait, Is.EqualTo("interesting trait"));
        }

        [Test]
        public void GetBaseAttackFromCombatGenerator()
        {
            GenerateCharacter();
            mockCombatGenerator.Verify(g => g.GenerateBaseAttackWith(characterClass, race), Times.Once);
        }

        [Test]
        public void GetAbilityFromGenerator()
        {
            var character = GenerateCharacter();
            Assert.That(character.Ability, Is.EqualTo(ability));
        }

        [Test]
        public void GetEquipmentFromGenerator()
        {
            var character = GenerateCharacter();
            Assert.That(character.Equipment, Is.EqualTo(equipment));
        }

        [Test]
        public void GetCombatFromGenerator()
        {
            var character = GenerateCharacter();
            Assert.That(character.Combat, Is.EqualTo(combat));
        }

        [Test]
        public void AdjustSkillsWithArmorCheckPenalty()
        {
            ability.Skills.Add("no penalty", new Skill { ArmorCheckPenalty = false });
            ability.Skills.Add("penalty", new Skill { ArmorCheckPenalty = true });
            equipment.Armor.Name = "armor name";

            var skillAdjustments = new Dictionary<String, Int32>();
            skillAdjustments.Add(equipment.Armor.Name, 5);
            skillAdjustments.Add("other armor", 10);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.ArmorCheckPenalties)).Returns(skillAdjustments);

            var character = GenerateCharacter();
            Assert.That(character.Ability.Skills["no penalty"].Bonus, Is.EqualTo(0));
            Assert.That(character.Ability.Skills["penalty"].Bonus, Is.EqualTo(-5));
        }

        [Test]
        public void SwimmingTakesDoubleArmorCheckPenalty()
        {
            ability.Skills.Add(SkillConstants.Swim, new Skill { ArmorCheckPenalty = true });
            equipment.Armor.Name = "armor name";

            var skillAdjustments = new Dictionary<String, Int32>();
            skillAdjustments.Add(equipment.Armor.Name, 5);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.ArmorCheckPenalties)).Returns(skillAdjustments);

            var character = GenerateCharacter();
            Assert.That(character.Ability.Skills[SkillConstants.Swim].Bonus, Is.EqualTo(-10));
        }
    }
}