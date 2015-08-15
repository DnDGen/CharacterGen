using CharacterGen.Common;
using CharacterGen.Common.Abilities;
using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Skills;
using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Combats;
using CharacterGen.Common.Items;
using CharacterGen.Common.Magics;
using CharacterGen.Common.Races;
using CharacterGen.Generators;
using CharacterGen.Generators.Abilities;
using CharacterGen.Generators.Combats;
using CharacterGen.Generators.Domain;
using CharacterGen.Generators.Items;
using CharacterGen.Generators.Magics;
using CharacterGen.Generators.Randomizers.Alignments;
using CharacterGen.Generators.Randomizers.CharacterClasses;
using CharacterGen.Generators.Randomizers.Races;
using CharacterGen.Generators.Randomizers.Stats;
using CharacterGen.Generators.Verifiers;
using CharacterGen.Generators.Verifiers.Exceptions;
using CharacterGen.Selectors;
using CharacterGen.Selectors.Objects;
using CharacterGen.Tables;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Tests.Unit.Generators
{
    [TestFixture]
    public class CharacterGeneratorTests
    {
        private const String BaseRace = "baserace";
        private const String BaseRacePlusOne = "baserace+1";
        private const String Metarace = "metarace";

        private Mock<IAlignmentGenerator> mockAlignmentGenerator;
        private Mock<ICharacterClassGenerator> mockCharacterClassGenerator;
        private Mock<IRaceGenerator> mockRaceGenerator;
        private Mock<IAbilitiesGenerator> mockAbilitiesGenerator;
        private Mock<ICombatGenerator> mockCombatGenerator;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private Mock<IRandomizerVerifier> mockRandomizerVerifier;
        private Mock<IPercentileSelector> mockPercentileSelector;
        private Mock<ILeadershipSelector> mockLeadershipSelector;
        private Mock<IEquipmentGenerator> mockTreasureGenerator;
        private Mock<IMagicGenerator> mockMagicGenerator;
        private Mock<ISetLevelRandomizer> mockSetLevelRandomizer;
        private Mock<ISetAlignmentRandomizer> mockSetAlignmentRandomizer;
        private Mock<IAlignmentRandomizer> mockAnyAlignmentRandomizer;
        private Mock<IClassNameRandomizer> mockAnyClassNameRandomizer;
        private Mock<IBaseRaceRandomizer> mockAnyBaseRaceRandomizer;
        private Mock<IMetaraceRandomizer> mockAnyMetaraceRandomizer;
        private Mock<IStatsRandomizer> mockRawStatRandomizer;
        private Mock<IBooleanPercentileSelector> mockBooleanPercentileSelector;
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private ICharacterGenerator characterGenerator;

        private Mock<IAlignmentRandomizer> mockAlignmentRandomizer;
        private Mock<IClassNameRandomizer> mockClassNameRandomizer;
        private Mock<ILevelRandomizer> mockLevelRandomizer;
        private Mock<IBaseRaceRandomizer> mockBaseRaceRandomizer;
        private Mock<IMetaraceRandomizer> mockMetaraceRandomizer;
        private Mock<IStatsRandomizer> mockStatsRandomizer;

        private CharacterClass characterClass;
        private Race race;
        private Dictionary<String, Int32> levelAdjustments;
        private Alignment alignment;
        private Ability ability;
        private Combat combat;
        private Equipment equipment;
        private BaseAttack baseAttack;
        private List<Feat> feats;
        private FollowerQuantities followerQuantities;
        private Magic magic;
        private List<String> allowedAlignments;

        [SetUp]
        public void Setup()
        {
            SetUpMockRandomizers();
            SetUpGenerators();

            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            levelAdjustments = new Dictionary<String, Int32>();
            mockRandomizerVerifier = new Mock<IRandomizerVerifier>();
            mockPercentileSelector = new Mock<IPercentileSelector>();
            mockLeadershipSelector = new Mock<ILeadershipSelector>();
            mockBooleanPercentileSelector = new Mock<IBooleanPercentileSelector>();
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            followerQuantities = new FollowerQuantities();
            allowedAlignments = new List<String>();

            levelAdjustments[BaseRace] = 0;
            levelAdjustments[BaseRacePlusOne] = 1;
            levelAdjustments[RaceConstants.Metaraces.None] = 0;
            levelAdjustments[Metarace] = 1;
            mockAdjustmentsSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments)).Returns(levelAdjustments);

            mockRandomizerVerifier.Setup(v => v.VerifyCompatibility(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object,
                mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object)).Returns(true);
            mockRandomizerVerifier.Setup(v => v.VerifyAlignmentCompatibility(It.IsAny<Alignment>(), mockClassNameRandomizer.Object,
                mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object)).Returns(true);
            mockRandomizerVerifier.Setup(v => v.VerifyCharacterClassCompatibility(It.IsAny<String>(), It.IsAny<CharacterClass>(),
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object)).Returns(true);
            mockRandomizerVerifier.Setup(v => v.VerifyAlignmentCompatibility(It.IsAny<Alignment>(), mockAnyClassNameRandomizer.Object,
                mockSetLevelRandomizer.Object, mockAnyBaseRaceRandomizer.Object, mockAnyMetaraceRandomizer.Object)).Returns(true);
            mockRandomizerVerifier.Setup(v => v.VerifyCharacterClassCompatibility(It.IsAny<String>(), It.IsAny<CharacterClass>(),
                mockAnyBaseRaceRandomizer.Object, mockAnyMetaraceRandomizer.Object)).Returns(true);

            mockLeadershipSelector.Setup(s => s.SelectFollowerQuantitiesFor(It.IsAny<Int32>())).Returns(new FollowerQuantities());
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.AlignmentGroups, It.IsAny<String>()))
                .Returns(() => allowedAlignments.Union(new[] { alignment.ToString() }));

            allowedAlignments.Add(String.Empty);
            allowedAlignments.Add(" ");

            characterGenerator = new CharacterGenerator(mockAlignmentGenerator.Object, mockCharacterClassGenerator.Object,
                mockRaceGenerator.Object, mockAdjustmentsSelector.Object, mockRandomizerVerifier.Object, mockPercentileSelector.Object,
                mockAbilitiesGenerator.Object, mockCombatGenerator.Object, mockTreasureGenerator.Object, mockSetAlignmentRandomizer.Object,
                mockSetLevelRandomizer.Object, mockAnyAlignmentRandomizer.Object, mockAnyClassNameRandomizer.Object, mockAnyBaseRaceRandomizer.Object,
                mockAnyMetaraceRandomizer.Object, mockRawStatRandomizer.Object, mockBooleanPercentileSelector.Object, mockLeadershipSelector.Object,
                mockCollectionsSelector.Object, mockMagicGenerator.Object);
        }

        private void SetUpMockRandomizers()
        {
            mockAlignmentRandomizer = new Mock<IAlignmentRandomizer>();
            mockClassNameRandomizer = new Mock<IClassNameRandomizer>();
            mockLevelRandomizer = new Mock<ILevelRandomizer>();
            mockStatsRandomizer = new Mock<IStatsRandomizer>();
            mockBaseRaceRandomizer = new Mock<IBaseRaceRandomizer>();
            mockMetaraceRandomizer = new Mock<IMetaraceRandomizer>();

            mockSetAlignmentRandomizer = new Mock<ISetAlignmentRandomizer>();
            mockAnyAlignmentRandomizer = new Mock<IAlignmentRandomizer>();
            mockSetLevelRandomizer = new Mock<ISetLevelRandomizer>();
            mockAnyClassNameRandomizer = new Mock<IClassNameRandomizer>();
            mockAnyBaseRaceRandomizer = new Mock<IBaseRaceRandomizer>();
            mockAnyMetaraceRandomizer = new Mock<IMetaraceRandomizer>();
            mockRawStatRandomizer = new Mock<IStatsRandomizer>();

            mockSetAlignmentRandomizer.SetupAllProperties();
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
            characterClass.ClassName = "class name";
            race.BaseRace = BaseRace;
            race.Metarace = RaceConstants.Metaraces.None;
            ability.Feats = feats;

            mockCharacterClassGenerator.Setup(g => g.GenerateWith(It.IsAny<Alignment>(), mockSetLevelRandomizer.Object, mockAnyClassNameRandomizer.Object)).Returns(() => new CharacterClass { Level = 1 });
            mockRaceGenerator.Setup(g => g.GenerateWith(It.IsAny<Alignment>(), It.IsAny<CharacterClass>(), mockAnyBaseRaceRandomizer.Object, mockAnyMetaraceRandomizer.Object)).Returns(() => new Race());
            mockCombatGenerator.Setup(g => g.GenerateBaseAttackWith(It.IsAny<CharacterClass>(), It.IsAny<Race>())).Returns(() => new BaseAttack());
            mockAbilitiesGenerator.Setup(g => g.GenerateWith(It.IsAny<CharacterClass>(), It.IsAny<Race>(), mockRawStatRandomizer.Object, It.IsAny<BaseAttack>())).Returns(() => new Ability());
            mockTreasureGenerator.Setup(g => g.GenerateWith(It.IsAny<IEnumerable<Feat>>(), It.IsAny<CharacterClass>())).Returns(() => new Equipment());
            mockCombatGenerator.Setup(g => g.GenerateWith(It.IsAny<BaseAttack>(), It.IsAny<CharacterClass>(), It.IsAny<Race>(), It.IsAny<IEnumerable<Feat>>(), It.IsAny<Dictionary<String, Stat>>(), It.IsAny<Equipment>())).Returns(() => new Combat());
            mockMagicGenerator.Setup(g => g.GenerateWith(It.IsAny<CharacterClass>(), It.IsAny<IEnumerable<Feat>>(), It.IsAny<Equipment>())).Returns(() => new Magic());

            mockAlignmentGenerator.Setup(g => g.GenerateWith(mockAlignmentRandomizer.Object)).Returns(alignment);
            mockAlignmentGenerator.Setup(g => g.GenerateWith(mockSetAlignmentRandomizer.Object)).Returns(() => mockSetAlignmentRandomizer.Object.SetAlignment);

            mockAlignmentGenerator.Setup(g => g.GenerateWith(mockAnyAlignmentRandomizer.Object)).Returns(() => new Alignment());
            mockCharacterClassGenerator.Setup(g => g.GenerateWith(alignment, mockLevelRandomizer.Object,
                mockClassNameRandomizer.Object)).Returns(characterClass);
            mockRaceGenerator.Setup(g => g.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object)).Returns(race);
            mockAbilitiesGenerator.Setup(g => g.GenerateWith(characterClass, race, mockStatsRandomizer.Object, baseAttack)).Returns(ability);
            mockTreasureGenerator.Setup(g => g.GenerateWith(ability.Feats, characterClass)).Returns(equipment);
            mockCombatGenerator.Setup(g => g.GenerateWith(baseAttack, characterClass, race, ability.Feats, ability.Stats, equipment)).Returns(combat);
            mockCombatGenerator.Setup(g => g.GenerateBaseAttackWith(characterClass, race)).Returns(baseAttack);
            mockMagicGenerator.Setup(g => g.GenerateWith(characterClass, ability.Feats, equipment)).Returns(magic);
        }

        [Test]
        public void CanSetValuesOnMockSetRandomizers()
        {
            var setAlignment = new Alignment();
            mockSetAlignmentRandomizer.Object.SetAlignment = setAlignment;
            mockSetLevelRandomizer.Object.SetLevel = 9266;

            Assert.That(mockSetAlignmentRandomizer.Object.SetAlignment, Is.Not.Null);
            Assert.That(mockSetAlignmentRandomizer.Object.SetAlignment, Is.EqualTo(setAlignment));
            Assert.That(mockSetLevelRandomizer.Object.SetLevel, Is.EqualTo(9266));
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
        public void IncompatibleComboOfBaseRaceAndMetaraceIsRegeneratedWithCompatibleBaseRace()
        {
            var higherRace = new Race();
            higherRace.BaseRace = BaseRacePlusOne;
            higherRace.Metarace = Metarace;
            race.BaseRace = BaseRace;
            race.Metarace = Metarace;

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
            higherRace.BaseRace = BaseRacePlusOne;
            higherRace.Metarace = Metarace;
            race.BaseRace = BaseRacePlusOne;
            race.Metarace = RaceConstants.Metaraces.None;

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

        [Test]
        public void GetMagicFromGenerator()
        {
            var character = GenerateCharacter();
            Assert.That(character.Magic, Is.EqualTo(magic));
        }

        [Test]
        public void IfNoLeadershipFeat_NoLeadership()
        {
            feats.Add(new Feat());
            feats[0].Name = "other feat";

            var character = GenerateCharacter();
            Assert.That(character.Leadership.Cohort, Is.Null);
            Assert.That(character.Leadership.Followers, Is.Empty);
            Assert.That(character.Leadership.Score, Is.EqualTo(0));
        }

        [Test]
        public void IfCharacterHasLeadershipFeat_LeadershipScoreIsLevelPlusCharismaModifier()
        {
            feats.Add(new Feat());
            feats[0].Name = FeatConstants.Leadership;

            ability.Stats[StatConstants.Charisma] = new Stat { Value = 16 };
            characterClass.Level = 5;

            var character = GenerateCharacter();
            Assert.That(character.Leadership.Score, Is.EqualTo(8));
        }

        [Test]
        public void IfCharacterHasLeadership_GenerateCohort()
        {
            feats.Add(new Feat());
            feats[0].Name = FeatConstants.Leadership;

            ability.Stats[StatConstants.Charisma] = new Stat { Value = 16 };
            characterClass.Level = 5;

            mockLeadershipSelector.Setup(s => s.SelectCohortLevelFor(8)).Returns(4);

            var character = GenerateCharacter();
            Assert.That(character.Leadership.Cohort, Is.Not.Null);

            var cohort = character.Leadership.Cohort;
            Assert.That(cohort.Leadership.Cohort, Is.Null);
            Assert.That(cohort.Leadership.Followers, Is.Empty);
            Assert.That(cohort.Leadership.Score, Is.EqualTo(0));
        }

        [Test]
        public void CharacterReputationGenerated()
        {
            feats.Add(new Feat());
            feats[0].Name = FeatConstants.Leadership;

            ability.Stats[StatConstants.Charisma] = new Stat { Value = 16 };
            characterClass.Level = 5;

            mockPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Percentile.Reputation)).Returns("reputable");

            var reputationAjustments = new Dictionary<String, Int32>();
            reputationAjustments["reputable"] = 0;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.LeadershipModifiers)).Returns(reputationAjustments);

            var character = GenerateCharacter();
            Assert.That(character.Leadership.LeadershipModifiers, Contains.Item("reputable"));
        }

        [Test]
        public void CharacterReputationAdjustmentIsApplied()
        {
            feats.Add(new Feat());
            feats[0].Name = FeatConstants.Leadership;

            ability.Stats[StatConstants.Charisma] = new Stat { Value = 16 };
            characterClass.Level = 5;

            mockPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Percentile.Reputation)).Returns("reputable");

            var reputationAjustments = new Dictionary<String, Int32>();
            reputationAjustments["reputable"] = 7;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.LeadershipModifiers)).Returns(reputationAjustments);

            var character = GenerateCharacter();
            Assert.That(character.Leadership.LeadershipModifiers, Contains.Item("reputable"));
            Assert.That(character.Leadership.Score, Is.EqualTo(15));
        }

        [Test]
        public void NegativeCharacterReputationAdjustmentIsApplied()
        {
            feats.Add(new Feat());
            feats[0].Name = FeatConstants.Leadership;

            ability.Stats[StatConstants.Charisma] = new Stat { Value = 16 };
            characterClass.Level = 5;

            mockPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Percentile.Reputation)).Returns("reputable");

            var reputationAjustments = new Dictionary<String, Int32>();
            reputationAjustments["reputable"] = -4;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.LeadershipModifiers)).Returns(reputationAjustments);

            var character = GenerateCharacter();
            Assert.That(character.Leadership.LeadershipModifiers, Contains.Item("reputable"));
            Assert.That(character.Leadership.Score, Is.EqualTo(4));
        }

        [Test]
        public void CohortLevelIsSetBasedOnCharacterLeadershipScore()
        {
            feats.Add(new Feat());
            feats[0].Name = FeatConstants.Leadership;

            ability.Stats[StatConstants.Charisma] = new Stat { Value = 16 };
            characterClass.Level = 5;

            mockLeadershipSelector.Setup(s => s.SelectCohortLevelFor(8)).Returns(2);

            var character = GenerateCharacter();
            Assert.That(character.Leadership.Score, Is.EqualTo(8));
            Assert.That(character.Leadership.Cohort, Is.Not.Null);
            mockCharacterClassGenerator.Verify(g => g.GenerateWith(It.IsAny<Alignment>(), It.IsAny<ILevelRandomizer>(), It.IsAny<IClassNameRandomizer>()), Times.Exactly(2));
            mockCharacterClassGenerator.Verify(g => g.GenerateWith(alignment, mockLevelRandomizer.Object, mockClassNameRandomizer.Object), Times.Once);
            mockCharacterClassGenerator.Verify(g => g.GenerateWith(It.IsAny<Alignment>(), It.Is<ISetLevelRandomizer>(r => r.SetLevel == 2), mockAnyClassNameRandomizer.Object), Times.Once);
        }

        [Test]
        public void CohortLevelIs2LessThanCharacterLevel()
        {
            feats.Add(new Feat());
            feats[0].Name = FeatConstants.Leadership;

            ability.Stats[StatConstants.Charisma] = new Stat { Value = 16 };
            characterClass.Level = 5;

            mockLeadershipSelector.Setup(s => s.SelectCohortLevelFor(8)).Returns(4);

            var character = GenerateCharacter();
            Assert.That(character.Leadership.Score, Is.EqualTo(8));
            Assert.That(character.Leadership.Cohort, Is.Not.Null);

            var cohort = character.Leadership.Cohort;
            mockCharacterClassGenerator.Verify(g => g.GenerateWith(It.IsAny<Alignment>(), It.IsAny<ILevelRandomizer>(), It.IsAny<IClassNameRandomizer>()), Times.Exactly(2));
            mockCharacterClassGenerator.Verify(g => g.GenerateWith(alignment, mockLevelRandomizer.Object, mockClassNameRandomizer.Object), Times.Once);
            mockCharacterClassGenerator.Verify(g => g.GenerateWith(It.IsAny<Alignment>(), It.Is<ISetLevelRandomizer>(r => r.SetLevel == 3), mockAnyClassNameRandomizer.Object), Times.Once);
        }

        [Test]
        public void CohortIsGeneratedIndependently()
        {
            feats.Add(new Feat());
            feats[0].Name = FeatConstants.Leadership;

            ability.Stats[StatConstants.Charisma] = new Stat { Value = 16 };
            characterClass.Level = 5;

            mockLeadershipSelector.Setup(s => s.SelectCohortLevelFor(8)).Returns(4);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.AlignmentGroups, alignment.ToString()))
                .Returns(new[] { alignment.ToString() });

            var cohortAlignment = new Alignment();
            cohortAlignment.Goodness = alignment.Goodness;
            cohortAlignment.Lawfulness = alignment.Lawfulness;

            var cohortClass = new CharacterClass();
            cohortClass.Level = 1;

            var cohortRace = new Race();
            var cohortAbility = new Ability();
            var cohortCombat = new Combat();
            var cohortEquipment = new Equipment();
            var cohortMagic = new Magic();
            var cohortBaseAttack = new BaseAttack();

            mockAlignmentGenerator.Setup(g => g.GenerateWith(mockSetAlignmentRandomizer.Object)).Returns(cohortAlignment);
            mockCharacterClassGenerator.Setup(g => g.GenerateWith(cohortAlignment, It.Is<ISetLevelRandomizer>(r => r.SetLevel == 3), mockAnyClassNameRandomizer.Object)).Returns(cohortClass);
            mockRaceGenerator.Setup(g => g.GenerateWith(cohortAlignment, cohortClass, mockAnyBaseRaceRandomizer.Object, mockAnyMetaraceRandomizer.Object)).Returns(cohortRace);
            mockAbilitiesGenerator.Setup(g => g.GenerateWith(cohortClass, cohortRace, mockRawStatRandomizer.Object, cohortBaseAttack)).Returns(cohortAbility);
            mockCombatGenerator.Setup(g => g.GenerateBaseAttackWith(cohortClass, cohortRace)).Returns(cohortBaseAttack);
            mockCombatGenerator.Setup(g => g.GenerateWith(cohortBaseAttack, cohortClass, cohortRace, cohortAbility.Feats, cohortAbility.Stats, cohortEquipment)).Returns(cohortCombat);
            mockTreasureGenerator.Setup(g => g.GenerateWith(cohortAbility.Feats, cohortClass)).Returns(cohortEquipment);
            mockMagicGenerator.Setup(g => g.GenerateWith(cohortClass, cohortAbility.Feats, cohortEquipment)).Returns(cohortMagic);
            mockPercentileSelector.SetupSequence(s => s.SelectFrom(TableNameConstants.Set.Percentile.Traits)).Returns("character is interesting").Returns("cohort is interesting");

            var character = GenerateCharacter();
            Assert.That(character.Leadership.Cohort, Is.Not.Null);

            var cohort = character.Leadership.Cohort;
            Assert.That(cohort.Alignment, Is.EqualTo(cohortAlignment));
            Assert.That(cohort.Class, Is.EqualTo(cohortClass));
            Assert.That(cohort.Race, Is.EqualTo(cohortRace));
            Assert.That(cohort.Ability, Is.EqualTo(cohortAbility));
            Assert.That(cohort.Combat, Is.EqualTo(cohortCombat));
            Assert.That(cohort.Equipment, Is.EqualTo(cohortEquipment));
            Assert.That(cohort.InterestingTrait, Is.EqualTo("cohort is interesting"));
            Assert.That(cohort.Magic, Is.EqualTo(cohortMagic));
        }

        [Test]
        public void FamiliarsDecreaseScoreOfAttractingCohorts()
        {
            feats.Add(new Feat());
            feats[0].Name = FeatConstants.Leadership;

            ability.Stats[StatConstants.Charisma] = new Stat { Value = 16 };
            characterClass.Level = 5;
            magic.Familiar.Animal = "animal";

            mockLeadershipSelector.Setup(s => s.SelectCohortLevelFor(6)).Returns(2);

            var character = GenerateCharacter();
            Assert.That(character.Leadership.Score, Is.EqualTo(8));
            Assert.That(character.Leadership.Cohort, Is.Not.Null);
            mockCharacterClassGenerator.Verify(g => g.GenerateWith(It.IsAny<Alignment>(), It.IsAny<ILevelRandomizer>(), It.IsAny<IClassNameRandomizer>()), Times.Exactly(2));
            mockCharacterClassGenerator.Verify(g => g.GenerateWith(alignment, mockLevelRandomizer.Object, mockClassNameRandomizer.Object), Times.Once);
            mockCharacterClassGenerator.Verify(g => g.GenerateWith(It.IsAny<Alignment>(), It.Is<ISetLevelRandomizer>(r => r.SetLevel == 2), mockAnyClassNameRandomizer.Object), Times.Once);
        }

        [Test]
        public void KillingCohortsDecreasesScoreOfAttractingCohorts()
        {
            feats.Add(new Feat());
            feats[0].Name = FeatConstants.Leadership;

            ability.Stats[StatConstants.Charisma] = new Stat { Value = 16 };
            characterClass.Level = 5;

            mockBooleanPercentileSelector.SetupSequence(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.KilledCohort)).Returns(true).Returns(false);
            mockLeadershipSelector.Setup(s => s.SelectCohortLevelFor(6)).Returns(2);

            var character = GenerateCharacter();
            Assert.That(character.Leadership.Score, Is.EqualTo(8));
            Assert.That(character.Leadership.Cohort, Is.Not.Null);
            Assert.That(character.Leadership.LeadershipModifiers, Contains.Item("Caused the death of 1 cohort(s)"));
            mockCharacterClassGenerator.Verify(g => g.GenerateWith(It.IsAny<Alignment>(), It.IsAny<ILevelRandomizer>(), It.IsAny<IClassNameRandomizer>()), Times.Exactly(2));
            mockCharacterClassGenerator.Verify(g => g.GenerateWith(alignment, mockLevelRandomizer.Object, mockClassNameRandomizer.Object), Times.Once);
            mockCharacterClassGenerator.Verify(g => g.GenerateWith(It.IsAny<Alignment>(), It.Is<ISetLevelRandomizer>(r => r.SetLevel == 2), mockAnyClassNameRandomizer.Object), Times.Once);
        }

        [Test]
        public void KillingMultipleCohortsDecreasesScoreOfAttractingCohorts()
        {
            feats.Add(new Feat());
            feats[0].Name = FeatConstants.Leadership;

            ability.Stats[StatConstants.Charisma] = new Stat { Value = 16 };
            characterClass.Level = 5;

            mockBooleanPercentileSelector.SetupSequence(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.KilledCohort))
                .Returns(true).Returns(true).Returns(false);
            mockLeadershipSelector.Setup(s => s.SelectCohortLevelFor(4)).Returns(2);

            var character = GenerateCharacter();
            Assert.That(character.Leadership.Score, Is.EqualTo(8));
            Assert.That(character.Leadership.Cohort, Is.Not.Null);
            Assert.That(character.Leadership.LeadershipModifiers, Contains.Item("Caused the death of 2 cohort(s)"));
            mockCharacterClassGenerator.Verify(g => g.GenerateWith(It.IsAny<Alignment>(), It.IsAny<ILevelRandomizer>(), It.IsAny<IClassNameRandomizer>()), Times.Exactly(2));
            mockCharacterClassGenerator.Verify(g => g.GenerateWith(alignment, mockLevelRandomizer.Object, mockClassNameRandomizer.Object), Times.Once);
            mockCharacterClassGenerator.Verify(g => g.GenerateWith(It.IsAny<Alignment>(), It.Is<ISetLevelRandomizer>(r => r.SetLevel == 2), mockAnyClassNameRandomizer.Object), Times.Once);
        }

        [Test]
        public void CohortCannotOpposeAlignment()
        {
            feats.Add(new Feat());
            feats[0].Name = FeatConstants.Leadership;

            ability.Stats[StatConstants.Charisma] = new Stat { Value = 16 };
            characterClass.Level = 5;
            alignment.Goodness = "goodness";
            alignment.Lawfulness = "lawfulness";

            mockLeadershipSelector.Setup(s => s.SelectCohortLevelFor(8)).Returns(4);

            var incompatibleAlignment1 = new Alignment();
            var incompatibleAlignment2 = new Alignment();
            var incompatibleAlignment3 = new Alignment();
            var cohortAlignment = new Alignment();

            incompatibleAlignment1.Goodness = "wrong goodness";
            incompatibleAlignment1.Lawfulness = "lawfulness";
            incompatibleAlignment2.Goodness = "goodness";
            incompatibleAlignment2.Lawfulness = "wrong lawfulness";
            incompatibleAlignment3.Goodness = "wrong goodness";
            incompatibleAlignment3.Lawfulness = "wrong lawfulness";
            cohortAlignment.Goodness = "cohort goodness";
            cohortAlignment.Lawfulness = "cohort lawfulness";

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.AlignmentGroups, alignment.ToString()))
                .Returns(new[] { "goodness lawfulness", cohortAlignment.ToString() });

            mockAlignmentGenerator.SetupSequence(g => g.GenerateWith(mockSetAlignmentRandomizer.Object))
                .Returns(incompatibleAlignment1).Returns(incompatibleAlignment3).Returns(incompatibleAlignment2).Returns(cohortAlignment);

            var character = GenerateCharacter();
            Assert.That(character.Leadership.Cohort, Is.Not.Null);

            var cohort = character.Leadership.Cohort;
            Assert.That(cohort.Alignment, Is.EqualTo(cohortAlignment));
        }

        [Test]
        public void AttractCohortOfSameAlignment()
        {
            feats.Add(new Feat());
            feats[0].Name = FeatConstants.Leadership;

            ability.Stats[StatConstants.Charisma] = new Stat { Value = 16 };
            characterClass.Level = 5;

            mockLeadershipSelector.Setup(s => s.SelectCohortLevelFor(8)).Returns(4);
            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.AttractCohortOfDifferentAlignment)).Returns(false);

            alignment.Goodness = "goodness";
            alignment.Lawfulness = "lawfulness";
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.AlignmentGroups, alignment.ToString()))
                .Returns(new[] { alignment.ToString() });

            var cohortAlignment = new Alignment();
            cohortAlignment.Goodness = alignment.Goodness;
            cohortAlignment.Lawfulness = alignment.Lawfulness;
            mockAlignmentGenerator.Setup(g => g.GenerateWith(mockSetAlignmentRandomizer.Object)).Returns(cohortAlignment);

            var character = GenerateCharacter();
            Assert.That(character.Leadership.Cohort, Is.Not.Null);

            var setAlignmentRandomizer = mockSetAlignmentRandomizer.Object;
            Assert.That(setAlignmentRandomizer.SetAlignment, Is.EqualTo(alignment));

            var cohort = character.Leadership.Cohort;
            Assert.That(cohort.Alignment, Is.EqualTo(cohortAlignment));
        }

        [Test]
        public void AttractCohortOfDifferingAlignment()
        {
            feats.Add(new Feat());
            feats[0].Name = FeatConstants.Leadership;

            ability.Stats[StatConstants.Charisma] = new Stat { Value = 16 };
            characterClass.Level = 5;

            mockLeadershipSelector.Setup(s => s.SelectCohortLevelFor(7)).Returns(4);
            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.AttractCohortOfDifferentAlignment)).Returns(true);

            var cohortAlignment = new Alignment();
            cohortAlignment.Goodness = "alignment";
            cohortAlignment.Lawfulness = "different";

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.AlignmentGroups, alignment.ToString()))
                .Returns(new[] { "lawfulness goodness", cohortAlignment.ToString() });

            mockAlignmentGenerator.Setup(g => g.GenerateWith(mockAnyAlignmentRandomizer.Object)).Returns(cohortAlignment);

            var character = GenerateCharacter();
            Assert.That(character.Leadership.Cohort, Is.Not.Null);

            var cohort = character.Leadership.Cohort;
            Assert.That(cohort.Alignment, Is.EqualTo(cohortAlignment));
        }

        [Test]
        public void IfSelectedCohortLevelIs0_DoNotGenerateCohort()
        {
            feats.Add(new Feat());
            feats[0].Name = FeatConstants.Leadership;

            ability.Stats[StatConstants.Charisma] = new Stat { Value = 16 };
            characterClass.Level = 5;

            mockLeadershipSelector.Setup(s => s.SelectCohortLevelFor(8)).Returns(0);

            var character = GenerateCharacter();
            Assert.That(character.Leadership.Score, Is.EqualTo(8));
            Assert.That(character.Leadership.Cohort, Is.Null);
        }

        [Test]
        public void CohortsCannotReceiveFollowers()
        {
            feats.Add(new Feat());
            feats[0].Name = FeatConstants.Leadership;

            ability.Stats[StatConstants.Charisma] = new Stat { Value = 16 };
            characterClass.Level = 5;

            mockLeadershipSelector.Setup(s => s.SelectCohortLevelFor(8)).Returns(4);

            var cohortAlignment = new Alignment();
            var cohortClass = characterClass;
            var cohortRace = new Race();
            var cohortAbility = new Ability();

            cohortAbility.Feats = feats;
            cohortAbility.Stats = ability.Stats;

            mockAlignmentGenerator.Setup(g => g.GenerateWith(mockSetAlignmentRandomizer.Object)).Returns(cohortAlignment);
            mockCharacterClassGenerator.Setup(g => g.GenerateWith(cohortAlignment, It.Is<ISetLevelRandomizer>(r => r.SetLevel == 3), mockAnyClassNameRandomizer.Object)).Returns(cohortClass);
            mockRaceGenerator.Setup(g => g.GenerateWith(cohortAlignment, cohortClass, mockAnyBaseRaceRandomizer.Object, mockAnyMetaraceRandomizer.Object)).Returns(cohortRace);
            mockAbilitiesGenerator.Setup(g => g.GenerateWith(cohortClass, cohortRace, mockRawStatRandomizer.Object, It.IsAny<BaseAttack>())).Returns(cohortAbility);

            var character = GenerateCharacter();
            Assert.That(character.Leadership.Cohort, Is.Not.Null);

            var cohort = character.Leadership.Cohort;
            Assert.That(cohort.Ability, Is.EqualTo(cohortAbility));
            Assert.That(cohort.Leadership.Cohort, Is.Null);
            Assert.That(cohort.Leadership.Followers, Is.Empty);
            Assert.That(cohort.Leadership.LeadershipModifiers, Is.Empty);
            Assert.That(cohort.Leadership.Score, Is.EqualTo(0));
        }

        [Test]
        public void FollowersGenerated()
        {
            feats.Add(new Feat());
            feats[0].Name = FeatConstants.Leadership;

            ability.Stats[StatConstants.Charisma] = new Stat { Value = 16 };
            characterClass.Level = 5;

            followerQuantities.Level1 = 6;
            followerQuantities.Level2 = 5;
            followerQuantities.Level3 = 4;
            followerQuantities.Level4 = 3;
            followerQuantities.Level5 = 2;
            followerQuantities.Level6 = 1;
            mockLeadershipSelector.Setup(s => s.SelectFollowerQuantitiesFor(8)).Returns(followerQuantities);

            var character = GenerateCharacter();
            Assert.That(character.Leadership.Followers.Count(), Is.EqualTo(21));

            foreach (var follower in character.Leadership.Followers)
            {
                Assert.That(follower.Leadership.Cohort, Is.Null);
                Assert.That(follower.Leadership.Followers, Is.Empty);
                Assert.That(follower.Leadership.Score, Is.EqualTo(0));
            }
        }

        [Test]
        public void FollowersGeneratedIndependently()
        {
            feats.Add(new Feat());
            feats[0].Name = FeatConstants.Leadership;

            ability.Stats[StatConstants.Charisma] = new Stat { Value = 16 };
            characterClass.Level = 5;

            followerQuantities.Level1 = 2;
            followerQuantities.Level2 = 2;
            followerQuantities.Level3 = 2;
            followerQuantities.Level4 = 2;
            followerQuantities.Level5 = 2;
            followerQuantities.Level6 = 2;
            mockLeadershipSelector.Setup(s => s.SelectFollowerQuantitiesFor(8)).Returns(followerQuantities);

            var followerAlignments = new List<Alignment>();
            var followerClasses = new List<CharacterClass>();
            var followerRaces = new List<Race>();
            var followerBaseAttacks = new List<BaseAttack>();
            var followerAbilities = new List<Ability>();
            var followerEquipments = new List<Equipment>();
            var followerCombats = new List<Combat>();
            var followerMagics = new List<Magic>();

            var followerAlignmentSequence = mockAlignmentGenerator.SetupSequence(g => g.GenerateWith(mockAnyAlignmentRandomizer.Object));

            while (followerAlignments.Count < 12)
            {
                var followerAlignment = new Alignment();
                var followerClass = new CharacterClass();
                var followerRace = new Race();
                var followerBaseAttack = new BaseAttack();
                var followerAbility = new Ability();
                var followerEquipment = new Equipment();
                var followerCombat = new Combat();
                var followerMagic = new Magic();

                followerAlignment.Goodness = followerAlignments.Count.ToString();
                followerAlignment.Lawfulness = "alignment";
                followerClass.Level = followerAlignments.Count / 2 + 1;

                followerAlignments.Add(followerAlignment);
                allowedAlignments.Add(followerAlignment.ToString());
                followerClasses.Add(followerClass);
                followerRaces.Add(followerRace);
                followerBaseAttacks.Add(followerBaseAttack);
                followerAbilities.Add(followerAbility);
                followerEquipments.Add(followerEquipment);
                followerCombats.Add(followerCombat);
                followerMagics.Add(followerMagic);

                followerAlignmentSequence = followerAlignmentSequence.Returns(followerAlignment);
                mockCharacterClassGenerator.Setup(g => g.GenerateWith(followerAlignment, It.Is<ISetLevelRandomizer>(r => r.SetLevel == followerClass.Level), mockAnyClassNameRandomizer.Object))
                    .Returns(followerClass);
                mockRaceGenerator.Setup(g => g.GenerateWith(followerAlignment, followerClass, mockAnyBaseRaceRandomizer.Object, mockAnyMetaraceRandomizer.Object))
                    .Returns(followerRace);
                mockCombatGenerator.Setup(g => g.GenerateBaseAttackWith(followerClass, followerRace))
                    .Returns(followerBaseAttack);
                mockAbilitiesGenerator.Setup(g => g.GenerateWith(followerClass, followerRace, mockRawStatRandomizer.Object, followerBaseAttack))
                    .Returns(followerAbility);
                mockTreasureGenerator.Setup(g => g.GenerateWith(followerAbility.Feats, followerClass))
                    .Returns(followerEquipment);
                mockCombatGenerator.Setup(g => g.GenerateWith(followerBaseAttack, followerClass, followerRace, followerAbility.Feats, followerAbility.Stats, followerEquipment))
                    .Returns(followerCombat);
                mockMagicGenerator.Setup(g => g.GenerateWith(followerClass, followerAbility.Feats, followerEquipment))
                    .Returns(followerMagic);
            }

            var followerTraitSequence = mockPercentileSelector.SetupSequence(s => s.SelectFrom(TableNameConstants.Set.Percentile.Traits)).Returns("character is interesting");
            for (var i = 0; i < followerAlignments.Count; i++)
                followerTraitSequence = followerTraitSequence.Returns(i.ToString());

            var character = GenerateCharacter();

            for (var i = 0; i < character.Leadership.Followers.Count(); i++)
            {
                var follower = character.Leadership.Followers.ElementAt(i);

                Assert.That(follower.Alignment, Is.EqualTo(followerAlignments[i]));
                Assert.That(follower.Class, Is.EqualTo(followerClasses[i]));
                Assert.That(follower.Race, Is.EqualTo(followerRaces[i]));
                Assert.That(follower.Ability, Is.EqualTo(followerAbilities[i]));
                Assert.That(follower.Combat, Is.EqualTo(followerCombats[i]));
                Assert.That(follower.Equipment, Is.EqualTo(followerEquipments[i]));
                Assert.That(follower.InterestingTrait, Is.EqualTo(i.ToString()));
                Assert.That(follower.Magic, Is.EqualTo(followerMagics[i]));
            }
        }

        [Test]
        public void GenerateLeadershipMovementFactorsAndApplyThem()
        {
            feats.Add(new Feat());
            feats[0].Name = FeatConstants.Leadership;

            ability.Stats[StatConstants.Charisma] = new Stat { Value = 16 };
            characterClass.Level = 5;

            mockPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Percentile.LeadershipMovement)).Returns("moves");

            var leadershipAdjustments = new Dictionary<String, Int32>();
            leadershipAdjustments["moves"] = 9266;
            leadershipAdjustments["murders"] = -5;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.LeadershipModifiers)).Returns(leadershipAdjustments);

            followerQuantities.Level1 = 6;
            followerQuantities.Level2 = 5;
            followerQuantities.Level3 = 4;
            followerQuantities.Level4 = 3;
            followerQuantities.Level5 = 2;
            followerQuantities.Level6 = 1;
            mockLeadershipSelector.Setup(s => s.SelectFollowerQuantitiesFor(9274)).Returns(followerQuantities);

            var character = GenerateCharacter();
            Assert.That(character.Leadership.Followers.Count(), Is.EqualTo(21));
        }

        [Test]
        public void CharacterDoesNotHaveEmptyLeadershipModifiers()
        {
            feats.Add(new Feat());
            feats[0].Name = FeatConstants.Leadership;

            ability.Stats[StatConstants.Charisma] = new Stat { Value = 16 };
            characterClass.Level = 5;

            mockPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Percentile.LeadershipMovement)).Returns(String.Empty);

            followerQuantities.Level1 = 6;
            followerQuantities.Level2 = 5;
            followerQuantities.Level3 = 4;
            followerQuantities.Level4 = 3;
            followerQuantities.Level5 = 2;
            followerQuantities.Level6 = 1;
            mockLeadershipSelector.Setup(s => s.SelectFollowerQuantitiesFor(8)).Returns(followerQuantities);

            var character = GenerateCharacter();
            Assert.That(character.Leadership.Followers.Count(), Is.EqualTo(21));
            Assert.That(character.Leadership.LeadershipModifiers, Is.Empty);
        }

        [Test]
        public void GenerateWhetherCharacterHasCausedFollowerDeathsAndApply()
        {
            feats.Add(new Feat());
            feats[0].Name = FeatConstants.Leadership;

            ability.Stats[StatConstants.Charisma] = new Stat { Value = 16 };
            characterClass.Level = 5;

            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.KilledFollowers)).Returns(true);

            followerQuantities.Level1 = 6;
            followerQuantities.Level2 = 5;
            followerQuantities.Level3 = 4;
            followerQuantities.Level4 = 3;
            followerQuantities.Level5 = 2;
            followerQuantities.Level6 = 1;
            mockLeadershipSelector.Setup(s => s.SelectFollowerQuantitiesFor(7)).Returns(followerQuantities);

            var character = GenerateCharacter();
            Assert.That(character.Leadership.Followers.Count(), Is.EqualTo(21));
        }

        [Test]
        public void FollowersCannotReceiveFollowers()
        {
            feats.Add(new Feat());
            feats[0].Name = FeatConstants.Leadership;

            ability.Stats[StatConstants.Charisma] = new Stat { Value = 16 };
            characterClass.Level = 5;

            followerQuantities.Level1 = 2;
            followerQuantities.Level2 = 2;
            followerQuantities.Level3 = 2;
            followerQuantities.Level4 = 2;
            followerQuantities.Level5 = 2;
            followerQuantities.Level6 = 2;
            mockLeadershipSelector.Setup(s => s.SelectFollowerQuantitiesFor(8)).Returns(followerQuantities);

            var followerAlignments = new List<Alignment>();
            var followerClasses = new List<CharacterClass>();
            var followerRaces = new List<Race>();
            var followerBaseAttacks = new List<BaseAttack>();
            var followerAbilities = new List<Ability>();
            var followerEquipments = new List<Equipment>();
            var followerCombats = new List<Combat>();
            var followerMagics = new List<Magic>();

            var followerAlignmentSequence = mockAlignmentGenerator.SetupSequence(g => g.GenerateWith(mockAnyAlignmentRandomizer.Object));

            while (followerAlignments.Count < 12)
            {
                var followerAlignment = new Alignment();
                var followerClass = new CharacterClass();
                var followerRace = new Race();
                var followerBaseAttack = new BaseAttack();
                var followerAbility = new Ability();

                followerAlignment.Goodness = followerAlignments.Count.ToString();
                followerAlignment.Lawfulness = "alignment";
                followerClass.Level = followerAlignments.Count / 2 + 1;
                followerAbility.Feats = feats;
                followerAbility.Stats = ability.Stats;

                followerAlignments.Add(followerAlignment);
                allowedAlignments.Add(followerAlignment.ToString());
                followerClasses.Add(followerClass);
                followerRaces.Add(followerRace);
                followerBaseAttacks.Add(followerBaseAttack);
                followerAbilities.Add(followerAbility);

                followerAlignmentSequence = followerAlignmentSequence.Returns(followerAlignment);
                mockCharacterClassGenerator.Setup(g => g.GenerateWith(followerAlignment, It.Is<ISetLevelRandomizer>(r => r.SetLevel == followerClass.Level), mockAnyClassNameRandomizer.Object))
                    .Returns(followerClass);
                mockRaceGenerator.Setup(g => g.GenerateWith(followerAlignment, followerClass, mockAnyBaseRaceRandomizer.Object, mockAnyMetaraceRandomizer.Object))
                    .Returns(followerRace);
                mockCombatGenerator.Setup(g => g.GenerateBaseAttackWith(followerClass, followerRace))
                    .Returns(followerBaseAttack);
                mockAbilitiesGenerator.Setup(g => g.GenerateWith(followerClass, followerRace, mockRawStatRandomizer.Object, followerBaseAttack))
                    .Returns(followerAbility);
            }

            var character = GenerateCharacter();
            Assert.That(character.Leadership.Followers.Count(), Is.EqualTo(12));

            for (var i = 0; i < character.Leadership.Followers.Count(); i++)
            {
                var follower = character.Leadership.Followers.ElementAt(i);

                Assert.That(follower.Leadership.Score, Is.EqualTo(0));
                Assert.That(follower.Leadership.Cohort, Is.Null);
                Assert.That(follower.Leadership.Followers, Is.Empty);
                Assert.That(follower.Leadership.LeadershipModifiers, Is.Empty);
            }
        }

        [Test]
        public void FollowersCannotOpposeAlignment()
        {
            feats.Add(new Feat());
            feats[0].Name = FeatConstants.Leadership;

            ability.Stats[StatConstants.Charisma] = new Stat { Value = 16 };
            characterClass.Level = 5;
            alignment.Goodness = "goodness";
            alignment.Lawfulness = "lawfulness";

            followerQuantities.Level1 = 2;
            followerQuantities.Level2 = 2;
            followerQuantities.Level3 = 2;
            followerQuantities.Level4 = 2;
            followerQuantities.Level5 = 2;
            followerQuantities.Level6 = 2;
            mockLeadershipSelector.Setup(s => s.SelectFollowerQuantitiesFor(8)).Returns(followerQuantities);

            var followerAlignments = new List<Alignment>();
            var followerAlignmentSequence = mockAlignmentGenerator.SetupSequence(g => g.GenerateWith(mockAnyAlignmentRandomizer.Object));

            while (followerAlignments.Count < 12)
            {
                var incompatibleAlignment1 = new Alignment();
                var incompatibleAlignment2 = new Alignment();
                var incompatibleAlignment3 = new Alignment();
                var followerAlignment = new Alignment();

                followerAlignment.Goodness = "follower goodness";
                followerAlignment.Lawfulness = "follower lawfulness";
                incompatibleAlignment1.Goodness = "wrong goodness";
                incompatibleAlignment1.Lawfulness = "lawfulness";
                incompatibleAlignment2.Goodness = "goodness";
                incompatibleAlignment2.Lawfulness = "wrong lawfulness";
                incompatibleAlignment3.Goodness = "wrong goodness";
                incompatibleAlignment3.Lawfulness = "wrong lawfulness";

                followerAlignments.Add(followerAlignment);
                followerAlignmentSequence = followerAlignmentSequence.Returns(incompatibleAlignment1).Returns(incompatibleAlignment3).Returns(incompatibleAlignment2).Returns(followerAlignment);
            }

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.AlignmentGroups, alignment.ToString()))
                .Returns(new[] { alignment.ToString(), followerAlignments[0].ToString() });

            var character = GenerateCharacter();
            Assert.That(character.Leadership.Followers.Count(), Is.EqualTo(12));

            for (var i = 0; i < character.Leadership.Followers.Count(); i++)
            {
                var follower = character.Leadership.Followers.ElementAt(i);
                Assert.That(follower.Alignment, Is.EqualTo(followerAlignments[i]));
            }
        }
    }
}