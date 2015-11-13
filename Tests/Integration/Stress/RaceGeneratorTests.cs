using CharacterGen.Common.Races;
using CharacterGen.Generators.Randomizers.Alignments;
using CharacterGen.Generators.Randomizers.Races;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Stress
{
    [TestFixture]
    public class RaceGeneratorTests : StressTests
    {
        [TearDown]
        public void TearDown()
        {
            AlignmentRandomizer = GetNewInstanceOf<IAlignmentRandomizer>(AlignmentRandomizerTypeConstants.Any);
            MetaraceRandomizer = GetNewInstanceOf<RaceRandomizer>(RaceRandomizerTypeConstants.Metarace.AnyMeta);
        }

        [TestCase("RaceGenerator")]
        public override void Stress(String stressSubject)
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
        }

        private Race GenerateRace()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);

            return RaceGenerator.GenerateWith(alignment, characterClass, BaseRaceRandomizer, MetaraceRandomizer);
        }

        [Test]
        public void WingsHappen()
        {
            AlignmentRandomizer = GetNewInstanceOf<IAlignmentRandomizer>(AlignmentRandomizerTypeConstants.NonNeutral);

            var forcableMetaraceRandomizer = MetaraceRandomizer as IForcableMetaraceRandomizer;
            forcableMetaraceRandomizer.ForceMetarace = true;

            var race = GenerateOrFail(GenerateRace, r => r.HasWings);
            Assert.That(race.HasWings, Is.True);
            Assert.That(race.AerialSpeed, Is.Positive);
            Assert.That(race.AerialSpeed % 10, Is.EqualTo(0));
        }

        [Test]
        public void WingsDoNotHappen()
        {
            var race = GenerateOrFail(GenerateRace, r => r.HasWings == false);
            Assert.That(race.HasWings, Is.False);
            Assert.That(race.AerialSpeed, Is.EqualTo(0));
        }
    }
}