using System;
using Ninject;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Generators.Interfaces.Abilities;
using NPCGen.Generators.Interfaces.Abilities.Feats;
using NPCGen.Generators.Interfaces.Combats;
using NPCGen.Generators.Interfaces.Randomizers.Stats;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Abilities.Feats
{
    [TestFixture]
    public class RacialFeatsGeneratorTests : StressTests
    {
        [Inject]
        public IRacialFeatsGenerator RacialFeatsGenerator { get; set; }
        [Inject]
        public ISkillsGenerator SkillsGenerator { get; set; }
        [Inject]
        public IStatsGenerator StatsGenerator { get; set; }
        [Inject, Named(StatsRandomizerTypeConstants.Raw)]
        public IStatsRandomizer StatsRandomizer { get; set; }
        [Inject]
        public ICombatGenerator CombatGenerator { get; set; }

        [TestCase("RacialFeatsGenerator")]
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

            var feats = RacialFeatsGenerator.GenerateWith(race, skills);

            foreach (var feat in feats)
            {
                Assert.That(feat.Name.Id, Is.Not.Empty);
                Assert.That(feat.Focus, Is.Not.Null);
                Assert.That(feat.Strength, Is.Positive);
                Assert.That(feat.Frequency.Quantity, Is.Positive);
                Assert.That(feat.Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Constant)
                    .Or.EqualTo(FeatConstants.Frequencies.AtWill)
                    .Or.EqualTo(FeatConstants.Frequencies.Day));
            }
        }
    }
}