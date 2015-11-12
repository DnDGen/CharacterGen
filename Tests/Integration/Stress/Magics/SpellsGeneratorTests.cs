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
            var spells = GenerateSpells();

            foreach (var spellLevel in spells)
            {
                Assert.That(spellLevel.Level, Is.Not.Negative, spellLevel.Level.ToString());
                Assert.That(spellLevel.Quantity, Is.Not.Negative, spellLevel.Level.ToString());
            }
        }

        [Test]
        public void SpellsHappen()
        {
            var spells = Generate(GenerateSpells, ss => ss.Any());
            Assert.That(spells, Is.Not.Empty);
        }

        [Test]
        public void SpellsDoNotHappen()
        {
            var spells = Generate(GenerateSpells, ss => ss.Any() == false);
            Assert.That(spells, Is.Empty);
        }

        [Test]
        public void DomainSpellsHappen()
        {
            var spells = Generate(GenerateSpells, ss => ss.Any(s => s.HasDomainSpell));

            if (spells.Any(s => s.Level == 0))
            {
                var cantrips = spells.First(s => s.Level == 0);
                Assert.That(cantrips.HasDomainSpell, Is.False);
                spells = spells.Except(new[] { cantrips });
            }

            foreach (var spellLevel in spells)
                Assert.That(spellLevel.HasDomainSpell, Is.True);
        }

        [Test]
        public void DomainSpellsDoNotHappen()
        {
            var spells = Generate(GenerateSpells, ss => ss.All(s => s.HasDomainSpell == false) && ss.Any());

            Assert.That(spells, Is.Not.Empty);
            foreach (var spellLevel in spells)
                Assert.That(spellLevel.HasDomainSpell, Is.False);
        }
    }
}
