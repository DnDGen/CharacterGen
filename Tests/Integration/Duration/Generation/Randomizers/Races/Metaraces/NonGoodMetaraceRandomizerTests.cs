using Ninject;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;
using NPCGen.Tests.Integration.Common;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Duration.Generation.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class NonGoodMetaraceRandomizerTests : DurationTest
    {
        [Inject]
        public NonGoodMetaraceRandomizer MetaraceRandomizer { get; set; }
        [Inject]
        public DependentDataCollection DependentData { get; set; }

        protected override IMetaraceRandomizer GetMetaraceRandomizer(IKernel kernel)
        {
            var randomizer = kernel.Get<NonGoodMetaraceRandomizer>();
            randomizer.AllowNoMetarace = false;
            return randomizer;
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
        public void NonGoodMetaraceRandomizationWithEmptyAllowed()
        {
            MetaraceRandomizer.AllowNoMetarace = true;
            MetaraceRandomizer.Randomize(DependentData.Alignment.Goodness, DependentData.CharacterClassPrototype);
            AssertDuration();
        }

        [Test]
        public void NonGoodMetaraceRandomizationWithEmptyNotAllowed()
        {
            MetaraceRandomizer.AllowNoMetarace = false;
            MetaraceRandomizer.Randomize(DependentData.Alignment.Goodness, DependentData.CharacterClassPrototype);
            AssertDuration();
        }
    }
}