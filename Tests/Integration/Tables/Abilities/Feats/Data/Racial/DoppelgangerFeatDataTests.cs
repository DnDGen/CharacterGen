using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Skills;
using CharacterGen.Common.Magics;
using CharacterGen.Common.Races;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Data.Racial
{
    [TestFixture]
    public class DoppelgangerFeatDataTests : RacialFeatDataTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Collection.RACEFeatData, RaceConstants.BaseRaces.Doppelganger); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.SkillBonus + SkillConstants.Bluff,
                FeatConstants.SkillBonus + SkillConstants.Disguise,
                FeatConstants.Darkvision,
                FeatConstants.NaturalArmor,
                FeatConstants.SpellLikeAbility,
                FeatConstants.ChangeShape,
                FeatConstants.ImmuneToEffect + "Sleep",
                FeatConstants.ImmuneToEffect + "Charm"
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.SkillBonus + SkillConstants.Bluff,
            FeatConstants.SkillBonus,
            SkillConstants.Bluff,
            0,
            "",
            0,
            "",
            4,
            0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Disguise,
            FeatConstants.SkillBonus,
            SkillConstants.Disguise,
            0,
            "",
            0,
            "",
            4,
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
        [TestCase(FeatConstants.NaturalArmor,
            FeatConstants.NaturalArmor,
            "",
            0,
            "",
            0,
            "",
            4,
            0)]
        [TestCase(FeatConstants.SpellLikeAbility,
            FeatConstants.SpellLikeAbility,
            SpellConstants.DetectThoughts,
            0,
            FeatConstants.Frequencies.Constant,
            0,
            "",
            0,
            0)]
        [TestCase(FeatConstants.ChangeShape,
            FeatConstants.ChangeShape,
            "",
            0,
            "",
            0,
            "",
            0,
            0)]
        [TestCase(FeatConstants.ImmuneToEffect + "Charm",
            FeatConstants.ImmuneToEffect,
            "Charm",
            0,
            "",
            0,
            "",
            0,
            0)]
        [TestCase(FeatConstants.ImmuneToEffect + "Sleep",
            FeatConstants.ImmuneToEffect,
            "Sleep",
            0,
            "",
            0,
            "",
            0,
            0)]
        public override void Data(String name, String feat, String focus, Int32 frequencyQuantity, String frequencyTimePeriod, Int32 minimumHitDiceRequirement, String sizeRequirement, Int32 strength, Int32 maximumHitDiceRequirement)
        {
            base.Data(name, feat, focus, frequencyQuantity, frequencyTimePeriod, minimumHitDiceRequirement, sizeRequirement, strength, maximumHitDiceRequirement);
        }
    }
}
