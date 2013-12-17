using Ninject;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.ClassNames;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Generation.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class AnyClassNameRandomizerTests : IntegrationTest
    {
        [Inject]
        public AnyClassNameRandomizer ClassNameRandomizer { get; set; }
        [Inject]
        public Alignment Alignment { get; set; }

        [SetUp]
        public void Setup()
        {
            StartTest();
        }

        [Test]
        public void AnyClassNameRandomizerReturnsClassName()
        {
            while (TestShouldKeepRunning())
            {
                var className = ClassNameRandomizer.Randomize(Alignment);
                Assert.That(className, Is.Not.Null);
                Assert.That(className, Is.Not.Empty);
            }
        }
    }
}