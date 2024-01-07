using DnDGen.CharacterGen.Randomizers.Races;
using Ninject;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Stress.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class SetMetaraceRandomizerTests : StressTests
    {
        [Inject]
        public ISetMetaraceRandomizer SetMetaraceRandomizer { get; set; }

        [SetUp]
        public void Setup()
        {
            var forcableMetaraceRandomizer = metaraceRandomizer as IForcableMetaraceRandomizer;
            forcableMetaraceRandomizer.ForceMetarace = true;
        }

        [TearDown]
        public void TearDown()
        {
            metaraceRandomizer = GetNewInstanceOf<RaceRandomizer>(RaceRandomizerTypeConstants.Metarace.AnyMeta);
        }

        [Test]
        public void StressSetMetarace()
        {
            stressor.Stress(AssertMetarace);
        }

        protected void AssertMetarace()
        {
            var prototype = GetCharacterPrototype();
            SetMetaraceRandomizer.SetMetarace = metaraceRandomizer.Randomize(prototype.Alignment, prototype.CharacterClass);

            var metarace = SetMetaraceRandomizer.Randomize(prototype.Alignment, prototype.CharacterClass);
            Assert.That(metarace, Is.EqualTo(SetMetaraceRandomizer.SetMetarace));
        }
    }
}