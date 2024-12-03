using DnDGen.CharacterGen.Abilities;
using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Characters;
using DnDGen.CharacterGen.Feats;
using DnDGen.CharacterGen.Generators.Characters;
using DnDGen.CharacterGen.Magics;
using DnDGen.CharacterGen.Races;
using DnDGen.CharacterGen.Randomizers.Abilities;
using DnDGen.CharacterGen.Randomizers.Alignments;
using DnDGen.CharacterGen.Randomizers.CharacterClasses;
using DnDGen.CharacterGen.Randomizers.Races;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.TreasureGen.Items;
using NUnit.Framework;
using System;
using System.Linq;

namespace DnDGen.CharacterGen.Tests.Integration.Generators.Characters
{
    internal class CharacterGeneratorTests : IntegrationTests
    {
        private ICharacterGenerator characterGenerator;
        private ICollectionSelector collectionSelector;

        [SetUp]
        public void Setup()
        {
            characterGenerator = GetNewInstanceOf<ICharacterGenerator>();
            collectionSelector = GetNewInstanceOf<ICollectionSelector>();
        }

        [Test]
        public void BUG_GenerateWith_ReturnsCharacter_WithoutMetarace()
        {
            var abilitiesRandomizer = GetNewInstanceOf<IAbilitiesRandomizer>(AbilitiesRandomizerTypeConstants.Raw);
            var metaraceRandomizer = GetNewInstanceOf<RaceRandomizer>(RaceRandomizerTypeConstants.Metarace.AnyMeta);
            var baseRaceRandomizer = GetNewInstanceOf<RaceRandomizer>(RaceRandomizerTypeConstants.BaseRace.AnyBase);
            var levelRandomizer = GetNewInstanceOf<ILevelRandomizer>(LevelRandomizerTypeConstants.Any);
            var classNameRandomizer = GetNewInstanceOf<IClassNameRandomizer>(ClassNameRandomizerTypeConstants.AnyPlayer);
            var alignmentRandomizer = GetNewInstanceOf<IAlignmentRandomizer>(AlignmentRandomizerTypeConstants.Any);

            //INFO: will try 2 times to see if we get a character without metarace. Should happen at least once, if not more than once
            var hasMeta = true;
            var attempts = 2;

            while (attempts-- > 0 && hasMeta)
            {
                var character = characterGenerator.GenerateWith(
                    alignmentRandomizer,
                    classNameRandomizer,
                    levelRandomizer,
                    baseRaceRandomizer,
                    metaraceRandomizer,
                    abilitiesRandomizer);
                Assert.That(character, Is.Not.Null);
                Assert.That(character.Summary, Is.Not.Empty);
                Assert.That(character.Alignment.Full, Is.Not.Empty);
                Assert.That(character.Class.Level, Is.AtLeast(1));
                Assert.That(character.Class.Summary, Is.Not.Empty);
                Assert.That(character.Race.Summary, Is.Not.Empty);

                hasMeta = character.Race.Metarace != RaceConstants.Metaraces.None;
            }

            Assert.That(hasMeta, Is.False);
        }

        [Repeat(1000)]
        [Test]
        public void BUG_GenerateWith_ReturnsCharacter_SpecialistWizard()
        {
            var abilitiesRandomizer = GetNewInstanceOf<IAbilitiesRandomizer>(AbilitiesRandomizerTypeConstants.Raw);
            var metaraceRandomizer = GetNewInstanceOf<RaceRandomizer>(RaceRandomizerTypeConstants.Metarace.NoMeta);
            var alignmentRandomizer = GetNewInstanceOf<IAlignmentRandomizer>(AlignmentRandomizerTypeConstants.Any);

            var baseRaceRandomizer = GetNewInstanceOf<ISetBaseRaceRandomizer>();
            baseRaceRandomizer.SetBaseRace = RaceConstants.BaseRaces.Human;

            var levelRandomizer = GetNewInstanceOf<ISetLevelRandomizer>();
            levelRandomizer.SetLevel = 1;

            var classNameRandomizer = GetNewInstanceOf<ISetClassNameRandomizer>();
            classNameRandomizer.SetClassName = CharacterClassConstants.Wizard;

            var character = characterGenerator.GenerateWith(
                alignmentRandomizer,
                classNameRandomizer,
                levelRandomizer,
                baseRaceRandomizer,
                metaraceRandomizer,
                abilitiesRandomizer);
            Assert.That(character, Is.Not.Null);
            Assert.That(character.Summary, Is.Not.Empty);

            if (character.Class.SpecialistFields.Any())
            {
                var message = $"{character.Summary}; S: {string.Join(", ", character.Class.SpecialistFields)}; P: {string.Join(", ", character.Class.ProhibitedFields)}";
                Assert.That(character.Class.SpecialistFields.Count(), Is.EqualTo(1), message);

                var intersect = character.Class.SpecialistFields.Intersect(character.Class.ProhibitedFields);
                Assert.That(intersect, Is.Empty, message);

                if (character.Class.SpecialistFields.First() == CharacterClassConstants.Schools.Divination)
                {
                    Assert.That(character.Class.ProhibitedFields.Count(), Is.EqualTo(1), message);
                }
                else
                {
                    Assert.That(character.Class.ProhibitedFields.Count(), Is.EqualTo(2), message);
                }
            }
        }

