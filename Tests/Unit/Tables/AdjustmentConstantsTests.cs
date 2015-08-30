using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Unit.Tables
{
    [TestFixture]
    public class AdjustmentConstantsTests
    {
        [TestCase(AdjustmentConstants.Adulthood, "Adulthood")]
        [TestCase(AdjustmentConstants.Base, "Base")]
        [TestCase(AdjustmentConstants.Quantity, "Quantity")]
        [TestCase(AdjustmentConstants.Die, "Die")]
        [TestCase(AdjustmentConstants.MiddleAge, "Middle Age")]
        [TestCase(AdjustmentConstants.Old, "Old")]
        [TestCase(AdjustmentConstants.Venerable, "Venerable")]
        public void CharacterClassFeatDataIndex(String constant, String value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}
