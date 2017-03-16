using CharacterGen.Abilities.Feats;
using CharacterGen.Abilities.Stats;
using CharacterGen.CharacterClasses;
using CharacterGen.Characters;
using CharacterGen.Magics;
using CharacterGen.Races;
using CharacterGen.Randomizers.CharacterClasses;
using CharacterGen.Randomizers.Races;
using CharacterGen.Randomizers.Stats;
using Ninject;
using NUnit.Framework;
using System;
using System.Linq;

namespace CharacterGen.Tests.Integration.Stress.Characters
{
    [TestFixture]
    public class CharacterGeneratorTests : StressTests
    {
        [Inject]
        public ICharacterGenerator CharacterGenerator { get; set; }
        [Inject, Named(StatsRandomizerTypeConstants.Raw)]
        public IStatsRandomizer RawStatsRandomizer { get; set; }
        [Inject, Named(ClassNameRandomizerTypeConstants.AnyNPC)]
        public IClassNameRandomizer NPCClassNameRandomizer { get; set; }
        [Inject, Named(StatsRandomizerTypeConstants.Heroic)]
        public IStatsRandomizer HeroicStatsRandomizer { get; set; }
        [Inject, Named(ClassNameRandomizerTypeConstants.Spellcaster)]
        public IClassNameRandomizer SpellcasterClassNameRandomizer { get; set; }
        [Inject]
        public ISetClassNameRandomizer SetClassNameRandomizer { get; set; }
        [Inject]
        public ISetLevelRandomizer SetLevelRandomizer { get; set; }
        [Inject]
        public ISetBaseRaceRandomizer SetBaseRaceRandomizer { get; set; }
        [Inject]
        public ISetMetaraceRandomizer SetMetaraceRandomizer { get; set; }
        [Inject]
        public CharacterVerifier CharacterVerifier { get; set; }
        [Inject]
        public Random Random { get; set; }

        [Test]
        public void StressCharacter()
        {
            Stress(AssertCharacter);
        }

        protected void AssertCharacter()
        {
            var character = CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer, MetaraceRandomizer, RawStatsRandomizer);

            CharacterVerifier.AssertCharacter(character);
            Assert.That(character.Equipment.Treasure.Items, Is.Not.Empty);
        }

        [Test]
        public void StressNPC()
        {
            Stress(AssertNPC);
        }

        private void AssertNPC()
        {
            var npc = CharacterGenerator.GenerateWith(AlignmentRandomizer, NPCClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer, MetaraceRandomizer, RawStatsRandomizer);

            CharacterVerifier.AssertCharacter(npc);
            Assert.That(npc.Class.IsNPC, Is.True);
        }

        [Test]
        public void BUG_StressFirstLevelCommoner()
        {
            Stress(AssertFirstLevelCommoner);
        }

        private void AssertFirstLevelCommoner()
        {
            SetClassNameRandomizer.SetClassName = CharacterClassConstants.Commoner;
            SetLevelRandomizer.SetLevel = 1;

            var commoner = CharacterGenerator.GenerateWith(AlignmentRandomizer, SetClassNameRandomizer, SetLevelRandomizer, BaseRaceRandomizer, MetaraceRandomizer, RawStatsRandomizer);

            CharacterVerifier.AssertCharacter(commoner);
            Assert.That(commoner.Class.IsNPC, Is.True);
            Assert.That(commoner.Class.Level, Is.EqualTo(1));
            Assert.That(commoner.Class.EffectiveLevel, Is.EqualTo(.5), commoner.Race.BaseRace + commoner.Race.Metarace);
            Assert.That(commoner.Class.Name, Is.EqualTo(CharacterClassConstants.Commoner));
            Assert.That(commoner.Class.ProhibitedFields, Is.Empty);
            Assert.That(commoner.Class.SpecialistFields, Is.Empty);
            Assert.That(commoner.Race.Metarace, Is.EqualTo(RaceConstants.Metaraces.None));
            Assert.That(commoner.Race.MetaraceSpecies, Is.Empty);
            Assert.That(commoner.ChallengeRating, Is.EqualTo(.5), commoner.Race.BaseRace + commoner.Race.Metarace);
            Assert.That(commoner.Magic.Animal, Is.Empty);
            Assert.That(commoner.Magic.ArcaneSpellFailure, Is.EqualTo(0));
            Assert.That(commoner.Magic.KnownSpells, Is.Empty);
            Assert.That(commoner.Magic.PreparedSpells, Is.Empty);
            Assert.That(commoner.Magic.SpellsPerDay, Is.Empty);
        }

