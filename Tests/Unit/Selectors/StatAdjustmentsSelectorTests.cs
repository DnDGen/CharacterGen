using System;
using System.Collections.Generic;
using Moq;
using NPCGen.Common.Races;
using NPCGen.Common.Stats;
using NPCGen.Mappers.Interfaces;
using NPCGen.Selectors;
using NPCGen.Selectors.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Selectors
{
    [TestFixture]
    public class StatAdjustmentsSelectorTests
    {
        private IStatAdjustmentsSelector Selector;
        private Race race;
        private Mock<IAdjustmentMapper> mockAdjustmentXmlMapper;
        private Dictionary<String, Int32> adjustments;

        [SetUp]
        public void Setup()
        {
            mockAdjustmentXmlMapper = new Mock<IAdjustmentMapper>();
            race = new Race();
            race.BaseRace = "base race";
            race.Metarace = "metarace";
            adjustments = new Dictionary<String, Int32>();
            adjustments.Add(race.BaseRace, 0);
            adjustments.Add(race.Metarace, 0);
            mockAdjustmentXmlMapper.Setup(p => p.Map(It.IsAny<String>())).Returns(adjustments);

            Selector = new StatAdjustmentsSelector(mockAdjustmentXmlMapper.Object);
        }

        [Test]
        public void AdjustmentsContainAllStats()
        {
            var adjustments = Selector.GetAdjustmentsFor(race);

            foreach (var stat in StatConstants.GetStats())
                Assert.That(adjustments.Keys, Contains.Item(stat));
        }

        [Test]
        public void GetAdjustmentsFromMapper()
        {
            Selector.GetAdjustmentsFor(race);

            foreach (var stat in StatConstants.GetStats())
            {
                var tableName = String.Format("{0}StatAdjustments", stat);
                mockAdjustmentXmlMapper.Verify(p => p.Map(tableName), Times.Once);
            }
        }
    }
}