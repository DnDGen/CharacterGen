using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;
using CharacterGen.Generators.Abilities;
using CharacterGen.Generators.Abilities.Feats;
using CharacterGen.Generators.Combats;
using CharacterGen.Generators.Randomizers.CharacterClasses;
using CharacterGen.Generators.Randomizers.Races;
using CharacterGen.Generators.Randomizers.Stats;
using Ninject;
using NUnit.Framework;
using System;
using System.Linq;

namespace CharacterGen.Tests.Integration.Stress.Abilities.Feats
{
    [TestFixture]
    public class FeatsGeneratorTests : StressTests
    {
        [Inject]
        public IFeatsGenerator FeatsGenerator { get; set; }
        [Inject]
        public ISkillsGenerator SkillsGenerator { get; set; }
        [Inject]
        public IStatsGenerator StatsGenerator { get; set; }
        [Inject, Named(StatsRandomizerTypeConstants.Raw)]
        public IStatsRandomizer StatsRandomizer { get; set; }
        [Inject, Named(CombatGeneratorTypeConstants.Character)]
        public ICombatGenerator CombatGenerator { get; set; }

        [TestCase("FeatsGenerator")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }

        [TearDown]
        public void TearDown()
        {
            ClassNameRandomizer = GetNewInstanceOf<IClassNameRandomizer>(ClassNameRandomizerTypeConstants.Any);
            LevelRandomizer = GetNewInstanceOf<ILevelRandomizer>(LevelRandomizerTypeConstants.Any);
        }

        protected override void MakeAssertions()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            var race = RaceGenerator.GenerateWith(alignment, characterClass, BaseRaceRandomizer, MetaraceRandomizer);
            var stats = StatsGenerator.GenerateWith(StatsRandomizer, characterClass, race);
            var skills = SkillsGenerator.GenerateWith(characterClass, race, stats);
            var baseAttack = CombatGenerator.GenerateBaseAttackWith(characterClass, race);

            var feats = FeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);

            var minimumFeats = characterClass.Level / 3 + 1;
            if (characterClass.ClassName == CharacterClassConstants.Fighter)
                minimumFeats += characterClass.Level / 2 + 1;

            var count = feats.Count();
            Assert.That(count, Is.AtLeast(minimumFeats));
            Assert.That(feats.Distinct().Count(), Is.EqualTo(count));

            foreach (var feat in feats)
            {
                Assert.That(feat.Name, Is.Not.Empty);
                Assert.That(feat.Focus, Is.Not.Null, feat.Name);
                Assert.That(feat.Strength, Is.Not.Negative, feat.Name);
                Assert.That(feat.Frequency.Quantity, Is.Not.Negative, feat.Name);
                Assert.That(feat.Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Constant)
                    .Or.EqualTo(FeatConstants.Frequencies.AtWill)
                    .Or.EqualTo(FeatConstants.Frequencies.Day)
                    .Or.EqualTo(FeatConstants.Frequencies.Week)
                    .Or.EqualTo(FeatConstants.Frequencies.Round)
                    .Or.Empty, feat.Name);
            }
        }

        [Test]
        public void DuplicateFeatsAreCorrectlyRemoved()
        {
            var setClassRandomizer = GetNewInstanceOf<ISetClassNameRandomizer>();
            setClassRandomizer.SetClassName = CharacterClassConstants.Barbarian;
            ClassNameRandomizer = setClassRandomizer;

            var setLevelRandomizer = GetNewInstanceOf<ISetLevelRandomizer>();
            setLevelRandomizer.SetLevel = 1;
            LevelRandomizer = setLevelRandomizer;

            var setRaceRandomizer = GetNewInstanceOf<ISetBaseRaceRandomizer>();
            setRaceRandomizer.SetBaseRace = RaceConstants.BaseRaces.Ogre;

            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            var race = RaceGenerator.GenerateWith(alignment, characterClass, setRaceRandomizer, MetaraceRandomizer);
            var stats = StatsGenerator.GenerateWith(StatsRandomizer, characterClass, race);
            var skills = SkillsGenerator.GenerateWith(characterClass, race, stats);
            var baseAttack = CombatGenerator.GenerateBaseAttackWith(characterClass, race);

            var feats = FeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            Assert.That(feats.Count(f => f.Name == FeatConstants.SimpleWeaponProficiency), Is.EqualTo(1), FeatConstants.SimpleWeaponProficiency);
            Assert.That(feats.Count(f => f.Name == FeatConstants.MartialWeaponProficiency), Is.EqualTo(1), FeatConstants.MartialWeaponProficiency);
            Assert.That(feats.Count(f => f.Name == FeatConstants.LightArmorProficiency), Is.EqualTo(1), FeatConstants.LightArmorProficiency);
            Assert.That(feats.Count(f => f.Name == FeatConstants.MediumArmorProficiency), Is.EqualTo(1), FeatConstants.MediumArmorProficiency);
            Assert.That(feats.Count(f => f.Name == FeatConstants.ShieldProficiency), Is.EqualTo(1), FeatConstants.ShieldProficiency);

            var simpleWeaponProficiencyFeat = feats.First(f => f.Name == FeatConstants.SimpleWeaponProficiency);
            Assert.That(simpleWeaponProficiencyFeat.Focus, Is.EqualTo(FeatConstants.Foci.All));

            var martialWeaponProficiencyFeat = feats.First(f => f.Name == FeatConstants.MartialWeaponProficiency);
            Assert.That(martialWeaponProficiencyFeat.Focus, Is.EqualTo(FeatConstants.Foci.All));
        }
    }
}