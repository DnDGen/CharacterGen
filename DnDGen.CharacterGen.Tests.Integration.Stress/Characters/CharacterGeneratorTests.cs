using DnDGen.CharacterGen.Abilities;
using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Characters;
using DnDGen.CharacterGen.Feats;
using DnDGen.CharacterGen.Magics;
using DnDGen.CharacterGen.Races;
using DnDGen.CharacterGen.Randomizers.Abilities;
using DnDGen.CharacterGen.Randomizers.CharacterClasses;
using DnDGen.CharacterGen.Randomizers.Races;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.TreasureGen.Items;
using Ninject;
using NUnit.Framework;
using System;
using System.Linq;

namespace DnDGen.CharacterGen.Tests.Integration.Stress.Characters
{
    [TestFixture]
    public class CharacterGeneratorTests : StressTests
    {
        [Inject, Named(ClassNameRandomizerTypeConstants.AnyNPC)]
        public IClassNameRandomizer NPCClassNameRandomizer { get; set; }
        [Inject, Named(ClassNameRandomizerTypeConstants.Spellcaster)]
        public IClassNameRandomizer SpellcasterClassNameRandomizer { get; set; }
        [Inject]
        public ISetClassNameRandomizer SetClassNameRandomizer { get; set; }
        [Inject, Named(LevelRandomizerTypeConstants.VeryHigh)]
        public ILevelRandomizer VeryHighLevelRandomizer { get; set; }
        [Inject]
        public ISetLevelRandomizer SetLevelRandomizer { get; set; }
        [Inject, Named(RaceRandomizerTypeConstants.BaseRace.AquaticBase)]
        public RaceRandomizer AquaticBaseRaceRandomizer { get; set; }
        [Inject, Named(RaceRandomizerTypeConstants.BaseRace.NonMonsterBase)]
        public RaceRandomizer NonMonsterBaseRaceRandomizer { get; set; }
        [Inject, Named(RaceRandomizerTypeConstants.BaseRace.MonsterBase)]
        public RaceRandomizer MonsterBaseRaceRandomizer { get; set; }
        [Inject]
        public ISetBaseRaceRandomizer SetBaseRaceRandomizer { get; set; }
        [Inject]
        public ISetMetaraceRandomizer SetMetaraceRandomizer { get; set; }
        [Inject, Named(RaceRandomizerTypeConstants.Metarace.UndeadMeta)]
        public IForcableMetaraceRandomizer UndeadMetaraceRandomizer { get; set; }
        [Inject, Named(AbilitiesRandomizerTypeConstants.Raw)]
        public IAbilitiesRandomizer RawAbilitiesRandomizer { get; set; }
        [Inject, Named(AbilitiesRandomizerTypeConstants.Heroic)]
        public IAbilitiesRandomizer HeroicAbilitiesRandomizer { get; set; }
        private CharacterVerifier characterVerifier;
        private ICollectionSelector collectionSelector;

        [SetUp]
        public void Setup()
        {
            characterVerifier = new CharacterVerifier();
            collectionSelector = GetNewInstanceOf<ICollectionSelector>();
        }

        [Test]
        public void StressCharacter()
        {
            stressor.Stress(GenerateAndAssertCharacter);
        }

        private void GenerateAndAssertCharacter()
        {
            var character = CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer, MetaraceRandomizer, RawAbilitiesRandomizer);

            characterVerifier.AssertCharacter(character);
            AssertPlayerCharacter(character);
        }

        [Test]
        public void StressMonster()
        {
            stressor.Stress(GenerateAndAssertMonster);
        }

        private void GenerateAndAssertMonster()
        {
            var character = CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, MonsterBaseRaceRandomizer, MetaraceRandomizer, RawAbilitiesRandomizer);