        //INFO: Sometimes a first-level commoner only knows unarmed strike. Other times, she only knows one weapon (melee or ranged), and is unable to generate the other
        [Repeat(100)]
        [Test]
        public void BUG_GenerateFirstLevelCommoner()
        {
            var abilitiesRandomizer = GetNewInstanceOf<IAbilitiesRandomizer>(AbilitiesRandomizerTypeConstants.Raw);
            var metaraceRandomizer = GetNewInstanceOf<RaceRandomizer>(RaceRandomizerTypeConstants.Metarace.NoMeta);
            var alignmentRandomizer = GetNewInstanceOf<IAlignmentRandomizer>(AlignmentRandomizerTypeConstants.Any);
            var baseRaceRandomizer = GetNewInstanceOf<RaceRandomizer>(RaceRandomizerTypeConstants.BaseRace.AnyBase);

            var levelRandomizer = GetNewInstanceOf<ISetLevelRandomizer>();
            levelRandomizer.SetLevel = 1;

            var classNameRandomizer = GetNewInstanceOf<ISetClassNameRandomizer>();
            classNameRandomizer.SetClassName = CharacterClassConstants.Commoner;

            var commoner = characterGenerator.GenerateWith(
                alignmentRandomizer,
                classNameRandomizer,
                levelRandomizer,
                baseRaceRandomizer,
                metaraceRandomizer,
                abilitiesRandomizer);

            Assert.That(commoner.Class.IsNPC, Is.True, commoner.Summary);
            Assert.That(commoner.Class.Level, Is.EqualTo(1), commoner.Summary);
            Assert.That(commoner.Class.Name, Is.EqualTo(CharacterClassConstants.Commoner), commoner.Summary);
            Assert.That(commoner.Class.ProhibitedFields, Is.Empty, commoner.Summary);
            Assert.That(commoner.Class.SpecialistFields, Is.Empty, commoner.Summary);
            Assert.That(commoner.Race.Metarace, Is.EqualTo(RaceConstants.Metaraces.None), commoner.Summary);
            Assert.That(commoner.Race.MetaraceSpecies, Is.Empty, commoner.Summary);
            Assert.That(commoner.Magic.Animal, Is.Empty, commoner.Summary);
            Assert.That(commoner.Magic.ArcaneSpellFailure, Is.Zero, commoner.Summary);
            Assert.That(commoner.Magic.KnownSpells, Is.Empty, commoner.Summary);
            Assert.That(commoner.Magic.PreparedSpells, Is.Empty, commoner.Summary);
            Assert.That(commoner.Magic.SpellsPerDay, Is.Empty, commoner.Summary);
        }

