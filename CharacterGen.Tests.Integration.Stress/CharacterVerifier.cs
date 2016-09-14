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
            Assert.That(character.Alignment.Goodness, Is.EqualTo(AlignmentConstants.Good)
                .Or.EqualTo(AlignmentConstants.Neutral)
                .Or.EqualTo(AlignmentConstants.Evil));
            Assert.That(character.Alignment.Lawfulness, Is.EqualTo(AlignmentConstants.Lawful)
                .Or.EqualTo(AlignmentConstants.Neutral)
                .Or.EqualTo(AlignmentConstants.Chaotic));

            Assert.That(character.Class.Name, Is.Not.Empty);
            Assert.That(character.Class.Level, Is.Positive);
            Assert.That(character.Class.ProhibitedFields, Is.Not.Null);
            Assert.That(character.Class.SpecialistFields, Is.Not.Null);

            Assert.That(character.InterestingTrait, Is.Not.Null);

            Assert.That(character.Race.BaseRace, Is.Not.Empty);
            Assert.That(character.Race.Metarace, Is.Not.Null);
            Assert.That(character.Race.AerialSpeed, Is.Not.Negative);
            Assert.That(character.Race.AerialSpeed % 10, Is.EqualTo(0));
            Assert.That(character.Race.Age.Stage, Is.EqualTo(RaceConstants.Ages.Adulthood)
                .Or.EqualTo(RaceConstants.Ages.MiddleAge)
                .Or.EqualTo(RaceConstants.Ages.Old)
                .Or.EqualTo(RaceConstants.Ages.Venerable));
            Assert.That(character.Race.Age.Years, Is.Positive);
            Assert.That(character.Race.Age.Maximum, Is.Positive);
            Assert.That(character.Race.Age.Years, Is.LessThanOrEqualTo(character.Race.Age.Maximum));
            Assert.That(character.Race.HeightInInches, Is.Positive);
            Assert.That(character.Race.WeightInPounds, Is.Positive);

            if (character.Race.HasWings)
                Assert.That(character.Race.AerialSpeed, Is.Positive);

            Assert.That(character.Race.LandSpeed, Is.Positive);
            Assert.That(character.Race.LandSpeed % 10, Is.EqualTo(0));
            Assert.That(character.Race.MetaraceSpecies, Is.Not.Null);
            Assert.That(character.Race.Size, Is.EqualTo(RaceConstants.Sizes.Large)
                .Or.EqualTo(RaceConstants.Sizes.Medium)
                .Or.EqualTo(RaceConstants.Sizes.Small));

            Assert.That(character.Ability.Stats.Count, Is.InRange(5, 6));
            Assert.That(character.Ability.Stats.Keys, Contains.Item(StatConstants.Charisma));
            Assert.That(character.Ability.Stats.Keys, Contains.Item(StatConstants.Dexterity));
            Assert.That(character.Ability.Stats.Keys, Contains.Item(StatConstants.Intelligence));
            Assert.That(character.Ability.Stats.Keys, Contains.Item(StatConstants.Strength));
            Assert.That(character.Ability.Stats.Keys, Contains.Item(StatConstants.Wisdom));

            foreach (var statKVP in character.Ability.Stats)
            {
                var stat = statKVP.Value;
                Assert.That(stat.Name, Is.EqualTo(statKVP.Key));
                Assert.That(stat.Value, Is.Positive);
            }

            Assert.That(character.Ability.Languages, Is.Not.Empty);
            Assert.That(character.Ability.Skills, Is.Not.Empty);

            foreach (var skillKVP in character.Ability.Skills)
            {
                var skill = skillKVP.Value;
                Assert.That(skill.Name, Is.EqualTo(skillKVP.Key));
                Assert.That(skill.ArmorCheckPenalty, Is.Not.Positive);
            }

            Assert.That(character.Ability.Feats, Is.Not.Empty);

            foreach (var feat in character.Ability.Feats)
            {
                Assert.That(feat.Name, Is.Not.Empty);
                Assert.That(feat.Foci, Is.Not.Null, feat.Name);
                Assert.That(feat.Power, Is.Not.Negative, feat.Name);
                Assert.That(feat.Frequency.Quantity, Is.Not.Negative, feat.Name);
                Assert.That(feat.Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Constant)
                    .Or.EqualTo(FeatConstants.Frequencies.AtWill)
                    .Or.EqualTo(FeatConstants.Frequencies.Day)
                    .Or.EqualTo(FeatConstants.Frequencies.Week)
                    .Or.EqualTo(FeatConstants.Frequencies.Round)
                    .Or.EqualTo(FeatConstants.Frequencies.Hit)
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
            }

            Assert.That(character.Equipment.Treasure, Is.Not.Null);
            Assert.That(character.Equipment.Treasure.Items, Is.Not.Null);

            foreach (var item in character.Equipment.Treasure.Items)
                Assert.That(item, Is.Not.Null);

            foreach (var spells in character.Magic.SpellsPerDay)
            {
                Assert.That(spells.Level, Is.Not.Negative, spells.Level.ToString());
                Assert.That(spells.Quantity, Is.Not.Negative, spells.Level.ToString());
            }

            foreach (var spell in character.Magic.KnownSpells)
            {
                Assert.That(spell.Level, Is.Not.Negative);
                Assert.That(spell.Metamagic, Is.Empty);
                Assert.That(spell.Name, Is.Not.Empty);
            }

            foreach (var spell in character.Magic.PreparedSpells)
            {
                Assert.That(spell.Level, Is.Not.Negative);
                Assert.That(spell.Metamagic, Is.Empty);
                Assert.That(spell.Name, Is.Not.Empty);

                var knownSpellNames = character.Magic.KnownSpells.Select(s => s.Name);
                Assert.That(knownSpellNames, Contains.Item(spell.Name), character.Class.Name);
            }

            Assert.That(character.Combat.BaseAttack.BaseBonus, Is.Not.Negative);
            Assert.That(character.Combat.BaseAttack.AllMeleeBonuses.Count, Is.LessThanOrEqualTo(4));
            Assert.That(character.Combat.BaseAttack.AllRangedBonuses.Count, Is.LessThanOrEqualTo(4));
            Assert.That(character.Combat.BaseAttack.DexterityBonus, Is.EqualTo(character.Ability.Stats[StatConstants.Dexterity].Bonus));
            Assert.That(character.Combat.BaseAttack.StrengthBonus, Is.EqualTo(character.Ability.Stats[StatConstants.Strength].Bonus));
            Assert.That(character.Combat.HitPoints, Is.AtLeast(character.Class.Level));
            Assert.That(character.Combat.ArmorClass.Full, Is.Positive);
            Assert.That(character.Combat.ArmorClass.FlatFooted, Is.Positive);
            Assert.That(character.Combat.ArmorClass.Touch, Is.Positive);
        }

        private string GetAllFeatsMessage(IEnumerable<Feat> feats)
        {
            var featsWithFoci = feats.Where(f => f.Foci.Any()).Select(f => $"{f.Name}: {string.Join(", ", f.Foci)}").OrderBy(f => f);
            return string.Join("; ", featsWithFoci);
        }
    }
}