        [Test]
        public void StressSpellcaster()
        {
            Stress(AssertSpellcaster);
        }

        private void AssertSpellcaster()
        {
            var spellcaster = Generate(() => CharacterGenerator.GenerateWith(AlignmentRandomizer, SpellcasterClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer, MetaraceRandomizer, HeroicStatsRandomizer),
                c => c.Class.Level > 3);

            CharacterVerifier.AssertCharacter(spellcaster);
            Assert.That(spellcaster.Equipment.Treasure.Items, Is.Not.Empty);

            AssertSpellcaster(spellcaster);
        }

        private void AssertSpellcaster(Character spellcaster)
        {
            Assert.That(spellcaster.Magic, Is.Not.Null);
            Assert.That(spellcaster.Magic.Animal, Is.Not.Null);
            Assert.That(spellcaster.Magic.ArcaneSpellFailure, Is.InRange(0, 100));

            Assert.That(spellcaster.Magic.SpellsPerDay, Is.Not.Empty, spellcaster.Class.Name);

            var levelsAndSources = spellcaster.Magic.SpellsPerDay.Select(s => s.Source + s.Level);
            Assert.That(levelsAndSources, Is.Unique);

            var spellsPerDayLevels = spellcaster.Magic.SpellsPerDay.Select(s => s.Level);
            var maxSpellLevel = spellsPerDayLevels.Max();
            var minSpellLevel = spellsPerDayLevels.Min();

            Assert.That(minSpellLevel, Is.InRange(0, 1));
            Assert.That(maxSpellLevel, Is.InRange(0, 9));

            foreach (var spellQuantity in spellcaster.Magic.SpellsPerDay)
            {
                Assert.That(spellQuantity.Level, Is.InRange(minSpellLevel, maxSpellLevel));
                Assert.That(spellQuantity.Quantity, Is.Not.Negative);
                Assert.That(spellQuantity.Source, Is.Not.Empty);

                if (spellQuantity.HasDomainSpell == false)
                    Assert.That(spellQuantity.Quantity, Is.Positive);

                if (spellQuantity.Level > 0 && spellQuantity.Source == spellcaster.Class.Name)
                    Assert.That(spellQuantity.HasDomainSpell, Is.EqualTo(spellcaster.Class.SpecialistFields.Any()));
                else
                    Assert.That(spellQuantity.HasDomainSpell, Is.False);
            }

            Assert.That(spellcaster.Magic.KnownSpells, Is.Not.Empty, spellcaster.Class.Name);

            //INFO: Adding 1 to max spell, because you might know a spell that you cannot yet cast
            var maxKnownSpellLevel = Math.Min(9, maxSpellLevel + 1);

            foreach (var knownSpell in spellcaster.Magic.KnownSpells)
                AssertSpell(knownSpell, minSpellLevel, maxKnownSpellLevel);

            if (spellcaster.Magic.SpellsPerDay.All(s => s.Source == CharacterClassConstants.Bard || s.Source == CharacterClassConstants.Sorcerer))
            {
                Assert.That(spellcaster.Magic.PreparedSpells, Is.Empty, spellcaster.Class.Name);
            }
            else
            {
                Assert.That(spellcaster.Magic.PreparedSpells, Is.Not.Empty, spellcaster.Class.Name);

                foreach (var preparedSpell in spellcaster.Magic.PreparedSpells)
                    AssertSpell(preparedSpell, minSpellLevel, maxSpellLevel);
            }
        }

        private void AssertSpell(Spell spell, int minSpellLevel, int maxSpellLevel)
        {
            Assert.That(spell.Name, Is.Not.Empty);
            Assert.That(spell.Source, Is.Not.Empty, spell.Name);
            Assert.That(spell.Level, Is.InRange(minSpellLevel, maxSpellLevel), spell.Source + spell.Name);
            Assert.That(spell.Metamagic, Is.Empty, spell.Source + spell.Name);
        }

        [Test]
        public void BUG_StressRakshasaSpellcaster()
        {
            Stress(AssertRakshasaSpellcaster);
        }

