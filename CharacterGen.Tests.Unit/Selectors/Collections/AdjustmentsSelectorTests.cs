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
        public void SelectAllAdjustments()
        {
            collections["first"] = new[] { "9266" };
            collections["second"] = new[] { "42" };

            var adjustments = adjustmentsSelector.SelectAllFrom(TableName);
            Assert.That(adjustments["first"], Is.EqualTo(9266));
            Assert.That(adjustments["second"], Is.EqualTo(42));
        }

        [Test]
        public void ThrowExceptionIfAnyEmptyCollections()
        {
            collections["first"] = Enumerable.Empty<string>();
            collections["second"] = new[] { "42" };

            Assert.That(() => adjustmentsSelector.SelectAllFrom(TableName), Throws.InstanceOf<InvalidOperationException>());
        }

        [Test]
        public void SelectAdjustment()
        {
            collections["first"] = new[] { "9266" };
            collections["second"] = new[] { "42" };

            var adjustment = adjustmentsSelector.SelectFrom(TableName, "second");
            Assert.That(adjustment, Is.EqualTo(42));
        }

        [Test]
        public void SelectAdjustmentThrowsExceptionIfAnyCollectionIsEmpty()
        {
            collections["first"] = Enumerable.Empty<string>();
            collections["second"] = new[] { "42" };

            Assert.That(() => adjustmentsSelector.SelectFrom(TableName, "second"), Throws.InstanceOf<InvalidOperationException>());
        }

        [Test]
        public void SelectAdjustmentThrowsExceptionIfNameNotThere()
        {
            collections["first"] = new[] { "9266" };

            Assert.That(() => adjustmentsSelector.SelectFrom(TableName, "second"), Throws.ArgumentException.With.Message.EqualTo("No adjustment in table name exists for second"));
        }
    }
}