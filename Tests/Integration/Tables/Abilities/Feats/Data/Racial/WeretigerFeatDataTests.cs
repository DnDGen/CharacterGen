using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Skills;
using CharacterGen.Common.Races;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Data.Racial
{
    [TestFixture]
    public class WeretigerFeatDataTests : RacialFeatDataTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Collection.RACEFeatData, RaceConstants.Metaraces.Weretiger); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.AlternateForm,
                FeatConstants.Empathy,
                FeatConstants.Lycanthropy,
                FeatConstants.ImprovedGrab,
                FeatConstants.Pounce,
                FeatConstants.Rake,
                FeatConstants.SkillBonus + SkillConstants.MoveSilently,
                FeatConstants.SkillBonus + SkillConstants.Balance,
                FeatConstants.SkillBonus + SkillConstants.Hide,
                FeatConstants.SaveBonus + "Reflex",
                FeatConstants.SaveBonus + "Fortitude",
                FeatConstants.SaveBonus + "Will",
                FeatConstants.SkillBonus + SkillConstants.Listen,
                FeatConstants.SkillBonus + SkillConstants.Spot,
                FeatConstants.Alertness,
                FeatConstants.IronWill,
                FeatConstants.LowLightVision,
                FeatConstants.Scent
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.AlternateForm,
            FeatConstants.AlternateForm,
            "Tiger",
            0,
            "",
            0,
            "",
            0,
            0, "", 0)]
        [TestCase(FeatConstants.Empathy,
            FeatConstants.Empathy,
            "Tiger",
            0,
            "",
            0,
            "",
            4,
            0, "", 0)]
        [TestCase(FeatConstants.Lycanthropy,
            FeatConstants.Lycanthropy,
            "Weretiger",
            0,
            "",
            0,
            "",
            0,
            0, "", 0)]
        [TestCase(FeatConstants.Disease,
            FeatConstants.Disease,
            "Filth fever",
            0,
            "",
            0,
            "",
            0,
            0, "", 0)]
        [TestCase(FeatConstants.IronWill,
            FeatConstants.IronWill,
            "",
            0,
            "",
            0,
            "",
            2,
            0, "", 0)]
        [TestCase(FeatConstants.Alertness,
            FeatConstants.Alertness,
            "",
            0,
            "",
            0,
            "",
            2,
            0, "", 0)]
        [TestCase(FeatConstants.SaveBonus,
            FeatConstants.SaveBonus,
            "All",
            0,
            "",
            0,
            "",
            2,
            0, "", 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Hide,
            FeatConstants.SkillBonus,
            SkillConstants.Hide,
            0,
            "",
            0,
            "",
            1,
            0, "", 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.MoveSilently,
            FeatConstants.SkillBonus,
            SkillConstants.MoveSilently,
            0,
            "",
            0,
            "",
            1,
            0, "", 0)]
        [TestCase(FeatConstants.NaturalArmor,
            FeatConstants.NaturalArmor,
            "",
            0,
            "",
            0,
            "",
            2,
            0, "", 0)]
        [TestCase(FeatConstants.WeaponFinesse,
            FeatConstants.WeaponFinesse,
            "",
            0,
            "",
            0,
            "",
            0,
            0, "", 0)]
        [TestCase(FeatConstants.LowLightVision,
            FeatConstants.LowLightVision,
            "",
            0,
            "",
            0,
            "",
            0,
            0, "", 0)]
        [TestCase(FeatConstants.Scent,
            FeatConstants.Scent,
            "",
            0,
            "",
            0,
            "",
            0,
            0, "", 0)]
        public override void Data(String name, String feat, String focus, Int32 frequencyQuantity, String frequencyTimePeriod, Int32 minimumHitDiceRequirement, String sizeRequirement, Int32 strength, Int32 maximumHitDiceRequirement, String requiredStat, Int32 requiredStatMinimumValue)
        {
            base.Data(name, feat, focus, frequencyQuantity, frequencyTimePeriod, minimumHitDiceRequirement, sizeRequirement, strength, maximumHitDiceRequirement, requiredStat, requiredStatMinimumValue);
        }
    }
}
