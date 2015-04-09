﻿using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NPCGen.Selectors;
using NPCGen.Selectors.Interfaces;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Selectors
{
    [TestFixture]
    public class FeatSelectorTests
    {
        private IFeatsSelector selector;
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            selector = new FeatsSelector(mockCollectionsSelector.Object, mockAdjustmentsSelector.Object);

            mockCollectionsSelector.Setup(s => s.SelectFrom(It.IsAny<String>(), It.IsAny<String>())).Returns(Enumerable.Empty<String>());
        }

        [Test]
        public void GetRacialFeats()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, TableNameConstants.Set.Collection.Groups.Racial))
                .Returns(new[] { "racial feat 1", "racial feat 2" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatData, "racial feat 1")).Returns(new[] { "ginormous", "9266" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatData, "racial feat 2")).Returns(new[] { String.Empty, "0" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.RacialFeatBaseRaceRequirements, "racial feat 1")).Returns(new[] { "base race 1", "base race 2" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.RacialFeatMetaraceRequirements, "racial feat 2")).Returns(new[] { "metarace 2", "metarace 3" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.RacialFeatHitDieRequirements, "racial feat 1")).Returns(new[] { "42", "90210" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.RacialFeatMetaraceSpeciesRequirements, "racial feat 2")).Returns(new[] { "species 2", "species 3" });

            var racialFeats = selector.SelectRacial();
            Assert.That(racialFeats.Count(), Is.EqualTo(2));

            var first = racialFeats.First();
            var last = racialFeats.Last();

            Assert.That(first.Name, Is.EqualTo("racial feat 1"));
            Assert.That(first.FeatStrength, Is.EqualTo(9266));
            Assert.That(first.SizeRequirement, Is.EqualTo("ginormous"));
            Assert.That(first.BaseRaceIdRequirements, Contains.Item("base race 1"));
            Assert.That(first.BaseRaceIdRequirements, Contains.Item("base race 2"));
            Assert.That(first.BaseRaceIdRequirements.Count(), Is.EqualTo(2));
            Assert.That(first.HitDieRequirements, Contains.Item(42));
            Assert.That(first.HitDieRequirements, Contains.Item(90210));
            Assert.That(first.HitDieRequirements.Count(), Is.EqualTo(2));
            Assert.That(first.MetaraceIdRequirements, Is.Empty);
            Assert.That(first.MetaraceSpeciesRequirements, Is.Empty);

            Assert.That(last.Name, Is.EqualTo("racial feat 2"));
            Assert.That(last.FeatStrength, Is.EqualTo(0));
            Assert.That(last.SizeRequirement, Is.Empty);
            Assert.That(last.BaseRaceIdRequirements, Is.Empty);
            Assert.That(last.HitDieRequirements, Is.Empty);
            Assert.That(last.MetaraceIdRequirements, Contains.Item("metarace 2"));
            Assert.That(last.MetaraceIdRequirements, Contains.Item("metarace 3"));
            Assert.That(last.MetaraceIdRequirements.Count(), Is.EqualTo(2));
            Assert.That(last.MetaraceSpeciesRequirements, Contains.Item("species 3"));
            Assert.That(last.MetaraceSpeciesRequirements, Contains.Item("species 2"));
            Assert.That(last.MetaraceSpeciesRequirements.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetClassFeats()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, TableNameConstants.Set.Collection.Groups.CharacterClasses))
                .Returns(new[] { "class feat 1", "class feat 2" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatData, "class feat 1")).Returns(new[] { "0", "class 1", "class 3" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatData, "class feat 2")).Returns(new[] { "5", "class 2", "class 3", "specialist" });

            var class1LevelRequirements = new Dictionary<String, Int32>();
            class1LevelRequirements["class feat 1"] = 1;
            class1LevelRequirements["class feat 2"] = 2;

            var tableName = String.Format(TableNameConstants.Formattable.Adjustments.CLASSFeatLevelRequirements, "class 1");
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(tableName)).Returns(class1LevelRequirements);

            var class2LevelRequirements = new Dictionary<String, Int32>();
            class2LevelRequirements["class feat 1"] = 11;
            class2LevelRequirements["class feat 2"] = 15;

            tableName = String.Format(TableNameConstants.Formattable.Adjustments.CLASSFeatLevelRequirements, "class 2");
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(tableName)).Returns(class2LevelRequirements);

            var class3LevelRequirements = new Dictionary<String, Int32>();
            class3LevelRequirements["class feat 1"] = 5;
            class3LevelRequirements["class feat 2"] = 3;

            tableName = String.Format(TableNameConstants.Formattable.Adjustments.CLASSFeatLevelRequirements, "class 3");
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(tableName)).Returns(class3LevelRequirements);

            var specialistLevelRequirements = new Dictionary<String, Int32>();
            specialistLevelRequirements["class feat 1"] = 0;
            specialistLevelRequirements["class feat 2"] = 0;

            tableName = String.Format(TableNameConstants.Formattable.Adjustments.CLASSFeatLevelRequirements, "specialist");
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(tableName)).Returns(specialistLevelRequirements);

            var classFeats = selector.SelectClassFeats();
            Assert.That(classFeats.Count(), Is.EqualTo(2));

            var first = classFeats.First();
            var last = classFeats.Last();

            Assert.That(first.Name, Is.EqualTo("class feat 1"));
            Assert.That(first.LevelRequirements["class 1"], Is.EqualTo(1));
            Assert.That(first.LevelRequirements["class 3"], Is.EqualTo(5));
            Assert.That(first.Strength, Is.EqualTo(0));

            Assert.That(last.Name, Is.EqualTo("class feat 2"));
            Assert.That(last.LevelRequirements["class 2"], Is.EqualTo(15));
            Assert.That(last.LevelRequirements["class 3"], Is.EqualTo(3));
            Assert.That(last.LevelRequirements["specialist"], Is.EqualTo(0));
            Assert.That(last.Strength, Is.EqualTo(5));
        }

        [Test]
        public void GetAdditionalFeats()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, TableNameConstants.Set.Collection.Groups.Racial))
                .Returns(new[] { "additional feat 1", "additional feat 2" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatData, "additional feat 1")).Returns(new[] { "True", "False", "9266", String.Empty });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatData, "additional feat 2")).Returns(new[] { "False", "True", "0", "specifics" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.AdditionalFeatClassNameRequirements, "additional feat 1")).Returns(new[] { "class 1", "class 3" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.AdditionalFeatFeatRequirements, "additional feat 1")).Returns(new[] { "feat 1", "feat 2" });

            //TODO adjustment setup

            var additionalFeats = selector.SelectAdditional();
            Assert.That(additionalFeats.Count(), Is.EqualTo(2));

            var first = additionalFeats.First();
            var last = additionalFeats.Last();

            Assert.That(first.Name, Is.EqualTo("additional feat 1"));
            Assert.That(first.IsFighterFeat, Is.True);
            Assert.That(first.IsWizardFeat, Is.False);
            Assert.That(first.RequiredBaseAttack, Is.EqualTo(9266));
            Assert.That(first.RequiredClassNames, Contains.Item("class 1"));
            Assert.That(first.RequiredClassNames, Contains.Item("class 3"));
            Assert.That(first.RequiredClassNames.Count(), Is.EqualTo(2));
            Assert.That(first.RequiredFeatIds, Contains.Item("feat 1"));
            Assert.That(first.RequiredFeatIds, Contains.Item("feat 2"));
            Assert.That(first.RequiredFeatIds.Count(), Is.EqualTo(2));
            Assert.That(first.RequiredSkillRanks, Is.Empty);
            Assert.That(first.RequiredStats, Is.Empty);
            Assert.That(first.SpecificApplicationType, Is.Empty);

            Assert.That(last.Name, Is.EqualTo("additional feat 2"));
            Assert.That(last.IsFighterFeat, Is.False);
            Assert.That(last.IsWizardFeat, Is.True);
            Assert.That(last.RequiredBaseAttack, Is.EqualTo(0));
            Assert.That(last.RequiredClassNames, Is.Empty);
            Assert.That(last.RequiredFeatIds, Is.Empty);
            Assert.That(last.RequiredSkillRanks["skill 1"], Is.EqualTo(5));
            Assert.That(last.RequiredSkillRanks["skill 2"], Is.EqualTo(4));
            Assert.That(last.RequiredSkillRanks.Count, Is.EqualTo(2));
            Assert.That(last.RequiredStats["stat 1"], Is.EqualTo(13));
            Assert.That(last.RequiredStats["stat 2"], Is.EqualTo(16));
            Assert.That(last.RequiredStats.Count, Is.EqualTo(2));
            Assert.That(last.SpecificApplicationType, Is.EqualTo("specifics"));
        }

        [Test]
        public void GetAdditionalFeat()
        {
            //TODO: setup

            var additionalFeat = selector.SelectAdditional("additional feat 1");
            Assert.That(additionalFeat.Name, Is.EqualTo("additional feat 1"));
            Assert.That(additionalFeat.IsFighterFeat, Is.True);
            Assert.That(additionalFeat.IsWizardFeat, Is.False);
            Assert.That(additionalFeat.RequiredBaseAttack, Is.EqualTo(9266));
            Assert.That(additionalFeat.RequiredClassNames, Contains.Item("class 1"));
            Assert.That(additionalFeat.RequiredClassNames, Contains.Item("class 3"));
            Assert.That(additionalFeat.RequiredClassNames.Count(), Is.EqualTo(2));
            Assert.That(additionalFeat.RequiredFeatIds, Contains.Item("feat 1"));
            Assert.That(additionalFeat.RequiredFeatIds, Contains.Item("feat 2"));
            Assert.That(additionalFeat.RequiredFeatIds.Count(), Is.EqualTo(2));
            Assert.That(additionalFeat.RequiredSkillRanks, Is.Empty);
            Assert.That(additionalFeat.RequiredStats, Is.Empty);
            Assert.That(additionalFeat.SpecificApplicationType, Is.Empty);
        }
    }
}