using System;
using System.Collections.Generic;
using Moq;
using NPCGen.Mappers.Interfaces;
using NPCGen.Selectors;
using NPCGen.Selectors.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Selectors
{
    [TestFixture]
    public class AdjustmentsSelectorTests
    {
        private IAdjustmentsSelector adjustmentsSelector;
        private Mock<IAdjustmentMapper> mockAdjustmentMapper;

        [SetUp]
        public void Setup()
        {
            mockAdjustmentMapper = new Mock<IAdjustmentMapper>();
            adjustmentsSelector = new AdjustmentsSelector(mockAdjustmentMapper.Object);
        }

        [Test]
        public void GetResultsFromMapper()
        {
            var adjustments = new Dictionary<String, Int32>();
            mockAdjustmentMapper.Setup(m => m.Map("table name")).Returns(adjustments);

            var selectedAdjustments = adjustmentsSelector.SelectAdjustmentsFrom("table name");
            Assert.That(selectedAdjustments, Is.EqualTo(adjustments));
        }
    }
}