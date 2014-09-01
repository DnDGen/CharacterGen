using System;
using NPCGen.Common.Items;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Common.Items
{
    [TestFixture]
    public class WeaponProficiencyConstantsTests
    {
        [TestCase(WeaponProficiencyConstants.Grapple, "Grapple")]
        [TestCase(WeaponProficiencyConstants.Ray, "Ray")]
        [TestCase(WeaponProficiencyConstants.UnarmedStrike, "Unarmed Strike")]
        public void Constant(String constant, String value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}