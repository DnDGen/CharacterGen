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

        [Test]
        public void AlignmentFactoryReturnsAlignment()
        {
            for (var i = 0; i < ConfidenceLevel; i++)
            {
                var alignment = AlignmentFactory.CreateWith(AlignmentRandomizer);
                Assert.That(alignment, Is.Not.Null);
            }
        }

        [Test]
        public void AlignmentFactoryReturnsAlignmentWithGoodness()
        {
            var goodnesses = AlignmentConstants.GetGoodnesses();

            for (var i = 0; i < ConfidenceLevel; i++)
            {
                var alignment = AlignmentFactory.CreateWith(AlignmentRandomizer);
                Assert.That(goodnesses.Contains(alignment.Goodness), Is.True);
            }
        }

        [Test]
        public void AlignmentFactoryReturnsAlignmentWithLawfulness()
        {
            var lawfulnesses = AlignmentConstants.GetLawfulnesses();

            for (var i = 0; i < ConfidenceLevel; i++)
            {
                var alignment = AlignmentFactory.CreateWith(AlignmentRandomizer);
                Assert.That(lawfulnesses.Contains(alignment.Lawfulness), Is.True);
            }
        }
    }
}