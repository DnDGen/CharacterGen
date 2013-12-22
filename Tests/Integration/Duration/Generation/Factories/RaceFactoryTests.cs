using Ninject;
using NPCGen.Core.Generation.Factories.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;
using NPCGen.Tests.Integration.Common;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Duration.Generation.Factories
{
    [TestFixture]
    public class RaceFactoryTests : DurationTest
    {
        [Inject]
        public IRaceFactory RaceFactory { get; set; }
        [Inject]
        public IBaseRaceRandomizer BaseRaceRandomizer { get; set; }
        [Inject]
        public IMetaraceRandomizer MetaraceRandomizer { get; set; }
        [Inject]
        public DependentDataCollection DependentData { get; set; }

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
        public void RaceGeneration()
        {
            RaceFactory.CreateWith(DependentData.Alignment.Goodness, DependentData.CharacterClassPrototype, BaseRaceRandomizer, MetaraceRandomizer);
            AssertDuration();
        }
    }
}