using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Generators.Abilities;
using CharacterGen.Generators.Abilities.Feats;
using CharacterGen.Generators.Randomizers.CharacterClasses;
using CharacterGen.Generators.Randomizers.Stats;
using Ninject;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Tests.Integration.Stress.Abilities.Feats
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

        [TearDown]
        public void TearDown()
        {
            ClassNameRandomizer = GetNewInstanceOf<IClassNameRandomizer>(ClassNameRandomizerTypeConstants.Any);
            LevelRandomizer = GetNewInstanceOf<ILevelRandomizer>(LevelRandomizerTypeConstants.Any);
        }

        [TestCase("ClassFeatsGenerator")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var feats = GetClassFeats();

            foreach (var feat in feats)
            {
                Assert.That(feat.Name, Is.Not.Empty);
                Assert.That(feat.Focus, Is.Not.Null, feat.Name);
                Assert.That(feat.Strength, Is.Not.Negative, feat.Name);
                Assert.That(feat.Frequency.Quantity, Is.Not.Negative, feat.Name);
                Assert.That(feat.Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Constant)
                    .Or.EqualTo(FeatConstants.Frequencies.AtWill)
                    .Or.EqualTo(FeatConstants.Frequencies.Day)
                    .Or.EqualTo(FeatConstants.Frequencies.Round)
                    .Or.EqualTo(FeatConstants.Frequencies.Week)
                    .Or.Empty, feat.Name);
            }
        }

        private IEnumerable<Feat> GetClassFeats()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            var race = GetNewRace(alignment, characterClass);
            var stats = StatsGenerator.GenerateWith(StatsRandomizer, characterClass, race);
            var skills = SkillsGenerator.GenerateWith(characterClass, race, stats);
            var racialFeats = RacialFeatsGenerator.GenerateWith(race, skills, stats);

            return ClassFeatsGenerator.GenerateWith(characterClass, race, stats, racialFeats, skills);
        }

        [Test]
        public void RangerCombatStyleFeatsHaveCorrectFocus()
        {
            var setClassNameRandomizer = GetNewInstanceOf<ISetClassNameRandomizer>();
            setClassNameRandomizer.SetClassName = CharacterClassConstants.Ranger;
            ClassNameRandomizer = setClassNameRandomizer;

            var setLevelRandomizer = GetNewInstanceOf<ISetLevelRandomizer>();
            setLevelRandomizer.SetLevel = 20;
            LevelRandomizer = setLevelRandomizer;

            var feats = GetClassFeats();

            var combatStyleFeat = feats.Single(f => f.Name == FeatConstants.CombatStyle);
            Assert.That(combatStyleFeat.Focus, Is.EqualTo(FeatConstants.TwoWeaponFighting).Or.EqualTo(FeatConstants.Foci.Archery));

            var improvedCombatStyleFeat = feats.Single(f => f.Name == FeatConstants.ImprovedCombatStyle);
            Assert.That(improvedCombatStyleFeat.Focus, Is.EqualTo(FeatConstants.TwoWeaponFighting).Or.EqualTo(FeatConstants.Foci.Archery));
            Assert.That(improvedCombatStyleFeat.Focus, Is.EqualTo(combatStyleFeat.Focus));

            var combatStyleMasteryFeat = feats.Single(f => f.Name == FeatConstants.CombatStyleMastery);
            Assert.That(combatStyleMasteryFeat.Focus, Is.EqualTo(FeatConstants.TwoWeaponFighting).Or.EqualTo(FeatConstants.Foci.Archery));
            Assert.That(combatStyleMasteryFeat.Focus, Is.EqualTo(combatStyleFeat.Focus));
        }
    }
}