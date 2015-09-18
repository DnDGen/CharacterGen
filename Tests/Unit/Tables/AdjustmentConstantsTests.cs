using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Unit.Tables
{
    [TestFixture]
    public class AdjustmentConstantsTests
    {
        [TestCase(AdjustmentConstants.Base, "Base")]
        [TestCase(AdjustmentConstants.Quantity, "Quantity")]
        [TestCase(AdjustmentConstants.Die, "Die")]
        public void CharacterClassFeatDataIndex(String constant, String value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}