        [Repeat(100)]
        [Test]
        public void BUG_GenerateHighLevelFighter()
        {
            var abilitiesRandomizer = GetNewInstanceOf<IAbilitiesRandomizer>(AbilitiesRandomizerTypeConstants.Raw);
            var metaraceRandomizer = GetNewInstanceOf<RaceRandomizer>(RaceRandomizerTypeConstants.Metarace.NoMeta);
            var alignmentRandomizer = GetNewInstanceOf<IAlignmentRandomizer>(AlignmentRandomizerTypeConstants.Any);
            var baseRaceRandomizer = GetNewInstanceOf<RaceRandomizer>(RaceRandomizerTypeConstants.BaseRace.AnyBase);

            var levelRandomizer = GetNewInstanceOf<ISetLevelRandomizer>();
            levelRandomizer.SetLevel = 20;

            var classNameRandomizer = GetNewInstanceOf<ISetClassNameRandomizer>();
            classNameRandomizer.SetClassName = CharacterClassConstants.Fighter;

            var fighter = characterGenerator.GenerateWith(
                alignmentRandomizer,
                classNameRandomizer,
                levelRandomizer,
                baseRaceRandomizer,
                metaraceRandomizer,
                abilitiesRandomizer);

            Assert.That(fighter.Class.IsNPC, Is.False, fighter.Summary);
            Assert.That(fighter.Class.Level, Is.EqualTo(20), fighter.Summary);
            Assert.That(fighter.Class.Name, Is.EqualTo(CharacterClassConstants.Fighter), fighter.Summary);
            Assert.That(fighter.Class.ProhibitedFields, Is.Empty, fighter.Summary);
            Assert.That(fighter.Class.SpecialistFields, Is.Empty, fighter.Summary);
            Assert.That(fighter.Magic.Animal, Is.Empty, fighter.Summary);
            Assert.That(fighter.Magic.ArcaneSpellFailure, Is.EqualTo(0), fighter.Summary);
            Assert.That(fighter.Magic.KnownSpells, Is.Empty, fighter.Summary);
            Assert.That(fighter.Magic.PreparedSpells, Is.Empty, fighter.Summary);
            Assert.That(fighter.Magic.SpellsPerDay, Is.Empty, fighter.Summary);
            Assert.That(fighter.Equipment.Armor, Is.Not.Null, fighter.Summary + " armor");
            Assert.That(fighter.Equipment.PrimaryHand, Is.Not.Null, fighter.Summary + " primary hand");
        }

        //INFO: The bug here is that the rare size (Huge) makes the equipment take much longer to generate
        [Repeat(100)]
        [Test]
        public void BUG_GenerateStormGiant()
        {
            var abilitiesRandomizer = GetNewInstanceOf<IAbilitiesRandomizer>(AbilitiesRandomizerTypeConstants.Raw);
            var metaraceRandomizer = GetNewInstanceOf<RaceRandomizer>(RaceRandomizerTypeConstants.Metarace.NoMeta);
            var alignmentRandomizer = GetNewInstanceOf<IAlignmentRandomizer>(AlignmentRandomizerTypeConstants.Any);
            var levelRandomizer = GetNewInstanceOf<ILevelRandomizer>(LevelRandomizerTypeConstants.Any);
            var classNameRandomizer = GetNewInstanceOf<IClassNameRandomizer>(ClassNameRandomizerTypeConstants.AnyPlayer);

            var baseRaceRandomizer = GetNewInstanceOf<ISetBaseRaceRandomizer>();
            baseRaceRandomizer.SetBaseRace = RaceConstants.BaseRaces.StormGiant;

            var stormGiant = characterGenerator.GenerateWith(
                alignmentRandomizer,
                classNameRandomizer,
                levelRandomizer,
                baseRaceRandomizer,
                metaraceRandomizer,
                abilitiesRandomizer);

            Assert.That(stormGiant.Class.IsNPC, Is.False, stormGiant.Summary);
            Assert.That(stormGiant.Race.BaseRace, Is.EqualTo(RaceConstants.BaseRaces.StormGiant), stormGiant.Summary);
            Assert.That(stormGiant.Race.Size, Is.EqualTo(RaceConstants.Sizes.Huge), stormGiant.Summary);
            Assert.That(stormGiant.Equipment.Armor, Is.Not.Null, stormGiant.Summary + " armor");
            Assert.That(stormGiant.Equipment.Armor.Size, Is.EqualTo(stormGiant.Race.Size), stormGiant.Summary);
            Assert.That(stormGiant.Equipment.PrimaryHand, Is.Not.Null, stormGiant.Summary + " primary hand");
            Assert.That(stormGiant.Equipment.PrimaryHand.Size, Is.EqualTo(stormGiant.Race.Size), stormGiant.Summary);

            if (stormGiant.Equipment.OffHand != null && stormGiant.Equipment.OffHand is Weapon)
            {
                var weapon = stormGiant.Equipment.OffHand as Weapon;
                Assert.That(weapon.Size, Is.EqualTo(stormGiant.Race.Size), stormGiant.Summary);
            }
            else if (stormGiant.Equipment.OffHand != null && stormGiant.Equipment.OffHand is Armor)
            {
                var armor = stormGiant.Equipment.OffHand as Armor;
                Assert.That(armor.Attributes, Contains.Item(AttributeConstants.Shield), stormGiant.Summary);
                Assert.That(armor.Size, Is.EqualTo(stormGiant.Race.Size), stormGiant.Summary);
            }
        }

