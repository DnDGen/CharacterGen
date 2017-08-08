using CharacterGen.Randomizers.CharacterClasses;
using CharacterGen.Randomizers.Races;
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

        [Test]
        public void Stress()
        {
            stressor.Stress(AssertBaseRace);
        }

        protected void AssertBaseRace()
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
            stressor.Stress(AssertBaseRace);
        }
    }
}