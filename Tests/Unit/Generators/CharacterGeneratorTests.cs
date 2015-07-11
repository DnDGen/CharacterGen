using System;
using System.Collections.Generic;
using Moq;
using NPCGen.Common;
using NPCGen.Common.Abilities;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Abilities.Skills;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Combats;
using NPCGen.Common.Items;
using NPCGen.Common.Magics;
using NPCGen.Common.Races;
using NPCGen.Generators;
using NPCGen.Generators.Interfaces;
using NPCGen.Generators.Interfaces.Abilities;
using NPCGen.Generators.Interfaces.Combats;
using NPCGen.Generators.Interfaces.Items;
using NPCGen.Generators.Interfaces.Magics;
using NPCGen.Generators.Interfaces.Randomizers.Alignments;
using NPCGen.Generators.Interfaces.Randomizers.CharacterClasses;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NPCGen.Generators.Interfaces.Randomizers.Stats;
using NPCGen.Generators.Interfaces.Verifiers;
using NPCGen.Generators.Interfaces.Verifiers.Exceptions;
using NPCGen.Selectors.Interfaces;
using NPCGen.Selectors.Interfaces.Objects;
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
        private Mock<ILeadershipSelector> mockLeadershipSelector;
        private Mock<IEquipmentGenerator> mockEquipmentGenerator;
        private Mock<IMagicGenerator> mockMagicGenerator;
        private Mock<ISetLevelRandomizer> mockSetLevelRandomizer;
        private Mock<ISetAlignmentRandomizer> mockSetAlignmentRandomizer;
        private Mock<IClassNameRandomizer> mockAnyClassNameRandomizer;
        private Mock<IBaseRaceRandomizer> mockAnyBaseRaceRandomizer;
        private Mock<IMetaraceRandomizer> mockAnyMetaraceRandomizer;
        private Mock<IStatsRandomizer> mockRawStatRandomizer;
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
            mockSetLevelRandomizer = new Mock<ISetLevelRandomizer>();
            mockSetAlignmentRandomizer = new Mock<ISetAlignmentRandomizer>();
            followerQuantities = new FollowerQuantities();

            levelAdjustments[BaseRaceId] = 0;
            levelAdjustments[BaseRacePlusOneId] = 1;
            levelAdjustments[RaceConstants.Metaraces.NoneId] = 0;
            levelAdjustments[MetaraceId] = 1;
            mockAdjustmentsSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments)).Returns(levelAdjustments);

            mockRandomizerVerifier.Setup(v => v.VerifyCompatibility(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object,
                mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object)).Returns(true);
            mockRandomizerVerifier.Setup(v => v.VerifyAlignmentCompatibility(It.IsAny<Alignment>(), mockClassNameRandomizer.Object,
                mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object)).Returns(true);
            mockRandomizerVerifier.Setup(v => v.VerifyCharacterClassCompatibility(It.IsAny<String>(), It.IsAny<CharacterClass>(),
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object)).Returns(true);

            mockLeadershipSelector.Setup(s => s.SelectFollowerQuantitiesFor(It.IsAny<Int32>())).Returns(new FollowerQuantities());

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

            mockSetAlignmentRandomizer = new Mock<ISetAlignmentRandomizer>();
            mockSetLevelRandomizer = new Mock<ISetLevelRandomizer>();
            mockAnyClassNameRandomizer = new Mock<IClassNameRandomizer>();
            mockAnyBaseRaceRandomizer = new Mock<IBaseRaceRandomizer>();
            mockAnyMetaraceRandomizer = new Mock<IMetaraceRandomizer>();
            mockRawStatRandomizer = new Mock<IStatsRandomizer>();

        }

        private void SetUpGenerators()
        {
            mockAlignmentGenerator = new Mock<IAlignmentGenerator>();
            mockAbilitiesGenerator = new Mock<IAbilitiesGenerator>();
            mockCombatGenerator = new Mock<ICombatGenerator>();
            mockEquipmentGenerator = new Mock<IEquipmentGenerator>();
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
            race.BaseRace.Id = BaseRaceId;
            race.Metarace.Id = RaceConstants.Metaraces.NoneId;
            ability.Feats = feats;

            mockAlignmentGenerator.Setup(f => f.GenerateWith(mockAlignmentRandomizer.Object)).Returns(alignment);
            mockCharacterClassGenerator.Setup(f => f.GenerateWith(alignment, mockLevelRandomizer.Object,
                mockClassNameRandomizer.Object)).Returns(characterClass);
            mockRaceGenerator.Setup(f => f.GenerateWith(alignment, characterClass, mockBaseRaceRandomizer.Object,
                mockMetaraceRandomizer.Object)).Returns(race);
            mockAbilitiesGenerator.Setup(g => g.GenerateWith(characterClass, race, mockStatsRandomizer.Object, baseAttack)).Returns(ability);
            mockEquipmentGenerator.Setup(g => g.GenerateWith(ability.Feats, characterClass)).Returns(equipment);
            mockCombatGenerator.Setup(g => g.GenerateWith(baseAttack, characterClass, race, ability.Feats, ability.Stats, equipment)).Returns(combat);
            mockCombatGenerator.Setup(g => g.GenerateBaseAttackWith(characterClass, race)).Returns(baseAttack);
            mockMagicGenerator.Setup(g => g.GenerateWith(characterClass, ability.Feats, equipment)).Returns(magic);
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
            feats[0].Name.Id = "other feat";

            var character = GenerateCharacter();
            Assert.That(character.Leadership.IsFollower, Is.EqualTo(false));
            Assert.That(character.Leadership.Cohort, Is.Null);
            Assert.That(character.Leadership.Followers, Is.Empty);
            Assert.That(character.Leadership.Score, Is.EqualTo(0));
        }

        [Test]
        public void IfCharacterHasLeadershipFeat_LeadershipScoreIsLevelPlusCharismaModifier()
        {
            feats.Add(new Feat());
            feats[0].Name.Id = FeatConstants.LeadershipId;

            ability.Stats[StatConstants.Charisma] = new Stat { Value = 16 };
            characterClass.Level = 5;

            var character = GenerateCharacter();
            Assert.That(character.Leadership.IsFollower, Is.False);
            Assert.That(character.Leadership.Score, Is.EqualTo(8));
        }

        [Test]
        public void IfCharacterHasLeadership_GenerateCohort()
        {
            feats.Add(new Feat());
            feats[0].Name.Id = FeatConstants.LeadershipId;

            ability.Stats[StatConstants.Charisma] = new Stat { Value = 16 };
            characterClass.Level = 5;

            var character = GenerateCharacter();
            Assert.That(character.Leadership.IsFollower, Is.False);
            Assert.That(character.Leadership.Cohort, Is.Not.Null);

            var cohort = character.Leadership.Cohort;
            Assert.That(cohort.Leadership.IsFollower, Is.True);
            Assert.That(cohort.Leadership.Cohort, Is.Null);
            Assert.That(cohort.Leadership.Followers, Is.Empty);
            Assert.That(cohort.Leadership.Score, Is.EqualTo(0));
        }

        [Test]
        public void IfLeadershipScoreIs1_NoCohort()
        {
            feats.Add(new Feat());
            feats[0].Name.Id = FeatConstants.LeadershipId;

            ability.Stats[StatConstants.Charisma] = new Stat { Value = 10 };
            characterClass.Level = 1;

            var character = GenerateCharacter();
            Assert.That(character.Leadership.Score, Is.EqualTo(1));
            Assert.That(character.Leadership.IsFollower, Is.False);
            Assert.That(character.Leadership.Cohort, Is.Null);
            Assert.That(character.Leadership.Followers, Is.Empty);
            Assert.That(character.Leadership.Score, Is.EqualTo(0));
        }

        [Test]
        public void IfLeadershipScoreIsLessThan1_NoCohort()
        {
            feats.Add(new Feat());
            feats[0].Name.Id = FeatConstants.LeadershipId;

            ability.Stats[StatConstants.Charisma] = new Stat { Value = 9 };
            characterClass.Level = 1;

            var character = GenerateCharacter();
            Assert.That(character.Leadership.Score, Is.EqualTo(0));
            Assert.That(character.Leadership.IsFollower, Is.False);
            Assert.That(character.Leadership.Cohort, Is.Null);
            Assert.That(character.Leadership.Followers, Is.Empty);
            Assert.That(character.Leadership.Score, Is.EqualTo(0));
        }

        [Test]
        public void CharacterReputationGenerated()
        {
            feats.Add(new Feat());
            feats[0].Name.Id = FeatConstants.LeadershipId;

            ability.Stats[StatConstants.Charisma] = new Stat { Value = 16 };
            characterClass.Level = 5;

            mockPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Percentile.Reputation)).Returns("reputable");

            var character = GenerateCharacter();
            Assert.That(character.Leadership.LeadershipModifiers, Contains.Item("reputable"));
        }

        [Test]
        public void CharacterReputationAdjustmentIsApplied()
        {
            feats.Add(new Feat());
            feats[0].Name.Id = FeatConstants.LeadershipId;

            ability.Stats[StatConstants.Charisma] = new Stat { Value = 16 };
            characterClass.Level = 5;

            mockPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Percentile.Reputation)).Returns("reputable");

            var reputationAjustments = new Dictionary<String, Int32>();
            reputationAjustments["reputable"] = 7;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.ReputationAdjustments)).Returns(reputationAjustments);

            var character = GenerateCharacter();
            Assert.That(character.Leadership.LeadershipModifiers, Contains.Item("reputable"));
            Assert.That(character.Leadership.Score, Is.EqualTo(15));
        }

        [Test]
        public void NegativeCharacterReputationAdjustmentIsApplied()
        {
            feats.Add(new Feat());
            feats[0].Name.Id = FeatConstants.LeadershipId;

            ability.Stats[StatConstants.Charisma] = new Stat { Value = 16 };
            characterClass.Level = 5;

            mockPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Percentile.Reputation)).Returns("reputable");

            var reputationAjustments = new Dictionary<String, Int32>();
            reputationAjustments["reputable"] = -4;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.ReputationAdjustments)).Returns(reputationAjustments);

            var character = GenerateCharacter();
            Assert.That(character.Leadership.LeadershipModifiers, Contains.Item("reputable"));
            Assert.That(character.Leadership.Score, Is.EqualTo(4));
        }

        [Test]
        public void CohortLevelIsSetBasedOnCharacterLeadershipScore()
        {
            feats.Add(new Feat());
            feats[0].Name.Id = FeatConstants.LeadershipId;

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
            feats[0].Name.Id = FeatConstants.LeadershipId;

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
            feats[0].Name.Id = FeatConstants.LeadershipId;

            ability.Stats[StatConstants.Charisma] = new Stat { Value = 16 };
            characterClass.Level = 5;

            mockLeadershipSelector.Setup(s => s.SelectCohortLevelFor(8)).Returns(4);

            var cohortAlignment = new Alignment();
            var cohortClass = new CharacterClass();
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
            mockEquipmentGenerator.Setup(g => g.GenerateWith(cohortAbility.Feats, cohortClass)).Returns(cohortEquipment);
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
        public void GenerateFactorsThatAffectAttractingCohorts()
        {
            feats.Add(new Feat());
            feats[0].Name.Id = FeatConstants.LeadershipId;

            ability.Stats[StatConstants.Charisma] = new Stat { Value = 16 };
            characterClass.Level = 5;

            //set up the factors

            mockLeadershipSelector.Setup(s => s.SelectCohortLevelFor(8)).Returns(2);

            var character = GenerateCharacter();
            Assert.That(character.Leadership.Score, Is.EqualTo(8));
            Assert.That(character.Leadership.Cohort, Is.Not.Null);
            mockCharacterClassGenerator.Verify(g => g.GenerateWith(It.IsAny<Alignment>(), It.IsAny<ILevelRandomizer>(), It.IsAny<IClassNameRandomizer>()), Times.Exactly(2));
            mockCharacterClassGenerator.Verify(g => g.GenerateWith(alignment, mockLevelRandomizer.Object, mockClassNameRandomizer.Object), Times.Once);
            mockCharacterClassGenerator.Verify(g => g.GenerateWith(It.IsAny<Alignment>(), It.Is<ISetLevelRandomizer>(r => r.SetLevel == 2), mockAnyClassNameRandomizer.Object), Times.Once);
        }

        [Test]
        public void CohortCannotOpposeAlignmentGoodness()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void CohortCannotOpposeAlignmentLawfulness()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void AttractCohortOfSameAlignment()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void AttractCohortOfDifferingAlignment()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void CohortsCannotReceiveFollowers()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void FollowersGenerated()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void FollowersGeneratedIndependently()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void GenerateFactorsThatAffectAttractingFollowers()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void FollowersCannotReceiveFollowers()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void FollowersCannotOpposeAlignmentGoodness()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void FollowersCannotOpposeAlignmentLawfulness()
        {
            throw new NotImplementedException();
        }
    }
}