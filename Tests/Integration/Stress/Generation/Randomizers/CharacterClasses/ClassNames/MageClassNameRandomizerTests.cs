using System.Linq;
using Ninject;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.ClassNames;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Generation.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class MageClassNameRandomizerTests : StressTest
    {
        [Inject]
        public MageClassNameRandomizer ClassNameRandomizer { get; set; }

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
        public void MageClassNameRandomizerReturnsClassName()
        {
            while (TestShouldKeepRunning())
            {
                var alignment = GetNewInstanceOf<Alignment>();
                var className = ClassNameRandomizer.Randomize(alignment);
                Assert.That(className, Is.Not.Null);
                Assert.That(className, Is.Not.Empty);
            }

            AssertIterations();
        }

        [Test]
        public void MageClassNameRandomizerAlwaysReturnsMage()
        {
            var mages = new[]
                {
                    CharacterClassConstants.Bard,
                    CharacterClassConstants.Ranger,
                    CharacterClassConstants.Sorcerer,
                    CharacterClassConstants.Wizard
                };

            while (TestShouldKeepRunning())
            {
                var alignment = GetNewInstanceOf<Alignment>();
                var className = ClassNameRandomizer.Randomize(alignment);
                Assert.That(mages.Contains(className), Is.True);
            }

            AssertIterations();
        }
    }
}
