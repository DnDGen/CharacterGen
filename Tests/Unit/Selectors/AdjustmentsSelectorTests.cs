using System;
using System.Collections.Generic;
using System.Linq;
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
        private Mock<ICollectionsMapper> mockCollectionsMapper;
        private Dictionary<String, IEnumerable<String>> collections;

        [SetUp]
        public void Setup()
        {
            mockCollectionsMapper = new Mock<ICollectionsMapper>();
            adjustmentsSelector = new AdjustmentsSelector(mockCollectionsMapper.Object);
            collections = new Dictionary<String, IEnumerable<String>>();

            mockCollectionsMapper.Setup(m => m.Map("table name")).Returns(collections);
        }

        [Test]
        public void SelectAdjustments()
        {
            collections["first"] = new[] { "9266" };
            collections["second"] = new[] { "42" };

            var adjustments = adjustmentsSelector.SelectFrom("table name");
            Assert.That(adjustments["first"], Is.EqualTo(9266));
            Assert.That(adjustments["second"], Is.EqualTo(42));
        }

        [Test]
        public void ThrowExceptionIfAnyEmptyCollections()
        {
            collections["first"] = Enumerable.Empty<String>();
            Assert.That(() => adjustmentsSelector.SelectFrom("table name"), Throws.Exception);
        }
    }
}