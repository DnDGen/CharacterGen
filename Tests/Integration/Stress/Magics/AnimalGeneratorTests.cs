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
        [Inject]
        public IAnimalGenerator AnimalGenerator { get; set; }
        [Inject, Named(AbilitiesGeneratorTypeConstants.Character)]
        public IAbilitiesGenerator AbilitiesGenerator { get; set; }
        [Inject, Named(StatsRandomizerTypeConstants.Raw)]
        public IStatsRandomizer StatsRandomizer { get; set; }
        [Inject, Named(AbilitiesGeneratorTypeConstants.Character)]
        public ICombatGenerator CombatGenerator { get; set; }

        [TestCase("AnimalGenerator")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var animal = GenerateAnimal();
            Assert.That(animal, Is.Not.Null);
        }

        private String GenerateAnimal()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            var race = RaceGenerator.GenerateWith(alignment, characterClass, BaseRaceRandomizer, MetaraceRandomizer);
            var baseAttack = CombatGenerator.GenerateBaseAttackWith(characterClass, race);
            var ability = AbilitiesGenerator.GenerateWith(characterClass, race, StatsRandomizer, baseAttack);

            return AnimalGenerator.GenerateFrom(alignment, characterClass, race, ability.Feats);
        }
    }
}
