using CharacterGen.Selectors;
using CharacterGen.Selectors.Domain;
using CharacterGen.Tables;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CharacterGen.Tests.Unit.Selectors
{
    [TestFixture]
    public class StatPrioritySelectorTests
    {
        private IStatPrioritySelector statPrioritySelector;
        private Mock<ICollectionsSelector> mockInnerSelector;
        private List<String> priorities;

        [SetUp]
        public void Setup()
        {
            priorities = new List<String>();
            mockInnerSelector = new Mock<ICollectionsSelector>();
            statPrioritySelector = new StatPrioritySelector(mockInnerSelector.Object);

            priorities.Add("first priority");
            priorities.Add("second priority");
            mockInnerSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.StatPriorities, "class name")).Returns(priorities);
        }

        [Test]
        public void GetStatPriortySelection()
        {
            var priority = statPrioritySelector.SelectFor("class name");
            Assert.That(priority.First, Is.EqualTo("first priority"));
            Assert.That(priority.Second, Is.EqualTo("second priority"));
        }
    }
}