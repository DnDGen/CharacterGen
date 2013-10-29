using Moq;
using NPCGen.Core.Generation.Providers;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Xml.Parsers.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Providers
{
    [TestFixture]
    public class LevelAdjustmentsProviderTests
    {
        private ILevelAdjustmentsProvider provider;
        private Mock<ILevelAdjustmentXmlParser> mockLevelAdjustmentXmlParser;

        [SetUp]
        public void Setup()
        {
            mockLevelAdjustmentXmlParser = new Mock<ILevelAdjustmentXmlParser>();
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