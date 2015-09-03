using CharacterGen.Common.Magics;
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
        [Inject]
        public IAbilitiesGenerator AbilitiesGenerator { get; set; }
        [Inject, Named(StatsRandomizerTypeConstants.Raw)]
        public IStatsRandomizer StatsRandomizer { get; set; }
        [Inject]
        public ICombatGenerator CombatGenerator { get; set; }

        [TestCase("AnimalGenerator")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var animal = GenerateAnimal();

            if (animal == null)
                return;

            Assert.That(animal.Type, Is.Not.Empty);
            Assert.That(animal.ArmorClass, Is.AtLeast(10));
            Assert.That(animal.Feats, Is.Not.Empty);
            Assert.That(animal.HitPoints, Is.Positive);
            Assert.That(animal.Tricks, Is.Positive);
        }

        private Animal GenerateAnimal()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            var race = GetNewRace(alignment, characterClass);
            var baseAttack = CombatGenerator.GenerateBaseAttackWith(characterClass, race);
            var ability = AbilitiesGenerator.GenerateWith(characterClass, race, StatsRandomizer, baseAttack);

            return AnimalGenerator.GenerateFrom(characterClass, ability.Feats);
        }

        [Test]
        public void AnimalHappens()
        {
            var animal = Generate(GenerateAnimal, a => a != null);
            Assert.That(animal, Is.Not.Null);
        }

        [Test]
        public void AnimalDoesNotHappen()
        {
            var animal = Generate(GenerateAnimal, a => a == null);
            Assert.That(animal, Is.Null);
        }
    }
}
