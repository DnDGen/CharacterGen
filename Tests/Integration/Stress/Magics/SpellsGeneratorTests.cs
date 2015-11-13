using CharacterGen.Common.Magics;
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
        [Inject, Named(AbilitiesGeneratorTypeConstants.Character)]
        public ICombatGenerator CombatGenerator { get; set; }
        [Inject]
        public ISpellsGenerator SpellsGenerator { get; set; }

        [TestCase("SpellsGenerator")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }

        private IEnumerable<Spells> GenerateSpells()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            var race = RaceGenerator.GenerateWith(alignment, characterClass, BaseRaceRandomizer, MetaraceRandomizer);
            var baseAttack = CombatGenerator.GenerateBaseAttackWith(characterClass, race);
            var ability = AbilitiesGenerator.GenerateWith(characterClass, race, StatsRandomizer, baseAttack);

            return SpellsGenerator.GenerateFrom(characterClass, ability.Stats);
        }

        protected override void MakeAssertions()
        {
            var spells = Generate(GenerateSpells, ss => ss.Any());

            foreach (var spellLevel in spells)
            {
                Assert.That(spellLevel.Level, Is.Not.Negative, spellLevel.Level.ToString());
                Assert.That(spellLevel.Quantity, Is.Not.Negative, spellLevel.Level.ToString());
            }
        }
    }
}
