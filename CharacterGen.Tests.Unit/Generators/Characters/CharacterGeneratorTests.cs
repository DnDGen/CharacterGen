using CharacterGen.Abilities;
using CharacterGen.Abilities.Feats;
using CharacterGen.Abilities.Skills;
using CharacterGen.Abilities.Stats;
using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Characters;
using CharacterGen.Combats;
using CharacterGen.Domain.Generators;
using CharacterGen.Domain.Generators.Abilities;
using CharacterGen.Domain.Generators.Alignments;
using CharacterGen.Domain.Generators.Characters;
using CharacterGen.Domain.Generators.Classes;
using CharacterGen.Domain.Generators.Combats;
using CharacterGen.Domain.Generators.Items;
using CharacterGen.Domain.Generators.Magics;
using CharacterGen.Domain.Generators.Races;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;
using CharacterGen.Domain.Tables;
using CharacterGen.Items;
using CharacterGen.Magics;
using CharacterGen.Races;
using CharacterGen.Randomizers.Alignments;
using CharacterGen.Randomizers.CharacterClasses;
using CharacterGen.Randomizers.Races;
using CharacterGen.Randomizers.Stats;
using CharacterGen.Verifiers;
using CharacterGen.Verifiers.Exceptions;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TreasureGen.Items;

namespace CharacterGen.Tests.Unit.Generators.Characters
{
    [TestFixture]
    public class CharacterGeneratorTests
    {
        private const string BaseRace = "baserace";
        private const string BaseRacePlusOne = "baserace+1";
        private const string Metarace = "metarace";

        private Mock<IAlignmentGenerator> mockAlignmentGenerator;
        private Mock<ICharacterClassGenerator> mockCharacterClassGenerator;
        private Mock<IRaceGenerator> mockRaceGenerator;
        private Mock<IAbilitiesGenerator> mockAbilitiesGenerator;
        private Mock<ICombatGenerator> mockCombatGenerator;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private Mock<IRandomizerVerifier> mockRandomizerVerifier;
        private Mock<IPercentileSelector> mockPercentileSelector;
        private Mock<IEquipmentGenerator> mockTreasureGenerator;
        private Mock<IMagicGenerator> mockMagicGenerator;
        private Generator generator;
        private ICharacterGenerator characterGenerator;
        private Mock<ICollectionsSelector> mockCollectionsSelector;

        private Mock<IAlignmentRandomizer> mockAlignmentRandomizer;
        private Mock<IClassNameRandomizer> mockClassNameRandomizer;
        private Mock<ILevelRandomizer> mockLevelRandomizer;
        private Mock<RaceRandomizer> mockBaseRaceRandomizer;
        private Mock<RaceRandomizer> mockMetaraceRandomizer;
        private Mock<IStatsRandomizer> mockStatsRandomizer;
        private Mock<ISetLevelRandomizer> mockSetLevelRandomizer;

        private CharacterClass characterClass;
        private Race race;
        private Dictionary<string, int> levelAdjustments;
        private Alignment alignment;
        private Ability ability;
        private Combat combat;
        private Equipment equipment;
        private BaseAttack baseAttack;
        private List<Feat> feats;
        private Magic magic;

        [SetUp]
        public void Setup()
        {
            SetUpMockRandomizers();
            SetUpGenerators();

            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            levelAdjustments = new Dictionary<string, int>();
            mockRandomizerVerifier = new Mock<IRandomizerVerifier>();
            mockPercentileSelector = new Mock<IPercentileSelector>();
            mockCollectionsSelector = new Mock<ICollectionsSelector>();

            levelAdjustments[BaseRace] = 0;
            levelAdjustments[BaseRacePlusOne] = 1;
            levelAdjustments[RaceConstants.Metaraces.None] = 0;
            levelAdjustments[Metarace] = 1;
            mockAdjustmentsSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments, It.IsAny<string>())).Returns((string table, string name) => levelAdjustments[name]);

