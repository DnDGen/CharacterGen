using CharacterGen.Races;
using CharacterGen.Randomizers.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Stress.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class NoMetaraceRandomizerTests : StressTests
    {
        [SetUp]
        public void Setup()
        {
            MetaraceRandomizer = GetNewInstanceOf<RaceRandomizer>(RaceRandomizerTypeConstants.Metarace.NoMeta);
        }

        [TestCase("No Metarace Randomizer")]
        public override void Stress(string stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);

            var metarace = MetaraceRandomizer.Randomize(alignment, characterClass);
            Assert.That(metarace, Is.EqualTo(RaceConstants.Metaraces.None));
        }
    }
}