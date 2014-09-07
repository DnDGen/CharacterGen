using System;
using System.Collections.Generic;
using Moq;
using NPCGen.Common.Alignments;
using NPCGen.Generators.Interfaces.Randomizers.CharacterClasses;
using NPCGen.Selectors.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public abstract class ClassNameRandomizerTests
    {
        protected const String ClassName = "class name";
        protected const String OtherClassName = "other class name";

        protected abstract IEnumerable<String> classNamesInGroup { get; }
        protected abstract String classNameGroup { get; }

        protected Mock<ICollectionsSelector> mockCollectionsSelector;
        protected Mock<IPercentileSelector> mockPercentileResultSelector;
        protected IClassNameRandomizer randomizer;
        protected Alignment alignment;
        protected List<String> alignmentClasses;

        [SetUp]
        public void ClassNameRandomizerTestsSetup()
        {
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            mockPercentileResultSelector = new Mock<IPercentileSelector>();
            alignment = new Alignment();
            alignmentClasses = new List<String>();

            alignment.Goodness = "goodness";
            alignment.Lawfulness = "lawfulness";
            mockCollectionsSelector.Setup(s => s.SelectFrom("ClassNameGroups", classNameGroup)).Returns(classNamesInGroup);
            mockPercentileResultSelector.Setup(s => s.SelectAllFrom(It.IsAny<String>())).Returns(new[] { ClassName, OtherClassName });
            mockCollectionsSelector.Setup(s => s.SelectFrom("ClassNameGroups", alignment.ToString())).Returns(alignmentClasses);
        }
    }
}