        //INFO: Want to verify rakshasas have native sorcerer spells and additional spellcaster class spells
        [Repeat(100)]
        [Test]
        public void BUG_GenerateRakshasaSpellcaster()
        {
            var abilitiesRandomizer = GetNewInstanceOf<IAbilitiesRandomizer>(AbilitiesRandomizerTypeConstants.Heroic);
            var metaraceRandomizer = GetNewInstanceOf<RaceRandomizer>(RaceRandomizerTypeConstants.Metarace.NoMeta);
            var alignmentRandomizer = GetNewInstanceOf<IAlignmentRandomizer>(AlignmentRandomizerTypeConstants.Any);
            var levelRandomizer = GetNewInstanceOf<ILevelRandomizer>(LevelRandomizerTypeConstants.Any);
            var classNameRandomizer = GetNewInstanceOf<IClassNameRandomizer>(ClassNameRandomizerTypeConstants.Spellcaster);

            var baseRaceRandomizer = GetNewInstanceOf<ISetBaseRaceRandomizer>();
            baseRaceRandomizer.SetBaseRace = RaceConstants.BaseRaces.Rakshasa;

            var spellcaster = characterGenerator.GenerateWith(
                alignmentRandomizer,
                classNameRandomizer,
                levelRandomizer,
                baseRaceRandomizer,
                metaraceRandomizer,
                abilitiesRandomizer);

            AssertSpellcaster(spellcaster);

            Assert.That(spellcaster.Class.IsNPC, Is.False, spellcaster.Summary);
            Assert.That(spellcaster.Race.BaseRace, Is.EqualTo(RaceConstants.BaseRaces.Rakshasa));
            //INFO: Rakshasa are 7th-level Sorcerers and can cast up to 4th-level spells
            Assert.That(spellcaster.Magic.SpellsPerDay.Any(q => q.Source == CharacterClassConstants.Sorcerer && q.Level == 0), Is.True, spellcaster.Summary);
            Assert.That(spellcaster.Magic.SpellsPerDay.Any(q => q.Source == CharacterClassConstants.Sorcerer && q.Level == 1), Is.True, spellcaster.Summary);
            Assert.That(spellcaster.Magic.SpellsPerDay.Any(q => q.Source == CharacterClassConstants.Sorcerer && q.Level == 2), Is.True, spellcaster.Summary);
            Assert.That(spellcaster.Magic.SpellsPerDay.Any(q => q.Source == CharacterClassConstants.Sorcerer && q.Level == 3), Is.True, spellcaster.Summary);
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

        //INFO: Want to verify rakshasas have native sorcerer spells and additional sorcerer spells at 7 levels higher, even when high level
        [Repeat(100)]
        [Test]
        public void BUG_GenerateHighLevelRakshasaSorcerer()
        {
            var abilitiesRandomizer = GetNewInstanceOf<IAbilitiesRandomizer>(AbilitiesRandomizerTypeConstants.Heroic);
            var metaraceRandomizer = GetNewInstanceOf<RaceRandomizer>(RaceRandomizerTypeConstants.Metarace.NoMeta);
            var alignmentRandomizer = GetNewInstanceOf<IAlignmentRandomizer>(AlignmentRandomizerTypeConstants.Any);
            var levelRandomizer = GetNewInstanceOf<ILevelRandomizer>(LevelRandomizerTypeConstants.VeryHigh);

            var classNameRandomizer = GetNewInstanceOf<ISetClassNameRandomizer>();
            classNameRandomizer.SetClassName = CharacterClassConstants.Sorcerer;

            var baseRaceRandomizer = GetNewInstanceOf<ISetBaseRaceRandomizer>();
            baseRaceRandomizer.SetBaseRace = RaceConstants.BaseRaces.Rakshasa;

            var sorcerer = characterGenerator.GenerateWith(
                alignmentRandomizer,
                classNameRandomizer,
                levelRandomizer,
                baseRaceRandomizer,
                metaraceRandomizer,
                abilitiesRandomizer);

            AssertSpellcaster(sorcerer);

            Assert.That(sorcerer.Class.IsNPC, Is.False, sorcerer.Summary);
            Assert.That(sorcerer.Race.BaseRace, Is.EqualTo(RaceConstants.BaseRaces.Rakshasa), sorcerer.Summary);
            Assert.That(sorcerer.Class.Name, Is.EqualTo(CharacterClassConstants.Sorcerer), sorcerer.Summary);
            Assert.That(sorcerer.Class.Level, Is.AtLeast(16), sorcerer.Summary);
            Assert.That(sorcerer.Class.EffectiveLevel, Is.AtLeast(18), sorcerer.Summary);
            Assert.That(sorcerer.Magic.SpellsPerDay.Any(q => q.Source == CharacterClassConstants.Sorcerer && q.Level == 0), Is.True, sorcerer.Summary);
            Assert.That(sorcerer.Magic.SpellsPerDay.Any(q => q.Source == CharacterClassConstants.Sorcerer && q.Level == 1), Is.True, sorcerer.Summary);
            Assert.That(sorcerer.Magic.SpellsPerDay.Any(q => q.Source == CharacterClassConstants.Sorcerer && q.Level == 2), Is.True, sorcerer.Summary);
            Assert.That(sorcerer.Magic.SpellsPerDay.Any(q => q.Source == CharacterClassConstants.Sorcerer && q.Level == 3), Is.True, sorcerer.Summary);
            Assert.That(sorcerer.Magic.SpellsPerDay.Any(q => q.Source == CharacterClassConstants.Sorcerer && q.Level == 4), Is.True, sorcerer.Summary);
            Assert.That(sorcerer.Magic.SpellsPerDay.Any(q => q.Source == CharacterClassConstants.Sorcerer && q.Level == 5), Is.True, sorcerer.Summary);
            Assert.That(sorcerer.Magic.SpellsPerDay.Any(q => q.Source == CharacterClassConstants.Sorcerer && q.Level == 6), Is.True, sorcerer.Summary);
            Assert.That(sorcerer.Magic.SpellsPerDay.Any(q => q.Source == CharacterClassConstants.Sorcerer && q.Level == 7), Is.True, sorcerer.Summary);
            Assert.That(sorcerer.Magic.SpellsPerDay.Any(q => q.Source == CharacterClassConstants.Sorcerer && q.Level == 8), Is.True, sorcerer.Summary);
            Assert.That(sorcerer.Magic.SpellsPerDay.Any(q => q.Source == CharacterClassConstants.Sorcerer && q.Level == 9), Is.True, sorcerer.Summary);
        }

        [Repeat(100)]
        [Test]
        public void BUG_GenerateUndeadCharacter()
        {
            var abilitiesRandomizer = GetNewInstanceOf<IAbilitiesRandomizer>(AbilitiesRandomizerTypeConstants.Raw);
            var metaraceRandomizer = GetNewInstanceOf<IForcableMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.UndeadMeta);
            var alignmentRandomizer = GetNewInstanceOf<IAlignmentRandomizer>(AlignmentRandomizerTypeConstants.Any);
            var levelRandomizer = GetNewInstanceOf<ILevelRandomizer>(LevelRandomizerTypeConstants.Any);
            var classNameRandomizer = GetNewInstanceOf<IClassNameRandomizer>(ClassNameRandomizerTypeConstants.AnyPlayer);
            var baseRaceRandomizer = GetNewInstanceOf<RaceRandomizer>(RaceRandomizerTypeConstants.BaseRace.AnyBase);

            metaraceRandomizer.ForceMetarace = true;

            var character = characterGenerator.GenerateWith(
                alignmentRandomizer,
                classNameRandomizer,
                levelRandomizer,
                baseRaceRandomizer,
                metaraceRandomizer,
                abilitiesRandomizer);

            Assert.That(character.Class.IsNPC, Is.False, character.Summary);
            AssertUndead(character);
        }

