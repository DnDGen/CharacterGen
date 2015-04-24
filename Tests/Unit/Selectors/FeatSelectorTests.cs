using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
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
        private Mock<INameSelector> mockNameSelector;

        private Race race;
        private CharacterClass characterClass;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockNameSelector = new Mock<INameSelector>();
            selector = new FeatsSelector(mockCollectionsSelector.Object, mockAdjustmentsSelector.Object, mockNameSelector.Object);
            race = new Race();
            characterClass = new CharacterClass();

            race.BaseRace.Id = Guid.NewGuid().ToString();
            race.Metarace.Id = Guid.NewGuid().ToString();
            race.MetaraceSpecies = Guid.NewGuid().ToString();
            characterClass.ClassName = Guid.NewGuid().ToString();
            characterClass.SpecialistFields = new[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() };

            mockCollectionsSelector.Setup(s => s.SelectFrom(It.IsAny<String>(), It.IsAny<String>())).Returns(Enumerable.Empty<String>());
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(It.IsAny<String>())).Returns(new Dictionary<String, Int32>());
        }

        [Test]
        public void GetBaseRaceFeats()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, race.BaseRace.Id)).Returns(new[] { "racial feat 1", "racial feat 2" });

            var baseRaceFeatTableName = String.Format(TableNameConstants.Formattable.Collection.RACEFeatData, race.BaseRace.Id);

            mockCollectionsSelector.Setup(s => s.SelectFrom(baseRaceFeatTableName, "racial feat 1")).Returns(new[] { "ginormous", "9266" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(baseRaceFeatTableName, "racial feat 2")).Returns(new[] { String.Empty, "0" });

            mockNameSelector.Setup(s => s.Select("racial feat 1")).Returns("racial feat 1 name");
            mockNameSelector.Setup(s => s.Select("racial feat 2")).Returns("racial feat 2 name");

            var racialFeats = selector.SelectFor(race);
            Assert.That(racialFeats.Count(), Is.EqualTo(2));

            var first = racialFeats.First();
            var last = racialFeats.Last();

            Assert.That(first.FeatId, Is.EqualTo("racial feat 1"));
            Assert.That(first.SizeRequirement, Is.EqualTo("ginormous"));
            Assert.That(first.MinimumHitDieRequirement, Is.EqualTo(9266));

            Assert.That(last.FeatId, Is.EqualTo("racial feat 2"));
            Assert.That(last.SizeRequirement, Is.Empty);
            Assert.That(first.MinimumHitDieRequirement, Is.EqualTo(0));
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

            mockNameSelector.Setup(s => s.Select("class feat 1")).Returns("class feat 1 name");
            mockNameSelector.Setup(s => s.Select("class feat 2")).Returns("class feat 2 name");

            var classFeats = selector.SelectFor(characterClass);
            Assert.That(classFeats.Count(), Is.EqualTo(2));

            var first = classFeats.First();
            var last = classFeats.Last();

            Assert.That(first.Name.Id, Is.EqualTo("class feat 1"));
            Assert.That(first.Name.Name, Is.EqualTo("class feat 1 name"));
            Assert.That(first.LevelRequirements["class 1"], Is.EqualTo(1));
            Assert.That(first.LevelRequirements["class 3"], Is.EqualTo(5));
            Assert.That(first.Strength, Is.EqualTo(0));

            Assert.That(last.Name.Id, Is.EqualTo("class feat 2"));
            Assert.That(last.Name.Name, Is.EqualTo("class feat 2 name"));
            Assert.That(last.LevelRequirements["class 2"], Is.EqualTo(15));
            Assert.That(last.LevelRequirements["class 3"], Is.EqualTo(3));
            Assert.That(last.LevelRequirements["specialist"], Is.EqualTo(0));
            Assert.That(last.Strength, Is.EqualTo(5));
        }

        [Test]
        public void GetAdditionalFeats()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, TableNameConstants.Set.Collection.Groups.Additional))
                .Returns(new[] { "additional feat 1", "additional feat 2" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatData, "additional feat 1")).Returns(new[] { "True", "False", "9266", String.Empty });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatData, "additional feat 2")).Returns(new[] { "False", "True", "0", "specifics" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.AdditionalFeatClassNameRequirements, "additional feat 1")).Returns(new[] { "class 1", "class 3" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.AdditionalFeatFeatRequirements, "additional feat 1")).Returns(new[] { "feat 1", "feat 2" });

            var feat2SkillRankRequirements = new Dictionary<String, Int32>();
            feat2SkillRankRequirements["skill 1"] = 5;
            feat2SkillRankRequirements["skill 2"] = 4;

            var tableName = String.Format(TableNameConstants.Formattable.Adjustments.FEATSkillRankRequirements, "additional feat 2");
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(tableName)).Returns(feat2SkillRankRequirements);

            var feat2StatRequirements = new Dictionary<String, Int32>();
            feat2StatRequirements["stat 1"] = 13;
            feat2StatRequirements["stat 2"] = 16;

            tableName = String.Format(TableNameConstants.Formattable.Adjustments.FEATStatRequirements, "additional feat 2");
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(tableName)).Returns(feat2StatRequirements);

            mockNameSelector.Setup(s => s.Select("additional feat 1")).Returns("additional feat 1 name");
            mockNameSelector.Setup(s => s.Select("additional feat 2")).Returns("additional feat 2 name");

            var additionalFeats = selector.SelectAdditional();
            Assert.That(additionalFeats.Count(), Is.EqualTo(2));

            var first = additionalFeats.First();
            var last = additionalFeats.Last();

            Assert.That(first.Name.Id, Is.EqualTo("additional feat 1"));
            Assert.That(first.Name.Name, Is.EqualTo("additional feat 1 name"));
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

            Assert.That(last.Name.Id, Is.EqualTo("additional feat 2"));
            Assert.That(last.Name.Name, Is.EqualTo("additional feat 2 name"));
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
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatData, "additional feat 1")).Returns(new[] { "True", "False", "9266", String.Empty });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.AdditionalFeatClassNameRequirements, "additional feat 1")).Returns(new[] { "class 1", "class 3" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.AdditionalFeatFeatRequirements, "additional feat 1")).Returns(new[] { "feat 1", "feat 2" });

            mockNameSelector.Setup(s => s.Select("additional feat 1")).Returns("additional feat 1 name");

            var additionalFeat = selector.SelectAdditional("additional feat 1");
            Assert.That(additionalFeat.Name.Id, Is.EqualTo("additional feat 1"));
            Assert.That(additionalFeat.Name.Name, Is.EqualTo("additional feat 1 name"));
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