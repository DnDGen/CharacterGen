using System;
using System.Collections.Generic;
using System.Linq;
using D20Dice;
using Moq;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Abilities.Skills;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Combats;
using NPCGen.Common.Items;
using NPCGen.Common.Races;
using NPCGen.Generators.Abilities.Feats;
using NPCGen.Generators.Interfaces.Abilities.Feats;
using NPCGen.Selectors.Interfaces;
using NPCGen.Selectors.Interfaces.Objects;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Abilities.Feats
{
    [TestFixture]
    public class AdditionalFeatsGeneratorTests
    {
        private IAdditionalFeatsGenerator additionalFeatsGenerator;
        private CharacterClass characterClass;
        private Race race;
        private Dictionary<String, Stat> stats;
        private Dictionary<String, Skill> skills;
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private Mock<IFeatsSelector> mockFeatsSelector;
        private List<AdditionalFeatSelection> additionalFeatSelections;
        private Mock<IDice> mockDice;
        private BaseAttack baseAttack;
        private List<Feat> preselectedFeats;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            mockFeatsSelector = new Mock<IFeatsSelector>();
            mockDice = new Mock<IDice>();
            additionalFeatsGenerator = new AdditionalFeatsGenerator(mockCollectionsSelector.Object, mockFeatsSelector.Object, mockDice.Object);
            characterClass = new CharacterClass();
            race = new Race();
            stats = new Dictionary<String, Stat>();
            skills = new Dictionary<String, Skill>();
            additionalFeatSelections = new List<AdditionalFeatSelection>();
            baseAttack = new BaseAttack();
            stats[StatConstants.Intelligence] = new Stat();
            preselectedFeats = new List<Feat>();

            mockFeatsSelector.Setup(s => s.SelectRacial(It.IsAny<String>())).Returns(Enumerable.Empty<RacialFeatSelection>());
            mockFeatsSelector.Setup(s => s.SelectAdditional()).Returns(additionalFeatSelections);
            mockFeatsSelector.Setup(s => s.SelectClass(It.IsAny<String>())).Returns(Enumerable.Empty<CharacterClassFeatSelection>());
            mockDice.Setup(d => d.Roll(1).d(It.IsAny<Int32>())).Returns(1);
        }

        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(3, 2)]
        [TestCase(4, 2)]
        [TestCase(5, 2)]
        [TestCase(6, 3)]
        [TestCase(7, 3)]
        [TestCase(8, 3)]
        [TestCase(9, 4)]
        [TestCase(10, 4)]
        [TestCase(11, 4)]
        [TestCase(12, 5)]
        [TestCase(13, 5)]
        [TestCase(14, 5)]
        [TestCase(15, 6)]
        [TestCase(16, 6)]
        [TestCase(17, 6)]
        [TestCase(18, 7)]
        [TestCase(19, 7)]
        [TestCase(20, 7)]
        public void GetAdditionalFeats(Int32 level, Int32 numberOfFeats)
        {
            characterClass.Level = level;
            AddFeatSelections(8);

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var featIds = feats.Select(f => f.Name.Id);

            for (var i = 0; i < numberOfFeats; i++)
                Assert.That(featIds, Contains.Item(additionalFeatSelections[i].FeatId));

            Assert.That(featIds.Count(), Is.EqualTo(numberOfFeats));
        }

        private void AddFeatSelections(Int32 quantity)
        {
            for (var i = 0; i < quantity; i++)
            {
                var selection = new AdditionalFeatSelection();
                selection.FeatId = String.Format("feat{0}", i + 1);

                additionalFeatSelections.Add(selection);
            }
        }

        [Test]
        public void DoNotGetAdditionalFeatIfNoneAvailable()
        {
            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            Assert.That(feats, Is.Empty);
        }

        [Test]
        public void DoNotGetMoreAdditionalFeatsIfNoneAvailable()
        {
            characterClass.Level = 1;
            AddFeatSelections(1);
            additionalFeatSelections[0].RequiredBaseAttack = 9266;

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            Assert.That(feats, Is.Empty);
        }

        [Test]
        public void FighterFeatsCanBeAdditionalFeats()
        {
            characterClass.Level = 1;
            AddFeatSelections(1);
            additionalFeatSelections[0].IsFighterFeat = true;

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var featIds = feats.Select(f => f.Name.Id);

            Assert.That(featIds, Contains.Item(additionalFeatSelections[0].FeatId));
            Assert.That(featIds.Count(), Is.EqualTo(1));
        }

        [Test]
        public void AdditionalFeatsPickedAtRandom()
        {
            characterClass.Level = 3;
            AddFeatSelections(3);

            mockDice.Setup(d => d.Roll(1).d(3)).Returns(3);
            mockDice.Setup(d => d.Roll(1).d(2)).Returns(1);

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var featIds = feats.Select(f => f.Name.Id);

            Assert.That(featIds, Contains.Item(additionalFeatSelections[0].FeatId));
            Assert.That(featIds, Contains.Item(additionalFeatSelections[2].FeatId));
            Assert.That(featIds.Count(), Is.EqualTo(2));
        }

        [Test]
        public void HumansGetAdditionalFeat()
        {
            race.BaseRace.Id = RaceConstants.BaseRaces.HumanId;
            characterClass.Level = 1;
            AddFeatSelections(3);

            mockDice.Setup(d => d.Roll(1).d(3)).Returns(3);
            mockDice.Setup(d => d.Roll(1).d(2)).Returns(1);

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var featIds = feats.Select(f => f.Name.Id);

            Assert.That(featIds, Contains.Item(additionalFeatSelections[0].FeatId));
            Assert.That(featIds, Contains.Item(additionalFeatSelections[2].FeatId));
            Assert.That(featIds.Count(), Is.EqualTo(2));
        }

        [Test]
        public void DoNotGetFeatsWithUnmetPrerequisite()
        {
            characterClass.Level = 1;
            AddFeatSelections(2);
            additionalFeatSelections[0].RequiredBaseAttack = 9266;
            mockDice.Setup(d => d.Roll(1).d(It.IsAny<Int32>())).Returns(1);

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var featIds = feats.Select(f => f.Name.Id);

            Assert.That(featIds, Contains.Item(additionalFeatSelections[1].FeatId));
            Assert.That(featIds.Count(), Is.EqualTo(1));
        }

        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 2)]
        [TestCase(4, 3)]
        [TestCase(5, 3)]
        [TestCase(6, 4)]
        [TestCase(7, 4)]
        [TestCase(8, 5)]
        [TestCase(9, 5)]
        [TestCase(10, 6)]
        [TestCase(11, 6)]
        [TestCase(12, 7)]
        [TestCase(13, 7)]
        [TestCase(14, 8)]
        [TestCase(15, 8)]
        [TestCase(16, 9)]
        [TestCase(17, 9)]
        [TestCase(18, 10)]
        [TestCase(19, 10)]
        [TestCase(20, 11)]
        public void FightersGetBonusFighterFeats(Int32 level, Int32 numberOfBonusFeats)
        {
            characterClass.ClassName = CharacterClassConstants.Fighter;
            characterClass.Level = level;
            AddFeatSelections(20);
            foreach (var selection in additionalFeatSelections)
                selection.IsFighterFeat = true;

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var featIds = feats.Select(f => f.Name.Id);
            var additionalFeats = level / 3 + 1;
            var totalFeats = numberOfBonusFeats + additionalFeats;

            for (var i = 0; i < totalFeats; i++)
                Assert.That(featIds, Contains.Item(additionalFeatSelections[i].FeatId));

            Assert.That(featIds.Count(), Is.EqualTo(totalFeats));
        }

        [Test]
        public void DoNotGetNonFighterFeats()
        {
            characterClass.ClassName = CharacterClassConstants.Fighter;
            characterClass.Level = 1;
            AddFeatSelections(3);
            additionalFeatSelections[2].IsFighterFeat = true;

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var featIds = feats.Select(f => f.Name.Id);

            Assert.That(featIds, Contains.Item(additionalFeatSelections[0].FeatId));
            Assert.That(featIds, Contains.Item(additionalFeatSelections[2].FeatId));
            Assert.That(featIds.Count(), Is.EqualTo(2));
        }

        [Test]
        public void DoNotGetFighterFeatsIfNoneAvailable()
        {
            characterClass.ClassName = CharacterClassConstants.Fighter;
            characterClass.Level = 1;
            AddFeatSelections(1);
            additionalFeatSelections[0].IsFighterFeat = true;

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var featIds = feats.Select(f => f.Name.Id);

            Assert.That(featIds, Contains.Item(additionalFeatSelections[0].FeatId));
            Assert.That(featIds.Count(), Is.EqualTo(1));
        }

        [Test]
        public void DoNotGetMoreFighterFeatsIfNoneAvailable()
        {
            characterClass.ClassName = CharacterClassConstants.Fighter;
            characterClass.Level = 2;
            AddFeatSelections(3);
            additionalFeatSelections[1].RequiredBaseAttack = 9266;
            additionalFeatSelections[1].IsFighterFeat = true;
            additionalFeatSelections[2].IsFighterFeat = true;

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var featIds = feats.Select(f => f.Name.Id);

            Assert.That(featIds, Contains.Item(additionalFeatSelections[0].FeatId));
            Assert.That(featIds, Contains.Item(additionalFeatSelections[2].FeatId));
            Assert.That(featIds.Count(), Is.EqualTo(2));
        }

        [Test]
        public void FighterFeatsPickedAtRandom()
        {
            characterClass.ClassName = CharacterClassConstants.Fighter;
            characterClass.Level = 2;
            AddFeatSelections(4);
            foreach (var feat in additionalFeatSelections)
                feat.IsFighterFeat = true;

            mockDice.Setup(d => d.Roll(1).d(4)).Returns(4);
            mockDice.Setup(d => d.Roll(1).d(3)).Returns(1);
            mockDice.Setup(d => d.Roll(1).d(2)).Returns(2);

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var featIds = feats.Select(f => f.Name.Id);

            Assert.That(featIds, Contains.Item(additionalFeatSelections[0].FeatId));
            Assert.That(featIds, Contains.Item(additionalFeatSelections[2].FeatId));
            Assert.That(featIds, Contains.Item(additionalFeatSelections[3].FeatId));
            Assert.That(featIds.Count(), Is.EqualTo(3));
        }

        [Test]
        public void DoNotGetFighterFeatWithUnmetPrerequisite()
        {
            characterClass.ClassName = CharacterClassConstants.Fighter;
            characterClass.Level = 1;
            AddFeatSelections(3);
            additionalFeatSelections[1].IsFighterFeat = true;
            additionalFeatSelections[1].RequiredFeatIds = new[] { "other feat" };
            additionalFeatSelections[2].IsFighterFeat = true;

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var featIds = feats.Select(f => f.Name.Id);

            Assert.That(featIds, Contains.Item(additionalFeatSelections[0].FeatId));
            Assert.That(featIds, Contains.Item(additionalFeatSelections[2].FeatId));
            Assert.That(featIds.Count(), Is.EqualTo(2));
        }

        [Test]
        public void IfNoFighterFeatsAvailable_ThenStop()
        {
            characterClass.ClassName = CharacterClassConstants.Fighter;
            characterClass.Level = 1;
            AddFeatSelections(3);
            additionalFeatSelections[1].IsFighterFeat = true;
            additionalFeatSelections[1].RequiredFeatIds = new[] { "other feat" };

            mockDice.Setup(d => d.Roll(1).d(2)).Returns(2);

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var featIds = feats.Select(f => f.Name.Id);

            Assert.That(featIds, Contains.Item(additionalFeatSelections[2].FeatId));
            Assert.That(featIds.Count(), Is.EqualTo(1));
        }

        [TestCase(1, 0)]
        [TestCase(2, 0)]
        [TestCase(3, 0)]
        [TestCase(4, 0)]
        [TestCase(5, 1)]
        [TestCase(6, 1)]
        [TestCase(7, 1)]
        [TestCase(8, 1)]
        [TestCase(9, 1)]
        [TestCase(10, 2)]
        [TestCase(11, 2)]
        [TestCase(12, 2)]
        [TestCase(13, 2)]
        [TestCase(14, 2)]
        [TestCase(15, 3)]
        [TestCase(16, 3)]
        [TestCase(17, 3)]
        [TestCase(18, 3)]
        [TestCase(19, 3)]
        [TestCase(20, 4)]
        public void WizardsGetBonusWizardFeats(Int32 level, Int32 numberOfBonusFeats)
        {
            characterClass.ClassName = CharacterClassConstants.Wizard;
            characterClass.Level = level;
            AddFeatSelections(20);
            foreach (var selection in additionalFeatSelections)
                selection.IsWizardFeat = true;

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var featIds = feats.Select(f => f.Name.Id);
            var additionalFeats = level / 3 + 1;
            var totalFeats = numberOfBonusFeats + additionalFeats;

            for (var i = 0; i < totalFeats; i++)
                Assert.That(featIds, Contains.Item(additionalFeatSelections[i].FeatId));

            Assert.That(featIds.Count(), Is.EqualTo(totalFeats));
        }

        [Test]
        public void DoNotGetNonWizardFeats()
        {
            characterClass.ClassName = CharacterClassConstants.Wizard;
            characterClass.Level = 5;
            AddFeatSelections(5);
            additionalFeatSelections[3].IsWizardFeat = true;

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var featIds = feats.Select(f => f.Name.Id);

            Assert.That(featIds, Contains.Item(additionalFeatSelections[0].FeatId));
            Assert.That(featIds, Contains.Item(additionalFeatSelections[1].FeatId));
            Assert.That(featIds, Contains.Item(additionalFeatSelections[3].FeatId));
            Assert.That(featIds.Count(), Is.EqualTo(3));
        }

        [Test]
        public void DoNotGetWizardFeatsIfNoneAvailable()
        {
            characterClass.ClassName = CharacterClassConstants.Wizard;
            characterClass.Level = 5;
            AddFeatSelections(4);
            additionalFeatSelections[0].IsWizardFeat = true;

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var featIds = feats.Select(f => f.Name.Id);

            Assert.That(featIds, Contains.Item(additionalFeatSelections[0].FeatId));
            Assert.That(featIds, Contains.Item(additionalFeatSelections[1].FeatId));
            Assert.That(featIds.Count(), Is.EqualTo(2));
        }

        [Test]
        public void DoNotGetMoreWizardFeatsIfNoneAvailable()
        {
            characterClass.ClassName = CharacterClassConstants.Wizard;
            characterClass.Level = 10;
            AddFeatSelections(6);
            additionalFeatSelections[1].IsWizardFeat = true;
            additionalFeatSelections[5].IsWizardFeat = true;

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var featIds = feats.Select(f => f.Name.Id);

            Assert.That(featIds, Contains.Item(additionalFeatSelections[0].FeatId));
            Assert.That(featIds, Contains.Item(additionalFeatSelections[1].FeatId));
            Assert.That(featIds, Contains.Item(additionalFeatSelections[2].FeatId));
            Assert.That(featIds, Contains.Item(additionalFeatSelections[3].FeatId));
            Assert.That(featIds, Contains.Item(additionalFeatSelections[5].FeatId));
            Assert.That(featIds.Count(), Is.EqualTo(5));
        }

        [Test]
        public void WizardFeatsPickedAtRandom()
        {
            characterClass.ClassName = CharacterClassConstants.Wizard;
            characterClass.Level = 10;
            AddFeatSelections(7);
            foreach (var feat in additionalFeatSelections)
                feat.IsWizardFeat = true;

            mockDice.Setup(d => d.Roll(1).d(7)).Returns(7);
            mockDice.Setup(d => d.Roll(1).d(6)).Returns(6);
            mockDice.Setup(d => d.Roll(1).d(5)).Returns(5);
            mockDice.Setup(d => d.Roll(1).d(4)).Returns(4);
            mockDice.Setup(d => d.Roll(1).d(3)).Returns(1);
            mockDice.Setup(d => d.Roll(1).d(2)).Returns(2);

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var featIds = feats.Select(f => f.Name.Id);

            Assert.That(featIds, Contains.Item(additionalFeatSelections[0].FeatId));
            Assert.That(featIds, Contains.Item(additionalFeatSelections[2].FeatId));
            Assert.That(featIds, Contains.Item(additionalFeatSelections[3].FeatId));
            Assert.That(featIds, Contains.Item(additionalFeatSelections[4].FeatId));
            Assert.That(featIds, Contains.Item(additionalFeatSelections[5].FeatId));
            Assert.That(featIds, Contains.Item(additionalFeatSelections[6].FeatId));
            Assert.That(featIds.Count(), Is.EqualTo(6));
        }

        [Test]
        public void ReassessPrerequisitesEveryFeat()
        {
            characterClass.Level = 3;
            AddFeatSelections(3);
            additionalFeatSelections[1].RequiredFeatIds = new[] { additionalFeatSelections[0].FeatId };

            mockDice.Setup(d => d.Roll(1).d(3)).Returns(1);
            mockDice.Setup(d => d.Roll(1).d(2)).Returns(1);

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var featIds = feats.Select(f => f.Name.Id);

            Assert.That(featIds, Contains.Item(additionalFeatSelections[0].FeatId));
            Assert.That(featIds, Contains.Item(additionalFeatSelections[1].FeatId));
            Assert.That(featIds.Count(), Is.EqualTo(2));
        }

        [Test]
        public void CannotPickAPreselectedFeat()
        {
            var feat = new Feat();
            feat.Name.Id = "feat1";
            preselectedFeats.Add(feat);

            AddFeatSelections(2);

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var featIds = feats.Select(f => f.Name.Id);
            Assert.That(featIds.Single(), Is.EqualTo("feat2"));
        }

        [Test]
        public void FeatsWithoutFociDoNotFill()
        {
            AddFeatSelections(1);

            var schools = new[] { "school 1" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, "specific application")).Returns(schools);

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var onlyFeat = feats.Single();

            mockCollectionsSelector.Verify(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, It.IsAny<String>()), Times.Never);
            Assert.That(onlyFeat.Name.Id, Is.EqualTo(additionalFeatSelections[0].FeatId));
            Assert.That(onlyFeat.Focus, Is.Empty);
        }

        [Test]
        public void FeatsWithFociAreFilled()
        {
            AddFeatSelections(1);
            additionalFeatSelections[0].FocusType = "specific application";

            var schools = new[] { "school 1" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, "specific application")).Returns(schools);

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var onlyFeat = feats.Single();

            Assert.That(onlyFeat.Name.Id, Is.EqualTo(additionalFeatSelections[0].FeatId));
            Assert.That(onlyFeat.Focus, Is.EqualTo("school 1"));
        }

        [Test]
        public void FeatsWithFociAreFilledRandomly()
        {
            AddFeatSelections(1);
            additionalFeatSelections[0].FocusType = "specific application";

            var schools = new[] { "school 1", "school 2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, "specific application")).Returns(schools);

            mockDice.Setup(d => d.Roll(1).d(2)).Returns(2);

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var onlyFeat = feats.Single();

            Assert.That(onlyFeat.Name.Id, Is.EqualTo(additionalFeatSelections[0].FeatId));
            Assert.That(onlyFeat.Focus, Is.EqualTo("school 2"));
        }

        [Test]
        public void FeatsWithFociCanBeFilledMoreThanOnce()
        {
            characterClass.Level = 3;
            AddFeatSelections(1);
            additionalFeatSelections[0].FocusType = "specific application";

            var schools = new[] { "school 1", "school 2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, "specific application")).Returns(schools);

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var firstFeat = feats.First();
            var lastFeat = feats.Last();

            Assert.That(firstFeat.Name.Id, Is.EqualTo(additionalFeatSelections[0].FeatId));
            Assert.That(firstFeat.Focus, Is.EqualTo("school 1"));
            Assert.That(lastFeat.Name.Id, Is.EqualTo(additionalFeatSelections[0].FeatId));
            Assert.That(lastFeat.Focus, Is.EqualTo("school 2"));
            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void SpellMasteryStrengthIsIntelligenceBonus()
        {
            characterClass.Level = 1;
            AddFeatSelections(1);
            additionalFeatSelections[0].FeatId = FeatConstants.SpellMasteryId;
            stats[StatConstants.Intelligence].Value = 14;

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var firstFeat = feats.First();

            Assert.That(firstFeat.Name.Id, Is.EqualTo(FeatConstants.SpellMasteryId));
            Assert.That(firstFeat.Strength, Is.EqualTo(stats[StatConstants.Intelligence].Bonus));
            Assert.That(feats.Count(), Is.EqualTo(1));
        }

        [Test]
        public void SkillMasteryStrengthIsIntelligenceBonusPlus3()
        {
            characterClass.Level = 1;
            AddFeatSelections(1);
            additionalFeatSelections[0].FeatId = FeatConstants.SkillMasteryId;
            stats[StatConstants.Intelligence].Value = 14;

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var firstFeat = feats.First();

            Assert.That(firstFeat.Name.Id, Is.EqualTo(FeatConstants.SkillMasteryId));
            Assert.That(firstFeat.Strength, Is.EqualTo(stats[StatConstants.Intelligence].Bonus + 3));
            Assert.That(feats.Count(), Is.EqualTo(1));
        }

        [Test]
        public void FeatsThatCanBeTakenMultipleTimesWithoutFociAreAllowed()
        {
            AddFeatSelections(1);
            characterClass.Level = 3;
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, TableNameConstants.Set.Collection.Groups.TakenMultipleTimes)).Returns(new[] { "feat1" });

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Name.Id, Is.EqualTo("feat1"));
            Assert.That(last.Name.Id, Is.EqualTo("feat1"));
            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void SpellcastersCanSelectRayForWeaponFoci()
        {
            AddFeatSelections(1);
            additionalFeatSelections[0].FocusType = TableNameConstants.Set.Collection.Groups.WeaponsWithUnarmedAndGrappleAndRay;

            var weapons = new[] { WeaponProficiencyConstants.Ray, "weapon" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, TableNameConstants.Set.Collection.Groups.WeaponsWithUnarmedAndGrappleAndRay))
                .Returns(weapons);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, TableNameConstants.Set.Collection.Groups.Spellcasters))
                .Returns(new[] { characterClass.ClassName });

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var onlyFeat = feats.Single();

            Assert.That(onlyFeat.Name.Id, Is.EqualTo(additionalFeatSelections[0].FeatId));
            Assert.That(onlyFeat.Focus, Is.EqualTo(WeaponProficiencyConstants.Ray));
        }

        [Test]
        public void NonSpellcastersCannotSelectRayForWeaponFoci()
        {
            AddFeatSelections(1);
            additionalFeatSelections[0].FocusType = TableNameConstants.Set.Collection.Groups.WeaponsWithUnarmedAndGrappleAndRay;

            var weapons = new[] { WeaponProficiencyConstants.Ray, "weapon" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, TableNameConstants.Set.Collection.Groups.WeaponsWithUnarmedAndGrappleAndRay))
                .Returns(weapons);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, TableNameConstants.Set.Collection.Groups.Spellcasters))
                .Returns(new[] { "other class name" });

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var onlyFeat = feats.Single();

            Assert.That(onlyFeat.Name.Id, Is.EqualTo(additionalFeatSelections[0].FeatId));
            Assert.That(onlyFeat.Focus, Is.EqualTo("weapon"));
        }

        [Test]
        public void FeatsWithoutFociButWithRequirementsThatHaveFociDoNotUseSameFocus()
        {
            characterClass.Level = 3;
            AddFeatSelections(2);
            additionalFeatSelections[0].FocusType = "specific application";
            additionalFeatSelections[1].RequiredFeatIds = new[] { additionalFeatSelections[0].FeatId };

            var schools = new[] { "school 1" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, "specific application")).Returns(schools);

            mockDice.SetupSequence(d => d.Roll(1).d(2)).Returns(2);

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var firstFeat = feats.First();
            var lastFeat = feats.Last();

            Assert.That(firstFeat.Name.Id, Is.EqualTo(additionalFeatSelections[0].FeatId));
            Assert.That(firstFeat.Focus, Is.EqualTo("school 1"));
            Assert.That(lastFeat.Name.Id, Is.EqualTo(additionalFeatSelections[1].FeatId));
            Assert.That(lastFeat.Focus, Is.Empty);
        }

        [Test]
        public void FeatsWithFociAndRequirementsThatHaveFociUseFocus()
        {
            characterClass.Level = 3;
            AddFeatSelections(2);
            additionalFeatSelections[0].FocusType = "specific application";
            additionalFeatSelections[1].RequiredFeatIds = new[] { additionalFeatSelections[0].FeatId };
            additionalFeatSelections[1].FocusType = "specific application";

            var schools = new[] { "school 1", "school 2", "school 3" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, "specific application")).Returns(schools);

            mockDice.SetupSequence(d => d.Roll(1).d(2)).Returns(2);
            mockDice.SetupSequence(d => d.Roll(1).d(3)).Returns(3).Returns(1);

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var firstFeat = feats.First();
            var lastFeat = feats.Last();

            Assert.That(firstFeat.Name.Id, Is.EqualTo(additionalFeatSelections[0].FeatId));
            Assert.That(firstFeat.Focus, Is.EqualTo("school 3"));
            Assert.That(lastFeat.Name.Id, Is.EqualTo(additionalFeatSelections[1].FeatId));
            Assert.That(lastFeat.Focus, Is.EqualTo("school 3"));
        }

        [Test]
        public void IfFeatRequirementHasMultipleFoci_PickRandomlyAmongThem()
        {
            characterClass.Level = 6;
            AddFeatSelections(2);
            additionalFeatSelections[0].FocusType = "specific application";
            additionalFeatSelections[1].RequiredFeatIds = new[] { additionalFeatSelections[0].FeatId };
            additionalFeatSelections[1].FocusType = "specific application";

            var schools = new[] { "school 1", "school 2", "school 3" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, "specific application")).Returns(schools);

            mockDice.SetupSequence(d => d.Roll(1).d(2)).Returns(1).Returns(1).Returns(2).Returns(2);
            mockDice.SetupSequence(d => d.Roll(1).d(3)).Returns(3).Returns(1);

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var firstFeat = feats.First();
            var secondFeat = feats.ElementAt(1);
            var lastFeat = feats.Last();

            Assert.That(firstFeat.Name.Id, Is.EqualTo(additionalFeatSelections[0].FeatId));
            Assert.That(firstFeat.Focus, Is.EqualTo("school 3"));
            Assert.That(secondFeat.Name.Id, Is.EqualTo(additionalFeatSelections[0].FeatId));
            Assert.That(secondFeat.Focus, Is.EqualTo("school 1"));
            Assert.That(lastFeat.Name.Id, Is.EqualTo(additionalFeatSelections[1].FeatId));
            Assert.That(lastFeat.Focus, Is.EqualTo("school 1"));
        }

        [Test]
        public void IfFocusTypeIsSchoolOfMagic_CannotPickProhibitedFieldAsSpecificApplication()
        {
            AddFeatSelections(1);
            additionalFeatSelections[0].FocusType = TableNameConstants.Set.Collection.Groups.SchoolsOfMagic;

            var schools = new[] { "school 1", "school 2", "school 3", "school 4" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, TableNameConstants.Set.Collection.Groups.SchoolsOfMagic)).Returns(schools);

            mockDice.Setup(d => d.Roll(1).d(2)).Returns(2);

            characterClass.ProhibitedFields = new[] { "school 1", "school 3" };

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var onlyFeat = feats.Single();

            Assert.That(onlyFeat.Name.Id, Is.EqualTo(additionalFeatSelections[0].FeatId));
            Assert.That(onlyFeat.Focus, Is.EqualTo("school 4"));
        }

        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(3, 2)]
        [TestCase(4, 2)]
        [TestCase(5, 2)]
        [TestCase(6, 3)]
        [TestCase(7, 3)]
        [TestCase(8, 3)]
        [TestCase(9, 4)]
        [TestCase(10, 5)]
        [TestCase(11, 5)]
        [TestCase(12, 6)]
        [TestCase(13, 7)]
        [TestCase(14, 7)]
        [TestCase(15, 8)]
        [TestCase(16, 9)]
        [TestCase(17, 9)]
        [TestCase(18, 10)]
        [TestCase(19, 11)]
        [TestCase(20, 11)]
        public void RoguesGetBonusFeats(Int32 level, Int32 quantity)
        {
            characterClass.Level = level;
            characterClass.ClassName = CharacterClassConstants.Rogue;
            AddFeatSelections(12);

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var featIds = feats.Select(f => f.Name.Id);

            for (var i = 0; i < quantity; i++)
                Assert.That(featIds, Contains.Item(additionalFeatSelections[i].FeatId));

            Assert.That(featIds.Count(), Is.EqualTo(quantity));
        }
    }
}