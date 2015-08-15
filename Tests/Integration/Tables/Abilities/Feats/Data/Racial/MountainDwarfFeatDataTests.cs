using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Skills;
using CharacterGen.Common.Races;
using CharacterGen.Tables;
using NUnit.Framework;
using System;
using TreasureGen.Common.Items;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Data.Racial
{
    [TestFixture]
    public class MountainDwarfFeatDataTests : RacialFeatDataTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Collection.RACEFeatData, RaceConstants.BaseRaces.MountainDwarf); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.Darkvision,
                FeatConstants.Stonecunning,
                FeatConstants.WeaponFamiliarity + WeaponConstants.DwarvenUrgrosh,
                FeatConstants.WeaponFamiliarity + WeaponConstants.DwarvenWaraxe,
                FeatConstants.Stability,
                FeatConstants.SaveBonus + "Poison",
                FeatConstants.SaveBonus + "Spell",
                FeatConstants.AttackBonus + RaceConstants.BaseRaces.Orc,
                FeatConstants.AttackBonus + RaceConstants.BaseRaces.Goblin,
                FeatConstants.DodgeBonus,
                FeatConstants.SkillBonus + SkillConstants.Appraise
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.SkillBonus + SkillConstants.Appraise,
            FeatConstants.SkillBonus,
            SkillConstants.Appraise + " (Stone or metal items)",
            0,
            "",
            0,
            "",
            2,
            0)]
        [TestCase(FeatConstants.AttackBonus + RaceConstants.BaseRaces.Goblin,
            FeatConstants.AttackBonus,
            "Goblinoids",
            0,
            "",
            0,
            "",
            1,
            0)]
        [TestCase(FeatConstants.AttackBonus + RaceConstants.BaseRaces.Orc,
            FeatConstants.AttackBonus,
            "Orcs",
            0,
            "",
            0,
            "",
            1,
            0)]
        [TestCase(FeatConstants.SaveBonus + "Spell",
            FeatConstants.SaveBonus,
            "Spells and spell-like effects",
            0,
            "",
            0,
            "",
            2,
            0)]
        [TestCase(FeatConstants.SaveBonus + "Poison",
            FeatConstants.SaveBonus,
            "Poison",
            0,
            "",
            0,
            "",
            2,
            0)]
        [TestCase(FeatConstants.DodgeBonus,
            FeatConstants.DodgeBonus,
            "Giants",
            0,
            "",
            0,
            "",
            4,
            0)]
        [TestCase(FeatConstants.Stability,
            FeatConstants.Stability,
            "",
            0,
            "",
            0,
            "",
            0,
            0)]
        [TestCase(FeatConstants.WeaponFamiliarity + WeaponConstants.DwarvenWaraxe,
            FeatConstants.WeaponFamiliarity,
            WeaponConstants.DwarvenWaraxe,
            0,
            "",
            0,
            "",
            0,
            0)]
        [TestCase(FeatConstants.WeaponFamiliarity + WeaponConstants.DwarvenUrgrosh,
            FeatConstants.WeaponFamiliarity,
            WeaponConstants.DwarvenUrgrosh,
            0,
            "",
            0,
            "",
            0,
            0)]
        [TestCase(FeatConstants.Stonecunning,
            FeatConstants.Stonecunning,
            "",
            0,
            "",
            0,
            "",
            0,
            0)]
        [TestCase(FeatConstants.Darkvision,
            FeatConstants.Darkvision,
            "",
            0,
            "",
            0,
            "",
            60,
            0)]
        public override void Data(String name, String feat, String focus, Int32 frequencyQuantity, String frequencyTimePeriod, Int32 minimumHitDiceRequirement, String sizeRequirement, Int32 strength, Int32 maximumHitDiceRequirement)
        {
            base.Data(name, feat, focus, frequencyQuantity, frequencyTimePeriod, minimumHitDiceRequirement, sizeRequirement, strength, maximumHitDiceRequirement);
        }
    }
}
