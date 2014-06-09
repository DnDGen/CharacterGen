using Moq;
using NPCGen.Core.Selectors;
using NPCGen.Core.Selectors.Interfaces;
using NPCGen.Core.Generation.Xml.Parsers.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Selectors
{
    [TestFixture]
    public class LevelAdjustmentsProviderTests
    {
        private ILevelAdjustmentsProvider provider;
        private Mock<IAdjustmentXmlParser> mockLevelAdjustmentXmlParser;

        [SetUp]
        public void Setup()
        {
            mockLevelAdjustmentXmlParser = new Mock<IAdjustmentXmlParser>();
            provider = new LevelAdjustmentsProvider(mockLevelAdjustmentXmlParser.Object);
        }

        [Test]
        public void GetResultsFromXmlParser()
        {
            provider.GetLevelAdjustments();
            mockLevelAdjustmentXmlParser.Verify(p => p.Parse("LevelAdjustments.xml"), Times.Once);
        }
    }
}