using CharacterGen.Abilities.Feats;
using CharacterGen.Abilities.Skills;
using CharacterGen.Abilities.Stats;
using CharacterGen.Alignments;
using CharacterGen.Characters;
using CharacterGen.Races;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TreasureGen.Items;

namespace CharacterGen.Tests.Integration.Stress.Characters
{
    public class CharacterVerifier
    {
        private readonly IEnumerable<string> skillsWithFoci;

        public CharacterVerifier()
        {
            skillsWithFoci = new[]
            {
                SkillConstants.Craft,
                SkillConstants.Knowledge,
                SkillConstants.Perform,
                SkillConstants.Profession,
            };
        }

        public void AssertCharacter(Character character)
        {
            VerifySummary(character);
            VerifyAlignment(character);
            VerifyCharacterClass(character);
            VerifyRace(character);
            VerifyAbilities(character);
            VerifyEquipment(character);
            VerifyMagic(character);
            VerifyCombat(character);

            Assert.That(character.InterestingTrait, Is.Not.Empty, character.Summary);
            Assert.That(character.ChallengeRating, Is.Positive, character.Summary);
            Assert.That(character.ChallengeRating, Is.AtLeast(character.Race.ChallengeRating), character.Summary);
        }

        private void VerifySummary(Character character)
        {
            Assert.That(character.Summary, Is.Not.Empty);
            Assert.That(character.Summary, Contains.Substring(character.Alignment.Full));
            Assert.That(character.Summary, Contains.Substring($"Level {character.Class.Level}"));
            Assert.That(character.Summary, Contains.Substring(character.Class.Name));
            Assert.That(character.Summary, Contains.Substring(character.Race.BaseRace));
            Assert.That(character.Summary, Contains.Substring(character.Race.Metarace));
        }

        private void VerifyAlignment(Character character)
        {
            Assert.That(character.Alignment.Goodness, Is.EqualTo(AlignmentConstants.Good)
                .Or.EqualTo(AlignmentConstants.Neutral)
                .Or.EqualTo(AlignmentConstants.Evil), character.Summary);
            Assert.That(character.Alignment.Lawfulness, Is.EqualTo(AlignmentConstants.Lawful)
                .Or.EqualTo(AlignmentConstants.Neutral)
                .Or.EqualTo(AlignmentConstants.Chaotic), character.Summary);
        }

        private void VerifyCharacterClass(Character character)
        {
            Assert.That(character.Class.Name, Is.Not.Empty, character.Summary);
            Assert.That(character.Class.Level, Is.Positive, character.Summary);
            Assert.That(character.Class.LevelAdjustment, Is.Not.Negative, character.Summary);
            Assert.That(character.Class.EffectiveLevel, Is.Positive, character.Summary);
            Assert.That(character.Class.ProhibitedFields, Is.Not.Null, character.Summary);
            Assert.That(character.Class.SpecialistFields, Is.Not.Null, character.Summary);
            Assert.That(character.Class.Summary, Is.Not.Empty, character.Summary);
        }

        private void VerifyRace(Character character)
        {
            Assert.That(character.Race.BaseRace, Is.Not.Empty, character.Summary);
            Assert.That(character.Race.Metarace, Is.Not.Null, character.Summary);
            Assert.That(character.Race.MetaraceSpecies, Is.Not.Null, character.Summary);
            Assert.That(character.Race.ChallengeRating, Is.Not.Negative, character.Summary);
            Assert.That(character.Race.Summary, Is.Not.Empty, character.Summary);
            Assert.That(character.Race.Size, Is.EqualTo(RaceConstants.Sizes.Large)
                .Or.EqualTo(RaceConstants.Sizes.Colossal)
                .Or.EqualTo(RaceConstants.Sizes.Gargantuan)
                .Or.EqualTo(RaceConstants.Sizes.Huge)
                .Or.EqualTo(RaceConstants.Sizes.Tiny)
                .Or.EqualTo(RaceConstants.Sizes.Medium)
                .Or.EqualTo(RaceConstants.Sizes.Small), character.Summary);

            VerifyLandSpeed(character);
            VerifyAerialSpeed(character);
            VerifyAge(character);
            VerifyMaximumAge(character);
            VerifyHeight(character);
            VerifyWeight(character);
        }

        private void VerifyLandSpeed(Character character)
        {
            Assert.That(character.Race.LandSpeed.Value, Is.Positive, character.Summary);
            Assert.That(character.Race.LandSpeed.Value % 10, Is.EqualTo(0), character.Summary);
            Assert.That(character.Race.LandSpeed.Unit, Is.EqualTo("feet per round"), character.Summary);
            Assert.That(character.Race.LandSpeed.Description, Is.Empty, character.Summary);
        }

