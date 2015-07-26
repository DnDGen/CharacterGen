using System;
using CharacterGen.Common.Items;
using NUnit.Framework;

namespace CharacterGen.Tests.Unit.Common.Items
{
    [TestFixture]
    public class ProficiencyConstantsTests
    {
        [TestCase(ProficiencyConstants.All, "All")]
        [TestCase(ProficiencyConstants.Grapple, "Grapple")]
        [TestCase(ProficiencyConstants.Ray, "Ray")]
        [TestCase(ProficiencyConstants.UnarmedStrike, "Unarmed Strike")]
        public void Constant(String constant, String value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}