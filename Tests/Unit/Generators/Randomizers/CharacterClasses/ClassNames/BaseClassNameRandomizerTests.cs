using System;
using System.Linq;
using Moq;
using NPCGen.Common.Alignments;
using NPCGen.Generators.Interfaces.Verifiers.Exceptions;
using NPCGen.Generators.Randomizers.CharacterClasses.ClassNames;
using NPCGen.Selectors.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.CharacterClasses.ClassNames
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
            mockPercentileResultSelector.Setup(p => p.GetAllResults(It.IsAny<String>())).Returns(new[] { firstClass, secondClass });
            mockPercentileResultSelector.Setup(p => p.GetPercentileFrom(It.IsAny<String>())).Returns(firstClass);

            randomizer = new TestClassRandomizer(mockPercentileResultSelector.Object);
        }

        [Test]
        public void RandomizeGetsAllPossibleResultsFromGetAllPossibleResults()
        {
            randomizer.Randomize(alignment);
            mockPercentileResultSelector.Verify(p => p.GetAllResults(It.IsAny<String>()), Times.Once);
        }

        [Test]
        public void RandomizeThrowsErrorIfNoPossibleResults()
        {
            mockPercentileResultSelector.Setup(p => p.GetAllResults(It.IsAny<String>())).Returns(Enumerable.Empty<String>());
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
            mockPercentileResultSelector.Verify(p => p.GetPercentileFrom(tableName), Times.Once);
        }

        [Test]
        public void RandomizeLoopsUntilAllowedClassNameIsRolled()
        {
            mockPercentileResultSelector.SetupSequence(p => p.GetPercentileFrom(It.IsAny<String>())).Returns("invalid class name")
                .Returns(firstClass);

            randomizer.Randomize(alignment);
            mockPercentileResultSelector.Verify(p => p.GetPercentileFrom(It.IsAny<String>()), Times.Exactly(2));
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
            mockPercentileResultSelector.Verify(p => p.GetAllResults(tableName), Times.Once);
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

            public TestClassRandomizer(IPercentileSelector percentileResultSelector)
                : base(percentileResultSelector) { }

            protected override Boolean CharacterClassIsAllowed(String className, Alignment alignment)
            {
                return className != NotAllowedClassName;
            }
        }
    }
}