            mockRandomizerVerifier.Setup(v => v.VerifyCompatibility(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object)).Returns(true);
            mockRandomizerVerifier.Setup(v => v.VerifyCompatibility(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockSetLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object)).Returns(true);
            mockRandomizerVerifier.Setup(v => v.VerifyAlignmentCompatibility(It.IsAny<Alignment>(), mockClassNameRandomizer.Object, mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object)).Returns(true);
            mockRandomizerVerifier.Setup(v => v.VerifyAlignmentCompatibility(It.IsAny<Alignment>(), mockClassNameRandomizer.Object, mockSetLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object)).Returns(true);
            mockRandomizerVerifier.Setup(v => v.VerifyCharacterClassCompatibility(It.IsAny<Alignment>(), It.IsAny<CharacterClass>(), mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object)).Returns(true);
            mockRandomizerVerifier.Setup(v => v.VerifyCharacterClassCompatibility(It.IsAny<Alignment>(), It.IsAny<CharacterClass>(), mockSetLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object)).Returns(true);
            mockRandomizerVerifier.Setup(v => v.VerifyRaceCompatibility(It.IsAny<Race>(), It.IsAny<CharacterClass>(), mockLevelRandomizer.Object)).Returns(true);
            mockRandomizerVerifier.Setup(v => v.VerifyRaceCompatibility(It.IsAny<Race>(), It.IsAny<CharacterClass>(), mockSetLevelRandomizer.Object)).Returns(true);

