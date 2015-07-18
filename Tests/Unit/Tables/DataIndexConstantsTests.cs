using System;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Tables
{
    [TestFixture]
    public class DataIndexConstantsTests
    {
        [TestCase(DataIndexConstants.CharacterClassFeatData.FeatNameIndex, 0)]
        [TestCase(DataIndexConstants.CharacterClassFeatData.MinimumLevelRequirementIndex, 1)]
        [TestCase(DataIndexConstants.CharacterClassFeatData.FocusTypeIndex, 2)]
        [TestCase(DataIndexConstants.CharacterClassFeatData.StrengthIndex, 3)]
        [TestCase(DataIndexConstants.CharacterClassFeatData.FrequencyQuantityIndex, 4)]
        [TestCase(DataIndexConstants.CharacterClassFeatData.FrequencyTimePeriodIndex, 5)]
        [TestCase(DataIndexConstants.CharacterClassFeatData.MaximumLevelRequirementIndex, 6)]
        [TestCase(DataIndexConstants.CharacterClassFeatData.FrequencyQuantityStatIndex, 7)]
        public void CharacterClassFeatDataIndex(Int32 constant, Int32 value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }

        [TestCase(DataIndexConstants.RacialFeatData.FeatNameIndex, 0)]
        [TestCase(DataIndexConstants.RacialFeatData.SizeRequirementIndex, 1)]
        [TestCase(DataIndexConstants.RacialFeatData.MinimumHitDiceRequirementIndex, 2)]
        [TestCase(DataIndexConstants.RacialFeatData.StrengthIndex, 3)]
        [TestCase(DataIndexConstants.RacialFeatData.FocusIndex, 4)]
        [TestCase(DataIndexConstants.RacialFeatData.FrequencyQuantityIndex, 5)]
        [TestCase(DataIndexConstants.RacialFeatData.FrequencyTimePeriodIndex, 6)]
        public void RacialFeatDataIndex(Int32 constant, Int32 value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }

        [TestCase(DataIndexConstants.AdditionalFeatData.BaseAttackRequirementIndex, 0)]
        [TestCase(DataIndexConstants.AdditionalFeatData.FocusTypeIndex, 1)]
        [TestCase(DataIndexConstants.AdditionalFeatData.StrengthIndex, 2)]
        [TestCase(DataIndexConstants.AdditionalFeatData.FrequencyQuantityIndex, 3)]
        [TestCase(DataIndexConstants.AdditionalFeatData.FrequencyTimePeriodIndex, 4)]
        public void AdditionalFeatDataIndex(Int32 constant, Int32 value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}