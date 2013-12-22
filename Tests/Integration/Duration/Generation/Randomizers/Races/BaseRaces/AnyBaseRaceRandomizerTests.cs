using Ninject;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;
using NPCGen.Tests.Integration.Common;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Duration.Generation.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class AnyBaseRaceRandomizerTests : DurationTest
    {
        [Inject]
        public AnyBaseRaceRandomizer BaseRaceRandomizer { get; set; }
        [Inject]
        public DependentDataCollection DependentData { get; set; }

        protected override IBaseRaceRandomizer GetBaseRaceRandomizer(IKernel kernel)
        {
            return kernel.Get<AnyBaseRaceRandomizer>();
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
        public void AnyBaseRaceRandomization()
        {
            BaseRaceRandomizer.Randomize(DependentData.Alignment.Goodness, DependentData.CharacterClassPrototype);
            AssertDuration();
        }
    }
}