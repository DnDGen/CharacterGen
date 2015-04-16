using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Generators.Interfaces.Abilities;
using NPCGen.Generators.Interfaces.Combats;
using NPCGen.Generators.Interfaces.Randomizers.Stats;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Abilities
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
        [Inject]
        public ICombatGenerator CombatGenerator { get; set; }

        private IEnumerable<String> allFeatIds;

        [SetUp]
        public void Setup()
        {
            allFeatIds = new[]
            {
                FeatConstants.AasimarDaylightId,
                FeatConstants.AmbidexterityId,
                FeatConstants.DarkvisionId,
                FeatConstants.LightArmorProficiencyId,
                FeatConstants.ResistanceToAcidId,
                FeatConstants.ResistanceToColdId,
                FeatConstants.ResistanceToElectricityId,
                FeatConstants.ScentId,
                FeatConstants.ShieldProficiencyId,
                FeatConstants.SkillFocusId,
                FeatConstants.SpellMasteryId,
                FeatConstants.StabilityId,
                FeatConstants.StonecunningId,
                FeatConstants.WeaponFamiliarityId
            };
        }

        [TestCase("FeatsGenerator")]
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

            var feats = FeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);

            var minimumFeats = characterClass.Level / 3 + 1;
            if (characterClass.ClassName == CharacterClassConstants.Fighter)
                minimumFeats += characterClass.Level / 2 + 1;

            var count = feats.Count();
            Assert.That(count, Is.AtLeast(minimumFeats));
            Assert.That(feats.Distinct().Count(), Is.EqualTo(count));

            foreach (var feat in feats)
            {
                Assert.That(allFeatIds, Contains.Item(feat.Name.Id));
                Assert.That(feat.Name.Name, Is.Not.Empty);
                Assert.That(feat.Focus, Is.Not.Null);
            }

            var specifics = feats.Where(f => feats.Count(c => c.Name.Id == f.Name.Id) > 1);
            foreach (var feat in specifics)
                Assert.That(feat.Focus, Is.Not.Empty, feat.Name.Name);
        }
    }
}