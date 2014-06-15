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
        private IStatAdjustmentsSelector provider;
        private Race race;
        private Mock<IAdjustmentMapper> mockAdjustmentXmlParser;
        private Dictionary<String, Int32> adjustments;

        [SetUp]
        public void Setup()
        {
            mockAdjustmentXmlParser = new Mock<IAdjustmentMapper>();
            race = new Race();
            race.BaseRace = "base race";
            race.Metarace = "metarace";
            adjustments = new Dictionary<String, Int32>();
            adjustments.Add(race.BaseRace, 0);
            adjustments.Add(race.Metarace, 0);
            mockAdjustmentXmlParser.Setup(p => p.Parse(It.IsAny<String>())).Returns(adjustments);

            provider = new StatAdjustmentsSelector(mockAdjustmentXmlParser.Object);
        }

        [Test]
        public void AdjustmentsContainAllStats()
        {
            var adjustments = provider.GetAdjustments(race);

            foreach (var stat in StatConstants.GetStats())
                Assert.That(adjustments.ContainsKey(stat), Is.True);
        }

        [Test]
        public void GetAdjustmentsFromParser()
        {
            provider.GetAdjustments(race);

            foreach (var stat in StatConstants.GetStats())
            {
                var filename = String.Format("{0}StatAdjustments.xml", stat);
                mockAdjustmentXmlParser.Verify(p => p.Parse(filename), Times.Once);
            }
        }
    }
}