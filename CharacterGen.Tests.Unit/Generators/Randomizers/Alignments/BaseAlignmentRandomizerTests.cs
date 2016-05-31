using CharacterGen.Alignments;
using CharacterGen.Domain.Generators;
using CharacterGen.Domain.Generators.Randomizers.Alignments;
using CharacterGen.Domain.Selectors.Percentiles;
using CharacterGen.Domain.Tables;
using CharacterGen.Verifiers.Exceptions;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.Alignments
{
    [TestFixture]
    public class BaseAlignmentRandomizerTests
    {
        private TestAlignmentRandomizer randomizer;
        private Mock<IPercentileSelector> mockPercentileResultSelector;

        [SetUp]
        public void Setup()
        {
            mockPercentileResultSelector = new Mock<IPercentileSelector>();
            var generator = new ConfigurableIterationGenerator(2);

            mockPercentileResultSelector.Setup(p => p.SelectAllFrom(TableNameConstants.Set.Percentile.AlignmentLawfulness)).Returns(new[] { "other lawfulness", "lawfulness" });
            mockPercentileResultSelector.Setup(p => p.SelectAllFrom(TableNameConstants.Set.Percentile.AlignmentGoodness)).Returns(new[] { "other goodness", "goodness" });
            mockPercentileResultSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Percentile.AlignmentLawfulness)).Returns("lawfulness");
            mockPercentileResultSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Percentile.AlignmentGoodness)).Returns("goodness");

            randomizer = new TestAlignmentRandomizer(mockPercentileResultSelector.Object, generator);
        }

        [Test]
        public void ReturnLawfulnessFromSelector()
        {
            var alignment = randomizer.Randomize();
            Assert.That(alignment.Lawfulness, Is.EqualTo("lawfulness"));
        }

        [Test]
        public void ReturnsGoodnessFromSelector()
        {
            var alignment = randomizer.Randomize();
            Assert.That(alignment.Goodness, Is.EqualTo("goodness"));
        }

        [Test]
        public void RandomizeThrowsErrorIfNoLawfulnessPossibleResults()
        {
            mockPercentileResultSelector.Setup(p => p.SelectAllFrom(TableNameConstants.Set.Percentile.AlignmentLawfulness)).Returns(Enumerable.Empty<String>());
            Assert.That(() => randomizer.Randomize(), Throws.InstanceOf<IncompatibleRandomizersException>());
        }

        [Test]
        public void RandomizeThrowsErrorIfNoGoodnessPossibleResults()
        {
            mockPercentileResultSelector.Setup(p => p.SelectAllFrom(TableNameConstants.Set.Percentile.AlignmentGoodness)).Returns(Enumerable.Empty<String>());
            Assert.That(() => randomizer.Randomize(), Throws.InstanceOf<IncompatibleRandomizersException>());
        }

        [Test]
        public void RandomizeLoopsUntilAlignmentWithAllowedGoodnessIsRolled()
        {
            randomizer.NotAllowedGoodness = "goodness";

            mockPercentileResultSelector.SetupSequence(p => p.SelectFrom(TableNameConstants.Set.Percentile.AlignmentGoodness))
                .Returns("goodness").Returns("other goodness");

            var alignment = randomizer.Randomize();
            Assert.That(alignment.Goodness, Is.EqualTo("other goodness"));
        }

        [Test]
        public void RandomizeLoopsUntilAlignmentWithAllowedLawfulnessIsRolled()
        {
            randomizer.NotAllowedLawfulness = "lawfulness";

            mockPercentileResultSelector.SetupSequence(p => p.SelectFrom(TableNameConstants.Set.Percentile.AlignmentLawfulness))
                .Returns("lawfulness").Returns("other lawfulness");

            var alignment = randomizer.Randomize();
            Assert.That(alignment.Lawfulness, Is.EqualTo("other lawfulness"));
        }

        [Test]
        public void GetAllPossibleResults()
        {
            var allAlignments = randomizer.GetAllPossibleResults();
            var alignmentStrings = allAlignments.Select(a => a.ToString());

            Assert.That(alignmentStrings, Contains.Item("lawfulness goodness"));
            Assert.That(alignmentStrings, Contains.Item("lawfulness other goodness"));
            Assert.That(alignmentStrings, Contains.Item("other lawfulness goodness"));
            Assert.That(alignmentStrings, Contains.Item("other lawfulness other goodness"));
            Assert.That(alignmentStrings.Count(), Is.EqualTo(4));
        }

        [Test]
        public void GetAllPossibleResultsFiltersOutUnallowedCharacterClasses()
        {
            randomizer.NotAllowedLawfulness = "lawfulness";
            randomizer.NotAllowedGoodness = "goodness";

            var results = randomizer.GetAllPossibleResults();
            Assert.That(results.Count(), Is.EqualTo(1));
            Assert.That(results.First().ToString(), Is.EqualTo("other lawfulness other goodness"));
        }

        private class TestAlignmentRandomizer : BaseAlignmentRandomizer
        {
            public string NotAllowedLawfulness { get; set; }
            public string NotAllowedGoodness { get; set; }

            public TestAlignmentRandomizer(IPercentileSelector innerSelector, Generator generator)
                : base(innerSelector, generator)
            { }

            protected override bool AlignmentIsAllowed(Alignment alignment)
            {
                return alignment.Lawfulness != NotAllowedLawfulness && alignment.Goodness != NotAllowedGoodness;
            }
        }
    }
}