using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Skills;
using CharacterGen.Common.Magics;
using CharacterGen.Common.Races;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Data.Racial.Metaraces
{
    [TestFixture]
    public class LichFeatDataTests : RacialFeatDataTests
    {
        protected override String tableName
        {
            get
            {
                return String.Format(TableNameConstants.Formattable.Collection.RACEFeatData, RaceConstants.Metaraces.Lich);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.ArmorBonus,
                FeatConstants.FearAura,
                FeatConstants.ParalyzingTouch,
                FeatConstants.TurnResistance,
                FeatConstants.DamageReduction + "Slashing",
                FeatConstants.DamageReduction + "Piercing",
                FeatConstants.ImmuneToEffect + FeatConstants.Foci.Cold,
                FeatConstants.ImmuneToEffect + FeatConstants.Foci.Electricity,
                FeatConstants.ImmuneToEffect + SpellConstants.Polymorph,
                FeatConstants.ImmuneToEffect + "Mind",
                FeatConstants.SkillBonus + SkillConstants.Hide,
                FeatConstants.SkillBonus + SkillConstants.Listen,
                FeatConstants.SkillBonus + SkillConstants.MoveSilently,
                FeatConstants.SkillBonus + SkillConstants.Search,
                FeatConstants.SkillBonus + SkillConstants.SenseMotive,
                FeatConstants.SkillBonus + SkillConstants.Spot
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.ArmorBonus,
            FeatConstants.ArmorBonus,
            "",
            0,
            "",
            0,
            "",
            5, 0, 0)]
        [TestCase(FeatConstants.FearAura,
            FeatConstants.FearAura,
            "",
            0,
            "",
            0,
            "",
            0, 0, 0)]
        [TestCase(FeatConstants.ParalyzingTouch,
            FeatConstants.ParalyzingTouch,
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
        [TestCase(FeatConstants.DamageReduction + "Slashing",
            FeatConstants.DamageReduction,
            "Non-magical slashing",
            1,
            FeatConstants.Frequencies.Hit,
            0,
            "",
            15, 0, 0)]
        [TestCase(FeatConstants.DamageReduction + "Piercing",
            FeatConstants.DamageReduction,
            "Non-magical piercing",
            1,
            FeatConstants.Frequencies.Hit,
            0,
            "",
            15, 0, 0)]
        [TestCase(FeatConstants.ImmuneToEffect + FeatConstants.Foci.Cold,
            FeatConstants.ImmuneToEffect,
            FeatConstants.Foci.Cold,
            0,
            "",
            0,
            "",
            0, 0, 0)]
        [TestCase(FeatConstants.ImmuneToEffect + FeatConstants.Foci.Electricity,
            FeatConstants.ImmuneToEffect,
            FeatConstants.Foci.Electricity,
            0,
            "",
            0,
            "",
            0, 0, 0)]
        [TestCase(FeatConstants.ImmuneToEffect + SpellConstants.Polymorph,
            FeatConstants.ImmuneToEffect,
            SpellConstants.Polymorph,
            0,
            "",
            0,
            "",
            0, 0, 0)]
        [TestCase(FeatConstants.ImmuneToEffect + "Mind",
            FeatConstants.ImmuneToEffect,
            "Mind-affecting attacks",
            0,
            "",
            0,
            "",
            0, 0, 0)]
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
        [TestCase(FeatConstants.SkillBonus + SkillConstants.MoveSilently,
            FeatConstants.SkillBonus,
            SkillConstants.MoveSilently,
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
        [TestCase(FeatConstants.SkillBonus + SkillConstants.SenseMotive,
            FeatConstants.SkillBonus,
            SkillConstants.SenseMotive,
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
