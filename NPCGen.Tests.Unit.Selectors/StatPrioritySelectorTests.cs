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
        private IStatPrioritySelector provider;
        private Mock<IStatPriorityMapper> mockStatPriorityParser;

        [SetUp]
        public void Setup()
        {
            var priorities = new Dictionary<String, StatPriority>();
            priorities.Add("class name", new StatPriority() { FirstPriority = "first priority", SecondPriority = "second priority" });
            mockStatPriorityParser = new Mock<IStatPriorityMapper>();
            mockStatPriorityParser.Setup(p => p.Parse(It.IsAny<String>())).Returns(priorities);

            provider = new StatPrioritySelector(mockStatPriorityParser.Object);
        }

        [Test]
        public void GetsStatPrioritiesFromParser()
        {
            provider.GetStatPriorities("class name");
            mockStatPriorityParser.Verify(p => p.Parse("StatPriorities.xml"), Times.Once);
        }

        [Test]
        public void ReturnsStatPriortyObjectReleventToClass()
        {
            var parsedPriorities = provider.GetStatPriorities("class name");
            Assert.That(parsedPriorities.FirstPriority, Is.EqualTo("first priority"));
            Assert.That(parsedPriorities.SecondPriority, Is.EqualTo("second priority"));
        }
    }
}