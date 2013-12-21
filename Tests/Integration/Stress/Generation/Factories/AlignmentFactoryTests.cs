using System.Linq;
using Ninject;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Factories;
using NPCGen.Core.Generation.Factories.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Generation.Factories
{
    [TestFixture]
    public class AlignmentFactoryTests : StressTest
    {
        [Inject]
        public IAlignmentFactory AlignmentFactory { get; set; }

        [SetUp]
        public void Setup()
        {
            StartTest();
        }

        [TearDown]
        public void TearDown()
        {
            StopTest();
        }

        [Test]
        public void AlignmentFactoryReturnsAlignment()
        {
            while (TestShouldKeepRunning())
            {
                var alignment = AlignmentFactory.CreateWith(GetAlignmentRandomizer(kernel));
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
                var alignment = AlignmentFactory.CreateWith(GetAlignmentRandomizer(kernel));
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
                var alignment = AlignmentFactory.CreateWith(GetAlignmentRandomizer(kernel));
                Assert.That(lawfulnesses.Contains(alignment.Lawfulness), Is.True);
            }

            AssertIterations();
        }
    }
}