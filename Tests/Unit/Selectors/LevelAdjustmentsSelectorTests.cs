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
        private ILevelAdjustmentsSelector levelAdjustmentsSelector;
        private Mock<IAdjustmentMapper> mockLevelAdjustmentMapper;

        [SetUp]
        public void Setup()
        {
            mockLevelAdjustmentMapper = new Mock<IAdjustmentMapper>();
            levelAdjustmentsSelector = new LevelAdjustmentsSelector(mockLevelAdjustmentMapper.Object);
        }

        [Test]
        public void GetResultsFromXmlMapper()
        {
            levelAdjustmentsSelector.GetLevelAdjustments();
            mockLevelAdjustmentMapper.Verify(p => p.Map("LevelAdjustments"), Times.Once);
        }
    }
}