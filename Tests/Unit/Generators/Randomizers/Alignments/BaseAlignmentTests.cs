using CharacterGen.Common.Alignments;
using CharacterGen.Generators.Domain.Randomizers.Alignments;
using CharacterGen.Generators.Verifiers.Exceptions;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.Alignments
{
    [TestFixture]
    public class BaseAlignmentTests
    {
        private TestAlignmentRandomizer randomizer;
        private Mock<IPercentileSelector> mockPercentileResultSelector;

        [SetUp]
        public void Setup()
        {
            mockPercentileResultSelector = new Mock<IPercentileSelector>();
            mockPercentileResultSelector.Setup(p => p.SelectAllFrom(TableNameConstants.Set.Percentile.AlignmentLawfulness)).Returns(new[] { "other lawfulness", "lawfulness" });
            mockPercentileResultSelector.Setup(p => p.SelectAllFrom(TableNameConstants.Set.Percentile.AlignmentGoodness)).Returns(new[] { "other goodness", "goodness" });
            mockPercentileResultSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Percentile.AlignmentLawfulness)).Returns("lawfulness");
            mockPercentileResultSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Percentile.AlignmentGoodness)).Returns("goodness");

            randomizer = new TestAlignmentRandomizer(mockPercentileResultSelector.Object);
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
            public String NotAllowedLawfulness { get; set; }
            public String NotAllowedGoodness { get; set; }

            public TestAlignmentRandomizer(IPercentileSelector innerSelector)
                : base(innerSelector)
            { }

            protected override Boolean AlignmentIsAllowed(Alignment alignment)
            {
                return alignment.Lawfulness != NotAllowedLawfulness && alignment.Goodness != NotAllowedGoodness;
            }
        }
    }
}