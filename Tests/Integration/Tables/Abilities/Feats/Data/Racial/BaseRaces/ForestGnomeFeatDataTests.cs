using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Skills;
using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Magics;
using CharacterGen.Common.Races;
using CharacterGen.Tables;
using NUnit.Framework;
using System;
using TreasureGen.Common.Items;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Data.Racial.BaseRaces
{
    [TestFixture]
    public class ForestGnomeFeatDataTests : RacialFeatDataTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Collection.RACEFeatData, RaceConstants.BaseRaces.ForestGnome); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.LowLightVision,
                FeatConstants.WeaponFamiliarity,
                FeatConstants.SaveBonus,
                FeatConstants.ImprovedSpell,
                FeatConstants.AttackBonus + RaceConstants.BaseRaces.Goblin,
                FeatConstants.AttackBonus + RaceConstants.BaseRaces.Kobold,
                FeatConstants.AttackBonus + RaceConstants.BaseRaces.Orc,
                FeatConstants.AttackBonus + RaceConstants.BaseRaces.Lizardfolk,
                FeatConstants.DodgeBonus,
                FeatConstants.SkillBonus + SkillConstants.Listen,
                FeatConstants.SpellLikeAbility + SpellConstants.SpeakWithAnimals,
                FeatConstants.SpellLikeAbility + SpellConstants.DancingLights,
                FeatConstants.SpellLikeAbility + SpellConstants.GhostSound,
                FeatConstants.SpellLikeAbility + SpellConstants.Prestidigitation,
                FeatConstants.SpellLikeAbility + SpellConstants.PassWithoutTrace,
                FeatConstants.SkillBonus + SkillConstants.Hide
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.LowLightVision,
            FeatConstants.LowLightVision,
            "",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.WeaponFamiliarity,
            FeatConstants.WeaponFamiliarity,
            WeaponConstants.GnomeHookedHammer,
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SaveBonus,
            FeatConstants.SaveBonus,
            CharacterClassConstants.Schools.Illusion,
            0,
            "",
            0,
            "",
            2,
            0, 0)]
        [TestCase(FeatConstants.ImprovedSpell,
            FeatConstants.ImprovedSpell,
            CharacterClassConstants.Schools.Illusion,
            0,
            "",
            0,
            "",
            1,
            0, 0)]
        [TestCase(FeatConstants.AttackBonus + RaceConstants.BaseRaces.Goblin,
            FeatConstants.AttackBonus,
            "Goblinoids",
            0,
            "",
            0,
            "",
            1,
            0, 0)]
        [TestCase(FeatConstants.AttackBonus + RaceConstants.BaseRaces.Kobold,
            FeatConstants.AttackBonus,
            RaceConstants.BaseRaces.Kobold,
            0,
            "",
            0,
            "",
            1,
            0, 0)]
        [TestCase(FeatConstants.AttackBonus + RaceConstants.BaseRaces.Orc,
            FeatConstants.AttackBonus,
            RaceConstants.BaseRaces.Orc,
            0,
            "",
            0,
            "",
            1,
            0, 0)]
        [TestCase(FeatConstants.AttackBonus + RaceConstants.BaseRaces.Lizardfolk,
            FeatConstants.AttackBonus,
            "Reptilian humanoids",
            0,
            "",
            0,
            "",
            1,
            0, 0)]
        [TestCase(FeatConstants.DodgeBonus,
            FeatConstants.DodgeBonus,
            "Giant",
            0,
            "",
            0,
            "",
            4,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Listen,
            FeatConstants.SkillBonus,
            SkillConstants.Listen,
            0,
            "",
            0,
            "",
            2,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.DancingLights,
            FeatConstants.SpellLikeAbility,
            SpellConstants.DancingLights,
            1,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 10, StatConstants.Charisma)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.GhostSound,
            FeatConstants.SpellLikeAbility,
            SpellConstants.GhostSound,
            1,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 10, StatConstants.Charisma)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.SpeakWithAnimals,
            FeatConstants.SpellLikeAbility,
            SpellConstants.SpeakWithAnimals + " (Forest animals)",
            0,
            FeatConstants.Frequencies.Constant,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.Prestidigitation,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Prestidigitation,
            1,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 10, StatConstants.Charisma)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.PassWithoutTrace,
            FeatConstants.SpellLikeAbility,
            SpellConstants.PassWithoutTrace,
            0,
            FeatConstants.Frequencies.Constant,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Hide,
            FeatConstants.SkillBonus,
            SkillConstants.Hide,
            0,
            "",
            0,
            "",
            4,
            0, 0)]
        public override void Data(String name, String feat, String focus, Int32 frequencyQuantity, String frequencyTimePeriod, Int32 minimumHitDiceRequirement, String sizeRequirement, Int32 strength, Int32 maximumHitDiceRequirement, Int32 requiredStatMinimumValue, params String[] minimumStats)
        {
            base.Data(name, feat, focus, frequencyQuantity, frequencyTimePeriod, minimumHitDiceRequirement, sizeRequirement, strength, maximumHitDiceRequirement, requiredStatMinimumValue, minimumStats);
        }
    }
}
