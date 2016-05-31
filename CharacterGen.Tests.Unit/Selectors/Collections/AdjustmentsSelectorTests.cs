using CharacterGen.Domain.Selectors.Collections;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Tests.Unit.Selectors.Collections
{
    [TestFixture]
    public class AdjustmentsSelectorTests
    {
        private const string TableName = "table name";

        private IAdjustmentsSelector adjustmentsSelector;
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private Dictionary<string, IEnumerable<string>> collections;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            adjustmentsSelector = new AdjustmentsSelector(mockCollectionsSelector.Object);
            collections = new Dictionary<string, IEnumerable<string>>();

            mockCollectionsSelector.Setup(m => m.SelectAllFrom(TableName)).Returns(collections);
        }

        [Test]
        public void SelectAdjustments()
        {
            collections["first"] = new[] { "9266" };
            collections["second"] = new[] { "42" };

            var adjustments = adjustmentsSelector.SelectFrom(TableName);
            Assert.That(adjustments["first"], Is.EqualTo(9266));
            Assert.That(adjustments["second"], Is.EqualTo(42));
        }

        [Test]
        public void ThrowExceptionIfAnyEmptyCollections()
        {
            collections["first"] = Enumerable.Empty<string>();
            Assert.That(() => adjustmentsSelector.SelectFrom(TableName), Throws.InstanceOf<InvalidOperationException>());
        }
    }
}