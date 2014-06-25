using System;
using NPCGen.Common.Items;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Common.Items
{
    [TestFixture]
    public class WeaponAttributeConstantsTests
    {
        [TestCase(WeaponAttributeConstants.TwoHanded, "Two-handed")]
        public void Constant(String constant, String value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}