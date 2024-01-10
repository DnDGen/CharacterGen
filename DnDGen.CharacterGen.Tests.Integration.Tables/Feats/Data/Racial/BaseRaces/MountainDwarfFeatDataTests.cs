using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Feats;
using DnDGen.CharacterGen.Races;
using DnDGen.CharacterGen.Skills;
using NUnit.Framework;
using System;
using DnDGen.TreasureGen.Items;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Feats.Data.Racial.BaseRaces
{
    [TestFixture]
    public class MountainDwarfFeatDataTests : RacialFeatDataTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Collection.RACEFeatData, RaceConstants.BaseRaces.MountainDwarf); }
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
        [TestCase(FeatConstants.AttackBonus + RaceConstants.BaseRaces.Orc,
            FeatConstants.AttackBonus,
            "Orcs",
            0,
            "",
            0,
            "",
            1,
            0, 0)]
        [TestCase(FeatConstants.SaveBonus + "Spell",
            FeatConstants.SaveBonus,
            "Spells and spell-like effects",
            0,
            "",
            0,
            "",
            2,
            0, 0)]
        [TestCase(FeatConstants.SaveBonus + "Poison",
            FeatConstants.SaveBonus,
            "Poison",
            0,
            "",
            0,
            "",
            2,
            0, 0)]
        [TestCase(FeatConstants.DodgeBonus,
            FeatConstants.DodgeBonus,
            "Giants",
            0,
            "",
            0,
            "",
            4,
            0, 0)]
        [TestCase(FeatConstants.Stability,
            FeatConstants.Stability,
            "",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.WeaponFamiliarity + WeaponConstants.DwarvenWaraxe,
            FeatConstants.WeaponFamiliarity,
            WeaponConstants.DwarvenWaraxe,
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.WeaponFamiliarity + WeaponConstants.DwarvenUrgrosh,
            FeatConstants.WeaponFamiliarity,
            WeaponConstants.DwarvenUrgrosh,
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.Stonecunning,
            FeatConstants.Stonecunning,
            "",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.Darkvision,
            FeatConstants.Darkvision,
            "",
            0,
            "",
            0,
            "",
            60,
            0, 0)]
        public override void RacialFeatData(String name, String feat, String focus, Int32 frequencyQuantity, String frequencyTimePeriod, Int32 minimumHitDiceRequirement, String sizeRequirement, Int32 strength, Int32 maximumHitDiceRequirement, Int32 requiredStatMinimumValue, params String[] minimumAbilities)
        {
            base.RacialFeatData(name, feat, focus, frequencyQuantity, frequencyTimePeriod, minimumHitDiceRequirement, sizeRequirement, strength, maximumHitDiceRequirement, requiredStatMinimumValue, minimumAbilities);
        }
    }
}
