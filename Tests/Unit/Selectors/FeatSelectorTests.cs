using System;
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

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            selector = new FeatsSelector(mockCollectionsSelector.Object);

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
        public void GetAdditionalFeats()
        {
            Assert.Fail();
        }

        [Test]
        public void GetAdditionalFeat()
        {
            Assert.Fail();
        }

        [Test]
        public void GetClassFeats()
        {
            Assert.Fail();
        }
    }
}