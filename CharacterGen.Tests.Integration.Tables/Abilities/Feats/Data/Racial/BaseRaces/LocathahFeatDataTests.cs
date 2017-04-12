using CharacterGen.Abilities.Feats;
using CharacterGen.Abilities.Skills;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;
using TreasureGen.Items;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Data.Racial.BaseRaces
{
    [TestFixture]
    public class LocathahFeatDataTests : RacialFeatDataTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Collection.RACEFeatData, RaceConstants.BaseRaces.Locathah); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.SimpleWeaponProficiency + WeaponConstants.Longspear,
                FeatConstants.SimpleWeaponProficiency + WeaponConstants.LightCrossbow,
                FeatConstants.WeaponFocus + WeaponConstants.Longspear,
                FeatConstants.SkillBonus + SkillConstants.Swim,
                FeatConstants.SkillBonus + SkillConstants.Swim + "Take 10",
                FeatConstants.Run + SkillConstants.Swim,
                FeatConstants.NaturalArmor,
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.SkillBonus + SkillConstants.Swim,
            FeatConstants.SkillBonus,
            SkillConstants.Swim,
            0,
            "",
            0,
            "",
            8,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Swim + "Take 10",
            FeatConstants.SkillBonus,
            SkillConstants.Swim + " (can always take 10)",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.Run + SkillConstants.Swim,
            FeatConstants.Run,
            "Can use while swimming in a straight line",
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
            3,
            0, 0)]
        [TestCase(FeatConstants.SimpleWeaponProficiency + WeaponConstants.Longspear,
            FeatConstants.SimpleWeaponProficiency,
            WeaponConstants.Longspear,
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SimpleWeaponProficiency + WeaponConstants.LightCrossbow,
            FeatConstants.SimpleWeaponProficiency,
            WeaponConstants.LightCrossbow,
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.WeaponFocus + WeaponConstants.Longspear,
            FeatConstants.WeaponFocus,
            WeaponConstants.Longspear,
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        public override void RacialFeatData(string name, string feat, string focus, int frequencyQuantity, string frequencyTimePeriod, int minimumHitDiceRequirement, string sizeRequirement, int strength, int maximumHitDiceRequirement, int requiredStatMinimumValue, params string[] minimumStats)
        {
            base.RacialFeatData(name, feat, focus, frequencyQuantity, frequencyTimePeriod, minimumHitDiceRequirement, sizeRequirement, strength, maximumHitDiceRequirement, requiredStatMinimumValue, minimumStats);
        }
    }
}
