using Ninject;
using NPCGen.Generators.Randomizers.Races.Metaraces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class SetMetaraceRandomizerTests : StressTests
    {
        [Inject]
        public SetMetaraceRandomizer SetMetaraceRandomizer { get; set; }

        protected override void MakeAssertions()
        {
            var data = GetNewDependentData();
            SetMetaraceRandomizer.Metarace = data.Race.Metarace;

            var metarace = SetMetaraceRandomizer.Randomize(data.Alignment.Goodness, data.CharacterClassPrototype);
            Assert.That(metarace, Is.EqualTo(data.Race.Metarace));
        }
    }
}