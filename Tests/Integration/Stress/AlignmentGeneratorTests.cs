using System.Linq;
using Ninject;
using NPCGen.Common.Alignments;
using NPCGen.Generators.Interfaces;
using NPCGen.Generators.Interfaces.Randomizers.Alignments;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress
{
    [TestFixture]
    public class AlignmentGeneratorTests : StressTests
    {
        [Inject]
        public IAlignmentGenerator AlignmentGenerator { get; set; }
        [Inject]
        public IAlignmentRandomizer AlignmentRandomizer { get; set; }

        [Test]
        public void AlignmentGeneratorReturnsAlignment()
        {
            while (TestShouldKeepRunning())
            {
                var alignment = AlignmentGenerator.CreateWith(AlignmentRandomizer);
                Assert.That(alignment, Is.Not.Null);
            }

            AssertIterations();
        }

        [Test]
        public void AlignmentGeneratorReturnsAlignmentWithGoodness()
        {
            var goodnesses = AlignmentConstants.GetGoodnesses();

            while (TestShouldKeepRunning())
            {
                var alignment = AlignmentGenerator.CreateWith(AlignmentRandomizer);
                Assert.That(goodnesses.Contains(alignment.Goodness), Is.True);
            }

            AssertIterations();
        }

        [Test]
        public void AlignmentGeneratorReturnsAlignmentWithLawfulness()
        {
            var lawfulnesses = AlignmentConstants.GetLawfulnesses();

            while (TestShouldKeepRunning())
            {
                var alignment = AlignmentGenerator.CreateWith(AlignmentRandomizer);
                Assert.That(lawfulnesses.Contains(alignment.Lawfulness), Is.True);
            }

            AssertIterations();
        }
    }
}