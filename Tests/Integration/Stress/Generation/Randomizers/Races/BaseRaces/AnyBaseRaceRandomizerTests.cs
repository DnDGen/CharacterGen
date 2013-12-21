using Ninject;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Generation.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class AnyBaseRaceRandomizerTests : StressTest
    {
        [Inject]
        public AnyBaseRaceRandomizer BaseRaceRandomizer { get; set; }

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
        public void AnyBaseRaceRandomizationReturnsBaseRace()
        {
            while (TestShouldKeepRunning())
            {
                var alignment = GetNewInstanceOf<Alignment>();
                var prototype = GetNewInstanceOf<CharacterClassPrototype>();

                var baseRace = BaseRaceRandomizer.Randomize(alignment.Goodness, prototype);
                Assert.That(baseRace, Is.Not.Null);
                Assert.That(baseRace, Is.Not.Empty);
            }

            AssertIterations();
        }
    }
}