using CharacterGen.Abilities.Feats;
using CharacterGen.Abilities.Stats;
using CharacterGen.Alignments;
using CharacterGen.Races;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TreasureGen.Items;

namespace CharacterGen.Tests.Integration.Stress
{
    public class CharacterVerifier
    {
        public void AssertCharacter(Character character)
        {
            Assert.That(character.Summary, Is.Not.Empty);
            Assert.That(character.Summary, Contains.Substring(character.Alignment.Full));
            Assert.That(character.Summary, Contains.Substring($"Level {character.Class.Level}"));
            Assert.That(character.Summary, Contains.Substring(character.Class.Name));
            Assert.That(character.Summary, Contains.Substring(character.Race.BaseRace));
            Assert.That(character.Summary, Contains.Substring(character.Race.Metarace));

            Assert.That(character.Alignment.Goodness, Is.EqualTo(AlignmentConstants.Good)
                .Or.EqualTo(AlignmentConstants.Neutral)
                .Or.EqualTo(AlignmentConstants.Evil), character.Summary);
            Assert.That(character.Alignment.Lawfulness, Is.EqualTo(AlignmentConstants.Lawful)
                .Or.EqualTo(AlignmentConstants.Neutral)
                .Or.EqualTo(AlignmentConstants.Chaotic), character.Summary);

            Assert.That(character.Class.Name, Is.Not.Empty, character.Summary);
            Assert.That(character.Class.Level, Is.Positive, character.Summary);
            Assert.That(character.Class.LevelAdjustment, Is.Not.Negative, character.Summary);
            Assert.That(character.Class.EffectiveLevel, Is.Positive, character.Summary);
            Assert.That(character.Class.ProhibitedFields, Is.Not.Null, character.Summary);
            Assert.That(character.Class.SpecialistFields, Is.Not.Null, character.Summary);

            Assert.That(character.InterestingTrait, Is.Not.Empty, character.Summary);
            Assert.That(character.ChallengeRating, Is.Positive, character.Summary);
            Assert.That(character.ChallengeRating, Is.AtLeast(character.Race.ChallengeRating), character.Summary);

            Assert.That(character.Race.BaseRace, Is.Not.Empty, character.Summary);
            Assert.That(character.Race.Metarace, Is.Not.Null, character.Summary);
            Assert.That(character.Race.AerialSpeed, Is.Not.Negative, character.Summary);
            Assert.That(character.Race.AerialSpeed % 10, Is.EqualTo(0), character.Summary);
            Assert.That(character.Race.Age.Stage, Is.EqualTo(RaceConstants.Ages.Adulthood)
                .Or.EqualTo(RaceConstants.Ages.MiddleAge)
                .Or.EqualTo(RaceConstants.Ages.Old)
                .Or.EqualTo(RaceConstants.Ages.Venerable), character.Summary);
            Assert.That(character.Race.Age.Years, Is.Positive, character.Summary);
            Assert.That(character.Race.Age.Maximum, Is.Positive.Or.EqualTo(RaceConstants.Ages.Ageless), character.Summary);

            if (character.Race.Age.Maximum != RaceConstants.Ages.Ageless)
                Assert.That(character.Race.Age.Years, Is.LessThanOrEqualTo(character.Race.Age.Maximum), character.Summary);

            Assert.That(character.Race.HeightInInches, Is.Positive, character.Summary);
            Assert.That(character.Race.WeightInPounds, Is.Positive, character.Summary);
            Assert.That(character.Race.ChallengeRating, Is.Not.Negative, character.Summary);

            if (character.Race.HasWings)
                Assert.That(character.Race.AerialSpeed, Is.Positive, character.Summary);

            Assert.That(character.Race.LandSpeed, Is.Positive, character.Summary);
            Assert.That(character.Race.LandSpeed % 10, Is.EqualTo(0), character.Summary);
            Assert.That(character.Race.MetaraceSpecies, Is.Not.Null, character.Summary);
            Assert.That(character.Race.Size, Is.EqualTo(RaceConstants.Sizes.Large)
                .Or.EqualTo(RaceConstants.Sizes.Colossal)
                .Or.EqualTo(RaceConstants.Sizes.Gargantuan)
                .Or.EqualTo(RaceConstants.Sizes.Huge)
                .Or.EqualTo(RaceConstants.Sizes.Tiny)
                .Or.EqualTo(RaceConstants.Sizes.Medium)
                .Or.EqualTo(RaceConstants.Sizes.Small), character.Summary);

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

            Assert.That(character.Ability.Languages, Is.Not.Empty, character.Summary);
            Assert.That(character.Ability.Skills, Is.Not.Empty, character.Summary);

            foreach (var skillKVP in character.Ability.Skills)
            {
                var skill = skillKVP.Value;
                Assert.That(skill.Name, Is.EqualTo(skillKVP.Key), character.Summary);
                Assert.That(skill.ArmorCheckPenalty, Is.Not.Positive, character.Summary);
                Assert.That(skill.Ranks, Is.AtMost(skill.RankCap), character.Summary);
                Assert.That(skill.RankCap, Is.Positive, character.Summary);
            }

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

            if (character.Ability.Stats[StatConstants.Dexterity].Bonus != character.Ability.Stats[StatConstants.Strength].Bonus)
                Assert.That(character.Combat.BaseAttack.AllMeleeBonuses, Is.Not.EquivalentTo(character.Combat.BaseAttack.AllRangedBonuses), character.Summary);

            Assert.That(character.Combat.HitPoints, Is.AtLeast(character.Class.Level), character.Summary);
            Assert.That(character.Combat.ArmorClass.Full, Is.Positive, character.Summary);
            Assert.That(character.Combat.ArmorClass.FlatFooted, Is.Positive, character.Summary);
            Assert.That(character.Combat.ArmorClass.Touch, Is.Positive, character.Summary);
        }

        private string GetAllFeatsMessage(IEnumerable<Feat> feats)
        {
            var featsWithFoci = feats.Where(f => f.Foci.Any()).Select(f => $"{f.Name}: {string.Join(", ", f.Foci)}").OrderBy(f => f);
            return string.Join("; ", featsWithFoci);
        }
    }
}
