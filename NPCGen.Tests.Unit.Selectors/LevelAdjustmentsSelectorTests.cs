using Moq;
using NPCGen.Mappers.Interfaces;
using NPCGen.Selectors;
using NPCGen.Selectors.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Selectors
{
    [TestFixture]
    public class LevelAdjustmentsSelectorTests
    {
        private ILevelAdjustmentsSelector provider;
        private Mock<IAdjustmentMapper> mockLevelAdjustmentXmlParser;

        [SetUp]
        public void Setup()
        {
            mockLevelAdjustmentXmlParser = new Mock<IAdjustmentMapper>();
            provider = new LevelAdjustmentsSelector(mockLevelAdjustmentXmlParser.Object);
        }

        [Test]
        public void GetResultsFromXmlParser()
        {
            provider.GetLevelAdjustments();
            mockLevelAdjustmentXmlParser.Verify(p => p.Parse("LevelAdjustments.xml"), Times.Once);
        }
    }
}