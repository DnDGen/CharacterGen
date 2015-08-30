using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Skills;
using CharacterGen.Common.Items;
using CharacterGen.Common.Races;
using CharacterGen.Tables;
using NUnit.Framework;
using System;
using TreasureGen.Common.Items;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Data.Racial.BaseRaces
{
    [TestFixture]
    public class MinotaurFeatDataTests : RacialFeatDataTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Collection.RACEFeatData, RaceConstants.BaseRaces.Minotaur); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.Darkvision,
                FeatConstants.MartialWeaponProficiency + WeaponConstants.Greataxe,
                FeatConstants.SimpleWeaponProficiency,
                FeatConstants.NaturalArmor,
                FeatConstants.NaturalWeapon,
                FeatConstants.PowerfulCharge,
                FeatConstants.NaturalCunning,
                FeatConstants.Scent,
                FeatConstants.SkillBonus + SkillConstants.Search,
                FeatConstants.SkillBonus + SkillConstants.Spot,
                FeatConstants.SkillBonus + SkillConstants.Listen
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.Darkvision,
            FeatConstants.Darkvision,
            "",
            0,
            "",
            0,
            "",
            60,
            0, 0)]
        [TestCase(FeatConstants.MartialWeaponProficiency + WeaponConstants.Greataxe,
            FeatConstants.MartialWeaponProficiency,
            WeaponConstants.Greataxe,
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SimpleWeaponProficiency,
            FeatConstants.SimpleWeaponProficiency,
            ProficiencyConstants.All,
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.NaturalArmor,
            FeatConstants.NaturalArmor,
            "",
            0,
            "",
            0,
            "",
            5,
            0, 0)]
        [TestCase(FeatConstants.NaturalWeapon,
            FeatConstants.NaturalWeapon,
            "Gore",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.PowerfulCharge,
            FeatConstants.PowerfulCharge,
            "",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.NaturalCunning,
            FeatConstants.NaturalCunning,
            "",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.Scent,
            FeatConstants.Scent,
            "",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Search,
            FeatConstants.SkillBonus,
            SkillConstants.Search,
            0,
            "",
            0,
            "",
            4,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Spot,
            FeatConstants.SkillBonus,
            SkillConstants.Spot,
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
            4,
            0, 0)]
        public override void Data(String name, String feat, String focus, Int32 frequencyQuantity, String frequencyTimePeriod, Int32 minimumHitDiceRequirement, String sizeRequirement, Int32 strength, Int32 maximumHitDiceRequirement, Int32 requiredStatMinimumValue, params String[] minimumStats)
        {
            base.Data(name, feat, focus, frequencyQuantity, frequencyTimePeriod, minimumHitDiceRequirement, sizeRequirement, strength, maximumHitDiceRequirement, requiredStatMinimumValue, minimumStats);
        }
    }
}
