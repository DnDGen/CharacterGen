﻿using System;
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
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private Mock<IFeatsSelector> mockFeatsSelector;
        private Mock<IDice> mockDice;
        private Mock<IFeatFocusGenerator> mockFeatFocusGenerator;
        private IAdditionalFeatsGenerator additionalFeatsGenerator;
        private CharacterClass characterClass;
        private Race race;
        private Dictionary<String, Stat> stats;
        private Dictionary<String, Skill> skills;
        private List<AdditionalFeatSelection> additionalFeatSelections;
        private BaseAttack baseAttack;
        private List<Feat> preselectedFeats;
        private List<String> fighterBonusFeats;
        private List<String> wizardBonusFeats;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            mockFeatsSelector = new Mock<IFeatsSelector>();
            mockDice = new Mock<IDice>();
            mockFeatFocusGenerator = new Mock<IFeatFocusGenerator>();
            additionalFeatsGenerator = new AdditionalFeatsGenerator(mockCollectionsSelector.Object, mockFeatsSelector.Object, mockDice.Object, mockFeatFocusGenerator.Object);
            characterClass = new CharacterClass();
            race = new Race();
            stats = new Dictionary<String, Stat>();
            skills = new Dictionary<String, Skill>();
            additionalFeatSelections = new List<AdditionalFeatSelection>();
            baseAttack = new BaseAttack();
            stats[StatConstants.Intelligence] = new Stat();
            preselectedFeats = new List<Feat>();
            fighterBonusFeats = new List<String>();
            wizardBonusFeats = new List<String>();

            mockFeatsSelector.Setup(s => s.SelectAdditional()).Returns(additionalFeatSelections);
            mockDice.Setup(d => d.Roll(1).d(It.IsAny<Int32>())).Returns(1);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.FighterBonusFeats)).Returns(fighterBonusFeats);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.WizardBonusFeats)).Returns(wizardBonusFeats);
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
            var featIds = feats.Select(f => f.Name);

            for (var i = 0; i < numberOfFeats; i++)
                Assert.That(featIds, Contains.Item(additionalFeatSelections[i].Feat));

            Assert.That(featIds.Count(), Is.EqualTo(numberOfFeats));
        }

        private void AddFeatSelections(Int32 quantity)
        {
            for (var i = 0; i < quantity; i++)
            {
                var selection = new AdditionalFeatSelection();
                selection.Feat = String.Format("feat{0}", i + 1);

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
            fighterBonusFeats.Add(additionalFeatSelections[0].Feat);

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var featIds = feats.Select(f => f.Name);

            Assert.That(featIds, Contains.Item(additionalFeatSelections[0].Feat));
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
            var featIds = feats.Select(f => f.Name);

            Assert.That(featIds, Contains.Item(additionalFeatSelections[0].Feat));
            Assert.That(featIds, Contains.Item(additionalFeatSelections[2].Feat));
            Assert.That(featIds.Count(), Is.EqualTo(2));
        }

        [Test]
        public void HumansGetAdditionalFeat()
        {
            race.BaseRace = RaceConstants.BaseRaces.Human;
            characterClass.Level = 1;
            AddFeatSelections(3);

            mockDice.Setup(d => d.Roll(1).d(3)).Returns(3);
            mockDice.Setup(d => d.Roll(1).d(2)).Returns(1);

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var featIds = feats.Select(f => f.Name);

            Assert.That(featIds, Contains.Item(additionalFeatSelections[0].Feat));
            Assert.That(featIds, Contains.Item(additionalFeatSelections[2].Feat));
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
            var featIds = feats.Select(f => f.Name);

            Assert.That(featIds, Contains.Item(additionalFeatSelections[1].Feat));
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
                fighterBonusFeats.Add(selection.Feat);

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var featIds = feats.Select(f => f.Name);
            var additionalFeats = level / 3 + 1;
            var totalFeats = numberOfBonusFeats + additionalFeats;

            for (var i = 0; i < totalFeats; i++)
                Assert.That(featIds, Contains.Item(additionalFeatSelections[i].Feat));

            Assert.That(featIds.Count(), Is.EqualTo(totalFeats));
        }

        [Test]
        public void DoNotGetNonFighterFeats()
        {
            characterClass.ClassName = CharacterClassConstants.Fighter;
            characterClass.Level = 1;
            AddFeatSelections(3);
            fighterBonusFeats.Add(additionalFeatSelections[2].Feat);

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var featIds = feats.Select(f => f.Name);

            Assert.That(featIds, Contains.Item(additionalFeatSelections[0].Feat));
            Assert.That(featIds, Contains.Item(additionalFeatSelections[2].Feat));
            Assert.That(featIds.Count(), Is.EqualTo(2));
        }

        [Test]
        public void DoNotGetFighterFeatsIfNoneAvailable()
        {
            characterClass.ClassName = CharacterClassConstants.Fighter;
            characterClass.Level = 1;
            AddFeatSelections(1);
            fighterBonusFeats.Add(additionalFeatSelections[0].Feat);

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var featIds = feats.Select(f => f.Name);

            Assert.That(featIds, Contains.Item(additionalFeatSelections[0].Feat));
            Assert.That(featIds.Count(), Is.EqualTo(1));
        }

        [Test]
        public void DoNotGetMoreFighterFeatsIfNoneAvailable()
        {
            characterClass.ClassName = CharacterClassConstants.Fighter;
            characterClass.Level = 2;
            AddFeatSelections(3);
            additionalFeatSelections[1].RequiredBaseAttack = 9266;
            fighterBonusFeats.Add(additionalFeatSelections[1].Feat);
            fighterBonusFeats.Add(additionalFeatSelections[2].Feat);

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var featIds = feats.Select(f => f.Name);

            Assert.That(featIds, Contains.Item(additionalFeatSelections[0].Feat));
            Assert.That(featIds, Contains.Item(additionalFeatSelections[2].Feat));
            Assert.That(featIds.Count(), Is.EqualTo(2));
        }

        [Test]
        public void FighterFeatsPickedAtRandom()
        {
            characterClass.ClassName = CharacterClassConstants.Fighter;
            characterClass.Level = 2;
            AddFeatSelections(4);

            foreach (var selection in additionalFeatSelections)
                fighterBonusFeats.Add(selection.Feat);

            mockDice.Setup(d => d.Roll(1).d(4)).Returns(4);
            mockDice.Setup(d => d.Roll(1).d(3)).Returns(1);
            mockDice.Setup(d => d.Roll(1).d(2)).Returns(2);

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var featIds = feats.Select(f => f.Name);

            Assert.That(featIds, Contains.Item(additionalFeatSelections[0].Feat));
            Assert.That(featIds, Contains.Item(additionalFeatSelections[2].Feat));
            Assert.That(featIds, Contains.Item(additionalFeatSelections[3].Feat));
            Assert.That(featIds.Count(), Is.EqualTo(3));
        }

        [Test]
        public void DoNotGetFighterFeatWithUnmetPrerequisite()
        {
            characterClass.ClassName = CharacterClassConstants.Fighter;
            characterClass.Level = 1;
            AddFeatSelections(3);
            additionalFeatSelections[1].RequiredFeats = new[] { new RequiredFeat { Feat = "other feat" } };
            fighterBonusFeats.Add(additionalFeatSelections[1].Feat);
            fighterBonusFeats.Add(additionalFeatSelections[2].Feat);

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var featIds = feats.Select(f => f.Name);

            Assert.That(featIds, Contains.Item(additionalFeatSelections[0].Feat));
            Assert.That(featIds, Contains.Item(additionalFeatSelections[2].Feat));
            Assert.That(featIds.Count(), Is.EqualTo(2));
        }

        [Test]
        public void IfNoFighterFeatsAvailable_ThenStop()
        {
            characterClass.ClassName = CharacterClassConstants.Fighter;
            characterClass.Level = 1;
            AddFeatSelections(3);
            fighterBonusFeats.Add(additionalFeatSelections[1].Feat);
            additionalFeatSelections[1].RequiredFeats = new[] { new RequiredFeat { Feat = "other feat" } };

            mockDice.Setup(d => d.Roll(1).d(2)).Returns(2);

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var featIds = feats.Select(f => f.Name);

            Assert.That(featIds, Contains.Item(additionalFeatSelections[2].Feat));
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
                wizardBonusFeats.Add(selection.Feat);

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var featIds = feats.Select(f => f.Name);
            var additionalFeats = level / 3 + 1;
            var totalFeats = numberOfBonusFeats + additionalFeats;

            for (var i = 0; i < totalFeats; i++)
                Assert.That(featIds, Contains.Item(additionalFeatSelections[i].Feat));

            Assert.That(featIds.Count(), Is.EqualTo(totalFeats));
        }

        [Test]
        public void DoNotGetNonWizardFeats()
        {
            characterClass.ClassName = CharacterClassConstants.Wizard;
            characterClass.Level = 5;
            AddFeatSelections(5);
            wizardBonusFeats.Add(additionalFeatSelections[3].Feat);

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var featIds = feats.Select(f => f.Name);

            Assert.That(featIds, Contains.Item(additionalFeatSelections[0].Feat));
            Assert.That(featIds, Contains.Item(additionalFeatSelections[1].Feat));
            Assert.That(featIds, Contains.Item(additionalFeatSelections[3].Feat));
            Assert.That(featIds.Count(), Is.EqualTo(3));
        }

        [Test]
        public void DoNotGetWizardFeatsIfNoneAvailable()
        {
            characterClass.ClassName = CharacterClassConstants.Wizard;
            characterClass.Level = 5;
            AddFeatSelections(4);
            wizardBonusFeats.Add(additionalFeatSelections[0].Feat);

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var featIds = feats.Select(f => f.Name);

            Assert.That(featIds, Contains.Item(additionalFeatSelections[0].Feat));
            Assert.That(featIds, Contains.Item(additionalFeatSelections[1].Feat));
            Assert.That(featIds.Count(), Is.EqualTo(2));
        }

        [Test]
        public void DoNotGetMoreWizardFeatsIfNoneAvailable()
        {
            characterClass.ClassName = CharacterClassConstants.Wizard;
            characterClass.Level = 10;
            AddFeatSelections(6);
            wizardBonusFeats.Add(additionalFeatSelections[1].Feat);
            wizardBonusFeats.Add(additionalFeatSelections[5].Feat);

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var featIds = feats.Select(f => f.Name);

            Assert.That(featIds, Contains.Item(additionalFeatSelections[0].Feat));
            Assert.That(featIds, Contains.Item(additionalFeatSelections[1].Feat));
            Assert.That(featIds, Contains.Item(additionalFeatSelections[2].Feat));
            Assert.That(featIds, Contains.Item(additionalFeatSelections[3].Feat));
            Assert.That(featIds, Contains.Item(additionalFeatSelections[5].Feat));
            Assert.That(featIds.Count(), Is.EqualTo(5));
        }

        [Test]
        public void WizardFeatsPickedAtRandom()
        {
            characterClass.ClassName = CharacterClassConstants.Wizard;
            characterClass.Level = 10;
            AddFeatSelections(7);

            foreach (var selection in additionalFeatSelections)
                wizardBonusFeats.Add(selection.Feat);

            mockDice.Setup(d => d.Roll(1).d(7)).Returns(7);
            mockDice.Setup(d => d.Roll(1).d(6)).Returns(6);
            mockDice.Setup(d => d.Roll(1).d(5)).Returns(5);
            mockDice.Setup(d => d.Roll(1).d(4)).Returns(4);
            mockDice.Setup(d => d.Roll(1).d(3)).Returns(1);
            mockDice.Setup(d => d.Roll(1).d(2)).Returns(2);

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var featIds = feats.Select(f => f.Name);

            Assert.That(featIds, Contains.Item(additionalFeatSelections[0].Feat));
            Assert.That(featIds, Contains.Item(additionalFeatSelections[2].Feat));
            Assert.That(featIds, Contains.Item(additionalFeatSelections[3].Feat));
            Assert.That(featIds, Contains.Item(additionalFeatSelections[4].Feat));
            Assert.That(featIds, Contains.Item(additionalFeatSelections[5].Feat));
            Assert.That(featIds, Contains.Item(additionalFeatSelections[6].Feat));
            Assert.That(featIds.Count(), Is.EqualTo(6));
        }

        [Test]
        public void ReassessPrerequisitesEveryFeat()
        {
            characterClass.Level = 3;
            AddFeatSelections(3);
            additionalFeatSelections[1].RequiredFeats = new[] { new RequiredFeat { Feat = additionalFeatSelections[0].Feat } };

            mockDice.Setup(d => d.Roll(1).d(3)).Returns(1);
            mockDice.Setup(d => d.Roll(1).d(2)).Returns(1);

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var featIds = feats.Select(f => f.Name);

            Assert.That(featIds, Contains.Item(additionalFeatSelections[0].Feat));
            Assert.That(featIds, Contains.Item(additionalFeatSelections[1].Feat));
            Assert.That(featIds.Count(), Is.EqualTo(2));
        }

        [Test]
        public void CannotPickAPreselectedFeat()
        {
            var feat = new Feat();
            feat.Name = "feat1";
            preselectedFeats.Add(feat);

            AddFeatSelections(2);

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var featIds = feats.Select(f => f.Name);
            Assert.That(featIds.Single(), Is.EqualTo("feat2"));
        }

        [Test]
        public void FeatFociAreFilled()
        {
            AddFeatSelections(1);
            additionalFeatSelections[0].FocusType = "focus type";
            mockFeatFocusGenerator.Setup(g => g.GenerateFrom("feat1", "focus type", skills, additionalFeatSelections[0].RequiredFeats, It.IsAny<IEnumerable<Feat>>(), characterClass))
                .Returns("focus");

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var onlyFeat = feats.Single();

            Assert.That(onlyFeat.Name, Is.EqualTo(additionalFeatSelections[0].Feat));
            Assert.That(onlyFeat.Focus, Is.EqualTo("focus"));
        }

        [Test]
        public void FeatFociAreFilledIndividually()
        {
            characterClass.Level = 3;
            AddFeatSelections(2);
            additionalFeatSelections[0].FocusType = "focus type";

            mockFeatFocusGenerator.Setup(g => g.GenerateFrom("feat1", "focus type", skills, additionalFeatSelections[0].RequiredFeats, It.IsAny<IEnumerable<Feat>>(), characterClass))
                .Returns("focus");
            mockFeatFocusGenerator.Setup(g => g.GenerateFrom("feat2", String.Empty, skills, additionalFeatSelections[1].RequiredFeats, It.IsAny<IEnumerable<Feat>>(), characterClass))
                .Returns(String.Empty);

            mockDice.SetupSequence(d => d.Roll(1).d(2)).Returns(1).Returns(2);

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            Assert.That(feats.Count(), Is.EqualTo(2));

            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Name, Is.EqualTo(additionalFeatSelections[0].Feat));
            Assert.That(first.Focus, Is.EqualTo("focus"));
            Assert.That(last.Name, Is.EqualTo(additionalFeatSelections[1].Feat));
            Assert.That(last.Focus, Is.Empty);
        }

        [Test]
        public void FeatsWithFociCanBeFilledMoreThanOnce()
        {
            characterClass.Level = 3;
            AddFeatSelections(1);
            additionalFeatSelections[0].FocusType = "focus type";

            mockFeatFocusGenerator.SetupSequence(g => g.GenerateFrom("feat1", "focus type", skills, additionalFeatSelections[0].RequiredFeats, It.IsAny<IEnumerable<Feat>>(), characterClass))
                .Returns("focus 1").Returns("focus 2");

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var firstFeat = feats.First();
            var lastFeat = feats.Last();

            Assert.That(firstFeat.Name, Is.EqualTo(additionalFeatSelections[0].Feat));
            Assert.That(firstFeat.Focus, Is.EqualTo("focus 1"));
            Assert.That(lastFeat.Name, Is.EqualTo(additionalFeatSelections[0].Feat));
            Assert.That(lastFeat.Focus, Is.EqualTo("focus 2"));
            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void SpellMasteryStrengthIsIntelligenceBonus()
        {
            characterClass.Level = 1;
            AddFeatSelections(1);
            additionalFeatSelections[0].Feat = FeatConstants.SpellMastery;
            stats[StatConstants.Intelligence].Value = 14;

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var firstFeat = feats.First();

            Assert.That(firstFeat.Name, Is.EqualTo(FeatConstants.SpellMastery));
            Assert.That(firstFeat.Strength, Is.EqualTo(stats[StatConstants.Intelligence].Bonus));
            Assert.That(feats.Count(), Is.EqualTo(1));
        }

        [Test]
        public void FeatsThatCanBeTakenMultipleTimesWithoutFociAreAllowed()
        {
            AddFeatSelections(1);
            characterClass.Level = 3;
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.TakenMultipleTimes)).Returns(new[] { "feat1" });

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Name, Is.EqualTo("feat1"));
            Assert.That(last.Name, Is.EqualTo("feat1"));
            Assert.That(feats.Count(), Is.EqualTo(2));
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
            var featIds = feats.Select(f => f.Name);

            for (var i = 0; i < quantity; i++)
                Assert.That(featIds, Contains.Item(additionalFeatSelections[i].Feat));

            Assert.That(featIds.Count(), Is.EqualTo(quantity));
        }

        [Test]
        public void IfAllFocusGenerated_CannotSelectFeat()
        {
            AddFeatSelections(1);
            additionalFeatSelections[0].FocusType = "focus type";

            mockFeatFocusGenerator.Setup(g => g.GenerateFrom(additionalFeatSelections[0].Feat, "focus type", skills, additionalFeatSelections[0].RequiredFeats, preselectedFeats, characterClass)).Returns(ProficiencyConstants.All);

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            Assert.That(feats, Is.Empty);
        }

        [Test]
        public void IfAllFocusGenerated_DoNotTryToSelectFeatAgain()
        {
            AddFeatSelections(2);
            additionalFeatSelections[0].FocusType = "focus type";

            mockFeatFocusGenerator.Setup(g => g.GenerateFrom(additionalFeatSelections[0].Feat, "focus type", skills, additionalFeatSelections[0].RequiredFeats, preselectedFeats, characterClass)).Returns(ProficiencyConstants.All);

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var onlyFeat = feats.Single();
            Assert.That(onlyFeat.Name, Is.EqualTo(additionalFeatSelections[1].Feat));
        }

        [Test]
        public void CanHaveFeatWithoutFocus()
        {
            AddFeatSelections(1);
            mockFeatFocusGenerator.Setup(g => g.GenerateFrom(additionalFeatSelections[0].Feat, String.Empty, skills, additionalFeatSelections[0].RequiredFeats, preselectedFeats, characterClass))
                .Returns(String.Empty);

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var feat = feats.Single();

            Assert.That(feat.Name, Is.EqualTo(additionalFeatSelections[0].Feat));
            Assert.That(feat.Focus, Is.Empty);
        }

        [Test]
        public void SkillMasteryIsBrokenIntoSeparateFeats()
        {
            AddFeatSelections(1);
            additionalFeatSelections[0].Feat = FeatConstants.SkillMastery;
            additionalFeatSelections[0].Strength = 2;
            additionalFeatSelections[0].FocusType = GroupConstants.Skills;
            stats[StatConstants.Intelligence].Value = 12;

            mockFeatFocusGenerator.SetupSequence(g => g.GenerateFrom(FeatConstants.SkillMastery, GroupConstants.Skills, skills, additionalFeatSelections[0].RequiredFeats, It.IsAny<IEnumerable<Feat>>(), characterClass))
                .Returns("skill 1").Returns("skill 2").Returns("skill 3").Returns("skill 4");

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);

            Assert.That(feats.Count(), Is.EqualTo(3));

            for (var i = 0; i < 3; i++)
            {
                var feat = feats.ElementAt(i);
                var focus = String.Format("skill {0}", i + 1);

                Assert.That(feat.Name, Is.EqualTo(FeatConstants.SkillMastery));
                Assert.That(feat.Focus, Is.EqualTo(focus));
                Assert.That(feat.Strength, Is.EqualTo(0));
            }
        }

        [Test]
        public void SkillMasteryDoesNotRepeatFociWithinOneFeatSelection()
        {
            AddFeatSelections(1);
            additionalFeatSelections[0].Feat = FeatConstants.SkillMastery;
            additionalFeatSelections[0].Strength = 1;
            additionalFeatSelections[0].FocusType = GroupConstants.Skills;
            stats[StatConstants.Intelligence].Value = 12;

            mockFeatFocusGenerator.Setup(g => g.GenerateFrom(FeatConstants.SkillMastery, GroupConstants.Skills, skills, additionalFeatSelections[0].RequiredFeats, preselectedFeats, characterClass))
                .Returns("skill 1");
            mockFeatFocusGenerator.Setup(g => g.GenerateFrom(FeatConstants.SkillMastery, GroupConstants.Skills, skills, additionalFeatSelections[0].RequiredFeats, It.Is<IEnumerable<Feat>>(fs => fs.Any(f => f.Name == FeatConstants.SkillMastery)), characterClass))
                .Returns("skill 2");

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);

            Assert.That(feats.Count(), Is.EqualTo(2));

            for (var i = 0; i < 2; i++)
            {
                var feat = feats.ElementAt(i);
                var focus = String.Format("skill {0}", i + 1);

                Assert.That(feat.Name, Is.EqualTo(FeatConstants.SkillMastery));
                Assert.That(feat.Focus, Is.EqualTo(focus));
                Assert.That(feat.Strength, Is.EqualTo(0));
            }
        }

        [Test]
        public void SkillMasteryDoesNotRepeatFociAcrossFeatSelections()
        {
            AddFeatSelections(1);
            additionalFeatSelections[0].Feat = FeatConstants.SkillMastery;
            additionalFeatSelections[0].Strength = 1;
            additionalFeatSelections[0].FocusType = GroupConstants.Skills;
            stats[StatConstants.Intelligence].Value = 10;
            characterClass.Level = 3;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.TakenMultipleTimes))
                .Returns(new[] { FeatConstants.SkillMastery });

            mockFeatFocusGenerator.Setup(g => g.GenerateFrom(FeatConstants.SkillMastery, GroupConstants.Skills, skills, additionalFeatSelections[0].RequiredFeats, preselectedFeats, characterClass))
                .Returns("skill 1");
            mockFeatFocusGenerator.Setup(g => g.GenerateFrom(FeatConstants.SkillMastery, GroupConstants.Skills, skills, additionalFeatSelections[0].RequiredFeats, It.Is<IEnumerable<Feat>>(fs => fs.Any(f => f.Name == FeatConstants.SkillMastery)), characterClass))
                .Returns("skill 2");

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);

            Assert.That(feats.Count(), Is.EqualTo(2));

            for (var i = 0; i < 2; i++)
            {
                var feat = feats.ElementAt(i);
                var focus = String.Format("skill {0}", i + 1);

                Assert.That(feat.Name, Is.EqualTo(FeatConstants.SkillMastery));
                Assert.That(feat.Focus, Is.EqualTo(focus));
                Assert.That(feat.Strength, Is.EqualTo(0));
            }
        }

        [Test]
        public void IfOutOfSkills_StopAddingSkillFoci()
        {
            AddFeatSelections(1);
            additionalFeatSelections[0].Feat = FeatConstants.SkillMastery;
            additionalFeatSelections[0].Strength = 1;
            additionalFeatSelections[0].FocusType = GroupConstants.Skills;
            stats[StatConstants.Intelligence].Value = 10;
            characterClass.Level = 3;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.TakenMultipleTimes))
                .Returns(new[] { FeatConstants.SkillMastery });

            mockFeatFocusGenerator.Setup(g => g.GenerateFrom(FeatConstants.SkillMastery, GroupConstants.Skills, skills, additionalFeatSelections[0].RequiredFeats, preselectedFeats, characterClass))
                .Returns("skill 1");
            mockFeatFocusGenerator.Setup(g => g.GenerateFrom(FeatConstants.SkillMastery, GroupConstants.Skills, skills, additionalFeatSelections[0].RequiredFeats, It.Is<IEnumerable<Feat>>(fs => fs.Any(f => f.Name == FeatConstants.SkillMastery)), characterClass))
                .Returns(ProficiencyConstants.All);

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var onlyFeat = feats.Single();

            Assert.That(onlyFeat.Name, Is.EqualTo(FeatConstants.SkillMastery));
            Assert.That(onlyFeat.Focus, Is.EqualTo("skill 1"));
            Assert.That(onlyFeat.Strength, Is.EqualTo(0));
        }

        [Test]
        public void IfOutOfSkills_DoNotTakeSkillMastery()
        {
            AddFeatSelections(1);
            additionalFeatSelections[0].Feat = FeatConstants.SkillMastery;
            additionalFeatSelections[0].Strength = 1;
            additionalFeatSelections[0].FocusType = GroupConstants.Skills;
            stats[StatConstants.Intelligence].Value = 10;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.TakenMultipleTimes))
                .Returns(new[] { FeatConstants.SkillMastery });

            mockFeatFocusGenerator.Setup(g => g.GenerateFrom(FeatConstants.SkillMastery, GroupConstants.Skills, skills, additionalFeatSelections[0].RequiredFeats, preselectedFeats, characterClass))
                .Returns(ProficiencyConstants.All);

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            Assert.That(feats, Is.Empty);
        }

        [Test]
        public void IfOutOfSkills_CanTakeDifferentFeat()
        {
            AddFeatSelections(2);
            additionalFeatSelections[0].Feat = FeatConstants.SkillMastery;
            additionalFeatSelections[0].Strength = 1;
            additionalFeatSelections[0].FocusType = GroupConstants.Skills;
            stats[StatConstants.Intelligence].Value = 10;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.TakenMultipleTimes))
                .Returns(new[] { FeatConstants.SkillMastery });

            mockFeatFocusGenerator.Setup(g => g.GenerateFrom(FeatConstants.SkillMastery, GroupConstants.Skills, skills, additionalFeatSelections[0].RequiredFeats, preselectedFeats, characterClass))
                .Returns(ProficiencyConstants.All);

            var feats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            var onlyFeat = feats.Single();
            Assert.That(onlyFeat.Name, Is.EqualTo("feat2"));
        }
    }
}