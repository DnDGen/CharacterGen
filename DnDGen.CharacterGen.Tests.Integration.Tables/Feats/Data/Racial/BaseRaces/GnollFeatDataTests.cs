﻿using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Feats;
using DnDGen.CharacterGen.Races;
using NUnit.Framework;
using System;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Feats.Data.Racial.BaseRaces
{
    [TestFixture]
    public class GnollFeatDataTests : RacialFeatDataTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Collection.RACEFeatData, RaceConstants.BaseRaces.Gnoll); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.Darkvision,
                FeatConstants.NaturalArmor
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
        [TestCase(FeatConstants.NaturalArmor,
            FeatConstants.NaturalArmor,
            "",
            0,
            "",
            0,
            "",
            1,
            0, 0)]
        public override void RacialFeatData(String name, String feat, String focus, Int32 frequencyQuantity, String frequencyTimePeriod, Int32 minimumHitDiceRequirement, String sizeRequirement, Int32 strength, Int32 maximumHitDiceRequirement, Int32 requiredStatMinimumValue, params String[] minimumAbilities)
        {
            base.RacialFeatData(name, feat, focus, frequencyQuantity, frequencyTimePeriod, minimumHitDiceRequirement, sizeRequirement, strength, maximumHitDiceRequirement, requiredStatMinimumValue, minimumAbilities);
        }
    }
}
