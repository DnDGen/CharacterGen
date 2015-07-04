using System;
using Ninject;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Generators.Interfaces.Abilities;
using NPCGen.Generators.Interfaces.Abilities.Feats;
using NPCGen.Generators.Interfaces.Randomizers.Stats;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Abilities.Feats
{
    [TestFixture]
    public class ClassFeatsGeneratorTests : StressTests
    {
        [Inject]
        public IClassFeatsGenerator ClassFeatsGenerator { get; set; }
        [Inject]
        public ISkillsGenerator SkillsGenerator { get; set; }
        [Inject]
        public IStatsGenerator StatsGenerator { get; set; }
        [Inject, Named(StatsRandomizerTypeConstants.Raw)]
        public IStatsRandomizer StatsRandomizer { get; set; }
        [Inject]
        public IRacialFeatsGenerator RacialFeatsGenerator { get; set; }

        [TestCase("ClassFeatsGenerator")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            var race = GetNewRace(alignment, characterClass);
            var stats = StatsGenerator.GenerateWith(StatsRandomizer, characterClass, race);
            var skills = SkillsGenerator.GenerateWith(characterClass, race, stats);
            var racialFeats = RacialFeatsGenerator.GenerateWith(race, skills);

            var feats = ClassFeatsGenerator.GenerateWith(characterClass, stats, racialFeats);

            foreach (var feat in feats)
            {
                Assert.That(feat.Name.Id, Is.Not.Empty);
                Assert.That(feat.Focus, Is.Not.Null);
                Assert.That(feat.Strength, Is.Positive);
                Assert.That(feat.Frequency.Quantity, Is.Positive);
                Assert.That(feat.Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Constant)
                    .Or.EqualTo(FeatConstants.Frequencies.AtWill)
                    .Or.EqualTo(FeatConstants.Frequencies.Day)
                    .Or.Empty);
            }
        }
    }
}