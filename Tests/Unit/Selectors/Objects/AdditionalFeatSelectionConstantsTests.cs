using System;
using NPCGen.Selectors.Interfaces.Objects;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Selectors.Objects
{
    [TestFixture]
    public class AdditionalFeatSelectionConstantsTests
    {
        [TestCase(AdditionalFeatSelectionConstants.SchoolsOfMagic, "Schools of magic")]
        [TestCase(AdditionalFeatSelectionConstants.Skills, "Skills")]
        [TestCase(AdditionalFeatSelectionConstants.Weapons, "Weapons")]
        [TestCase(AdditionalFeatSelectionConstants.WeaponsWithUnarmedAndGrapple, "Weapons with unarmed and grapple")]
        [TestCase(AdditionalFeatSelectionConstants.WeaponsWithUnarmedAndGrappleAndRay, "Weapons with unarmed, grapple, and ray")]
        public void Constant(String constant, String value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}