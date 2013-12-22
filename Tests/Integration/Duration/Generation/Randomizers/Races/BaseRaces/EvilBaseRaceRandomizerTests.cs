using Ninject;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;
using NPCGen.Tests.Integration.Common;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Duration.Generation.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class EvilBaseRaceRandomizerTests : DurationTest
    {
        [Inject]
        public EvilBaseRaceRandomizer BaseRaceRandomizer { get; set; }
        [Inject]
        public DependentDataCollection DependentData { get; set; }

        protected override IBaseRaceRandomizer GetBaseRaceRandomizer(IKernel kernel)
        {
            return kernel.Get<EvilBaseRaceRandomizer>();
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
        public void EvilBaseRaceSingleRandomization()
        {
            BaseRaceRandomizer.Randomize(DependentData.Alignment.Goodness, DependentData.CharacterClassPrototype);
            AssertDuration();
        }
    }
}