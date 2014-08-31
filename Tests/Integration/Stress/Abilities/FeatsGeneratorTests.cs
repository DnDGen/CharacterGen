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

        private IEnumerable<String> allFeats;

        [SetUp]
        public void Setup()
        {
            allFeats = FeatConstants.GetAllFeats();
        }

        protected override void MakeAssertions()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            var race = GetNewRace(alignment, characterClass);
            var stats = StatsGenerator.GenerateWith(StatsRandomizer, characterClass, race);
            var skills = SkillsGenerator.GenerateWith(characterClass, stats);
            var baseAttack = CombatGenerator.GenerateBaseAttackWith(characterClass);

            var feats = FeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);

            var minimumFeats = characterClass.Level / 3 + 1;
            if (characterClass.ClassName == CharacterClassConstants.Fighter)
                minimumFeats += characterClass.Level / 2 + 1;

            Assert.That(feats.Count(), Is.AtLeast(minimumFeats));

            foreach (var feat in feats)
                Assert.That(allFeats, Contains.Item(feat));
        }
    }
}