        private void AssertUndead(Character character)
        {
            Assert.That(character.Race.Metarace, Is.EqualTo(RaceConstants.Metaraces.Ghost)
                .Or.EqualTo(RaceConstants.Metaraces.Lich)
                .Or.EqualTo(RaceConstants.Metaraces.Mummy)
                .Or.EqualTo(RaceConstants.Metaraces.Vampire));
            Assert.That(character.Race.ChallengeRating, Is.Positive, character.Summary);
            Assert.That(character.Abilities.Keys, Is.All.Not.EqualTo(AbilityConstants.Constitution), character.Summary);
            Assert.That(character.Combat.SavingThrows.HasFortitudeSave, Is.False, character.Summary);
        }

        [Repeat(100)]
        [Test]
        public void BUG_GeneratePlanetouchedCharacter()
        {
            var abilitiesRandomizer = GetNewInstanceOf<IAbilitiesRandomizer>(AbilitiesRandomizerTypeConstants.Raw);
            var metaraceRandomizer = GetNewInstanceOf<RaceRandomizer>(RaceRandomizerTypeConstants.Metarace.NoMeta);
            var alignmentRandomizer = GetNewInstanceOf<IAlignmentRandomizer>(AlignmentRandomizerTypeConstants.Any);
            var levelRandomizer = GetNewInstanceOf<ILevelRandomizer>(LevelRandomizerTypeConstants.Any);
            var classNameRandomizer = GetNewInstanceOf<IClassNameRandomizer>(ClassNameRandomizerTypeConstants.Spellcaster);

            var baseRaceRandomizer = GetNewInstanceOf<ISetBaseRaceRandomizer>();
            var planetouched = new[] { RaceConstants.BaseRaces.Aasimar, RaceConstants.BaseRaces.Tiefling };
            baseRaceRandomizer.SetBaseRace = collectionSelector.SelectRandomFrom(planetouched);

            var character = characterGenerator.GenerateWith(
                alignmentRandomizer,
                classNameRandomizer,
                levelRandomizer,
                baseRaceRandomizer,
                metaraceRandomizer,
                abilitiesRandomizer);

            Assert.That(character.Class.IsNPC, Is.False, character.Summary);
            Assert.That(character.Race.BaseRace, Is.EqualTo(RaceConstants.BaseRaces.Aasimar).Or.EqualTo(RaceConstants.BaseRaces.Tiefling));
            Assert.That(character.Class.LevelAdjustment, Is.Positive);
        }

