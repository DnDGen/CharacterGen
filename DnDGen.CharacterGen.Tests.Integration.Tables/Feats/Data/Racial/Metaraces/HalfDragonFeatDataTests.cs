﻿using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Feats;
using DnDGen.CharacterGen.Races;
using NUnit.Framework;
using System;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Feats.Data.Racial.Metaraces
{
    [TestFixture]
    public class HalfDragonFeatDataTests : RacialFeatDataTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Collection.RACEFeatData, RaceConstants.Metaraces.HalfDragon); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.NaturalArmor,
                FeatConstants.Darkvision,
                FeatConstants.LowLightVision,
                FeatConstants.ImmuneToEffect + "Sleep",
                FeatConstants.ImmuneToEffect + "Paralysis",
                FeatConstants.NaturalWeapon + "Claw",
                FeatConstants.NaturalWeapon + "Bite"
            };

            AssertCollectionNames(names);
        }

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
        [TestCase(FeatConstants.ImmuneToEffect + "Paralysis",
            FeatConstants.ImmuneToEffect,
            "Paralysis",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.ImmuneToEffect + "Sleep",
            FeatConstants.ImmuneToEffect,
            "Sleep",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.LowLightVision,
            FeatConstants.LowLightVision,
            "",
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
            4,
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
