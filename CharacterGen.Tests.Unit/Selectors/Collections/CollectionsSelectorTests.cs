using CharacterGen.Domain.Mappers.Collections;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Selections;
using Moq;
using NUnit.Framework;
using RollGen;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Tests.Unit.Selectors.Collections
{
    [TestFixture]
    public class CollectionsSelectorTests
    {
        private const string TableName = "table name";

        private ICollectionsSelector selector;
        private Mock<CollectionsMapper> mockMapper;
        private Mock<Dice> mockDice;
        private Dictionary<string, IEnumerable<string>> allCollections;

        [SetUp]
        public void Setup()
        {
            mockMapper = new Mock<CollectionsMapper>();
            mockDice = new Mock<Dice>();
            selector = new CollectionsSelector(mockMapper.Object, mockDice.Object);
            allCollections = new Dictionary<string, IEnumerable<string>>();

            mockMapper.Setup(m => m.Map(TableName)).Returns(allCollections);
        }

        [Test]
        public void SelectCollection()
        {
            allCollections["entry"] = Enumerable.Empty<string>();
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
            mockDice.Setup(d => d.Roll(1).d(3).AsSum()).Returns(2);

            var item = selector.SelectRandomFrom(collection);
            Assert.That(item, Is.EqualTo("item 2"));
        }

        [Test]
        public void SelectRandomItemFromTable()
        {
            allCollections["entry"] = new[] { "item 1", "item 2", "item 3" };
            mockDice.Setup(d => d.Roll(1).d(3).AsSum()).Returns(2);

            var item = selector.SelectRandomFrom(TableName, "entry");
            Assert.That(item, Is.EqualTo("item 2"));
        }

        [Test]
        public void CannotSelectRandomFromEmptyCollection()
        {
            var collection = Enumerable.Empty<string>();
            Assert.That(() => selector.SelectRandomFrom(collection), Throws.ArgumentException.With.Message.EqualTo("Cannot select random from an empty collection"));
        }

        [Test]
        public void CannotSelectRandomFromEmptyTable()
        {
            allCollections["entry"] = Enumerable.Empty<string>();
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

            mockDice.Setup(d => d.Roll(1).d(2).AsSum()).Returns(2);

            var item = selector.SelectRandomFrom(collection);
            Assert.That(item, Is.InstanceOf<AdditionalFeatSelection>());
            Assert.That(item.Feat, Is.EqualTo("feat 2"));
        }

        [Test]
        public void FindGroupOfItem()
        {
            allCollections["entry"] = new[] { "first", "second" };
            allCollections["other entry"] = new[] { "third", "fourth" };
            allCollections["wrong entry"] = new[] { "third", "fourth" };

            var group = selector.FindGroupOf(TableName, "fourth", "entry", "other entry");
            Assert.That(group, Is.EqualTo("other entry"));
        }

        [Test]
        public void FindGroupOfItemThrowsExceptionIfNotInFilteredGroup()
        {
            allCollections["entry"] = new[] { "first", "second" };
            allCollections["other entry"] = new[] { "third", "fifth" };
            allCollections["wrong entry"] = new[] { "third", "fourth" };

            Assert.That(() => selector.FindGroupOf(TableName, "fourth", "entry", "other entry"), Throws.ArgumentException.With.Message.EqualTo("No filtered group from [entry, other entry] in table name is listed for fourth"));
        }
    }
}