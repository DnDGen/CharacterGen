using System;
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
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(It.IsAny<String>())).Returns(new Dictionary<String, Int32>());
        }

        [Test]
        public void GetRacialFeats()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, "race")).Returns(new[] { "racial feat 1", "racial feat 2" });

            var racialFeatTableName = String.Format(TableNameConstants.Formattable.Collection.RACEFeatData, "race");
            mockCollectionsSelector.Setup(s => s.SelectFrom(racialFeatTableName, "racial feat 1")).Returns(new[] { "racialFeat1", "ginormous", "9266", "0", String.Empty, "0", "never" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(racialFeatTableName, "racial feat 2")).Returns(new[] { "racialFeat2", String.Empty, "0", "90210", "focusness", "42", "fortnight" });

            var racialFeats = selector.SelectRacial("race");
            Assert.That(racialFeats.Count(), Is.EqualTo(2));

            var first = racialFeats.First();
            var last = racialFeats.Last();

            Assert.That(first.FeatId, Is.EqualTo("racialFeat1"));
            Assert.That(first.SizeRequirement, Is.EqualTo("ginormous"));
            Assert.That(first.MinimumHitDieRequirement, Is.EqualTo(9266));
            Assert.That(first.Strength, Is.EqualTo(0));
            Assert.That(first.FocusType, Is.Empty);
            Assert.That(first.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(first.Frequency.TimePeriod, Is.EqualTo("never"));

            Assert.That(last.FeatId, Is.EqualTo("racialFeat2"));
            Assert.That(last.SizeRequirement, Is.Empty);
            Assert.That(last.MinimumHitDieRequirement, Is.EqualTo(0));
            Assert.That(last.Strength, Is.EqualTo(90210));
            Assert.That(last.FocusType, Is.EqualTo("focusness"));
            Assert.That(last.Frequency.Quantity, Is.EqualTo(42));
            Assert.That(last.Frequency.TimePeriod, Is.EqualTo("fortnight"));
        }

        [Test]
        public void GetDifferentRacialFeatsWithSameId()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, "race")).Returns(new[] { "racial feat 1", "racial feat 2" });

            var racialFeatTableName = String.Format(TableNameConstants.Formattable.Collection.RACEFeatData, "race");
            mockCollectionsSelector.Setup(s => s.SelectFrom(racialFeatTableName, "racial feat 1")).Returns(new[] { "racialFeat1", "ginormous", "9266", "0", String.Empty, "0", "never" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(racialFeatTableName, "racial feat 2")).Returns(new[] { "racialFeat1", String.Empty, "0", "90210", "focusness", "42", "fortnight" });

            var racialFeats = selector.SelectRacial("race");
            Assert.That(racialFeats.Count(), Is.EqualTo(2));

            var first = racialFeats.First();
            var last = racialFeats.Last();

            Assert.That(first.FeatId, Is.EqualTo("racialFeat1"));
            Assert.That(first.SizeRequirement, Is.EqualTo("ginormous"));
            Assert.That(first.MinimumHitDieRequirement, Is.EqualTo(9266));
            Assert.That(first.Strength, Is.EqualTo(0));
            Assert.That(first.FocusType, Is.Empty);
            Assert.That(first.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(first.Frequency.TimePeriod, Is.EqualTo("never"));

            Assert.That(last.FeatId, Is.EqualTo("racialFeat1"));
            Assert.That(last.SizeRequirement, Is.Empty);
            Assert.That(last.MinimumHitDieRequirement, Is.EqualTo(0));
            Assert.That(last.Strength, Is.EqualTo(90210));
            Assert.That(last.FocusType, Is.EqualTo("focusness"));
            Assert.That(last.Frequency.Quantity, Is.EqualTo(42));
            Assert.That(last.Frequency.TimePeriod, Is.EqualTo("fortnight"));
        }

        [Test]
        public void GetClassFeats()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, "class name"))
                .Returns(new[] { "class feat 1", "class feat 2" });

            var classFeatTableName = String.Format(TableNameConstants.Formattable.Collection.CLASSFeatData, "class name");
            mockCollectionsSelector.Setup(s => s.SelectFrom(classFeatTableName, "class feat 1")).Returns(new[] { "classFeat1", "1", "focus type A", "0", "3", "Daily", "4", "stat" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(classFeatTableName, "class feat 2")).Returns(new[] { "classFeat2", "5", String.Empty, "9266", "0", "never", "0", String.Empty });

            var featRequirements = new Dictionary<String, IEnumerable<String>>();
            featRequirements["classFeat1"] = new[] { "feat 1", "feat 2" };
            mockCollectionsSelector.Setup(s => s.SelectAllFrom(TableNameConstants.Set.Collection.RequiredFeats)).Returns(featRequirements);

            var classFeats = selector.SelectClass("class name");
            Assert.That(classFeats.Count(), Is.EqualTo(2));

            var first = classFeats.First();
            var last = classFeats.Last();

            Assert.That(first.FeatId, Is.EqualTo("classFeat1"));
            Assert.That(first.FocusType, Is.EqualTo("focus type A"));
            Assert.That(first.MinimumLevel, Is.EqualTo(1));
            Assert.That(first.Strength, Is.EqualTo(0));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(3));
            Assert.That(first.Frequency.TimePeriod, Is.EqualTo("Daily"));
            Assert.That(first.FrequencyQuantityStat, Is.EqualTo("stat"));
            Assert.That(first.MaximumLevel, Is.EqualTo(4));
            Assert.That(first.RequiredFeatIds, Contains.Item("feat 1"));
            Assert.That(first.RequiredFeatIds, Contains.Item("feat 2"));
            Assert.That(first.RequiredFeatIds.Count(), Is.EqualTo(2));

            Assert.That(last.FeatId, Is.EqualTo("classFeat2"));
            Assert.That(last.FocusType, Is.Empty);
            Assert.That(last.MinimumLevel, Is.EqualTo(5));
            Assert.That(last.Strength, Is.EqualTo(9266));
            Assert.That(last.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(last.Frequency.TimePeriod, Is.EqualTo("never"));
            Assert.That(last.FrequencyQuantityStat, Is.Empty);
            Assert.That(last.MaximumLevel, Is.EqualTo(0));
            Assert.That(last.RequiredFeatIds, Is.Empty);
        }

        [Test]
        public void GetDifferentClassFeatsWithSameId()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, "class name"))
                .Returns(new[] { "class feat 1", "class feat 2" });

            var classFeatTableName = String.Format(TableNameConstants.Formattable.Collection.CLASSFeatData, "class name");
            mockCollectionsSelector.Setup(s => s.SelectFrom(classFeatTableName, "class feat 1")).Returns(new[] { "classFeat1", "1", "focus type A", "0", "3", "Daily", "4", "stat" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(classFeatTableName, "class feat 2")).Returns(new[] { "classFeat1", "5", String.Empty, "9266", "0", "never", "0", String.Empty });

            var featRequirements = new Dictionary<String, IEnumerable<String>>();
            featRequirements["classFeat1"] = new[] { "feat 1", "feat 2" };
            mockCollectionsSelector.Setup(s => s.SelectAllFrom(TableNameConstants.Set.Collection.RequiredFeats)).Returns(featRequirements);

            var classFeats = selector.SelectClass("class name");
            Assert.That(classFeats.Count(), Is.EqualTo(2));

            var first = classFeats.First();
            var last = classFeats.Last();

            Assert.That(first.FeatId, Is.EqualTo("classFeat1"));
            Assert.That(first.FocusType, Is.EqualTo("focus type A"));
            Assert.That(first.MinimumLevel, Is.EqualTo(1));
            Assert.That(first.Strength, Is.EqualTo(0));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(3));
            Assert.That(first.Frequency.TimePeriod, Is.EqualTo("Daily"));
            Assert.That(first.FrequencyQuantityStat, Is.EqualTo("stat"));
            Assert.That(first.MaximumLevel, Is.EqualTo(4));
            Assert.That(first.RequiredFeatIds, Contains.Item("feat 1"));
            Assert.That(first.RequiredFeatIds, Contains.Item("feat 2"));
            Assert.That(first.RequiredFeatIds.Count(), Is.EqualTo(2));

            Assert.That(last.FeatId, Is.EqualTo("classFeat1"));
            Assert.That(last.FocusType, Is.Empty);
            Assert.That(last.MinimumLevel, Is.EqualTo(5));
            Assert.That(last.Strength, Is.EqualTo(9266));
            Assert.That(last.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(last.Frequency.TimePeriod, Is.EqualTo("never"));
            Assert.That(last.FrequencyQuantityStat, Is.Empty);
            Assert.That(last.MaximumLevel, Is.EqualTo(0));
            Assert.That(last.RequiredFeatIds, Contains.Item("feat 1"));
            Assert.That(last.RequiredFeatIds, Contains.Item("feat 2"));
            Assert.That(last.RequiredFeatIds.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetAdditionalFeats()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.Additional))
                .Returns(new[] { "additional feat 1", "additional feat 2" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.AdditionalFeatData, "additional feat 1")).Returns(new[] { "True", "False", "9266", String.Empty, "42", "0", String.Empty });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.AdditionalFeatData, "additional feat 2")).Returns(new[] { "False", "True", "0", "focus", "0", "9266", "occasionally" });

            var featRequirements = new Dictionary<String, IEnumerable<String>>();
            featRequirements["additional feat 1"] = new[] { "feat 1", "feat 2" };
            mockCollectionsSelector.Setup(s => s.SelectAllFrom(TableNameConstants.Set.Collection.RequiredFeats)).Returns(featRequirements);

            var feat1ClassRequirements = new Dictionary<String, Int32>();
            feat1ClassRequirements["class 1"] = 3;
            feat1ClassRequirements["class 3"] = 5;

            var tableName = String.Format(TableNameConstants.Formattable.Adjustments.FEATClassRequirements, "additional feat 1");
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(tableName)).Returns(feat1ClassRequirements);

            var feat2SkillRankRequirements = new Dictionary<String, Int32>();
            feat2SkillRankRequirements["skill 1"] = 5;
            feat2SkillRankRequirements["skill 2"] = 4;

            tableName = String.Format(TableNameConstants.Formattable.Adjustments.FEATSkillRankRequirements, "additional feat 2");
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(tableName)).Returns(feat2SkillRankRequirements);

            var feat2StatRequirements = new Dictionary<String, Int32>();
            feat2StatRequirements["stat 1"] = 13;
            feat2StatRequirements["stat 2"] = 16;

            tableName = String.Format(TableNameConstants.Formattable.Adjustments.FEATStatRequirements, "additional feat 2");
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(tableName)).Returns(feat2StatRequirements);

            var additionalFeats = selector.SelectAdditional();
            Assert.That(additionalFeats.Count(), Is.EqualTo(2));

            var first = additionalFeats.First();
            var last = additionalFeats.Last();

            Assert.That(first.FeatId, Is.EqualTo("additional feat 1"));
            Assert.That(first.Strength, Is.EqualTo(42));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(first.Frequency.TimePeriod, Is.Empty);
            Assert.That(first.IsFighterFeat, Is.True);
            Assert.That(first.IsWizardFeat, Is.False);
            Assert.That(first.RequiredBaseAttack, Is.EqualTo(9266));
            Assert.That(first.RequiredCharacterClasses["class 1"], Is.EqualTo(3));
            Assert.That(first.RequiredCharacterClasses["class 3"], Is.EqualTo(5));
            Assert.That(first.RequiredCharacterClasses.Count, Is.EqualTo(2));
            Assert.That(first.RequiredFeatIds, Contains.Item("feat 1"));
            Assert.That(first.RequiredFeatIds, Contains.Item("feat 2"));
            Assert.That(first.RequiredFeatIds.Count(), Is.EqualTo(2));
            Assert.That(first.RequiredSkillRanks, Is.Empty);
            Assert.That(first.RequiredStats, Is.Empty);
            Assert.That(first.FocusType, Is.Empty);

            Assert.That(last.FeatId, Is.EqualTo("additional feat 2"));
            Assert.That(last.Strength, Is.EqualTo(0));
            Assert.That(last.Frequency.Quantity, Is.EqualTo(9266));
            Assert.That(last.Frequency.TimePeriod, Is.EqualTo("occasionally"));
            Assert.That(last.IsFighterFeat, Is.False);
            Assert.That(last.IsWizardFeat, Is.True);
            Assert.That(last.RequiredBaseAttack, Is.EqualTo(0));
            Assert.That(last.RequiredCharacterClasses, Is.Empty);
            Assert.That(last.RequiredFeatIds, Is.Empty);
            Assert.That(last.RequiredSkillRanks["skill 1"], Is.EqualTo(5));
            Assert.That(last.RequiredSkillRanks["skill 2"], Is.EqualTo(4));
            Assert.That(last.RequiredSkillRanks.Count, Is.EqualTo(2));
            Assert.That(last.RequiredStats["stat 1"], Is.EqualTo(13));
            Assert.That(last.RequiredStats["stat 2"], Is.EqualTo(16));
            Assert.That(last.RequiredStats.Count, Is.EqualTo(2));
            Assert.That(last.FocusType, Is.EqualTo("focus"));
        }
    }
}