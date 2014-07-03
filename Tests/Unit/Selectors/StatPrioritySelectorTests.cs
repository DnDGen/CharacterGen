using System;
using System.Collections.Generic;
using Moq;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Mappers.Interfaces;
using NPCGen.Selectors;
using NPCGen.Selectors.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Selectors
{
    [TestFixture]
    public class StatPrioritySelectorTests
    {
        private IStatPrioritySelector statPrioritySelector;
        private Mock<IStatPriorityMapper> mockStatPriorityMapper;

        [SetUp]
        public void Setup()
        {
            var priorities = new Dictionary<String, StatPriority>();
            mockStatPriorityMapper = new Mock<IStatPriorityMapper>();
            statPrioritySelector = new StatPrioritySelector(mockStatPriorityMapper.Object);

            priorities.Add("class name", new StatPriority { FirstPriority = "first priority", SecondPriority = "second priority" });
            mockStatPriorityMapper.Setup(p => p.Map(It.IsAny<String>())).Returns(priorities);
        }

        [Test]
        public void GetsStatPrioritiesFromMapper()
        {
            statPrioritySelector.GetStatPrioritiesFor("class name");
            mockStatPriorityMapper.Verify(p => p.Map("StatPriorities"), Times.Once);
        }

        [Test]
        public void ReturnsStatPriortyReleventToClass()
        {
            var parsedPriorities = statPrioritySelector.GetStatPrioritiesFor("class name");
            Assert.That(parsedPriorities.FirstPriority, Is.EqualTo("first priority"));
            Assert.That(parsedPriorities.SecondPriority, Is.EqualTo("second priority"));
        }
    }
}