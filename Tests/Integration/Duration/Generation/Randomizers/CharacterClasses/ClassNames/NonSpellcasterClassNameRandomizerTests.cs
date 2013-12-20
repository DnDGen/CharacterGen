using Ninject;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.ClassNames;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Duration.Generation.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class NonSpellcasterClassNameRandomizerTests : DurationTest
    {
        [Inject]
        public Alignment Alignment { get; set; }

        public NonSpellcasterClassNameRandomizerTests()
        {
            ClassNameRandomizer = kernel.Get<NonSpellcasterClassNameRandomizer>();
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