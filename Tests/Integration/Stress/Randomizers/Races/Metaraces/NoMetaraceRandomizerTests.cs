using Ninject;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NPCGen.Generators.Randomizers.Races.Metaraces;
using NPCGen.Tests.Integration.Common;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class NoMetaraceRandomizerTests : StressTests
    {
        [Inject]
        public NoMetaraceRandomizer MetaraceRandomizer { get; set; }

        protected override IMetaraceRandomizer GetMetaraceRandomizer(IKernel kernel)
        {
            var randomizer = kernel.Get<NoMetaraceRandomizer>();
            return randomizer;
        }

        [Test]
        public void NoMetaraceRandomizerReturnsMetaraceOrEmpty()
        {
            while (TestShouldKeepRunning())
            {
                var data = GetNewInstanceOf<DependentDataCollection>();
                var metarace = MetaraceRandomizer.Randomize(data.Alignment.Goodness, data.CharacterClassPrototype);
                Assert.That(metarace, Is.Empty);
            }

            AssertIterations();
        }
    }
}