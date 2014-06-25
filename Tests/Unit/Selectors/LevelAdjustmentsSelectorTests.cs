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
        private Mock<IAdjustmentMapper> mockAdjustmentMapper;

        [SetUp]
        public void Setup()
        {
            mockAdjustmentMapper = new Mock<IAdjustmentMapper>();
            levelAdjustmentsSelector = new LevelAdjustmentsSelector(mockAdjustmentMapper.Object);
        }

        [Test]
        public void GetResultsFromMapper()
        {
            levelAdjustmentsSelector.GetAdjustments();
            mockAdjustmentMapper.Verify(p => p.Map("LevelAdjustments"), Times.Once);
        }
    }
}