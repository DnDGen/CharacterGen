﻿using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Feats;
using DnDGen.CharacterGen.Races;
using DnDGen.CharacterGen.Skills;
using NUnit.Framework;
using System;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Feats.Data.Racial.BaseRaces
{
    [TestFixture]
    public class GoblinFeatDataTests : RacialFeatDataTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Collection.RACEFeatData, RaceConstants.BaseRaces.Goblin); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.Darkvision,
                FeatConstants.SkillBonus + SkillConstants.MoveSilently,
                FeatConstants.SkillBonus + SkillConstants.Ride
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.SkillBonus + SkillConstants.MoveSilently,
            FeatConstants.SkillBonus,
            SkillConstants.MoveSilently,
            0,
            "",
            0,
            "",
            4,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Ride,
            FeatConstants.SkillBonus,
            SkillConstants.Ride,
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