            characterVerifier.AssertCharacter(character);
            AssertPlayerCharacter(character);
        }

        private void AssertPlayerCharacter(Character character)
        {
            Assert.That(character.Class.IsNPC, Is.False, character.Summary);
        }

        [Test]
        public void StressNPC()
        {
            stressor.Stress(GenerateAndAssertNPC);
        }

        private void GenerateAndAssertNPC()
        {
            var npc = CharacterGenerator.GenerateWith(AlignmentRandomizer, NPCClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer, MetaraceRandomizer, RawAbilitiesRandomizer);

            characterVerifier.AssertCharacter(npc);
            Assert.That(npc.Class.IsNPC, Is.True);
        }

        [Test]
        public void StressAquatic()
        {
            stressor.Stress(GenerateAndAssertAquatic);
        }

        private void GenerateAndAssertAquatic()
        {
            var aquaticCharacter = CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, AquaticBaseRaceRandomizer, MetaraceRandomizer, RawAbilitiesRandomizer);

            characterVerifier.AssertCharacter(aquaticCharacter);
            AssertPlayerCharacter(aquaticCharacter);
            Assert.That(aquaticCharacter.Race.BaseRace, Is.EqualTo(RaceConstants.BaseRaces.AquaticElf)
                .Or.EqualTo(RaceConstants.BaseRaces.Kapoacinth)
                .Or.EqualTo(RaceConstants.BaseRaces.KuoToa)
                .Or.EqualTo(RaceConstants.BaseRaces.Locathah)
                .Or.EqualTo(RaceConstants.BaseRaces.Merfolk)
                .Or.EqualTo(RaceConstants.BaseRaces.Merrow)
                .Or.EqualTo(RaceConstants.BaseRaces.Sahuagin)
                .Or.EqualTo(RaceConstants.BaseRaces.Scrag));
            Assert.That(aquaticCharacter.Race.SwimSpeed.Value, Is.Positive);
        }

        //INFO: Sometimes a first-level commoner only knows unarmed strike. Other times, she only knows one weapon (melee or ranged), and is unable to generate the other
        [Test]
        public void BUG_StressFirstLevelCommoner()
        {
            stressor.Stress(GenerateAndAssertFirstLevelCommoner);
        }

        private void GenerateAndAssertFirstLevelCommoner()
        {
            SetClassNameRandomizer.SetClassName = CharacterClassConstants.Commoner;
            SetLevelRandomizer.SetLevel = 1;

            var commoner = CharacterGenerator.GenerateWith(AlignmentRandomizer, SetClassNameRandomizer, SetLevelRandomizer, BaseRaceRandomizer, MetaraceRandomizer, RawAbilitiesRandomizer);

            characterVerifier.AssertCharacter(commoner);
            Assert.That(commoner.Class.IsNPC, Is.True);
            Assert.That(commoner.Class.Level, Is.EqualTo(1));
            Assert.That(commoner.Class.Name, Is.EqualTo(CharacterClassConstants.Commoner));
            Assert.That(commoner.Class.ProhibitedFields, Is.Empty);
            Assert.That(commoner.Class.SpecialistFields, Is.Empty);
            Assert.That(commoner.Race.Metarace, Is.EqualTo(RaceConstants.Metaraces.None));
            Assert.That(commoner.Race.MetaraceSpecies, Is.Empty);
            Assert.That(commoner.Magic.Animal, Is.Empty);
            Assert.That(commoner.Magic.ArcaneSpellFailure, Is.EqualTo(0));
            Assert.That(commoner.Magic.KnownSpells, Is.Empty);
            Assert.That(commoner.Magic.PreparedSpells, Is.Empty);
            Assert.That(commoner.Magic.SpellsPerDay, Is.Empty);
        }

        [Test]
        public void BUG_StressHighLevelFighter()
        {
            stressor.Stress(GenerateAndAssertHighLevelFighter);
        }

        private void GenerateAndAssertHighLevelFighter()
        {
            SetClassNameRandomizer.SetClassName = CharacterClassConstants.Fighter;
            SetLevelRandomizer.SetLevel = 20;

            //INFO: Using the non-monster so that we don't worry about giant sizes throwing off generation time
            var fighter = CharacterGenerator.GenerateWith(AlignmentRandomizer, SetClassNameRandomizer, SetLevelRandomizer, NonMonsterBaseRaceRandomizer, MetaraceRandomizer, RawAbilitiesRandomizer);

            characterVerifier.AssertCharacter(fighter);
            AssertPlayerCharacter(fighter);
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
        [Test]
        public void BUG_StressStormGiant()
        {
            stressor.Stress(GenerateAndAssertStormGiant);
        }

        private void GenerateAndAssertStormGiant()
        {
            SetBaseRaceRandomizer.SetBaseRace = RaceConstants.BaseRaces.StormGiant;

            var stormGiant = CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, SetBaseRaceRandomizer, MetaraceRandomizer, RawAbilitiesRandomizer);

            characterVerifier.AssertCharacter(stormGiant);
            AssertPlayerCharacter(stormGiant);
            Assert.That(stormGiant.Race.BaseRace, Is.EqualTo(RaceConstants.BaseRaces.StormGiant));
            Assert.That(stormGiant.Race.Size, Is.EqualTo(RaceConstants.Sizes.Huge));
            Assert.That(stormGiant.Equipment.Armor, Is.Not.Null, stormGiant.Summary + " armor");
            Assert.That(stormGiant.Equipment.Armor.Size, Is.EqualTo(stormGiant.Race.Size));
            Assert.That(stormGiant.Equipment.PrimaryHand, Is.Not.Null, stormGiant.Summary + " primary hand");
            Assert.That(stormGiant.Equipment.PrimaryHand.Size, Is.EqualTo(stormGiant.Race.Size));

            if (stormGiant.Equipment.OffHand != null && stormGiant.Equipment.OffHand is Weapon)
            {
                var weapon = stormGiant.Equipment.OffHand as Weapon;
                Assert.That(weapon.Size, Is.EqualTo(stormGiant.Race.Size));
            }
            else if (stormGiant.Equipment.OffHand != null && stormGiant.Equipment.OffHand is Armor)
            {
                var armor = stormGiant.Equipment.OffHand as Armor;
                Assert.That(armor.Attributes, Contains.Item(AttributeConstants.Shield));
                Assert.That(armor.Size, Is.EqualTo(stormGiant.Race.Size));
            }
        }

        [Test]
        public void StressSpellcaster()
        {
            stressor.Stress(GenerateAndAssertSpellcaster);
        }

        private void GenerateAndAssertSpellcaster()
        {
            //INFO: Using the non-monster so that we don't worry about giant sizes throwing off generation time
            var spellcaster = stressor.Generate(
                () => CharacterGenerator.GenerateWith(
                    AlignmentRandomizer,
                    SpellcasterClassNameRandomizer,
                    LevelRandomizer,
                    NonMonsterBaseRaceRandomizer,
                    MetaraceRandomizer,
                    HeroicAbilitiesRandomizer),
                c => c.Class.Level > 3);

            characterVerifier.AssertCharacter(spellcaster);
            AssertPlayerCharacter(spellcaster);
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

        //INFO: Want to verify rakshasas have native sorcerer spells and additional spellcaster class spells
        [Test]
        public void BUG_StressRakshasaSpellcaster()
        {
            stressor.Stress(GenerateAndAssertRakshasaSpellcaster);
        }

        private void GenerateAndAssertRakshasaSpellcaster()
        {
            SetBaseRaceRandomizer.SetBaseRace = RaceConstants.BaseRaces.Rakshasa;

            var spellcaster = stressor.Generate(
                () => CharacterGenerator.GenerateWith(
                    AlignmentRandomizer,
                    SpellcasterClassNameRandomizer,
                    LevelRandomizer,
                    SetBaseRaceRandomizer,
                    MetaraceRandomizer,
                    HeroicAbilitiesRandomizer),
                c => true);

            characterVerifier.AssertCharacter(spellcaster);
            AssertPlayerCharacter(spellcaster);
            AssertSpellcaster(spellcaster);

            Assert.That(spellcaster.Race.BaseRace, Is.EqualTo(RaceConstants.BaseRaces.Rakshasa));
        }

        //INFO: Want to verify rakshasas have native sorcerer spells and additional sorcerer spells at 7 levels higher, even when high level
        [Test]
        public void BUG_StressHighLevelRakshasaSorcerer()
        {
            stressor.Stress(GenerateAndAssertHighLevelRakshasaSorcerer);
        }

        private void GenerateAndAssertHighLevelRakshasaSorcerer()
        {
            SetBaseRaceRandomizer.SetBaseRace = RaceConstants.BaseRaces.Rakshasa;
            SetClassNameRandomizer.SetClassName = CharacterClassConstants.Sorcerer;

            var sorcerer = stressor.Generate(
                () => CharacterGenerator.GenerateWith(AlignmentRandomizer, SetClassNameRandomizer, VeryHighLevelRandomizer, SetBaseRaceRandomizer, MetaraceRandomizer, RawAbilitiesRandomizer),
                c => true);

            characterVerifier.AssertCharacter(sorcerer);
            AssertPlayerCharacter(sorcerer);
            AssertSpellcaster(sorcerer);

            Assert.That(sorcerer.Race.BaseRace, Is.EqualTo(RaceConstants.BaseRaces.Rakshasa));
            Assert.That(sorcerer.Class.Name, Is.EqualTo(CharacterClassConstants.Sorcerer));
            Assert.That(sorcerer.Class.Level, Is.AtLeast(16));
        }

        [Test]
        public void BUG_StressUndeadCharacter()
        {
            stressor.Stress(GenerateAndAssertUndead);
        }

        private void GenerateAndAssertUndead()
        {
            UndeadMetaraceRandomizer.ForceMetarace = true;

            var character = CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer, UndeadMetaraceRandomizer, RawAbilitiesRandomizer);

            characterVerifier.AssertCharacter(character);
            AssertPlayerCharacter(character);
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

        [Test]
        public void BUG_StressPlanetouchedCharacter()
        {
            stressor.Stress(GenerateAndAssertPlanetouched);
        }

        private void GenerateAndAssertPlanetouched()
        {
            var planetouched = new[] { RaceConstants.BaseRaces.Aasimar, RaceConstants.BaseRaces.Tiefling };
            SetBaseRaceRandomizer.SetBaseRace = collectionSelector.SelectRandomFrom(planetouched);

            var character = CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, SetBaseRaceRandomizer, MetaraceRandomizer, RawAbilitiesRandomizer);

            characterVerifier.AssertCharacter(character);
            AssertPlayerCharacter(character);
            Assert.That(character.Race.BaseRace, Is.EqualTo(RaceConstants.BaseRaces.Aasimar).Or.EqualTo(RaceConstants.BaseRaces.Tiefling));
            Assert.That(character.Class.LevelAdjustment, Is.Positive);
        }

        [Test]
        public void BUG_StressGhost()
        {
            stressor.Stress(() => GenerateAndAssertGhost());
        }

        private void GenerateAndAssertGhost()
        {
            var character = GetGhost();

            characterVerifier.AssertCharacter(character);
            AssertPlayerCharacter(character);
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
            SetMetaraceRandomizer.SetMetarace = RaceConstants.Metaraces.Ghost;
            return CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer, SetMetaraceRandomizer, RawAbilitiesRandomizer);
        }
    }
}