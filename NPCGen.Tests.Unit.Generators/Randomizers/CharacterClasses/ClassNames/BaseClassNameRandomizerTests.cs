using System;
using System.Linq;
using Moq;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generators.Providers.Interfaces;
using NPCGen.Core.Generators.Randomizers.CharacterClasses.ClassNames;
using NPCGen.Core.Generators.Verifiers.Exceptions;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class BaseClassNameRandomizerTests
    {
        private TestClassRandomizer randomizer;
        private Mock<IPercentileResultProvider> mockPercentileResultProvider;
        private Alignment alignment;

        private String firstClass = "first class";
        private String secondClass = "second class";

        [SetUp]
        public void Setup()
        {
            alignment = new Alignment();
            alignment.Goodness = AlignmentConstants.Good;

            mockPercentileResultProvider = new Mock<IPercentileResultProvider>();
            mockPercentileResultProvider.Setup(p => p.GetAllResults(It.IsAny<String>())).Returns(new[] { firstClass, secondClass });
            mockPercentileResultProvider.Setup(p => p.GetPercentileResult(It.IsAny<String>())).Returns(firstClass);

            randomizer = new TestClassRandomizer(mockPercentileResultProvider.Object);
        }

        [Test]
        public void RandomizeGetsAllPossibleResultsFromGetAllPossibleResults()
        {
            randomizer.Randomize(alignment);
            mockPercentileResultProvider.Verify(p => p.GetAllResults(It.IsAny<String>()), Times.Once);
        }

        [Test, ExpectedException(typeof(IncompatibleRandomizersException))]
        public void RandomizeThrowsErrorIfNoPossibleResults()
        {
            mockPercentileResultProvider.Setup(p => p.GetAllResults(It.IsAny<String>())).Returns(Enumerable.Empty<String>());
            randomizer.Randomize(alignment);
        }

        [Test]
        public void RandomizeReturnCharacterClassFromPercentileResultProvider()
        {
            var result = randomizer.Randomize(alignment);
            Assert.That(result, Is.EqualTo(firstClass));
        }

        [Test]
        public void RandomizeAccessesTableAlignmentGoodnessClassNames()
        {
            randomizer.Randomize(alignment);

            var tableName = String.Format("{0}CharacterClasses", alignment.Goodness);
            mockPercentileResultProvider.Verify(p => p.GetPercentileResult(tableName), Times.Once);
        }

        [Test]
        public void RandomizeLoopsUntilAllowedClassNameIsRolled()
        {
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>())).Returns("invalid class name")
                .Returns(firstClass);

            randomizer.Randomize(alignment);
            mockPercentileResultProvider.Verify(p => p.GetPercentileResult(It.IsAny<String>()), Times.Exactly(2));
        }

        [Test]
        public void GetAllPossibleResultsGetsResultsFromProvider()
        {
            var classNames = randomizer.GetAllPossibleResults(alignment);

            Assert.That(classNames.Contains(firstClass), Is.True);
            Assert.That(classNames.Contains(secondClass), Is.True);
            Assert.That(classNames.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetAllPossibleResultsAccessesTableAlignmentGoodnessClassNames()
        {
            randomizer.GetAllPossibleResults(alignment);

            var tableName = String.Format("{0}CharacterClasses", alignment.Goodness);
            mockPercentileResultProvider.Verify(p => p.GetAllResults(tableName), Times.Once);
        }

        [Test]
        public void GetAllPossibleResultsFiltersOutUnallowedCharacterClasses()
        {
            randomizer.NotAllowedClassName = firstClass;
            var results = randomizer.GetAllPossibleResults(alignment);

            Assert.That(results.Contains(secondClass), Is.True);
            Assert.That(results.Count(), Is.EqualTo(1));
        }

        private class TestClassRandomizer : BaseClassNameRandomizer
        {
            public String NotAllowedClassName { get; set; }

            public TestClassRandomizer(IPercentileResultProvider percentileResultProvider)
                : base(percentileResultProvider) { }

            protected override Boolean CharacterClassIsAllowed(String className, Alignment alignment)
            {
                return className != NotAllowedClassName;
            }
        }
    }
}