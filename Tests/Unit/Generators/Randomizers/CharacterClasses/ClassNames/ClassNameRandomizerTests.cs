using CharacterGen.Common.Alignments;
using CharacterGen.Generators;
using CharacterGen.Generators.Randomizers.CharacterClasses;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public abstract class ClassNameRandomizerTests
    {
        protected const string ClassName = "class name";
        protected const string AlignmentClassName = "alignment class name";
        protected const string GroupClassName = "group class name";

        protected abstract string classNameGroup { get; }

        protected Mock<ICollectionsSelector> mockCollectionsSelector;
        protected Mock<IPercentileSelector> mockPercentileResultSelector;
        protected Generator generator;
        protected IClassNameRandomizer randomizer;
        protected Alignment alignment;
        protected List<string> alignmentClasses;
        protected List<string> groupClasses;

        [SetUp]
        public void ClassNameRandomizerTestsSetup()
        {
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            mockPercentileResultSelector = new Mock<IPercentileSelector>();
            generator = new ConfigurableIterationGenerator();
            alignment = new Alignment();
            alignmentClasses = new List<string>();
            groupClasses = new List<string>();

            alignment.Goodness = "goodness";
            alignment.Lawfulness = "lawfulness";
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, classNameGroup)).Returns(groupClasses);
            mockPercentileResultSelector.Setup(s => s.SelectAllFrom(It.IsAny<string>())).Returns(new[] { ClassName, AlignmentClassName, GroupClassName });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, alignment.ToString())).Returns(alignmentClasses);
            alignmentClasses.Add(AlignmentClassName);
            groupClasses.Add(GroupClassName);
        }
    }
}