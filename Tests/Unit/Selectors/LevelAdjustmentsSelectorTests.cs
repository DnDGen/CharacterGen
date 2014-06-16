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
        private ILevelAdjustmentsSelector Selector;
        private Mock<IAdjustmentMapper> mockLevelAdjustmentXmlMapper;

        [SetUp]
        public void Setup()
        {
            mockLevelAdjustmentXmlMapper = new Mock<IAdjustmentMapper>();
            Selector = new LevelAdjustmentsSelector(mockLevelAdjustmentXmlMapper.Object);
        }

        [Test]
        public void GetResultsFromXmlMapper()
        {
            Selector.GetLevelAdjustments();
            mockLevelAdjustmentXmlMapper.Verify(p => p.Parse("LevelAdjustments.xml"), Times.Once);
        }
    }
}