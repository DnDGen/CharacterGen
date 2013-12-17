using System;
using System.Collections.Generic;
using Moq;
using NPCGen.Core.Generation.Providers;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Xml.Parsers.Interfaces;
using NPCGen.Core.Generation.Xml.Parsers.Objects;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generation.Providers
{
    [TestFixture]
    public class StatPriorityProviderTests
    {
        private IStatPriorityProvider provider;
        private Mock<IStatPriorityXmlParser> mockStatPriorityParser;

        [SetUp]
        public void Setup()
        {
            var priorities = new Dictionary<String, StatPriorityObject>();
            priorities.Add("class name", new StatPriorityObject() { FirstPriority = "first priority", SecondPriority = "second priority" });
            mockStatPriorityParser = new Mock<IStatPriorityXmlParser>();
            mockStatPriorityParser.Setup(p => p.Parse(It.IsAny<String>())).Returns(priorities);

            provider = new StatPriorityProvider(mockStatPriorityParser.Object);
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