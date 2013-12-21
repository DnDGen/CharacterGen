using Ninject;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.ClassNames;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Duration.Generation.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class NonSpellcasterClassNameRandomizerTests : DurationTest
    {
        [Inject]
        public NonSpellcasterClassNameRandomizer ClassNameRandomizer { get; set; }
        [Inject]
        public Alignment Alignment { get; set; }

        protected override IClassNameRandomizer GetClassNameRandomizer(IKernel kernel)
        {
            return kernel.Get<NonSpellcasterClassNameRandomizer>();
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
        public void NonSpellcasterClassNameSingleRandomization()
        {
            ClassNameRandomizer.Randomize(Alignment);
            AssertDuration();
        }
    }
}