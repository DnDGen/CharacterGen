using System;
using NPCGen.Common.Skills;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Common.Skills
{
    [TestFixture]
    public class SkillConstantsTests
    {
        [TestCase(SkillConstants.Swim, "Swim")]
        public void Constant(String constant, String value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}