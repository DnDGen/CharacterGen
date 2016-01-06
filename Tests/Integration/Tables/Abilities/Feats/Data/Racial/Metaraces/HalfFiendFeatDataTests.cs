using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.Magics;
using CharacterGen.Common.Races;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Data.Racial.Metaraces
{
    [TestFixture]
    public class HalfFiendFeatDataTests : RacialFeatDataTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Collection.RACEFeatData, RaceConstants.Metaraces.HalfFiend); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.NaturalArmor,
                FeatConstants.NaturalWeapon + "Claw",
                FeatConstants.NaturalWeapon + "Bite",
                FeatConstants.SmiteGood,
                FeatConstants.SpellLikeAbility + SpellConstants.Darkness,
                FeatConstants.SpellLikeAbility + SpellConstants.Desecrate,
                FeatConstants.SpellLikeAbility + SpellConstants.UnholyBlight,
                FeatConstants.SpellLikeAbility + SpellConstants.Poison,
                FeatConstants.SpellLikeAbility + SpellConstants.Contagion,
                FeatConstants.SpellLikeAbility + SpellConstants.Blasphemy,
                FeatConstants.SpellLikeAbility + SpellConstants.UnholyAura,
                FeatConstants.SpellLikeAbility + SpellConstants.Unhallow,
                FeatConstants.SpellLikeAbility + SpellConstants.HorridWilting,
                FeatConstants.SpellLikeAbility + SpellConstants.SummonMonsterIX,
                FeatConstants.SpellLikeAbility + SpellConstants.Destruction,
                FeatConstants.Darkvision,
                FeatConstants.ImmuneToEffect,
                FeatConstants.Resistance + FeatConstants.Foci.Acid,
                FeatConstants.Resistance + FeatConstants.Foci.Cold,
                FeatConstants.Resistance + FeatConstants.Foci.Electricity,
                FeatConstants.Resistance + FeatConstants.Foci.Fire,
                FeatConstants.DamageReduction + "11-",
                FeatConstants.DamageReduction + "12+",
                FeatConstants.SpellResistance
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.SmiteGood,
            FeatConstants.SmiteGood,
            "",
            1,
            FeatConstants.Frequencies.Day,
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
            1,
            0, 0)]
        [TestCase(FeatConstants.NaturalWeapon + "Claw",
            FeatConstants.NaturalWeapon,
            "Claw (x2)",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.NaturalWeapon + "Bite",
            FeatConstants.NaturalWeapon,
            "Bite",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.ImmuneToEffect,
            FeatConstants.ImmuneToEffect,
            "Poison",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.Resistance + FeatConstants.Foci.Acid,
            FeatConstants.Resistance,
            FeatConstants.Foci.Acid,
            0,
            "",
            0,
            "",
            10,
            0, 0)]
        [TestCase(FeatConstants.Resistance + FeatConstants.Foci.Cold,
            FeatConstants.Resistance,
            FeatConstants.Foci.Cold,
            0,
            "",
            0,
            "",
            10,
            0, 0)]
        [TestCase(FeatConstants.Resistance + FeatConstants.Foci.Electricity,
            FeatConstants.Resistance,
            FeatConstants.Foci.Electricity,
            0,
            "",
            0,
            "",
            10,
            0, 0)]
        [TestCase(FeatConstants.Resistance + FeatConstants.Foci.Fire,
            FeatConstants.Resistance,
            FeatConstants.Foci.Fire,
            0,
            "",
            0,
            "",
            10,
            0, 0)]
        [TestCase(FeatConstants.DamageReduction + "11-",
            FeatConstants.DamageReduction,
            "Non-magical",
            1,
            FeatConstants.Frequencies.Hit,
            1,
            "",
            5,
            11, 0)]
        [TestCase(FeatConstants.DamageReduction + "12+",
            FeatConstants.DamageReduction,
            "Non-magical",
            1,
            FeatConstants.Frequencies.Hit,
            12,
            "",
            10,
            0, 0)]
        [TestCase(FeatConstants.SpellResistance,
            FeatConstants.SpellResistance,
            "",
            0,
            "",
            0,
            "",
            10,
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
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.Darkness,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Darkness,
            3,
            FeatConstants.Frequencies.Day,
            1,
            "",
            0,
            0,
            8, StatConstants.Wisdom, StatConstants.Intelligence)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.Desecrate,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Desecrate,
            1,
            FeatConstants.Frequencies.Day,
            3,
            "",
            0,
            0,
            8, StatConstants.Wisdom, StatConstants.Intelligence)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.UnholyBlight,
            FeatConstants.SpellLikeAbility,
            SpellConstants.UnholyBlight,
            1,
            FeatConstants.Frequencies.Day,
            5,
            "",
            0,
            0,
            8, StatConstants.Wisdom, StatConstants.Intelligence)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.Poison,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Poison,
            3,
            FeatConstants.Frequencies.Day,
            7,
            "",
            0,
            0,
            8, StatConstants.Wisdom, StatConstants.Intelligence)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.Contagion,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Contagion,
            1,
            FeatConstants.Frequencies.Day,
            9,
            "",
            0,
            0,
            8, StatConstants.Wisdom, StatConstants.Intelligence)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.Blasphemy,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Blasphemy,
            1,
            FeatConstants.Frequencies.Day,
            11,
            "",
            0,
            0,
            8, StatConstants.Wisdom, StatConstants.Intelligence)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.UnholyAura,
            FeatConstants.SpellLikeAbility,
            SpellConstants.UnholyAura,
            3,
            FeatConstants.Frequencies.Day,
            13,
            "",
            0,
            0,
            8, StatConstants.Wisdom, StatConstants.Intelligence)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.Unhallow,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Unhallow,
            1,
            FeatConstants.Frequencies.Day,
            13,
            "",
            0,
            0,
            8, StatConstants.Wisdom, StatConstants.Intelligence)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.HorridWilting,
            FeatConstants.SpellLikeAbility,
            SpellConstants.HorridWilting,
            1,
            FeatConstants.Frequencies.Day,
            15,
            "",
            0,
            0,
            8, StatConstants.Wisdom, StatConstants.Intelligence)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.SummonMonsterIX,
            FeatConstants.SpellLikeAbility,
            SpellConstants.SummonMonsterIX + " (Fiends only)",
            1,
            FeatConstants.Frequencies.Day,
            17,
            "",
            0,
            0,
            8, StatConstants.Wisdom, StatConstants.Intelligence)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.Destruction,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Destruction,
            1,
            FeatConstants.Frequencies.Day,
            19,
            "",
            0,
            0,
            8, StatConstants.Wisdom, StatConstants.Intelligence)]
        public override void Data(String name, String feat, String focus, Int32 frequencyQuantity, String frequencyTimePeriod, Int32 minimumHitDiceRequirement, String sizeRequirement, Int32 strength, Int32 maximumHitDiceRequirement, Int32 requiredStatMinimumValue, params String[] minimumStats)
        {
            base.Data(name, feat, focus, frequencyQuantity, frequencyTimePeriod, minimumHitDiceRequirement, sizeRequirement, strength, maximumHitDiceRequirement, requiredStatMinimumValue, minimumStats);
        }
    }
}
