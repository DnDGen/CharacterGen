using Ninject;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NPCGen.Tests.Integration.Common;
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
                var data = GetNewInstanceOf<DependentDataCollection>();
                var baseRace = BaseRaceRandomizer.Randomize(data.Alignment.Goodness, data.CharacterClassPrototype);
                Assert.That(baseRace, Is.Not.Null);
                Assert.That(baseRace, Is.Not.Empty);
            }

            AssertIterations();
        }
    }
}