        private void AssertRakshasaSpellcaster()
        {
            SetBaseRaceRandomizer.SetBaseRace = RaceConstants.BaseRaces.Rakshasa;

            var spellcaster = Generate(
                () => CharacterGenerator.GenerateWith(AlignmentRandomizer, SpellcasterClassNameRandomizer, LevelRandomizer, SetBaseRaceRandomizer, MetaraceRandomizer, HeroicStatsRandomizer),
                c => true);

            CharacterVerifier.AssertCharacter(spellcaster);
            Assert.That(spellcaster.Equipment.Treasure.Items, Is.Not.Empty);
            Assert.That(spellcaster.Race.BaseRace, Is.EqualTo(RaceConstants.BaseRaces.Rakshasa));

            AssertSpellcaster(spellcaster);
        }

        [Test]
        public void BUG_StressUndeadCharacter()
        {
            Stress(AssertUndead);
        }

        private void AssertUndead()
        {
            var undeadMetaraceRandomizer = GetNewInstanceOf<IForcableMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.UndeadMeta);
            undeadMetaraceRandomizer.ForceMetarace = true;

            var character = CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer, undeadMetaraceRandomizer, RawStatsRandomizer);

            CharacterVerifier.AssertCharacter(character);
            Assert.That(character.Race.Metarace, Is.EqualTo(RaceConstants.Metaraces.Ghost)
                .Or.EqualTo(RaceConstants.Metaraces.Lich)
                .Or.EqualTo(RaceConstants.Metaraces.Mummy)
                .Or.EqualTo(RaceConstants.Metaraces.Vampire));
            Assert.That(character.Race.ChallengeRating, Is.Positive, character.Summary);
            Assert.That(character.Equipment.Treasure.Items, Is.Not.Empty, character.Summary);
            Assert.That(character.Ability.Stats.Keys, Is.All.Not.EqualTo(StatConstants.Constitution), character.Summary);
            Assert.That(character.Combat.SavingThrows.HasFortitudeSave, Is.False, character.Summary);
        }

        [Test]
        public void BUG_StressPlanetouchedCharacter()
        {
            Stress(AssertPlanetouched);
        }

        private void AssertPlanetouched()
        {
            var planetouched = new[] { RaceConstants.BaseRaces.Aasimar, RaceConstants.BaseRaces.Tiefling };
            var randomIndex = Random.Next(2);
            SetBaseRaceRandomizer.SetBaseRace = planetouched[randomIndex];

            var character = CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, SetBaseRaceRandomizer, MetaraceRandomizer, RawStatsRandomizer);

            CharacterVerifier.AssertCharacter(character);
            Assert.That(character.Race.BaseRace, Is.EqualTo(RaceConstants.BaseRaces.Aasimar).Or.EqualTo(RaceConstants.BaseRaces.Tiefling));
            Assert.That(character.Equipment.Treasure.Items, Is.Not.Empty, character.Summary);
            Assert.That(character.Class.LevelAdjustment, Is.Positive);
        }

        [Test]
        public void BUG_StressGhost()
        {
            Stress(AssertGhost);
        }

        private void AssertGhost()
        {
            SetMetaraceRandomizer.SetMetarace = RaceConstants.Metaraces.Ghost;

            var character = CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer, SetMetaraceRandomizer, RawStatsRandomizer);

            CharacterVerifier.AssertCharacter(character);
            Assert.That(character.Equipment.Treasure.Items, Is.Not.Empty, character.Summary);
            Assert.That(character.Race.Metarace, Is.EqualTo(RaceConstants.Metaraces.Ghost));
            Assert.That(character.Race.AerialSpeed.Value, Is.Positive);
            Assert.That(character.Race.AerialSpeed.Description, Is.Not.Empty);

            var ghostSpecialAttacks = new[]
            {
                FeatConstants.CorruptingGaze,
                FeatConstants.CorruptingTouch,
                FeatConstants.DrainingTouch,
                FeatConstants.FrightfulMoan,
                FeatConstants.HorrificAppearance,
                FeatConstants.Malevolence,
                FeatConstants.Telekinesis,
            };

            var featNames = character.Ability.Feats.Select(f => f.Name);
            var ghostSpecialAttackFeats = featNames.Intersect(ghostSpecialAttacks);
            var ghostSpecialAttackFeat = character.Ability.Feats.Single(f => f.Name == FeatConstants.GhostSpecialAttack);

            Assert.That(ghostSpecialAttackFeats.Count, Is.EqualTo(ghostSpecialAttackFeat.Foci.Count()));
            Assert.That(ghostSpecialAttackFeats.Count, Is.InRange(1, 3));
        }
    }
}