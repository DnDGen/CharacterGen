using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Combats;
using CharacterGen.Common.Magics;
using CharacterGen.Common.Races;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Data.Racial.BaseRaces
{
    [TestFixture]
    public class OgreMageFeatDataTests : RacialFeatDataTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Collection.RACEFeatData, RaceConstants.BaseRaces.OgreMage); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.Darkvision,
                FeatConstants.SaveBonus + SavingThrowConstants.Reflex,
                FeatConstants.SaveBonus + SavingThrowConstants.Will,
                FeatConstants.SaveBonus + SavingThrowConstants.Fortitude,
                FeatConstants.NaturalArmor,
                FeatConstants.SpellLikeAbility + SpellConstants.Darkness,
                FeatConstants.SpellLikeAbility + SpellConstants.Invisibility,
                FeatConstants.SpellLikeAbility + SpellConstants.CharmPerson,
                FeatConstants.SpellLikeAbility + SpellConstants.ConeOfCold,
                FeatConstants.SpellLikeAbility + SpellConstants.GaseousForm,
                FeatConstants.SpellLikeAbility + SpellConstants.Sleep,
                FeatConstants.Flight,
                FeatConstants.ChangeShape,
                FeatConstants.Regeneration,
                FeatConstants.SpellResistance
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
        [TestCase(FeatConstants.SaveBonus + SavingThrowConstants.Fortitude,
            FeatConstants.SaveBonus,
            SavingThrowConstants.Fortitude,
            0,
            "",
            0,
            "",
            4,
            0, 0)]
        [TestCase(FeatConstants.SaveBonus + SavingThrowConstants.Will,
            FeatConstants.SaveBonus,
            SavingThrowConstants.Will,
            0,
            "",
            0,
            "",
            1,
            0, 0)]
        [TestCase(FeatConstants.SaveBonus + SavingThrowConstants.Reflex,
            FeatConstants.SaveBonus,
            SavingThrowConstants.Reflex,
            0,
            "",
            0,
            "",
            1,
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
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.Darkness,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Darkness,
            0,
            FeatConstants.Frequencies.AtWill,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.Invisibility,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Invisibility,
            0,
            FeatConstants.Frequencies.AtWill,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.CharmPerson,
            FeatConstants.SpellLikeAbility,
            SpellConstants.CharmPerson,
            1,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.ConeOfCold,
            FeatConstants.SpellLikeAbility,
            SpellConstants.ConeOfCold,
            1,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.GaseousForm,
            FeatConstants.SpellLikeAbility,
            SpellConstants.GaseousForm,
            1,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.Sleep,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Sleep,
            1,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.Flight,
            FeatConstants.Flight,
            "",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.ChangeShape,
            FeatConstants.ChangeShape,
            "",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.Regeneration,
            FeatConstants.Regeneration,
            "",
            5,
            FeatConstants.Frequencies.Round,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellResistance,
            FeatConstants.SpellResistance,
            "",
            0,
            "",
            0,
            "",
            19,
            0, 0)]
        public override void Data(String name, String feat, String focus, Int32 frequencyQuantity, String frequencyTimePeriod, Int32 minimumHitDiceRequirement, String sizeRequirement, Int32 strength, Int32 maximumHitDiceRequirement, Int32 requiredStatMinimumValue, params String[] minimumStats)
        {
            base.Data(name, feat, focus, frequencyQuantity, frequencyTimePeriod, minimumHitDiceRequirement, sizeRequirement, strength, maximumHitDiceRequirement, requiredStatMinimumValue, minimumStats);
        }
    }
}
