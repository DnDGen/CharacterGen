using CharacterGen.Generators.Abilities;
using CharacterGen.Generators.Combats;
using CharacterGen.Generators.Items;
using CharacterGen.Generators.Magics;
using CharacterGen.Generators.Randomizers.Stats;
using Ninject;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CharacterGen.Tests.Integration.Stress.Magics
{
    [TestFixture]
    public class SpellsGeneratorTests : StressTests
    {
        [Inject]
        public IAbilitiesGenerator AbilitiesGenerator { get; set; }
        [Inject, Named(StatsRandomizerTypeConstants.Raw)]
        public IStatsRandomizer StatsRandomizer { get; set; }
        [Inject]
        public IEquipmentGenerator EquipmentGenerator { get; set; }
        [Inject]
        public ICombatGenerator CombatGenerator { get; set; }
        [Inject]
        public ISpellsGenerator SpellsGenerator { get; set; }

        [TestCase("SpellsGenerator")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }

        private Dictionary<Int32, IEnumerable<String>> GenerateSpells()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            var race = GetNewRace(alignment, characterClass);
            var baseAttack = CombatGenerator.GenerateBaseAttackWith(characterClass, race);
            var ability = AbilitiesGenerator.GenerateWith(characterClass, race, StatsRandomizer, baseAttack);
            var equipment = EquipmentGenerator.GenerateWith(ability.Feats, characterClass, race);

            return SpellsGenerator.GenerateFrom(characterClass, ability.Feats, equipment);
        }

        protected override void MakeAssertions()
        {
            var spells = GenerateSpells();

            if (spells.Count == 0)
                return;

            foreach (var level in spells.Keys)
                Assert.That(spells[level], Is.Not.Empty);
        }

        [Test]
        public void SpellsHappen()
        {
            var spells = Generate(GenerateSpells, s => s.Count > 0);
            Assert.That(spells, Is.Not.Empty);
        }

        [Test]
        public void SpellsDoNotHappen()
        {
            var spells = Generate(GenerateSpells, s => s.Count == 0);
            Assert.That(spells, Is.Empty);
        }
    }
}
