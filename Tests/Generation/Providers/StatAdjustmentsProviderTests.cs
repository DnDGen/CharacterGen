using System;
using System.Collections.Generic;
using Moq;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Data.Stats;
using NPCGen.Core.Generation.Providers;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Xml.Parsers.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Providers
{
    [TestFixture]
    public class StatAdjustmentsProviderTests
    {
        private IStatAdjustmentsProvider provider;
        private Race race;
        private Mock<IStatAdjustmentXmlParser> mockStatAdjustmentXmlParser;
        private Dictionary<String, Int32> adjustments;

        [SetUp]
        public void Setup()
        {
            mockStatAdjustmentXmlParser = new Mock<IStatAdjustmentXmlParser>();
            race = new Race();
            race.BaseRace = "base race";
            race.Metarace = "metarace";
            adjustments = new Dictionary<String, Int32>();
            adjustments.Add(race.BaseRace, 0);
            adjustments.Add(race.Metarace, 0);
            mockStatAdjustmentXmlParser.Setup(p => p.Parse(It.IsAny<String>())).Returns(adjustments);

            provider = new StatAdjustmentsProvider(mockStatAdjustmentXmlParser.Object);
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
                mockStatAdjustmentXmlParser.Verify(p => p.Parse(filename), Times.Once);
            }
        }
    }
}