        private void VerifyAerialSpeed(Character character)
        {
            Assert.That(character.Race.AerialSpeed.Value, Is.Not.Negative, character.Summary);
            Assert.That(character.Race.AerialSpeed.Value % 10, Is.EqualTo(0), character.Summary);
            Assert.That(character.Race.AerialSpeed.Unit, Is.EqualTo("feet per round"), character.Summary);

            if (character.Race.AerialSpeed.Value == 0)
                Assert.That(character.Race.AerialSpeed.Description, Is.Empty, character.Summary);
            else
                Assert.That(character.Race.AerialSpeed.Description, Is.Not.Empty, character.Summary);

            if (character.Race.HasWings)
                Assert.That(character.Race.AerialSpeed.Value, Is.Positive, character.Summary);
        }

        private void VerifyAge(Character character)
        {
            Assert.That(character.Race.Age.Description, Is.EqualTo(RaceConstants.Ages.Adulthood)
                .Or.EqualTo(RaceConstants.Ages.MiddleAge)
                .Or.EqualTo(RaceConstants.Ages.Old)
                .Or.EqualTo(RaceConstants.Ages.Venerable), character.Summary);
            Assert.That(character.Race.Age.Value, Is.Positive, character.Summary);
            Assert.That(character.Race.Age.Unit, Is.EqualTo("Years"), character.Summary);

            if (character.Race.MaximumAge.Value != RaceConstants.Ages.Ageless)
                Assert.That(character.Race.Age.Value, Is.LessThanOrEqualTo(character.Race.MaximumAge.Value), character.Summary);
        }

        private void VerifyMaximumAge(Character character)
        {
            Assert.That(character.Race.MaximumAge.Value, Is.Positive.Or.EqualTo(RaceConstants.Ages.Ageless), character.Summary);
            Assert.That(character.Race.MaximumAge.Unit, Is.EqualTo("Years"), character.Summary);

            if (character.Race.MaximumAge.Value == RaceConstants.Ages.Ageless)
                Assert.That(character.Race.MaximumAge.Description, Is.EqualTo("Immortal"), character.Summary);
            else if (character.Race.BaseRace == RaceConstants.BaseRaces.Pixie)
                Assert.That(character.Race.MaximumAge.Description, Is.EqualTo("Will return to their plane of origin"), character.Summary);
            else
                Assert.That(character.Race.MaximumAge.Description, Is.EqualTo("Will die of natural causes"), character.Summary);
        }

        private void VerifyHeight(Character character)
        {
            Assert.That(character.Race.Height.Value, Is.Positive, character.Summary);
            Assert.That(character.Race.Height.Unit, Is.EqualTo("Inches"), character.Summary);
            Assert.That(character.Race.Height.Description, Is.EqualTo("Short").Or.EqualTo("Average").Or.EqualTo("Tall"), character.Summary);
        }

        private void VerifyWeight(Character character)
        {
            Assert.That(character.Race.Weight.Value, Is.Positive, character.Summary);
            Assert.That(character.Race.Weight.Unit, Is.EqualTo("Pounds"), character.Summary);
            Assert.That(character.Race.Weight.Description, Is.EqualTo("Light").Or.EqualTo("Average").Or.EqualTo("Heavy"), character.Summary);
        }

        private void VerifyAbilities(Character character)
        {
            VerifyStats(character);
            VerifySkills(character);
            VerifyFeats(character);
            Assert.That(character.Ability.Languages, Is.Not.Empty, character.Summary);
        }

        private void VerifyStats(Character character)
        {
            Assert.That(character.Ability.Stats.Count, Is.InRange(5, 6), character.Summary);
            Assert.That(character.Ability.Stats.Keys, Contains.Item(StatConstants.Charisma), character.Summary);
            Assert.That(character.Ability.Stats.Keys, Contains.Item(StatConstants.Dexterity), character.Summary);
            Assert.That(character.Ability.Stats.Keys, Contains.Item(StatConstants.Intelligence), character.Summary);
            Assert.That(character.Ability.Stats.Keys, Contains.Item(StatConstants.Strength), character.Summary);
            Assert.That(character.Ability.Stats.Keys, Contains.Item(StatConstants.Wisdom), character.Summary);

            if (character.Ability.Stats.Count == 6)
                Assert.That(character.Ability.Stats.Keys, Contains.Item(StatConstants.Constitution), character.Summary);

            foreach (var statKVP in character.Ability.Stats)
            {
                var stat = statKVP.Value;
                Assert.That(stat.Name, Is.EqualTo(statKVP.Key), character.Summary);
                Assert.That(stat.Value, Is.AtLeast(3), character.Summary);
            }
        }