            characterGenerator = new CharacterGenerator(mockAlignmentGenerator.Object, mockCharacterClassGenerator.Object,
                mockRaceGenerator.Object, mockAdjustmentsSelector.Object, mockRandomizerVerifier.Object, mockPercentileSelector.Object,
                mockAbilitiesGenerator.Object, mockCombatGenerator.Object, mockTreasureGenerator.Object, mockMagicGenerator.Object, generator,
                mockCollectionsSelector.Object);
        }

        private void SetUpMockRandomizers()
        {
            mockAlignmentRandomizer = new Mock<IAlignmentRandomizer>();
            mockClassNameRandomizer = new Mock<IClassNameRandomizer>();
            mockLevelRandomizer = new Mock<ILevelRandomizer>();
            mockStatsRandomizer = new Mock<IStatsRandomizer>();
            mockBaseRaceRandomizer = new Mock<RaceRandomizer>();
            mockMetaraceRandomizer = new Mock<RaceRandomizer>();
            mockSetLevelRandomizer = new Mock<ISetLevelRandomizer>();

            mockSetLevelRandomizer.SetupAllProperties();
        }

        private void SetUpGenerators()
        {
            mockAlignmentGenerator = new Mock<IAlignmentGenerator>();
            mockAbilitiesGenerator = new Mock<IAbilitiesGenerator>();
            mockCombatGenerator = new Mock<ICombatGenerator>();
            mockTreasureGenerator = new Mock<IEquipmentGenerator>();
            mockCharacterClassGenerator = new Mock<ICharacterClassGenerator>();
            mockRaceGenerator = new Mock<IRaceGenerator>();
            mockMagicGenerator = new Mock<IMagicGenerator>();
            generator = new ConfigurableIterationGenerator(4);
            alignment = new Alignment();
            characterClass = new CharacterClass();
            race = new Race();
            ability = new Ability();
            equipment = new Equipment();
            combat = new Combat();
            baseAttack = new BaseAttack();
            feats = new List<Feat>();
            magic = new Magic();

            alignment.Goodness = "goodness";
            alignment.Lawfulness = "lawfulness";
            characterClass.Level = 1;
            characterClass.Name = "class name";
            race.BaseRace = BaseRace;
            race.Metarace = RaceConstants.Metaraces.None;
            ability.Feats = feats;
            ability.Stats["stat"] = new Stat("stat");

            mockCombatGenerator.Setup(g => g.GenerateBaseAttackWith(It.IsAny<CharacterClass>(), It.IsAny<Race>(), It.IsAny<Dictionary<string, Stat>>())).Returns(() => new BaseAttack());
            mockTreasureGenerator.Setup(g => g.GenerateWith(It.IsAny<IEnumerable<Feat>>(), It.IsAny<CharacterClass>(), It.IsAny<Race>())).Returns(() => new Equipment());
            mockCombatGenerator.Setup(g => g.GenerateWith(It.IsAny<BaseAttack>(), It.IsAny<CharacterClass>(), It.IsAny<Race>(), It.IsAny<IEnumerable<Feat>>(), It.IsAny<Dictionary<string, Stat>>(), It.IsAny<Equipment>())).Returns(() => new Combat());
            mockMagicGenerator.Setup(g => g.GenerateWith(It.IsAny<Alignment>(), It.IsAny<CharacterClass>(), It.IsAny<Race>(), It.IsAny<Dictionary<string, Stat>>(), It.IsAny<IEnumerable<Feat>>(), It.IsAny<Equipment>())).Returns(() => new Magic());

            mockAlignmentGenerator.Setup(g => g.GenerateWith(mockAlignmentRandomizer.Object)).Returns(alignment);

            mockCharacterClassGenerator.Setup(g => g.GenerateWith(alignment, mockLevelRandomizer.Object, mockClassNameRandomizer.Object)).Returns(characterClass);
            mockCharacterClassGenerator.Setup(g => g.GenerateWith(alignment, mockSetLevelRandomizer.Object, mockClassNameRandomizer.Object)).Returns(characterClass);
            mockRaceGenerator.Setup(g => g.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object)).Returns(race);
            mockAbilitiesGenerator.Setup(g => g.GenerateStats(characterClass, race, mockStatsRandomizer.Object)).Returns(ability.Stats);
            mockAbilitiesGenerator.Setup(g => g.GenerateWith(characterClass, race, ability.Stats, baseAttack)).Returns(ability);
            mockTreasureGenerator.Setup(g => g.GenerateWith(ability.Feats, characterClass, race)).Returns(equipment);
            mockCombatGenerator.Setup(g => g.GenerateWith(baseAttack, characterClass, race, ability.Feats, ability.Stats, equipment)).Returns(combat);
            mockCombatGenerator.Setup(g => g.GenerateBaseAttackWith(characterClass, race, ability.Stats)).Returns(baseAttack);
            mockMagicGenerator.Setup(g => g.GenerateWith(alignment, characterClass, race, ability.Stats, ability.Feats, equipment)).Returns(magic);
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
            return characterGenerator.GenerateWith(
                mockAlignmentRandomizer.Object,
                mockClassNameRandomizer.Object,
                mockLevelRandomizer.Object,
                mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object,
                mockStatsRandomizer.Object);
        }

        [Test]
        public void IncompatibleAlignmentIsRegenerated()
        {
            mockRandomizerVerifier
                .SetupSequence(v => v.VerifyAlignmentCompatibility(
                    It.IsAny<Alignment>(),
                    mockClassNameRandomizer.Object,
                    mockLevelRandomizer.Object,
                    mockBaseRaceRandomizer.Object,
                    mockMetaraceRandomizer.Object))
                .Returns(false)
                .Returns(true);

            GenerateCharacter();
            mockAlignmentGenerator.Verify(f => f.GenerateWith(mockAlignmentRandomizer.Object), Times.Exactly(2));
        }

        [Test]
        public void NullAlignmentIndicatesIncompatibleRandomizers()
        {
            mockRandomizerVerifier
                .Setup(v => v.VerifyAlignmentCompatibility(
                    It.IsAny<Alignment>(),
                    mockClassNameRandomizer.Object,
                    mockLevelRandomizer.Object,
                    mockBaseRaceRandomizer.Object,
                    mockMetaraceRandomizer.Object))
                .Returns(false);

            Assert.That(GenerateCharacter, Throws.InstanceOf<IncompatibleRandomizersException>());
        }

        [Test]
        public void IncompatibleCharacterClassIsRegenerated()
        {
            mockRandomizerVerifier
                .SetupSequence(v => v.VerifyCharacterClassCompatibility(
                    It.IsAny<Alignment>(),
                    It.IsAny<CharacterClass>(),
                    mockLevelRandomizer.Object,
                    mockBaseRaceRandomizer.Object,
                    mockMetaraceRandomizer.Object
                )).Returns(false)
                .Returns(true);

            GenerateCharacter();
            mockCharacterClassGenerator.Verify(f => f.GenerateWith(alignment, mockLevelRandomizer.Object, mockClassNameRandomizer.Object), Times.Exactly(2));
        }

        [Test]
        public void NullCharacterClassIndicatesIncompatibleRandomizers()
        {
            mockRandomizerVerifier
                .Setup(v => v.VerifyCharacterClassCompatibility(
                    It.IsAny<Alignment>(),
                    It.IsAny<CharacterClass>(),
                    mockLevelRandomizer.Object,
                    mockBaseRaceRandomizer.Object,
                    mockMetaraceRandomizer.Object))
                .Returns(false);

            Assert.That(GenerateCharacter, Throws.InstanceOf<IncompatibleRandomizersException>());
        }

        [Test]
        public void IncompatibleRaceIsRegenerated()
        {
            mockRandomizerVerifier
                .SetupSequence(v => v.VerifyRaceCompatibility(
                    It.IsAny<Race>(),
                    It.IsAny<CharacterClass>(),
                    mockLevelRandomizer.Object))
                .Returns(false)
                .Returns(true);

            GenerateCharacter();
            mockRaceGenerator.Verify(f => f.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object), Times.Exactly(2));
        }

        [Test]
        public void NullRaceIndicatesIncompatibleRandomizers()
        {
            mockRandomizerVerifier
                .Setup(v => v.VerifyRaceCompatibility(
                    It.IsAny<Race>(),
                    It.IsAny<CharacterClass>(),
                    mockLevelRandomizer.Object))
                .Returns(false);

            characterClass.Level = 2;

            Assert.That(GenerateCharacter, Throws.InstanceOf<IncompatibleRandomizersException>());
        }

        [Test]
        public void AppliesBaseRaceLevelAdjustment()
        {
            characterClass.Level = 2;
            race.BaseRace = BaseRacePlusOne;

            GenerateCharacter();
            Assert.That(characterClass.Level, Is.EqualTo(1));
            Assert.That(characterClass.LevelAdjustment, Is.EqualTo(1));
            Assert.That(characterClass.EffectiveLevel, Is.EqualTo(2));
        }

        [Test]
        public void AppliesMetaraceLevelAdjustment()
        {
            characterClass.Level = 2;
            race.Metarace = Metarace;

            GenerateCharacter();
            Assert.That(characterClass.Level, Is.EqualTo(1));
            Assert.That(characterClass.LevelAdjustment, Is.EqualTo(1));
            Assert.That(characterClass.EffectiveLevel, Is.EqualTo(2));
        }

        [Test]
        public void ApplyBaseRaceAndMetaraceLevelAdjustments()
        {
            race.BaseRace = BaseRacePlusOne;
            race.Metarace = Metarace;
            characterClass.Level = 3;

            GenerateCharacter();
            Assert.That(characterClass.Level, Is.EqualTo(1));
            Assert.That(characterClass.LevelAdjustment, Is.EqualTo(2));
            Assert.That(characterClass.EffectiveLevel, Is.EqualTo(3));
        }

        [Test]
        public void RegenerateSpecialistFields()
        {
            mockCharacterClassGenerator.Setup(g => g.RegenerateSpecialistFields(alignment, characterClass, race)).Returns(new[] { "new specialist field", "other new specialist field" });
            GenerateCharacter();
            Assert.That(characterClass.SpecialistFields, Contains.Item("new specialist field"));
            Assert.That(characterClass.SpecialistFields, Contains.Item("other new specialist field"));
            Assert.That(characterClass.SpecialistFields.Count(), Is.EqualTo(2));
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
            mockCombatGenerator.Verify(g => g.GenerateBaseAttackWith(characterClass, race, ability.Stats), Times.Once);
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
        public void GetMagicFromGenerator()
        {
            var character = GenerateCharacter();
            Assert.That(character.Magic, Is.EqualTo(magic));
        }

        [Test]
        public void AdjustLevel()
        {
            race.BaseRace = BaseRacePlusOne;
            race.Metarace = Metarace;
            characterClass.Level = 3;

            var character = characterGenerator.GenerateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockStatsRandomizer.Object);

            Assert.That(character.Class.Level, Is.EqualTo(1));
            Assert.That(character.Class.LevelAdjustment, Is.EqualTo(2));
            Assert.That(character.Class.EffectiveLevel, Is.EqualTo(3));
        }

        [Test]
        public void DoNotAdjustSetLevel()
        {
            race.BaseRace = BaseRacePlusOne;
            race.Metarace = Metarace;
            characterClass.Level = 3;

            mockSetLevelRandomizer.Object.AllowAdjustments = false;

            var character = characterGenerator.GenerateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockSetLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockStatsRandomizer.Object);

            Assert.That(character.Class.Level, Is.EqualTo(3));
            Assert.That(character.Class.LevelAdjustment, Is.EqualTo(2));
            Assert.That(character.Class.EffectiveLevel, Is.EqualTo(5));
        }

        [Test]
        public void AdjustSetLevel()
        {
            race.BaseRace = BaseRacePlusOne;
            race.Metarace = Metarace;
            characterClass.Level = 3;

            mockSetLevelRandomizer.Object.AllowAdjustments = true;

            var character = characterGenerator.GenerateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockSetLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockStatsRandomizer.Object);

            Assert.That(character.Class.Level, Is.EqualTo(1));
            Assert.That(character.Class.LevelAdjustment, Is.EqualTo(2));
            Assert.That(character.Class.EffectiveLevel, Is.EqualTo(3));
        }

        [Test]
        public void AdjustNPCSetLevel()
        {
            race.BaseRace = BaseRacePlusOne;
            race.Metarace = Metarace;
            characterClass.Level = 3;
            characterClass.IsNPC = true;

            mockSetLevelRandomizer.Object.AllowAdjustments = true;

            var character = characterGenerator.GenerateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockSetLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockStatsRandomizer.Object);

            Assert.That(character.Class.Level, Is.EqualTo(1));
            Assert.That(character.Class.LevelAdjustment, Is.EqualTo(2));
            Assert.That(character.Class.EffectiveLevel, Is.EqualTo(1));
        }

        [Test]
        public void ApplyArmorCheckPenaltiesForArmor()
        {
            ability.Skills = new[]
            {
                new Skill("skill", ability.Stats["stat"], 1) { HasArmorCheckPenalty = false },
                new Skill("other skill", ability.Stats["stat"], 1) { HasArmorCheckPenalty = true }
            };

            equipment.Armor = new Armor { Name = "armor", ArmorCheckPenalty = -9266 };

            var character = characterGenerator.GenerateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockSetLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockStatsRandomizer.Object);

            Assert.That(character.Ability.Skills.First(s => s.Name == "skill").ArmorCheckPenalty, Is.EqualTo(0));
            Assert.That(character.Ability.Skills.First(s => s.Name == "other skill").ArmorCheckPenalty, Is.EqualTo(-9266));
        }

        [Test]
        public void DoNotApplyPenaltyCheckForArmorIfNoArmor()
        {
            ability.Skills = new[]
            {
                new Skill("skill", ability.Stats["stat"], 1) { HasArmorCheckPenalty = false },
                new Skill("other skill", ability.Stats["stat"], 1) { HasArmorCheckPenalty = true }
            };

            equipment.Armor = null;

            var character = characterGenerator.GenerateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockSetLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockStatsRandomizer.Object);

            Assert.That(character.Ability.Skills.First(s => s.Name == "skill").ArmorCheckPenalty, Is.EqualTo(0));
            Assert.That(character.Ability.Skills.First(s => s.Name == "other skill").ArmorCheckPenalty, Is.EqualTo(0));
        }

        [Test]
        public void ApplyArmorCheckPenaltiesForShield()
        {
            ability.Skills = new[]
            {
                new Skill("skill", ability.Stats["stat"], 1) { HasArmorCheckPenalty = false },
                new Skill("other skill", ability.Stats["stat"], 1) { HasArmorCheckPenalty = true }
            };

            equipment.OffHand = new Armor { Name = "shield", ArmorCheckPenalty = -9266 };
            equipment.OffHand.Attributes = new[] { AttributeConstants.Shield };

            var character = characterGenerator.GenerateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockSetLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockStatsRandomizer.Object);

            Assert.That(character.Ability.Skills.First(s => s.Name == "skill").ArmorCheckPenalty, Is.EqualTo(0));
            Assert.That(character.Ability.Skills.First(s => s.Name == "other skill").ArmorCheckPenalty, Is.EqualTo(-9266));
        }

        [Test]
        public void DoNotApplyPenaltyCheckForNonShields()
        {
            ability.Skills = new[]
            {
                new Skill("skill", ability.Stats["stat"], 1) { HasArmorCheckPenalty = false },
                new Skill("other skill", ability.Stats["stat"], 1) { HasArmorCheckPenalty = true }
            };

            equipment.OffHand = new Item { Name = "shield" };

            var character = characterGenerator.GenerateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockSetLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockStatsRandomizer.Object);

            Assert.That(character.Ability.Skills.First(s => s.Name == "skill").ArmorCheckPenalty, Is.EqualTo(0));
            Assert.That(character.Ability.Skills.First(s => s.Name == "other skill").ArmorCheckPenalty, Is.EqualTo(0));
        }

        [Test]
        public void ApplyArmorCheckPenaltiesForArmorAndShield()
        {
            ability.Skills = new[]
            {
                new Skill("skill", ability.Stats["stat"], 1) { HasArmorCheckPenalty = false },
                new Skill("other skill", ability.Stats["stat"], 1) { HasArmorCheckPenalty = true }
            };

            equipment.Armor = new Armor { Name = "armor", ArmorCheckPenalty = -42 };
            equipment.OffHand = new Armor { Name = "shield", ArmorCheckPenalty = -9266 };
            equipment.OffHand.Attributes = new[] { AttributeConstants.Shield };

            var character = characterGenerator.GenerateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockSetLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockStatsRandomizer.Object);

            Assert.That(character.Ability.Skills.First(s => s.Name == "skill").ArmorCheckPenalty, Is.EqualTo(0));
            Assert.That(character.Ability.Skills.First(s => s.Name == "other skill").ArmorCheckPenalty, Is.EqualTo(-9308));
        }

        [Test]
        public void SwimTakesDoubleArmorCheckPenalty()
        {
            ability.Skills = new[]
            {
                new Skill("skill", ability.Stats["stat"], 1) { HasArmorCheckPenalty = false },
                new Skill("other skill", ability.Stats["stat"], 1) { HasArmorCheckPenalty = true },
                new Skill(SkillConstants.Swim, ability.Stats["stat"], 1) { HasArmorCheckPenalty = true },
            };

            equipment.Armor = new Armor { Name = "armor", ArmorCheckPenalty = -42 };
            equipment.OffHand = new Armor { Name = "shield", ArmorCheckPenalty = -9266 };
            equipment.OffHand.Attributes = new[] { AttributeConstants.Shield };

            var character = characterGenerator.GenerateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockSetLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockStatsRandomizer.Object);

            Assert.That(character.Ability.Skills.First(s => s.Name == "skill").ArmorCheckPenalty, Is.EqualTo(0));
            Assert.That(character.Ability.Skills.First(s => s.Name == "other skill").ArmorCheckPenalty, Is.EqualTo(-9308));
            Assert.That(character.Ability.Skills.First(s => s.Name == SkillConstants.Swim).ArmorCheckPenalty, Is.EqualTo(-9308 * 2));
        }

        [Test]
        public void SwimTakesNoPenaltyForPlateArmorOfTheDeep()
        {
            ability.Skills = new[]
            {
                new Skill("skill", ability.Stats["stat"], 1) { HasArmorCheckPenalty = false },
                new Skill("other skill", ability.Stats["stat"], 1) { HasArmorCheckPenalty = true },
                new Skill(SkillConstants.Swim, ability.Stats["stat"], 1) { HasArmorCheckPenalty = true },
            };

            equipment.Armor = new Armor { Name = ArmorConstants.PlateArmorOfTheDeep, ArmorCheckPenalty = -9266 };

            var character = characterGenerator.GenerateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockSetLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockStatsRandomizer.Object);

            Assert.That(character.Ability.Skills.First(s => s.Name == "skill").ArmorCheckPenalty, Is.EqualTo(0));
            Assert.That(character.Ability.Skills.First(s => s.Name == "other skill").ArmorCheckPenalty, Is.EqualTo(-9266));
            Assert.That(character.Ability.Skills.First(s => s.Name == SkillConstants.Swim).ArmorCheckPenalty, Is.EqualTo(0));
        }

        [Test]
        public void SwimTakesPenaltyForShieldWithPlateArmorOfTheDeep()
        {
            ability.Skills = new[]
            {
                new Skill("skill", ability.Stats["stat"], 1) { HasArmorCheckPenalty = false },
                new Skill("other skill", ability.Stats["stat"], 1) { HasArmorCheckPenalty = true },
                new Skill(SkillConstants.Swim, ability.Stats["stat"], 1) { HasArmorCheckPenalty = true },
            };

            equipment.Armor = new Armor { Name = ArmorConstants.PlateArmorOfTheDeep, ArmorCheckPenalty = -9266 };
            equipment.OffHand = new Armor { Name = "shield", ArmorCheckPenalty = -90210 };
            equipment.OffHand.Attributes = new[] { AttributeConstants.Shield };

            var character = characterGenerator.GenerateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockSetLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockStatsRandomizer.Object);

            Assert.That(character.Ability.Skills.First(s => s.Name == "skill").ArmorCheckPenalty, Is.EqualTo(0));
            Assert.That(character.Ability.Skills.First(s => s.Name == "other skill").ArmorCheckPenalty, Is.EqualTo(-9266 - 90210));
            Assert.That(character.Ability.Skills.First(s => s.Name == SkillConstants.Swim).ArmorCheckPenalty, Is.EqualTo(-90210 * 2));
        }
    }
}