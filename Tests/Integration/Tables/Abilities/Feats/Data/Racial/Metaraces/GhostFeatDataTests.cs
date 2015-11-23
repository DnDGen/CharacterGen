using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Skills;
using CharacterGen.Common.Races;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Data.Racial.Metaraces
{
    [TestFixture]
    public class GhostFeatDataTests : RacialFeatDataTests
    {
        protected override String tableName
        {
            get
            {
                return String.Format(TableNameConstants.Formattable.Collection.RACEFeatData, RaceConstants.Metaraces.Ghost);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.CorruptingGaze,
                FeatConstants.CorruptingTouch,
                FeatConstants.DrainingTouch,
                FeatConstants.FrightfulMoan,
                FeatConstants.HorrificAppearance,
                FeatConstants.Malevolence,
                FeatConstants.Manifestation,
                FeatConstants.Telekinesis,
                FeatConstants.Rejuvenation,
                FeatConstants.TurnResistance,
                FeatConstants.SkillBonus + SkillConstants.Hide,
                FeatConstants.SkillBonus + SkillConstants.Listen,
                FeatConstants.SkillBonus + SkillConstants.Search,
                FeatConstants.SkillBonus + SkillConstants.Spot
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.CorruptingGaze,
            FeatConstants.CorruptingGaze,
            "",
            0,
            "",
            0,
            "",
            0, 0, 0)]
        [TestCase(FeatConstants.CorruptingTouch,
            FeatConstants.CorruptingTouch,
            "",
            0,
            "",
            0,
            "",
            0, 0, 0)]
        [TestCase(FeatConstants.DrainingTouch,
            FeatConstants.DrainingTouch,
            "",
            0,
            "",
            0,
            "",
            0, 0, 0)]
        [TestCase(FeatConstants.FrightfulMoan,
            FeatConstants.FrightfulMoan,
            "",
            0,
            "",
            0,
            "",
            0, 0, 0)]
        [TestCase(FeatConstants.HorrificAppearance,
            FeatConstants.HorrificAppearance,
            "",
            0,
            "",
            0,
            "",
            0, 0, 0)]
        [TestCase(FeatConstants.Malevolence,
            FeatConstants.Malevolence,
            "",
            1,
            FeatConstants.Frequencies.Round,
            0,
            "",
            0, 0, 0)]
        [TestCase(FeatConstants.Manifestation,
            FeatConstants.Manifestation,
            "",
            0,
            "",
            0,
            "",
            0, 0, 0)]
        [TestCase(FeatConstants.Telekinesis,
            FeatConstants.Telekinesis,
            "",
            0,
            "",
            0,
            "",
            0, 0, 0)]
        [TestCase(FeatConstants.Rejuvenation,
            FeatConstants.Rejuvenation,
            "",
            0,
            "",
            0,
            "",
            0, 0, 0)]
        [TestCase(FeatConstants.TurnResistance,
            FeatConstants.TurnResistance,
            "",
            0,
            "",
            0,
            "",
            4, 0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Hide,
            FeatConstants.SkillBonus,
            SkillConstants.Hide,
            0,
            "",
            0,
            "",
            8, 0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Listen,
            FeatConstants.SkillBonus,
            SkillConstants.Listen,
            0,
            "",
            0,
            "",
            8, 0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Search,
            FeatConstants.SkillBonus,
            SkillConstants.Search,
            0,
            "",
            0,
            "",
            8, 0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Spot,
            FeatConstants.SkillBonus,
            SkillConstants.Spot,
            0,
            "",
            0,
            "",
            8, 0, 0)]
        public override void Data(String name, String feat, String focus, Int32 frequencyQuantity, String frequencyTimePeriod, Int32 minimumHitDiceRequirement, String sizeRequirement, Int32 strength, Int32 maximumHitDiceRequirement, Int32 requiredStatMinimumValue, params String[] minimumStats)
        {
            base.Data(name, feat, focus, frequencyQuantity, frequencyTimePeriod, minimumHitDiceRequirement, sizeRequirement, strength, maximumHitDiceRequirement, requiredStatMinimumValue, minimumStats);
        }
    }
}
