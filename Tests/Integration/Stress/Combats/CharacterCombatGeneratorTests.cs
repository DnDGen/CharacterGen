using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Generators.Abilities;
using CharacterGen.Generators.Combats;
using CharacterGen.Generators.Items;
using CharacterGen.Generators.Randomizers.Stats;
using Ninject;
using NUnit.Framework;
using System;
using System.Linq;

namespace CharacterGen.Tests.Integration.Stress.Combats
{
    [TestFixture]
    public class CharacterCombatGeneratorTests : StressTests
    {
        [Inject, Named(AbilitiesGeneratorTypeConstants.Character)]
        public IAbilitiesGenerator CharacterAbilitiesGenerator { get; set; }
        [Inject, Named(StatsRandomizerTypeConstants.Raw)]
        public IStatsRandomizer StatsRandomizer { get; set; }
        [Inject, Named(CombatGeneratorTypeConstants.Character)]
        public ICombatGenerator CharacterCombatGenerator { get; set; }
        [Inject]
        public IEquipmentGenerator EquipmentGenerator { get; set; }

        [TestCase("Character Combat Generator")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            var race = RaceGenerator.GenerateWith(alignment, characterClass, BaseRaceRandomizer, MetaraceRandomizer);
            var baseAttack = CharacterCombatGenerator.GenerateBaseAttackWith(characterClass, race);
            var ability = CharacterAbilitiesGenerator.GenerateWith(characterClass, race, StatsRandomizer, baseAttack);
            var equipment = EquipmentGenerator.GenerateWith(ability.Feats, characterClass, race);

            var combat = CharacterCombatGenerator.GenerateWith(baseAttack, characterClass, race, ability.Feats, ability.Stats, equipment);
            Assert.That(combat.AdjustedDexterityBonus, Is.AtMost(ability.Stats[StatConstants.Dexterity].Bonus));
            Assert.That(combat.ArmorClass.FlatFooted, Is.Positive);
            Assert.That(combat.ArmorClass.Full, Is.Positive);
            Assert.That(combat.ArmorClass.Touch, Is.Positive);
            Assert.That(combat.BaseAttack, Is.EqualTo(baseAttack));
            Assert.That(combat.HitPoints, Is.AtLeast(characterClass.Level));
            Assert.That(combat.SavingThrows, Is.Not.Null);
        }

        [Test]
        public void StressBaseAttack()
        {
            Stress(AssertBaseAttack);
        }

        private void AssertBaseAttack()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            var race = RaceGenerator.GenerateWith(alignment, characterClass, BaseRaceRandomizer, MetaraceRandomizer);

            var baseAttack = CharacterCombatGenerator.GenerateBaseAttackWith(characterClass, race);
            Assert.That(baseAttack.Bonus, Is.Not.Negative);
            Assert.That(baseAttack.AllBonuses, Contains.Item(baseAttack.Bonus));

            var tempBonus = baseAttack.Bonus;

            while (tempBonus > 0)
            {
                Assert.That(baseAttack.AllBonuses, Contains.Item(tempBonus));
                tempBonus -= 5;
            }

            var expectedCount = (baseAttack.Bonus - 1) / 5 + 1;
            Assert.That(baseAttack.AllBonuses.Count(), Is.EqualTo(expectedCount), baseAttack.Bonus.ToString());
        }
    }
}
