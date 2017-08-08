using CharacterGen.Abilities;
using CharacterGen.CharacterClasses;
using CharacterGen.Characters;
using CharacterGen.Feats;
using CharacterGen.Magics;
using CharacterGen.Races;
using CharacterGen.Randomizers.Abilities;
using CharacterGen.Randomizers.CharacterClasses;
using CharacterGen.Randomizers.Races;
using Ninject;
using NUnit.Framework;
using System;
using System.Linq;
using TreasureGen.Items;

namespace CharacterGen.Tests.Integration.Stress.Characters
{
    [TestFixture]
    public class CharacterGeneratorTests : StressTests
    {
        [Inject]
        public ICharacterGenerator CharacterGenerator { get; set; }
        [Inject, Named(AbilitiesRandomizerTypeConstants.Raw)]
        public IAbilitiesRandomizer RawAbilitiesRandomizer { get; set; }
        [Inject, Named(ClassNameRandomizerTypeConstants.AnyNPC)]
        public IClassNameRandomizer NPCClassNameRandomizer { get; set; }
        [Inject, Named(AbilitiesRandomizerTypeConstants.Heroic)]
        public IAbilitiesRandomizer HeroicAbilitiesRandomizer { get; set; }
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
        [Inject, Named(RaceRandomizerTypeConstants.BaseRace.AquaticBase)]
        public RaceRandomizer AquaticBaseRaceRandomizer { get; set; }

        [Test]
        public void StressCharacter()
        {
            stressor.Stress(GenerateAndAssertCharacter);
        }

        private void GenerateAndAssertCharacter()
        {
            var character = CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer, MetaraceRandomizer, RawAbilitiesRandomizer);

            CharacterVerifier.AssertCharacter(character);
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

            CharacterVerifier.AssertCharacter(npc);
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

            CharacterVerifier.AssertCharacter(aquaticCharacter);
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

        //INFO: Sometimes a first-level commoner only knows unarmed strike.  Other times, she only knows one weapon (melee or ranged), and is unable to generate the other
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

        //INFO: We are testing the efficiency of the weapon and armor generators here
        [Test]
        [Ignore("Takes too long, often overruns time limit")]
        public void BUG_StressHighLevelFighter()
        {
            stressor.Stress(GenerateAndAssertHighLevelFighter);
        }

        private void GenerateAndAssertHighLevelFighter()
        {
            SetClassNameRandomizer.SetClassName = CharacterClassConstants.Fighter;
            SetLevelRandomizer.SetLevel = 20;
            SetLevelRandomizer.AllowAdjustments = false;

            var fighter = CharacterGenerator.GenerateWith(AlignmentRandomizer, SetClassNameRandomizer, SetLevelRandomizer, BaseRaceRandomizer, MetaraceRandomizer, RawAbilitiesRandomizer);

            CharacterVerifier.AssertCharacter(fighter);
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
        [Ignore("Takes too long, often overruns time limit")]
        public void BUG_StressStormGiant()
        {
            stressor.Stress(GenerateAndAssertStormGiant);
        }

        private void GenerateAndAssertStormGiant()
        {
            SetBaseRaceRandomizer.SetBaseRace = RaceConstants.BaseRaces.StormGiant;

            var stormGiant = CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, SetBaseRaceRandomizer, MetaraceRandomizer, RawAbilitiesRandomizer);

            CharacterVerifier.AssertCharacter(stormGiant);
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
            var spellcaster = stressor.Generate(() => CharacterGenerator.GenerateWith(AlignmentRandomizer, SpellcasterClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer, MetaraceRandomizer, HeroicAbilitiesRandomizer),
                c => c.Class.Level > 3);

            CharacterVerifier.AssertCharacter(spellcaster);
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
        [Ignore("Takes too long, often overruns time limit")]
        public void BUG_StressRakshasaSpellcaster()
        {
            stressor.Stress(GenerateAndAssertRakshasaSpellcaster);
        }

        private void GenerateAndAssertRakshasaSpellcaster()
        {
            SetBaseRaceRandomizer.SetBaseRace = RaceConstants.BaseRaces.Rakshasa;

            var spellcaster = stressor.Generate(
                () => CharacterGenerator.GenerateWith(AlignmentRandomizer, SpellcasterClassNameRandomizer, LevelRandomizer, SetBaseRaceRandomizer, MetaraceRandomizer, HeroicAbilitiesRandomizer),
                c => true);

            CharacterVerifier.AssertCharacter(spellcaster);
            AssertPlayerCharacter(spellcaster);
            AssertSpellcaster(spellcaster);

            Assert.That(spellcaster.Race.BaseRace, Is.EqualTo(RaceConstants.BaseRaces.Rakshasa));
        }

        [Test]
        [Ignore("Takes too long, often overruns time limit")]
        public void BUG_StressUndeadCharacter()
        {
            stressor.Stress(GenerateAndAssertUndead);
        }

        private void GenerateAndAssertUndead()
        {
            var undeadMetaraceRandomizer = GetNewInstanceOf<IForcableMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.UndeadMeta);
            undeadMetaraceRandomizer.ForceMetarace = true;

            var character = CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer, undeadMetaraceRandomizer, RawAbilitiesRandomizer);

            CharacterVerifier.AssertCharacter(character);
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
        [Ignore("Takes too long, often overruns time limit")]
        public void BUG_StressPlanetouchedCharacter()
        {
            stressor.Stress(GenerateAndAssertPlanetouched);
        }

        private void GenerateAndAssertPlanetouched()
        {
            var planetouched = new[] { RaceConstants.BaseRaces.Aasimar, RaceConstants.BaseRaces.Tiefling };
            var randomIndex = Random.Next(2);
            SetBaseRaceRandomizer.SetBaseRace = planetouched[randomIndex];

            var character = CharacterGenerator.GenerateWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, SetBaseRaceRandomizer, MetaraceRandomizer, RawAbilitiesRandomizer);

            CharacterVerifier.AssertCharacter(character);
            AssertPlayerCharacter(character);
            Assert.That(character.Race.BaseRace, Is.EqualTo(RaceConstants.BaseRaces.Aasimar).Or.EqualTo(RaceConstants.BaseRaces.Tiefling));
            Assert.That(character.Class.LevelAdjustment, Is.Positive);
        }

        [Test]
        [Ignore("Takes too long, often overruns time limit")]
        public void BUG_StressGhost()
        {
            stressor.Stress(() => GenerateAndAssertGhost());
        }

        private void GenerateAndAssertGhost()
        {
            var character = GetGhost();

            CharacterVerifier.AssertCharacter(character);
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