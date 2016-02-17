using CharacterGen.Generators.Randomizers.CharacterClasses;
using CharacterGen.Generators.Randomizers.Races;
using Ninject;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Stress.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class SetBaseRaceRandomizerTests : StressTests
    {
        [Inject]
        public ISetBaseRaceRandomizer SetBaseRaceRandomizer { get; set; }

        [TearDown]
        public void TearDown()
        {
            ClassNameRandomizer = GetNewInstanceOf<IClassNameRandomizer>(ClassNameRandomizerTypeConstants.AnyPlayer);
        }

        [TestCase("Set Base Race Randomizer")]
        public override void Stress(string stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            SetBaseRaceRandomizer.SetBaseRace = BaseRaceRandomizer.Randomize(alignment, characterClass);

            var baseRace = SetBaseRaceRandomizer.Randomize(alignment, characterClass);
            Assert.That(baseRace, Is.EqualTo(SetBaseRaceRandomizer.SetBaseRace));
        }

        [Test]
        public void StressNPCSetBaseRace()
        {
            ClassNameRandomizer = GetNewInstanceOf<IClassNameRandomizer>(ClassNameRandomizerTypeConstants.AnyNPC);
            Stress(MakeAssertions);
        }
    }
}