using System;
using NPCGen.Selectors.Interfaces.Objects;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Selectors.Objects
{
    [TestFixture]
    public class FeatSelectionConstantsTests
    {
        [TestCase(FeatSelectionConstants.SchoolsOfMagic, "Schools of magic")]
        [TestCase(FeatSelectionConstants.Skills, "Skills")]
        [TestCase(FeatSelectionConstants.Weapons, "Weapons")]
        [TestCase(FeatSelectionConstants.WeaponsWithUnarmedAndGrapple, "Weapons with unarmed and grapple")]
        [TestCase(FeatSelectionConstants.WeaponsWithUnarmedAndGrappleAndRay, "Weapons with unarmed, grapple, and ray")]
        public void Constant(String constant, String value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}