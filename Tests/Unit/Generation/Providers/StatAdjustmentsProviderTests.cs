using System;
using System.Collections.Generic;
using Moq;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Data.Stats;
using NPCGen.Core.Generation.Providers;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Xml.Parsers.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generation.Providers
{
    [TestFixture]
    public class StatAdjustmentsProviderTests
    {
        private IStatAdjustmentsProvider provider;
        private Race race;
        private Mock<IAdjustmentXmlParser> mockAdjustmentXmlParser;
        private Dictionary<String, Int32> adjustments;

        [SetUp]
        public void Setup()
        {
            mockAdjustmentXmlParser = new Mock<IAdjustmentXmlParser>();
            race = new Race();
            race.BaseRace = "base race";
            race.Metarace = "metarace";
            adjustments = new Dictionary<String, Int32>();
            adjustments.Add(race.BaseRace, 0);
            adjustments.Add(race.Metarace, 0);
            mockAdjustmentXmlParser.Setup(p => p.Parse(It.IsAny<String>())).Returns(adjustments);

            provider = new StatAdjustmentsProvider(mockAdjustmentXmlParser.Object);
        }

        [Test]
        public void AdjustmentsContainAllStats()
        {
            var adjustments = provider.GetAdjustments(race);

            foreach(var stat in StatConstants.GetStats())
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