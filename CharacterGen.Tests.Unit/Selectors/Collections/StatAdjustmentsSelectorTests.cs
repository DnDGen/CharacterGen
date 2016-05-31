using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CharacterGen.Tests.Unit.Selectors.Collections
{
    [TestFixture]
    public class StatAdjustmentsSelectorTests
    {
        private IStatAdjustmentsSelector selector;
        private Race race;
        private Mock<IAdjustmentsSelector> mockInnerSelector;
        private Dictionary<String, Int32> defaultAdjustments;
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private List<String> statNames;

        [SetUp]
        public void Setup()
        {
            mockInnerSelector = new Mock<IAdjustmentsSelector>();
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            selector = new StatAdjustmentsSelector(mockInnerSelector.Object, mockCollectionsSelector.Object);
            race = new Race();
            defaultAdjustments = new Dictionary<String, Int32>();
            statNames = new List<String>();

            race.BaseRace = "base race";
            race.Metarace = "metarace";
            defaultAdjustments[race.BaseRace] = 0;
            defaultAdjustments[race.Metarace] = 0;
            mockInnerSelector.Setup(s => s.SelectFrom(It.IsAny<String>())).Returns(defaultAdjustments);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.StatGroups, GroupConstants.All)).Returns(statNames);

            statNames.Add("first stat");
            statNames.Add("second stat");
        }

        [Test]
        public void AdjustmentsContainAllStats()
        {
            var adjustments = selector.SelectFor(race);

            foreach (var stat in statNames)
                Assert.That(adjustments.Keys, Contains.Item(stat));
        }

        [Test]
        public void EachStatAdjustmentIsIndividual()
        {
            var allAdjustments = new Dictionary<String, Dictionary<String, Int32>>();
            foreach (var stat in statNames)
                allAdjustments[stat] = new Dictionary<String, Int32>();

            allAdjustments[statNames[0]][race.BaseRace] = 9266;
            allAdjustments[statNames[0]][race.Metarace] = 42;
            allAdjustments[statNames[1]][race.BaseRace] = 90210;
            allAdjustments[statNames[1]][race.Metarace] = -9266;

            foreach (var stat in statNames)
            {
                var tableName = String.Format("{0}StatAdjustments", stat);
                mockInnerSelector.Setup(s => s.SelectFrom(tableName)).Returns(allAdjustments[stat]);
            }

            var adjustments = selector.SelectFor(race);
            Assert.That(adjustments[statNames[0]], Is.EqualTo(9308));
            Assert.That(adjustments[statNames[1]], Is.EqualTo(80944));
        }
    }
}