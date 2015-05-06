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

        private Race race;
        private CharacterClass characterClass;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            selector = new FeatsSelector(mockCollectionsSelector.Object, mockAdjustmentsSelector.Object);
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
            mockCollectionsSelector.Setup(s => s.SelectFrom(baseRaceFeatTableName, "racial feat 1")).Returns(new[] { "ginormous", "9266", "0", String.Empty, "0", "never" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(baseRaceFeatTableName, "racial feat 2")).Returns(new[] { String.Empty, "0", "90210", "focusness", "42", "fortnight" });

            var racialFeats = selector.SelectRacial(race.BaseRace.Id);
            Assert.That(racialFeats.Count(), Is.EqualTo(2));

            var first = racialFeats.First();
            var last = racialFeats.Last();

            Assert.That(first.FeatId, Is.EqualTo("racial feat 1"));
            Assert.That(first.SizeRequirement, Is.EqualTo("ginormous"));
            Assert.That(first.MinimumHitDieRequirement, Is.EqualTo(9266));
            Assert.That(first.Strength, Is.EqualTo(0));
            Assert.That(first.Focus, Is.Empty);
            Assert.That(first.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(first.Frequency.TimePeriod, Is.EqualTo("never"));

            Assert.That(last.FeatId, Is.EqualTo("racial feat 2"));
            Assert.That(last.SizeRequirement, Is.Empty);
            Assert.That(last.MinimumHitDieRequirement, Is.EqualTo(0));
            Assert.That(last.Strength, Is.EqualTo(90210));
            Assert.That(last.Focus, Is.EqualTo("focusness"));
            Assert.That(last.Frequency.Quantity, Is.EqualTo(42));
            Assert.That(last.Frequency.TimePeriod, Is.EqualTo("fortnight"));
        }

        [Test]
        public void GetClassFeats()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, characterClass.ClassName))
                .Returns(new[] { "class feat 1", "class feat 2" });

            var classFeatTableName = String.Format(TableNameConstants.Formattable.Collection.CLASSFeatData, characterClass.ClassName);
            mockCollectionsSelector.Setup(s => s.SelectFrom(classFeatTableName, "class feat 1")).Returns(new[] { "1", "focus type A", "0", "3", "Daily" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(classFeatTableName, "class feat 2")).Returns(new[] { "5", String.Empty, "9266", "0", "never" });

            var classFeats = selector.SelectClass(characterClass.ClassName);
            Assert.That(classFeats.Count(), Is.EqualTo(2));

            var first = classFeats.First();
            var last = classFeats.Last();

            Assert.That(first.FeatId, Is.EqualTo("class feat 1"));
            Assert.That(first.FocusType, Is.EqualTo("focus type A"));
            Assert.That(first.MinimumLevel, Is.EqualTo(1));
            Assert.That(first.Strength, Is.EqualTo(0));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(3));
            Assert.That(first.Frequency.TimePeriod, Is.EqualTo("Daily"));

            Assert.That(last.FeatId, Is.EqualTo("class feat 2"));
            Assert.That(last.FocusType, Is.Empty);
            Assert.That(last.MinimumLevel, Is.EqualTo(5));
            Assert.That(last.Strength, Is.EqualTo(9266));
            Assert.That(last.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(last.Frequency.TimePeriod, Is.EqualTo("never"));
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