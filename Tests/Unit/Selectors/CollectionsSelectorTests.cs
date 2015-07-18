using System;
using System.Collections.Generic;
using System.Linq;
using D20Dice;
using Moq;
using NPCGen.Mappers.Interfaces;
using NPCGen.Selectors;
using NPCGen.Selectors.Interfaces;
using NPCGen.Selectors.Interfaces.Objects;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Selectors
{
    [TestFixture]
    public class CollectionsSelectorTests
    {
        private const String TableName = "table name";

        private ICollectionsSelector selector;
        private Mock<ICollectionsMapper> mockMapper;
        private Mock<IDice> mockDice;
        private Dictionary<String, IEnumerable<String>> allCollections;

        [SetUp]
        public void Setup()
        {
            mockMapper = new Mock<ICollectionsMapper>();
            mockDice = new Mock<IDice>();
            selector = new CollectionsSelector(mockMapper.Object, mockDice.Object);
            allCollections = new Dictionary<String, IEnumerable<String>>();

            mockMapper.Setup(m => m.Map(TableName)).Returns(allCollections);
        }

        [Test]
        public void SelectCollection()
        {
            allCollections["entry"] = Enumerable.Empty<String>();
            var collection = selector.SelectFrom(TableName, "entry");
            Assert.That(collection, Is.EqualTo(allCollections["entry"]));
        }

        [Test]
        public void SelectAllCollections()
        {
            var collections = selector.SelectAllFrom(TableName);
            Assert.That(collections, Is.EqualTo(allCollections));
        }

        [Test]
        public void IfEntryNotPresentInTable_ThrowException()
        {
            Assert.That(() => selector.SelectFrom(TableName, "entry"), Throws.Exception.With.Message.EqualTo("entry is not a valid entry in the table table name"));
        }

        [Test]
        public void SelectRandomItemFromCollection()
        {
            var collection = new[] { "item 1", "item 2", "item 3" };
            mockDice.Setup(d => d.Roll(1).d(3)).Returns(2);

            var item = selector.SelectRandomFrom(collection);
            Assert.That(item, Is.EqualTo("item 2"));
        }

        [Test]
        public void SelectRandomItemFromTable()
        {
            allCollections["entry"] = new[] { "item 1", "item 2", "item 3" };
            mockDice.Setup(d => d.Roll(1).d(3)).Returns(2);

            var item = selector.SelectRandomFrom(TableName, "entry");
            Assert.That(item, Is.EqualTo("item 2"));
        }

        [Test]
        public void CannotSelectRandomFromEmptyCollection()
        {
            var collection = Enumerable.Empty<String>();
            Assert.That(() => selector.SelectRandomFrom(collection), Throws.ArgumentException.With.Message.EqualTo("Cannot select random from an empty collection"));
        }

        [Test]
        public void CannotSelectRandomFromEmptyTable()
        {
            allCollections["entry"] = Enumerable.Empty<String>();
            Assert.That(() => selector.SelectRandomFrom(TableName, "entry"), Throws.ArgumentException.With.Message.EqualTo("Cannot select random from an empty collection"));
        }

        [Test]
        public void CannotSelectRandomFromInvalidEntry()
        {
            Assert.That(() => selector.SelectRandomFrom(TableName, "entry"), Throws.Exception.With.Message.EqualTo("entry is not a valid entry in the table table name"));
        }

        [Test]
        public void SelectRandomFromNonStringCollection()
        {
            var collection = new[] 
            { 
                new AdditionalFeatSelection { Feat = "feat 1" },
                new AdditionalFeatSelection { Feat = "feat 2" }
            };

            mockDice.Setup(d => d.Roll(1).d(2)).Returns(2);

            var item = selector.SelectRandomFrom<AdditionalFeatSelection>(collection);
            Assert.That(item, Is.InstanceOf<AdditionalFeatSelection>());
            Assert.That(item.Feat, Is.EqualTo("feat 2"));
        }
    }
}