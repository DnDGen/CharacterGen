using Ninject;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.ClassNames;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Duration.Generation.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class HealerClassNameRandomizerTests : DurationTest
    {
        [Inject]
        public HealerClassNameRandomizer ClassNameRandomizer { get; set; }
        [Inject]
        public Alignment Alignment { get; set; }

        protected override IClassNameRandomizer GetClassNameRandomizer(IKernel kernel)
        {
            return kernel.Get<HealerClassNameRandomizer>();
        }

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
        public void HealerClassNameSingleRandomization()
        {
            ClassNameRandomizer.Randomize(Alignment);
            AssertDuration();
        }
    }
}