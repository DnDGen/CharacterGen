using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Generators.Abilities;
using CharacterGen.Generators.Abilities.Feats;
using CharacterGen.Generators.Combats;
using CharacterGen.Generators.Randomizers.Stats;
using Ninject;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Tests.Integration.Stress.Abilities.Feats
{
    [TestFixture]
    public class AdditionalFeatsGeneratorTests : StressTests
    {
        [Inject]
        public IAdditionalFeatsGenerator AdditionalFeatsGenerator { get; set; }
        [Inject]
        public IRacialFeatsGenerator RacialFeatsGenerator { get; set; }
        [Inject]
        public IClassFeatsGenerator ClassFeatsGenerator { get; set; }
        [Inject]
        public ISkillsGenerator SkillsGenerator { get; set; }
        [Inject]
        public IStatsGenerator StatsGenerator { get; set; }
        [Inject, Named(StatsRandomizerTypeConstants.Raw)]
        public IStatsRandomizer StatsRandomizer { get; set; }
        [Inject]
        public ICombatGenerator CombatGenerator { get; set; }

        [TestCase("AdditionalFeatsGenerator")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var feats = GetAdditionalFeats();
            Assert.That(feats, Is.Not.Empty);

            foreach (var feat in feats)
            {
                Assert.That(feat.Name, Is.Not.Empty);
                Assert.That(feat.Foci, Is.Not.Null, feat.Name);
                Assert.That(feat.Power, Is.Not.Negative, feat.Name);
                Assert.That(feat.Frequency.Quantity, Is.Not.Negative, feat.Name);
                Assert.That(feat.Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Constant)
                    .Or.EqualTo(FeatConstants.Frequencies.AtWill)
                    .Or.EqualTo(FeatConstants.Frequencies.Day)
                    .Or.EqualTo(FeatConstants.Frequencies.Week)
                    .Or.EqualTo(FeatConstants.Frequencies.Round)
                    .Or.Empty, feat.Name);
            }
        }

        private IEnumerable<Feat> GetAdditionalFeats()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            var race = RaceGenerator.GenerateWith(alignment, characterClass, BaseRaceRandomizer, MetaraceRandomizer);
            var stats = StatsGenerator.GenerateWith(StatsRandomizer, characterClass, race);
            var skills = SkillsGenerator.GenerateWith(characterClass, race, stats);
            var baseAttack = CombatGenerator.GenerateBaseAttackWith(characterClass, race);
            var racialFeats = RacialFeatsGenerator.GenerateWith(race, skills, stats);
            var classFeats = ClassFeatsGenerator.GenerateWith(characterClass, race, stats, racialFeats, skills);
            var preselectedFeats = classFeats.Union(racialFeats);

            return AdditionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
        }
    }
}