        [Repeat(100)]
        [Test]
        public void BUG_GenerateGhost()
        {
            var character = GetGhost();

            Assert.That(character.Class.IsNPC, Is.False, character.Summary);
            AssertGhost(character);
        }

        private void AssertGhost(Character character)
        {
            AssertUndead(character);

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

            var featNames = character.Feats.All.Select(f => f.Name);
            var ghostSpecialAttackFeats = featNames.Intersect(ghostSpecialAttacks);
            var ghostSpecialAttackFeat = character.Feats.All.Single(f => f.Name == FeatConstants.GhostSpecialAttack);

            Assert.That(ghostSpecialAttackFeats.Count, Is.EqualTo(ghostSpecialAttackFeat.Foci.Count()));
            Assert.That(ghostSpecialAttackFeats.Count, Is.InRange(1, 3));
        }

        private Character GetGhost()
        {
            var abilitiesRandomizer = GetNewInstanceOf<IAbilitiesRandomizer>(AbilitiesRandomizerTypeConstants.Raw);
            var metaraceRandomizer = GetNewInstanceOf<ISetMetaraceRandomizer>();
            var alignmentRandomizer = GetNewInstanceOf<IAlignmentRandomizer>(AlignmentRandomizerTypeConstants.Any);
            var levelRandomizer = GetNewInstanceOf<ILevelRandomizer>(LevelRandomizerTypeConstants.Any);
            var classNameRandomizer = GetNewInstanceOf<IClassNameRandomizer>(ClassNameRandomizerTypeConstants.AnyPlayer);
            var baseRaceRandomizer = GetNewInstanceOf<RaceRandomizer>(RaceRandomizerTypeConstants.BaseRace.AnyBase);

            metaraceRandomizer.SetMetarace = RaceConstants.Metaraces.Ghost;

            return characterGenerator.GenerateWith(
                alignmentRandomizer,
                classNameRandomizer,
                levelRandomizer,
                baseRaceRandomizer,
                metaraceRandomizer,
                abilitiesRandomizer);
        }
    }
}
