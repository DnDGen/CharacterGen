using CharacterGen.Alignments;
using CharacterGen.Domain.Generators;
using CharacterGen.Domain.Generators.Randomizers.CharacterClasses.ClassNames;
using CharacterGen.Domain.Selectors.Percentiles;
using CharacterGen.Verifiers.Exceptions;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class BaseClassNameRandomizerTests
    {
        private TestClassRandomizer randomizer;
        private Mock<IPercentileSelector> mockPercentileResultSelector;
        private Alignment alignment;

        private string firstClass = "first class";
        private string secondClass = "second class";

        [SetUp]
        public void Setup()
        {
            alignment = new Alignment();
            alignment.Goodness = AlignmentConstants.Good;

            mockPercentileResultSelector = new Mock<IPercentileSelector>();
            var generator = new ConfigurableIterationGenerator(2);

            mockPercentileResultSelector.Setup(p => p.SelectAllFrom(It.IsAny<string>())).Returns(new[] { firstClass, secondClass });
            mockPercentileResultSelector.Setup(p => p.SelectFrom(It.IsAny<string>())).Returns(firstClass);

            randomizer = new TestClassRandomizer(mockPercentileResultSelector.Object, generator);
        }

        [Test]
        public void RandomizeGetsAllPossibleResultsFromGetAllPossibleResults()
        {
            randomizer.Randomize(alignment);
            mockPercentileResultSelector.Verify(p => p.SelectAllFrom(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void RandomizeThrowsErrorIfNoPossibleResults()
        {
            mockPercentileResultSelector.Setup(p => p.SelectAllFrom(It.IsAny<string>())).Returns(Enumerable.Empty<string>());
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

            var tableName = string.Format("{0}CharacterClasses", alignment.Goodness);
            mockPercentileResultSelector.Verify(p => p.SelectFrom(tableName), Times.Once);
        }

        [Test]
        public void RandomizeLoopsUntilAllowedClassNameIsRolled()
        {
            mockPercentileResultSelector.SetupSequence(p => p.SelectFrom(It.IsAny<string>())).Returns("invalid class name")
                .Returns(firstClass);

            randomizer.Randomize(alignment);
            mockPercentileResultSelector.Verify(p => p.SelectFrom(It.IsAny<string>()), Times.Exactly(2));
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

            var tableName = string.Format("{0}CharacterClasses", alignment.Goodness);
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
            public string NotAllowedClassName { get; set; }

            public TestClassRandomizer(IPercentileSelector percentileResultSelector, Generator generator)
                : base(percentileResultSelector, generator)
            { }

            protected override bool CharacterClassIsAllowed(string className, Alignment alignment)
            {
                return className != NotAllowedClassName;
            }
        }
    }
}