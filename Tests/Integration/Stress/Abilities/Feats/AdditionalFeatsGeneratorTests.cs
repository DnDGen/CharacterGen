using System;
using System.Linq;
using Ninject;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Generators.Interfaces.Abilities;
using NPCGen.Generators.Interfaces.Abilities.Feats;
using NPCGen.Generators.Interfaces.Combats;
using NPCGen.Generators.Interfaces.Randomizers.Stats;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Abilities.Feats
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
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            var race = GetNewRace(alignment, characterClass);
            var stats = StatsGenerator.GenerateWith(StatsRandomizer, characterClass, race);
            var skills = SkillsGenerator.GenerateWith(characterClass, race, stats);
            var baseAttack = CombatGenerator.GenerateBaseAttackWith(characterClass, race);
            var racialFeats = RacialFeatsGenerator.GenerateWith(race, skills);
            var classFeats = ClassFeatsGenerator.GenerateWith(characterClass, stats, racialFeats, skills);
            var preselectedFeats = classFeats.Union(racialFeats);

            var feats = AdditionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);

            var additionalFeatCount = characterClass.Level / 3 + 1;
            if (characterClass.ClassName == CharacterClassConstants.Fighter)
                additionalFeatCount += characterClass.Level / 2 + 1;

            var count = feats.Count();
            Assert.That(count, Is.EqualTo(additionalFeatCount), characterClass.ClassName);

            foreach (var feat in feats)
            {
                Assert.That(feat.Name, Is.Not.Empty);
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