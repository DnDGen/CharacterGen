using CharacterGen.Common.Races;
using CharacterGen.Generators.Randomizers.CharacterClasses;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Stress
{
    [TestFixture]
    public class RaceGeneratorTests : StressTests
    {
        [TearDown]
        public void TearDown()
        {
            ClassNameRandomizer = GetNewInstanceOf<IClassNameRandomizer>(ClassNameRandomizerTypeConstants.AnyPlayer);
        }

        [TestCase("Race Generator")]
        public override void Stress(string stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var race = GenerateRace();
            Assert.That(race.BaseRace, Is.Not.Empty);
            Assert.That(race.Metarace, Is.Not.Null);
            Assert.That(race.Size, Is.EqualTo(RaceConstants.Sizes.Large).Or.EqualTo(RaceConstants.Sizes.Medium).Or.EqualTo(RaceConstants.Sizes.Small));
            Assert.That(race.LandSpeed, Is.Positive);
            Assert.That(race.LandSpeed % 10, Is.EqualTo(0));
            Assert.That(race.AerialSpeed, Is.Not.Negative);
            Assert.That(race.MetaraceSpecies, Is.Not.Null);
            Assert.That(race.Age.Stage, Is.Not.Empty);
            Assert.That(race.Age.Years, Is.Positive);
            Assert.That(race.HeightInInches, Is.Positive);
            Assert.That(race.WeightInPounds, Is.Positive);
        }

        private Race GenerateRace()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);

            return RaceGenerator.GenerateWith(alignment, characterClass, BaseRaceRandomizer, MetaraceRandomizer);
        }

        [Test]
        public void NPCRaces()
        {
            ClassNameRandomizer = GetNewInstanceOf<IClassNameRandomizer>(ClassNameRandomizerTypeConstants.AnyNPC);
            Stress(MakeAssertions);
        }
    }
}