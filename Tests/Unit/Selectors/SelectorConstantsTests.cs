using System;
using NPCGen.Selectors.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Selectors
{
    [TestFixture]
    public class SelectorConstantsTests
    {
        [TestCase(SelectorConstants.RacialFeatIdIndex, 0)]
        [TestCase(SelectorConstants.RacialSizeRequirementIndex, 1)]
        [TestCase(SelectorConstants.RacialMinimumHitDiceRequirementIndex, 2)]
        [TestCase(SelectorConstants.RacialStrengthIndex, 3)]
        [TestCase(SelectorConstants.RacialFocusIndex, 4)]
        [TestCase(SelectorConstants.RacialFrequencyQuantityIndex, 5)]
        [TestCase(SelectorConstants.RacialFrequencyTimePeriodIndex, 6)]
        public void RacialIndexConstant(Int32 constant, Int32 value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }

        [TestCase(SelectorConstants.ClassFeatIdIndex, 0)]
        [TestCase(SelectorConstants.ClassMinimumLevelRequirementIndex, 1)]
        [TestCase(SelectorConstants.ClassFocusTypeIndex, 2)]
        [TestCase(SelectorConstants.ClassStrengthIndex, 3)]
        [TestCase(SelectorConstants.ClassFrequencyQuantityIndex, 4)]
        [TestCase(SelectorConstants.ClassFrequencyTimePeriodIndex, 5)]
        public void ClassIndexConstant(Int32 constant, Int32 value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }

        [TestCase(SelectorConstants.AdditionalIsFighterFeatIndex, 0)]
        [TestCase(SelectorConstants.AdditionalIsWizardFeatIndex, 1)]
        [TestCase(SelectorConstants.AdditionalBaseAttackRequirementIndex, 2)]
        [TestCase(SelectorConstants.AdditionalFocusTypeIndex, 3)]
        [TestCase(SelectorConstants.AdditionalStrengthIndex, 4)]
        [TestCase(SelectorConstants.AdditionalFrequencyQuantityIndex, 5)]
        [TestCase(SelectorConstants.AdditionalFrequencyTimePeriodIndex, 6)]
        public void AdditionalIndexConstant(Int32 constant, Int32 value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}