        private void VerifySkills(Character character)
        {
            Assert.That(character.Ability.Skills, Is.Not.Empty, character.Summary);

            foreach (var skill in character.Ability.Skills)
            {
                Assert.That(skill.ArmorCheckPenalty, Is.Not.Positive, character.Summary);
                Assert.That(skill.Ranks, Is.AtMost(skill.RankCap), character.Summary);
                Assert.That(skill.RankCap, Is.Positive, character.Summary);
                Assert.That(skill.Bonus, Is.Not.Negative);
                Assert.That(skill.BaseStat, Is.Not.Null);
                Assert.That(character.Ability.Stats.Values, Contains.Item(skill.BaseStat));
                Assert.That(skill.Focus, Is.Not.Null);

                if (skillsWithFoci.Contains(skill.Name))
                    Assert.That(skill.Focus, Is.Not.Empty);
                else
                    Assert.That(skill.Focus, Is.Empty);
            }

            var skillNamesAndFoci = character.Ability.Skills.Select(s => s.Name + s.Focus);
            Assert.That(skillNamesAndFoci, Is.Unique);
        }

        private void VerifyFeats(Character character)
        {
            Assert.That(character.Ability.Feats, Is.Not.Empty, character.Summary);

            foreach (var feat in character.Ability.Feats)
            {
                Assert.That(feat.Name, Is.Not.Empty, character.Summary);
                Assert.That(feat.Foci, Is.Not.Null, feat.Name);
                Assert.That(feat.Power, Is.Not.Negative, feat.Name);
                Assert.That(feat.Frequency.Quantity, Is.Not.Negative, feat.Name);
                Assert.That(feat.Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Constant)
                    .Or.EqualTo(FeatConstants.Frequencies.AtWill)
                    .Or.EqualTo(FeatConstants.Frequencies.Hit)
                    .Or.EqualTo(FeatConstants.Frequencies.Round)
                    .Or.EqualTo(FeatConstants.Frequencies.Turn)
                    .Or.EqualTo(FeatConstants.Frequencies.Day)
                    .Or.EqualTo(FeatConstants.Frequencies.Week)
                    .Or.Empty, feat.Name);

                if (feat.Name == FeatConstants.SaveBonus)
                    Assert.That(feat.Foci, Is.Not.Empty, character.Race.BaseRace);
            }
        }

        private void VerifyEquipment(Character character)
        {
            if (character.Ability.Feats.SelectMany(f => f.Foci).Any(f => f == FeatConstants.Foci.UnarmedStrike) == false)
            {
                var feats = GetAllFeatsMessage(character.Ability.Feats);

                Assert.That(character.Equipment.PrimaryHand, Is.Not.Null, feats);
                Assert.That(character.Equipment.PrimaryHand.ItemType, Is.EqualTo(ItemTypeConstants.Weapon), feats);
                Assert.That(character.Equipment.PrimaryHand.Name, Is.Not.Empty, feats);
                Assert.That(character.Equipment.PrimaryHand.Quantity, Is.Positive, feats);
            }

            Assert.That(character.Equipment.Treasure, Is.Not.Null, character.Summary);
            Assert.That(character.Equipment.Treasure.Items, Is.Not.Null, character.Summary);
            Assert.That(character.Equipment.Treasure.Items, Is.All.Not.Null, character.Summary);

            foreach (var item in character.Equipment.Treasure.Items)
            {
                Assert.That(item.Name, Is.Not.Empty, character.Summary);
                Assert.That(item.Quantity, Is.Positive, item.Name);
            }
        }

        private void VerifyMagic(Character character)
        {
            Assert.That(character.Magic.Animal, Is.Not.Null, character.Summary);

            foreach (var spells in character.Magic.SpellsPerDay)
            {
                Assert.That(spells.Level, Is.Not.Negative, spells.Level.ToString());
                Assert.That(spells.Quantity, Is.Not.Negative, spells.Level.ToString());
                Assert.That(spells.Source, Is.Not.Empty, spells.Level.ToString());
            }

            foreach (var spell in character.Magic.KnownSpells)
            {
                Assert.That(spell.Level, Is.Not.Negative);
                Assert.That(spell.Metamagic, Is.Empty);
                Assert.That(spell.Name, Is.Not.Empty);
                Assert.That(spell.Source, Is.Not.Empty);
            }

            foreach (var spell in character.Magic.PreparedSpells)
            {
                Assert.That(spell.Level, Is.Not.Negative);
                Assert.That(spell.Metamagic, Is.Empty);
                Assert.That(spell.Name, Is.Not.Empty);
                Assert.That(spell.Source, Is.Not.Empty);

                var knownSpellNames = character.Magic.KnownSpells.Select(s => s.Name);
                Assert.That(knownSpellNames, Contains.Item(spell.Name), character.Class.Name);
            }
        }

