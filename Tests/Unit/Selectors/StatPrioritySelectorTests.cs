using System;
using System.Collections.Generic;
using Moq;
using NPCGen.Common;
using NPCGen.Mappers.Interfaces;
using NPCGen.Selectors;
using NPCGen.Selectors.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Selectors
{
    [TestFixture]
    public class StatPrioritySelectorTests
    {
        private IStatPrioritySelector Selector;
        private Mock<IStatPriorityMapper> mockStatPriorityMapper;

        [SetUp]
        public void Setup()
        {
            var priorities = new Dictionary<String, StatPriority>();
            priorities.Add("class name", new StatPriority() { FirstPriority = "first priority", SecondPriority = "second priority" });
            mockStatPriorityMapper = new Mock<IStatPriorityMapper>();
            mockStatPriorityMapper.Setup(p => p.Parse(It.IsAny<String>())).Returns(priorities);

            Selector = new StatPrioritySelector(mockStatPriorityMapper.Object);
        }

        [Test]
        public void GetsStatPrioritiesFromMapper()
        {
            Selector.GetStatPriorities("class name");
            mockStatPriorityMapper.Verify(p => p.Parse("StatPriorities.xml"), Times.Once);
        }

        [Test]
        public void ReturnsStatPriortyObjectReleventToClass()
        {
            var parsedPriorities = Selector.GetStatPriorities("class name");
            Assert.That(parsedPriorities.FirstPriority, Is.EqualTo("first priority"));
            Assert.That(parsedPriorities.SecondPriority, Is.EqualTo("second priority"));
        }
    }
}