using CharacterGen.Common.Alignments;
using CharacterGen.Generators;
using CharacterGen.Generators.Domain.Randomizers.CharacterClasses.ClassNames;
using CharacterGen.Generators.Verifiers.Exceptions;
using CharacterGen.Selectors;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class BaseClassNameRandomizerTests
    {
        private TestClassRandomizer randomizer;
        private Mock<IPercentileSelector> mockPercentileResultSelector;
        private Alignment alignment;

        private String firstClass = "first class";
        private String secondClass = "second class";

        [SetUp]
        public void Setup()
        {
            alignment = new Alignment();
            alignment.Goodness = AlignmentConstants.Good;

            mockPercentileResultSelector = new Mock<IPercentileSelector>();
            var generator = new ConfigurableIterationGenerator(2);

            mockPercentileResultSelector.Setup(p => p.SelectAllFrom(It.IsAny<String>())).Returns(new[] { firstClass, secondClass });
            mockPercentileResultSelector.Setup(p => p.SelectFrom(It.IsAny<String>())).Returns(firstClass);

            randomizer = new TestClassRandomizer(mockPercentileResultSelector.Object, generator);
        }

        [Test]
        public void RandomizeGetsAllPossibleResultsFromGetAllPossibleResults()
        {
            randomizer.Randomize(alignment);
            mockPercentileResultSelector.Verify(p => p.SelectAllFrom(It.IsAny<String>()), Times.Once);
        }

        [Test]
        public void RandomizeThrowsErrorIfNoPossibleResults()
        {
            mockPercentileResultSelector.Setup(p => p.SelectAllFrom(It.IsAny<String>())).Returns(Enumerable.Empty<String>());
            Assert.That(() => randomizer.Randomize(alignment), Throws.InstanceOf<IncompatibleRandomizersException>());
        }

        [Test]
        public void RandomizeReturnCharacterClassFromPercentileResultSelector()
        {
            var result = randomizer.Randomize(alignment);
            Assert.That(result, Is.EqualTo(firstClass));
        }

        [Test]
        public void RandomizeAccessesTableAlignmentGoodnessClassNames()
        {
            randomizer.Randomize(alignment);

            var tableName = String.Format("{0}CharacterClasses", alignment.Goodness);
            mockPercentileResultSelector.Verify(p => p.SelectFrom(tableName), Times.Once);
        }

        [Test]
        public void RandomizeLoopsUntilAllowedClassNameIsRolled()
        {
            mockPercentileResultSelector.SetupSequence(p => p.SelectFrom(It.IsAny<String>())).Returns("invalid class name")
                .Returns(firstClass);

            randomizer.Randomize(alignment);
            mockPercentileResultSelector.Verify(p => p.SelectFrom(It.IsAny<String>()), Times.Exactly(2));
        }

        [Test]
        public void GetAllPossibleResultsGetsResultsFromSelector()
        {
            var classNames = randomizer.GetAllPossibleResults(alignment);

            Assert.That(classNames, Contains.Item(firstClass));
            Assert.That(classNames, Contains.Item(secondClass));
            Assert.That(classNames.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetAllPossibleResultsAccessesTableAlignmentGoodnessClassNames()
        {
            randomizer.GetAllPossibleResults(alignment);

            var tableName = String.Format("{0}CharacterClasses", alignment.Goodness);
            mockPercentileResultSelector.Verify(p => p.SelectAllFrom(tableName), Times.Once);
        }

        [Test]
        public void GetAllPossibleResultsFiltersOutUnallowedCharacterClasses()
        {
            randomizer.NotAllowedClassName = firstClass;
            var results = randomizer.GetAllPossibleResults(alignment);

            Assert.That(results, Contains.Item(secondClass));
            Assert.That(results.Count(), Is.EqualTo(1));
        }

        private class TestClassRandomizer : BaseClassNameRandomizer
        {
            public String NotAllowedClassName { get; set; }

            public TestClassRandomizer(IPercentileSelector percentileResultSelector, Generator generator)
                : base(percentileResultSelector, generator)
            { }

            protected override Boolean CharacterClassIsAllowed(String className, Alignment alignment)
            {
                return className != NotAllowedClassName;
            }
        }
    }
}