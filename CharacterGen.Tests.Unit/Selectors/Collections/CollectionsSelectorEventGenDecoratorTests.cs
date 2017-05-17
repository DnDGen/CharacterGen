using CharacterGen.Domain.Selectors.Collections;
using EventGen;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace CharacterGen.Tests.Unit.Selectors.Collections
{
    [TestFixture]
    public class CollectionsSelectorEventGenDecoratorTests
    {
        private ICollectionsSelector decorator;
        private Mock<ICollectionsSelector> mockInnerSelector;
        private Mock<GenEventQueue> mockEventQueue;

        [SetUp]
        public void Setup()
        {
            mockInnerSelector = new Mock<ICollectionsSelector>();
            mockEventQueue = new Mock<GenEventQueue>();
            decorator = new CollectionsSelectorEventGenDecorator(mockInnerSelector.Object, mockEventQueue.Object);
        }

        [Test]
        public void ReturnInnerGroupName()
        {
            mockInnerSelector.Setup(s => s.FindGroupOf("table name", "item", "group 1", "group 2")).Returns("inner group");

            var group = decorator.FindGroupOf("table name", "item", "group 1", "group 2");
            Assert.That(group, Is.EqualTo("inner group"));
        }

        [Test]
        public void LogEventsForGroupNameSelection()
        {
            mockInnerSelector.Setup(s => s.FindGroupOf("table name", "item", "group 1", "group 2")).Returns("inner group");

            var group = decorator.FindGroupOf("table name", "item", "group 1", "group 2");
            Assert.That(group, Is.EqualTo("inner group"));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Beginning selection of group for item in table name from [group 1, group 2]"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Completed selection of inner group"), Times.Once);
        }

        [Test]
        public void ReturnInnerAll()
        {
            var allCollections = new Dictionary<string, IEnumerable<string>>();
            allCollections["group 1"] = new[] { "item 1", "item 2" };
            allCollections["group 2"] = new[] { "item 3", "item 4" };

            mockInnerSelector.Setup(s => s.SelectAllFrom("table name")).Returns(allCollections);

            var collections = decorator.SelectAllFrom("table name");
            Assert.That(collections, Is.EqualTo(allCollections));
        }

        [Test]
        public void LogEventsForAllSelection()
        {
            var allCollections = new Dictionary<string, IEnumerable<string>>();
            allCollections["group 1"] = new[] { "item 1", "item 2" };
            allCollections["group 2"] = new[] { "item 3", "item 4" };

            mockInnerSelector.Setup(s => s.SelectAllFrom("table name")).Returns(allCollections);

            var collections = decorator.SelectAllFrom("table name");
            Assert.That(collections, Is.EqualTo(allCollections));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Beginning selection of all in table name"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Completed selection of all in table name"), Times.Once);
        }

        [Test]
        public void ReturnInnerGroup()
        {
            var innerGroup = new[] { "item 3", "item 4" };
            mockInnerSelector.Setup(s => s.SelectFrom("table name", "item")).Returns(innerGroup);

            var group = decorator.SelectFrom("table name", "item");
            Assert.That(group, Is.EqualTo(innerGroup));
        }

        [Test]
        public void LogEventsForGroupSelection()
        {
            var innerGroup = new[] { "item 3", "item 4" };
            mockInnerSelector.Setup(s => s.SelectFrom("table name", "item")).Returns(innerGroup);

            var group = decorator.SelectFrom("table name", "item");
            Assert.That(group, Is.EqualTo(innerGroup));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Beginning selection of item in table name"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Completed selection of [item 3, item 4]"), Times.Once);
        }

        [Test]
        public void ReturnInnerRandomString()
        {
            mockInnerSelector.Setup(s => s.SelectRandomFrom("table name", "item")).Returns("random item");

            var randomItem = decorator.SelectRandomFrom("table name", "item");
            Assert.That(randomItem, Is.EqualTo("random item"));
        }

        [Test]
        public void LogEventsForRandomStringSelection()
        {
            mockInnerSelector.Setup(s => s.SelectRandomFrom("table name", "item")).Returns("random item");

            var randomItem = decorator.SelectRandomFrom("table name", "item");
            Assert.That(randomItem, Is.EqualTo("random item"));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Beginning random selection from item in table name"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Completed random selection of random item"), Times.Once);
        }

        [Test]
        public void ReturnInnerRandomItem()
        {
            var collection = new[] { 9266, 90210, 42 };
            mockInnerSelector.Setup(s => s.SelectRandomFrom(collection)).Returns(600);

            var randomItem = decorator.SelectRandomFrom(collection);
            Assert.That(randomItem, Is.EqualTo(600));
        }

        [Test]
        public void LogEventsForRandomItemSelection()
        {
            var collection = new[] { 9266, 90210, 42 };
            mockInnerSelector.Setup(s => s.SelectRandomFrom(collection)).Returns(600);

            var randomItem = decorator.SelectRandomFrom(collection);
            Assert.That(randomItem, Is.EqualTo(600));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Beginning random selection from [9266, 90210, 42]"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Completed random selection of 600"), Times.Once);
        }
    }
}