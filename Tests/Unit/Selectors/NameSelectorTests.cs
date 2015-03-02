using System;
using System.Collections.Generic;
using Moq;
using NPCGen.Selectors;
using NPCGen.Selectors.Interfaces;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Selectors
{
    [TestFixture]
    public class NameSelectorTests
    {
        private INameSelector nameSelector;
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private List<String> names;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            nameSelector = new NameSelector(mockCollectionsSelector.Object);
            names = new List<String>();

            names.Add("name");
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceNames, "id")).Returns(names);
        }

        [Test]
        public void SelectorSelectsName()
        {
            var name = nameSelector.Select("id");
            Assert.That(name, Is.EqualTo("name"));
        }

        [Test]
        public void SelectorSelectsOnlyFirstName()
        {
            names.Add("other name");
            Assert.That(() => nameSelector.Select("id"), Throws.Exception);
        }
    }
}