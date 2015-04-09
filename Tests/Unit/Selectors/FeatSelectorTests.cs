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

            Assert.That(first.FeatName, Is.EqualTo("racial feat 1"));
            Assert.That(first.FeatStrength, Is.EqualTo(9266));
            Assert.That(first.SizeRequirement, Is.EqualTo("ginormous"));
            Assert.That(first.BaseRaceRequirements, Contains.Item("base race 1"));
            Assert.That(first.BaseRaceRequirements, Contains.Item("base race 2"));
            Assert.That(first.BaseRaceRequirements.Count(), Is.EqualTo(2));
            Assert.That(first.HitDieRequirements, Contains.Item(42));
            Assert.That(first.HitDieRequirements, Contains.Item(90210));
            Assert.That(first.HitDieRequirements.Count(), Is.EqualTo(2));
            Assert.That(first.MetaraceRequirements, Is.Empty);
            Assert.That(first.MetaraceSpeciesRequirements, Is.Empty);

            Assert.That(last.FeatName, Is.EqualTo("racial feat 2"));
            Assert.That(last.FeatStrength, Is.EqualTo(0));
            Assert.That(last.SizeRequirement, Is.Empty);
            Assert.That(last.BaseRaceRequirements, Is.Empty);
            Assert.That(last.HitDieRequirements, Is.Empty);
            Assert.That(last.MetaraceRequirements, Contains.Item("metarace 2"));
            Assert.That(last.MetaraceRequirements, Contains.Item("metarace 3"));
            Assert.That(last.MetaraceRequirements.Count(), Is.EqualTo(2));
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

            Assert.That(first.FeatName, Is.EqualTo("class feat 1"));
            Assert.That(first.LevelRequirements["class 1"], Is.EqualTo(1));
            Assert.That(first.LevelRequirements["class 3"], Is.EqualTo(5));
            Assert.That(first.Strength, Is.EqualTo(0));

            Assert.That(last.FeatName, Is.EqualTo("class feat 2"));
            Assert.That(last.LevelRequirements["class 2"], Is.EqualTo(15));
            Assert.That(last.LevelRequirements["class 3"], Is.EqualTo(3));
            Assert.That(last.LevelRequirements["specialist"], Is.EqualTo(0));
            Assert.That(last.Strength, Is.EqualTo(5));
        }

        [Test]
        public void GetAdditionalFeats()
        {
            Assert.Fail();
        }

        [Test]
        public void GetAdditionalFeat()
        {
            Assert.Fail();
        }
    }
}