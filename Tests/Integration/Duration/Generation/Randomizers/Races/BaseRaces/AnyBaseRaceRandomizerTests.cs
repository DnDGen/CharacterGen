using Ninject;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Duration.Generation.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class AnyBaseRaceRandomizerTests : DurationTest
    {
        [Inject]
        public AnyBaseRaceRandomizer BaseRaceRandomizer { get; set; }
        [Inject]
        public Alignment Alignment { get; set; }
        [Inject]
        public CharacterClassPrototype CharacterClassPrototype { get; set; }

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
            BaseRaceRandomizer.Randomize(Alignment.Goodness, CharacterClassPrototype);
            AssertDuration();
        }
    }
}