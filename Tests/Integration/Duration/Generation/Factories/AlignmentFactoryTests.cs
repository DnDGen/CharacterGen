using Ninject;
using NPCGen.Core.Generation.Factories;
using NPCGen.Core.Generation.Factories.Interfaces;
using NPCGen.Core.Generation.Randomizers.Alignments;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Duration.Generation.Factories
{
    [TestFixture]
    public class AlignmentFactoryTests : DurationTest
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

        [TearDown]
        public void TearDown()
        {
            StopTest();
        }

        [Test]
        public void AlignmentGeneration()
        {
            AlignmentFactory.CreateWith(AlignmentRandomizer);
            AssertDuration();
        }
    }
}