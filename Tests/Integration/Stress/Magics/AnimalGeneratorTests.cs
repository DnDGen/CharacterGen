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
            //var animal = GenerateAnimal();

            //if (animal == null)
            //    return;

            //Assert.That(animal.Race.BaseRace, Is.Not.Empty);
            //Assert.That(animal.Race.Metarace, Is.Empty, animal.Race.BaseRace);
            //Assert.That(animal.Race.MetaraceSpecies, Is.Empty, animal.Race.BaseRace);
            //Assert.That(animal.Race.AerialSpeed, Is.Not.Negative, animal.Race.BaseRace);
            ////Assert.That(animal.Race.Age.Years, Is.Positive, animal.Race.BaseRace);
            ////Assert.That(animal.Race.Age.Stage, Is.Not.Empty);
            ////Assert.That(animal.Race.HeightInInches, Is.Positive, animal.Race.BaseRace);
            //Assert.That(animal.Race.LandSpeed, Is.Positive, animal.Race.BaseRace);
            //Assert.That(animal.Race.LandSpeed % 10, Is.EqualTo(0), animal.Race.BaseRace);
            //Assert.That(animal.Race.Size, Is.Not.Empty, animal.Race.BaseRace);
            ////Assert.That(animal.Race.WeightInPounds, Is.Positive, animal.Race.BaseRace);
            //Assert.That(animal.Ability.Feats, Is.Not.Empty, animal.Race.BaseRace);
            //Assert.That(animal.Ability.Languages, Is.Empty, animal.Race.BaseRace);
            //Assert.That(animal.Ability.Skills, Is.Not.Empty, animal.Race.BaseRace);
            //Assert.That(animal.Ability.Stats.Count, Is.EqualTo(6), animal.Race.BaseRace);
            //Assert.That(animal.Ability.Stats.Keys, Contains.Item(StatConstants.Charisma), animal.Race.BaseRace);
            //Assert.That(animal.Ability.Stats.Keys, Contains.Item(StatConstants.Constitution), animal.Race.BaseRace);
            //Assert.That(animal.Ability.Stats.Keys, Contains.Item(StatConstants.Dexterity), animal.Race.BaseRace);
            //Assert.That(animal.Ability.Stats.Keys, Contains.Item(StatConstants.Intelligence), animal.Race.BaseRace);
            //Assert.That(animal.Ability.Stats.Keys, Contains.Item(StatConstants.Strength), animal.Race.BaseRace);
            //Assert.That(animal.Ability.Stats.Keys, Contains.Item(StatConstants.Wisdom), animal.Race.BaseRace);
            //Assert.That(animal.Ability.Stats[StatConstants.Charisma].Value, Is.Positive, animal.Race.BaseRace);
            //Assert.That(animal.Ability.Stats[StatConstants.Constitution].Value, Is.Positive, animal.Race.BaseRace);
            //Assert.That(animal.Ability.Stats[StatConstants.Dexterity].Value, Is.Positive, animal.Race.BaseRace);
            //Assert.That(animal.Ability.Stats[StatConstants.Intelligence].Value, Is.Positive, animal.Race.BaseRace);
            //Assert.That(animal.Ability.Stats[StatConstants.Strength].Value, Is.Positive, animal.Race.BaseRace);
            //Assert.That(animal.Ability.Stats[StatConstants.Wisdom].Value, Is.Positive, animal.Race.BaseRace);
            //Assert.That(animal.Combat.AdjustedDexterityBonus, Is.EqualTo(animal.Ability.Stats[StatConstants.Dexterity].Bonus), animal.Race.BaseRace);
            //Assert.That(animal.Combat.ArmorClass, Is.Positive, animal.Race.BaseRace);
            //Assert.That(animal.Combat.BaseAttack, Is.Not.Negative, animal.Race.BaseRace);
            //Assert.That(animal.Combat.HitPoints, Is.Positive, animal.Race.BaseRace);
            //Assert.That(animal.Combat.InitiativeBonus, Is.EqualTo(0), animal.Race.BaseRace);
            //Assert.That(animal.Combat.SavingThrows.Fortitude, Is.Not.Negative, animal.Race.BaseRace);
            //Assert.That(animal.Combat.SavingThrows.Reflex, Is.Not.Negative, animal.Race.BaseRace);
            //Assert.That(animal.Combat.SavingThrows.Will, Is.Not.Negative, animal.Race.BaseRace);
            //Assert.That(animal.Tricks, Is.Positive, animal.Race.BaseRace);
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

        [Test]
        public void AnimalHappens()
        {
            var animal = Generate(GenerateAnimal, a => String.IsNullOrEmpty(a) == false);
            Assert.That(animal, Is.Not.Empty);
        }

        [Test]
        public void AnimalDoesNotHappen()
        {
            var animal = Generate(GenerateAnimal, a => String.IsNullOrEmpty(a));
            Assert.That(animal, Is.Empty);
        }
    }
}
