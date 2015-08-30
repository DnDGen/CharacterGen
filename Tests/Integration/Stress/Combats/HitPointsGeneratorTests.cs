using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Generators.Abilities;
using CharacterGen.Generators.Combats;
using CharacterGen.Generators.Randomizers.Stats;
using Ninject;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Stress.Combats
{
    [TestFixture]
    public class HitPointsGeneratorTests : StressTests
    {
        [Inject]
        public IHitPointsGenerator HitPointsGenerator { get; set; }
        [Inject]
        public IStatsGenerator StatsGenerator { get; set; }
        [Inject, Named(StatsRandomizerTypeConstants.Raw)]
        public IStatsRandomizer StatsRandomizer { get; set; }
        [Inject]
        public IAbilitiesGenerator AbilitiesGenerator { get; set; }
        [Inject]
        public ICombatGenerator CombatGenerator { get; set; }

        [TestCase("HitPointsGenerator")]
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
            var baseAttack = CombatGenerator.GenerateBaseAttackWith(characterClass, race);
            var abilities = AbilitiesGenerator.GenerateWith(characterClass, race, StatsRandomizer, baseAttack);

            var hitPoints = HitPointsGenerator.GenerateWith(characterClass, stats[StatConstants.Constitution].Bonus, race, abilities.Feats);
            Assert.That(hitPoints, Is.AtLeast(characterClass.Level));
        }
    }
}