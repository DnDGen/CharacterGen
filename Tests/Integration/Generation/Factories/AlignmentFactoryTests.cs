using System.Linq;
using Ninject;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Factories;
using NPCGen.Core.Generation.Factories.Interfaces;
using NPCGen.Core.Generation.Randomizers.Alignments;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Generation.Factories
{
    [TestFixture]
    public class AlignmentFactoryTests : IntegrationTest
    {
        [Inject]
        public IAlignmentFactory AlignmentFactory { get; set; }
        [Inject]
        public AnyAlignmentRandomizer AlignmentRandomizer { get; set; }

        [SetUp]
        public void Setup()
        {
            StartTest();
        }

        [Test]
        public void AlignmentFactoryReturnsAlignment()
        {
            while (TestShouldKeepRunning())
            {
                var alignment = AlignmentFactory.CreateWith(AlignmentRandomizer);
                Assert.That(alignment, Is.Not.Null);
            }
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
        }
    }
}