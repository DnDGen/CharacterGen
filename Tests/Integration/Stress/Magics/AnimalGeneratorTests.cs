using CharacterGen.Generators.Abilities;
using CharacterGen.Generators.Combats;
using CharacterGen.Generators.Magics;
using CharacterGen.Generators.Randomizers.Stats;
using Ninject;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Stress.Magics
{
    [TestFixture]
    public class AnimalGeneratorTests : StressTests
    {
        [Inject, Named(AbilitiesGeneratorTypeConstants.Character)]
        public IAbilitiesGenerator CharacterAbilitiesGenerator { get; set; }
        [Inject, Named(StatsRandomizerTypeConstants.Raw)]
        public IStatsRandomizer StatsRandomizer { get; set; }
        [Inject, Named(CombatGeneratorTypeConstants.Character)]
        public ICombatGenerator CharacterCombatGenerator { get; set; }
        [Inject]
        public IAnimalGenerator AnimalGenerator { get; set; }

        [TestCase("Animal Generator")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var alignment = GetNewAlignment();
            var characterClass = Generate(() => GetNewCharacterClass(alignment), c => c.Level > 3);
            var race = RaceGenerator.GenerateWith(alignment, characterClass, BaseRaceRandomizer, MetaraceRandomizer);
            var baseAttack = CharacterCombatGenerator.GenerateBaseAttackWith(characterClass, race);
            var ability = CharacterAbilitiesGenerator.GenerateWith(characterClass, race, StatsRandomizer, baseAttack);

            var animal = AnimalGenerator.GenerateFrom(alignment, characterClass, race, ability.Feats);
            Assert.That(animal, Is.Not.Null);
        }
    }
}
