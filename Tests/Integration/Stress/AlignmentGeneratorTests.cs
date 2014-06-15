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
        public IAlignmentGenerator AlignmentFactory { get; set; }
        [Inject]
        public IAlignmentRandomizer AlignmentRandomizer { get; set; }

        [Test]
        public void AlignmentFactoryReturnsAlignment()
        {
            while (TestShouldKeepRunning())
            {
                var alignment = AlignmentFactory.CreateWith(AlignmentRandomizer);
                Assert.That(alignment, Is.Not.Null);
            }

            AssertIterations();
        }

        [Test]
        public void AlignmentFactoryReturnsAlignmentWithGoodness()
        {
            var goodnesses = AlignmentConstants.GetGoodnesses();

            while (TestShouldKeepRunning())
            {
                var alignment = AlignmentFactory.CreateWith(AlignmentRandomizer);
                Assert.That(goodnesses.Contains(alignment.Goodness), Is.True);
            }

            AssertIterations();
        }

        [Test]
        public void AlignmentFactoryReturnsAlignmentWithLawfulness()
        {
            var lawfulnesses = AlignmentConstants.GetLawfulnesses();

            while (TestShouldKeepRunning())
            {
                var alignment = AlignmentFactory.CreateWith(AlignmentRandomizer);
                Assert.That(lawfulnesses.Contains(alignment.Lawfulness), Is.True);
            }

            AssertIterations();
        }
    }
}