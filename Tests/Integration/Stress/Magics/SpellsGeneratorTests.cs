using CharacterGen.Generators.Abilities;
using CharacterGen.Generators.Combats;
using CharacterGen.Generators.Magics;
using CharacterGen.Generators.Randomizers.Stats;
using Ninject;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Tests.Integration.Stress.Magics
{
    [TestFixture]
    public class SpellsGeneratorTests : StressTests
    {
        [Inject, Named(AbilitiesGeneratorTypeConstants.Character)]
        public IAbilitiesGenerator AbilitiesGenerator { get; set; }
        [Inject, Named(StatsRandomizerTypeConstants.Raw)]
        public IStatsRandomizer StatsRandomizer { get; set; }
        //[Inject]
        //public IEquipmentGenerator EquipmentGenerator { get; set; }
        [Inject, Named(AbilitiesGeneratorTypeConstants.Character)]
        public ICombatGenerator CombatGenerator { get; set; }
        [Inject]
        public ISpellsGenerator SpellsGenerator { get; set; }

        [TestCase("SpellsGenerator")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }

        private Dictionary<Int32, Int32> GenerateSpells()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            var race = GetNewRace(alignment, characterClass);
            var baseAttack = CombatGenerator.GenerateBaseAttackWith(characterClass, race);
            var ability = AbilitiesGenerator.GenerateWith(characterClass, race, StatsRandomizer, baseAttack);

            return SpellsGenerator.GenerateFrom(characterClass, ability.Stats);
        }

        protected override void MakeAssertions()
        {
            var spells = GenerateSpells();

            foreach (var spellLevel in spells.Keys)
                Assert.That(spells[spellLevel], Is.Positive);
        }

        [Test]
        public void SpellsHappen()
        {
            var spells = Generate(GenerateSpells, s => s.Any());
            Assert.That(spells, Is.Not.Empty);
        }

        [Test]
        public void SpellsDoNotHappen()
        {
            var spells = Generate(GenerateSpells, s => s.Any() == false);
            Assert.That(spells, Is.Empty);
        }
    }
}