        private void VerifyCombat(Character character)
        {
            Assert.That(character.Combat.BaseAttack.BaseBonus, Is.Not.Negative, character.Summary);
            Assert.That(character.Combat.BaseAttack.DexterityBonus, Is.EqualTo(character.Ability.Stats[StatConstants.Dexterity].Bonus), character.Summary);
            Assert.That(character.Combat.BaseAttack.StrengthBonus, Is.EqualTo(character.Ability.Stats[StatConstants.Strength].Bonus), character.Summary);
            Assert.That(character.Combat.BaseAttack.AllMeleeBonuses.Count, Is.InRange(1, 4), character.Summary);
            Assert.That(character.Combat.BaseAttack.AllRangedBonuses.Count, Is.InRange(1, 4), character.Summary);
            Assert.That(character.Combat.BaseAttack.AllRangedBonuses.Count, Is.EqualTo(character.Combat.BaseAttack.AllMeleeBonuses.Count()), character.Summary);
            Assert.That(character.Combat.BaseAttack.AllMeleeBonuses, Is.Unique, character.Summary);
            Assert.That(character.Combat.BaseAttack.AllMeleeBonuses, Is.Ordered.Descending, character.Summary);
            Assert.That(character.Combat.BaseAttack.AllRangedBonuses, Is.Unique, character.Summary);
            Assert.That(character.Combat.BaseAttack.AllRangedBonuses, Is.Ordered.Descending, character.Summary);
            Assert.That(character.Combat.BaseAttack.AllMeleeBonuses.First(), Is.EqualTo(character.Combat.BaseAttack.MeleeBonus));
            Assert.That(character.Combat.BaseAttack.AllRangedBonuses.First(), Is.EqualTo(character.Combat.BaseAttack.RangedBonus));
            Assert.That(character.Combat.BaseAttack.RacialModifier, Is.Not.Negative);

            if (character.Ability.Stats[StatConstants.Dexterity].Bonus != character.Ability.Stats[StatConstants.Strength].Bonus)
                Assert.That(character.Combat.BaseAttack.AllMeleeBonuses, Is.Not.EquivalentTo(character.Combat.BaseAttack.AllRangedBonuses), character.Summary);

            Assert.That(character.Combat.HitPoints, Is.AtLeast(character.Class.Level), character.Summary);
            Assert.That(character.Combat.ArmorClass.Full, Is.Positive, character.Summary);
            Assert.That(character.Combat.ArmorClass.FlatFooted, Is.Positive, character.Summary);
            Assert.That(character.Combat.ArmorClass.Touch, Is.Positive, character.Summary);
            Assert.That(character.Combat.AdjustedDexterityBonus, Is.AtMost(character.Ability.Stats[StatConstants.Dexterity].Bonus), character.Summary);
            Assert.That(character.Combat.InitiativeBonus, Is.AtLeast(character.Combat.AdjustedDexterityBonus));

            Assert.That(character.Combat.SavingThrows.Reflex, Is.AtLeast(character.Ability.Stats[StatConstants.Dexterity].Bonus));
            Assert.That(character.Combat.SavingThrows.Will, Is.AtLeast(character.Ability.Stats[StatConstants.Wisdom].Bonus));
            Assert.That(character.Combat.SavingThrows.HasFortitudeSave, Is.EqualTo(character.Ability.Stats.ContainsKey(StatConstants.Constitution)));

            if (character.Combat.SavingThrows.HasFortitudeSave)
                Assert.That(character.Combat.SavingThrows.Fortitude, Is.AtLeast(character.Ability.Stats[StatConstants.Constitution].Bonus));
        }

        private string GetAllFeatsMessage(IEnumerable<Feat> feats)
        {
            var featsWithFoci = feats.Where(f => f.Foci.Any()).Select(f => $"{f.Name}: {string.Join(", ", f.Foci)}").OrderBy(f => f);
            return string.Join("; ", featsWithFoci);
        }
    }
}
