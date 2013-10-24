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

            Assert.That(adjustments.ContainsKey(StatConstants.Strength), Is.True);
            Assert.That(adjustments.ContainsKey(StatConstants.Constitution), Is.True);
            Assert.That(adjustments.ContainsKey(StatConstants.Dexterity), Is.True);
            Assert.That(adjustments.ContainsKey(StatConstants.Intelligence), Is.True);
            Assert.That(adjustments.ContainsKey(StatConstants.Wisdom), Is.True);
            Assert.That(adjustments.ContainsKey(StatConstants.Charisma), Is.True);
        }

        [Test]
        public void GetAdjustmentsFromParser()
        {
            provider.GetAdjustments(race);

            mockStatAdjustmentXmlParser.Verify(p => p.Parse("StrengthStatAdjustments"), Times.Once);
            mockStatAdjustmentXmlParser.Verify(p => p.Parse("ConstitutionStatAdjustments"), Times.Once);
            mockStatAdjustmentXmlParser.Verify(p => p.Parse("DexterityStatAdjustments"), Times.Once);
            mockStatAdjustmentXmlParser.Verify(p => p.Parse("IntelligenceStatAdjustments"), Times.Once);
            mockStatAdjustmentXmlParser.Verify(p => p.Parse("WisdomStatAdjustments"), Times.Once);
            mockStatAdjustmentXmlParser.Verify(p => p.Parse("CharismaStatAdjustments"), Times.Once);
        }
    }
}