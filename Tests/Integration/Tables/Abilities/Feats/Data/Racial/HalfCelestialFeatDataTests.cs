﻿using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.Magics;
using CharacterGen.Common.Races;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Data.Racial
{
    [TestFixture]
    public class HalfCelestialFeatDataTests : RacialFeatDataTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Collection.RACEFeatData, RaceConstants.Metaraces.HalfCelestial); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.SpellLikeAbility + SpellConstants.Daylight,
                FeatConstants.SmiteEvil,
                FeatConstants.SpellLikeAbility + SpellConstants.ProtectionFromEvil,
                FeatConstants.SpellLikeAbility + SpellConstants.Bless,
                FeatConstants.SpellLikeAbility + SpellConstants.Aid,
                FeatConstants.SpellLikeAbility + SpellConstants.DetectEvil,
                FeatConstants.SpellLikeAbility + SpellConstants.CureSeriousWounds,
                FeatConstants.SpellLikeAbility + SpellConstants.NeutralizePoison,
                FeatConstants.SpellLikeAbility + SpellConstants.HolySmite,
                FeatConstants.SpellLikeAbility + SpellConstants.RemoveDisease,
                FeatConstants.SpellLikeAbility + SpellConstants.DispelEvil,
                FeatConstants.SpellLikeAbility + SpellConstants.HolyWord,
                FeatConstants.SpellLikeAbility + SpellConstants.HolyAura,
                FeatConstants.SpellLikeAbility + SpellConstants.Hallow,
                FeatConstants.SpellLikeAbility + SpellConstants.MassCharmMonster,
                FeatConstants.SpellLikeAbility + SpellConstants.SummonMonsterIX,
                FeatConstants.SpellLikeAbility + SpellConstants.Resurrection,
                FeatConstants.NaturalArmor,
                FeatConstants.Darkvision,
                FeatConstants.ImmuneToEffect,
                FeatConstants.Resistance + FeatConstants.Foci.Acid,
                FeatConstants.Resistance + FeatConstants.Foci.Cold,
                FeatConstants.Resistance + FeatConstants.Foci.Electricity,
                FeatConstants.DamageReduction + "11-",
                FeatConstants.DamageReduction + "12+",
                FeatConstants.SpellResistance,
                FeatConstants.SaveBonus
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.Daylight,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Daylight,
            0,
            FeatConstants.Frequencies.AtWill,
            0,
            "",
            0,
            0, "", 0)]
        [TestCase(FeatConstants.SmiteEvil,
            FeatConstants.SmiteEvil,
            "",
            1,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, "", 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.ProtectionFromEvil,
            FeatConstants.SpellLikeAbility,
            SpellConstants.ProtectionFromEvil,
            3,
            FeatConstants.Frequencies.Day,
            1,
            "",
            0,
            0, StatConstants.Wisdom, 8)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.Bless,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Bless,
            1,
            FeatConstants.Frequencies.Day,
            1,
            "",
            0,
            0, StatConstants.Wisdom, 8)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.Aid,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Aid,
            1,
            FeatConstants.Frequencies.Day,
            3,
            "",
            0,
            0, StatConstants.Wisdom, 8)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.DetectEvil,
            FeatConstants.SpellLikeAbility,
            SpellConstants.DetectEvil,
            1,
            FeatConstants.Frequencies.Day,
            3,
            "",
            0,
            0, StatConstants.Wisdom, 8)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.CureSeriousWounds,
            FeatConstants.SpellLikeAbility,
            SpellConstants.CureSeriousWounds,
            1,
            FeatConstants.Frequencies.Day,
            5,
            "",
            0,
            0, StatConstants.Wisdom, 8)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.NeutralizePoison,
            FeatConstants.SpellLikeAbility,
            SpellConstants.NeutralizePoison,
            1,
            FeatConstants.Frequencies.Day,
            5,
            "",
            0,
            0, StatConstants.Wisdom, 8)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.HolySmite,
            FeatConstants.SpellLikeAbility,
            SpellConstants.HolySmite,
            1,
            FeatConstants.Frequencies.Day,
            7,
            "",
            0,
            0, StatConstants.Wisdom, 8)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.RemoveDisease,
            FeatConstants.SpellLikeAbility,
            SpellConstants.RemoveDisease,
            1,
            FeatConstants.Frequencies.Day,
            7,
            "",
            0,
            0, StatConstants.Wisdom, 8)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.DispelEvil,
            FeatConstants.SpellLikeAbility,
            SpellConstants.DispelEvil,
            1,
            FeatConstants.Frequencies.Day,
            9,
            "",
            0,
            0, StatConstants.Wisdom, 8)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.HolyWord,
            FeatConstants.SpellLikeAbility,
            SpellConstants.HolyWord,
            1,
            FeatConstants.Frequencies.Day,
            11,
            "",
            0,
            0, StatConstants.Wisdom, 8)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.HolyAura,
            FeatConstants.SpellLikeAbility,
            SpellConstants.HolyAura,
            3,
            FeatConstants.Frequencies.Day,
            13,
            "",
            0,
            0, StatConstants.Wisdom, 8)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.Hallow,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Hallow,
            1,
            FeatConstants.Frequencies.Day,
            13,
            "",
            0,
            0, StatConstants.Wisdom, 8)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.MassCharmMonster,
            FeatConstants.SpellLikeAbility,
            SpellConstants.MassCharmMonster,
            1,
            FeatConstants.Frequencies.Day,
            15,
            "",
            0,
            0, StatConstants.Wisdom, 8)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.SummonMonsterIX,
            FeatConstants.SpellLikeAbility,
            SpellConstants.SummonMonsterIX + " (Celestials only)",
            1,
            FeatConstants.Frequencies.Day,
            17,
            "",
            0,
            0, StatConstants.Wisdom, 8)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.Resurrection,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Resurrection,
            1,
            FeatConstants.Frequencies.Day,
            19,
            "",
            0,
            0, StatConstants.Wisdom, 8)]
        [TestCase(FeatConstants.NaturalArmor,
            FeatConstants.NaturalArmor,
            "",
            0,
            "",
            0,
            "",
            1,
            0, "", 0)]
        [TestCase(FeatConstants.ImmuneToEffect,
            FeatConstants.ImmuneToEffect,
            "Disease",
            0,
            "",
            0,
            "",
            0,
            0, "", 0)]
        [TestCase(FeatConstants.Resistance + FeatConstants.Foci.Acid,
            FeatConstants.Resistance,
            FeatConstants.Foci.Acid,
            0,
            "",
            0,
            "",
            10,
            0, "", 0)]
        [TestCase(FeatConstants.Resistance + FeatConstants.Foci.Cold,
            FeatConstants.Resistance,
            FeatConstants.Foci.Cold,
            0,
            "",
            0,
            "",
            10,
            0, "", 0)]
        [TestCase(FeatConstants.Resistance + FeatConstants.Foci.Electricity,
            FeatConstants.Resistance,
            FeatConstants.Foci.Electricity,
            0,
            "",
            0,
            "",
            10,
            0, "", 0)]
        [TestCase(FeatConstants.DamageReduction + "11-",
            FeatConstants.DamageReduction,
            "Magic",
            0,
            "",
            1,
            "",
            5,
            11, "", 0)]
        [TestCase(FeatConstants.DamageReduction + "12+",
            FeatConstants.DamageReduction,
            "Magic",
            0,
            "",
            12,
            "",
            10,
            0, "", 0)]
        [TestCase(FeatConstants.SpellResistance,
            FeatConstants.SpellResistance,
            "Add creature's monster hit dice to strength (non-monsters add only 1)",
            0,
            "",
            0,
            "",
            10,
            0, "", 0)]
        [TestCase(FeatConstants.SaveBonus,
            FeatConstants.SaveBonus,
            "Fortitude (Poison)",
            0,
            "",
            0,
            "",
            4,
            0, "", 0)]
        [TestCase(FeatConstants.Darkvision,
            FeatConstants.Darkvision,
            "",
            0,
            "",
            0,
            "",
            60,
            0, "", 0)]
        public override void Data(String name, String feat, String focus, Int32 frequencyQuantity, String frequencyTimePeriod, Int32 minimumHitDiceRequirement, String sizeRequirement, Int32 strength, Int32 maximumHitDiceRequirement, String requiredStat, Int32 requiredStatMinimumValue)
        {
            base.Data(name, feat, focus, frequencyQuantity, frequencyTimePeriod, minimumHitDiceRequirement, sizeRequirement, strength, maximumHitDiceRequirement, requiredStat, requiredStatMinimumValue);
        }
    }
}
