using Moq;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.ClassNames;
using NUnit.Framework;
using System;
using System.Linq;

namespace NPCGen.Tests.Generation.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class BaseClassNameRandomizerTests
    {
        private TestClassRandomizer randomizer;
        private Mock<IPercentileResultProvider> mockPercentileResultProvider;
        private Alignment alignment;

        [SetUp]
        public void Setup()
        {
            alignment = new Alignment();
            alignment.Goodness = AlignmentConstants.Good;

            mockPercentileResultProvider = new Mock<IPercentileResultProvider>();
            randomizer = new TestClassRandomizer(mockPercentileResultProvider.Object);
            randomizer.ClassIsAllowed = true;
            randomizer.SwitchAfterFirst = false;
        }

        [Test]
        public void LoopUntilCharacterClassIsAllowed()
        {
            randomizer.ClassIsAllowed = false;
            randomizer.SwitchAfterFirst = true;

            randomizer.Randomize(alignment);
            mockPercentileResultProvider.Verify(p => p.GetPercentileResult(It.IsAny<String>()), Times.Exactly(2));
        }

        [Test]
        public void ReturnCharacterClassFromPercentileResultProvider()
        {
            mockPercentileResultProvider.Setup(p => p.GetPercentileResult(It.IsAny<String>())).Returns("result");

            var result = randomizer.Randomize(alignment);
            Assert.That(result, Is.EqualTo("result"));
        }

        [Test]
        public void AccessesTableAlignmentGoodnessClassNames()
        {
            var result = randomizer.Randomize(alignment);
            var tableName = String.Format("{0}CharacterClasses", alignment.GetGoodnessString());
            mockPercentileResultProvider.Verify(p => p.GetPercentileResult(tableName), Times.Once());
        }

        [Test]
        public void ReturnsAllUnfilteredResults()
        {
            var results = new[] { "item 1", "item 2", "item 3", "item 4", "item 5" };
            mockPercentileResultProvider.Setup(p => p.GetAllResults(It.IsAny<String>())).Returns(results);

            var classNames = randomizer.GetAllPossibleResults(alignment);

            foreach (var className in classNames)
                Assert.That(results.Contains(className), Is.True);

            Assert.That(classNames.Count(), Is.EqualTo(results.Count()));
        }

        private class TestClassRandomizer : BaseClassNameRandomizer
        {
            public Boolean ClassIsAllowed { get; set; }
            public Boolean SwitchAfterFirst { get; set; }

            private Boolean first;

            public TestClassRandomizer(IPercentileResultProvider percentileResultProvider)
                : base(percentileResultProvider)
            {
                first = true;
            }

            protected override Boolean CharacterClassIsAllowed(String className, Alignment alignment)
            {
                var toReturn = ClassIsAllowed;

                if (first && SwitchAfterFirst)
                    ClassIsAllowed = !ClassIsAllowed;

                first = false;

                return toReturn;
            }
        }
    }
}