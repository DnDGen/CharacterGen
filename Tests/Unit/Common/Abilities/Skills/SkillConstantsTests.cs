using System;
using System.Linq;
using NPCGen.Common.Abilities.Skills;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Common.Abilities.Skills
{
    [TestFixture]
    public class SkillConstantsTests
    {
        [TestCase(SkillConstants.Swim, "Swim")]
        public void Constant(String constant, String value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }

        public void AllSkills()
        {
            var skills = SkillConstants.GetSkills();

            Assert.That(skills, Contains.Item(SkillConstants.Swim));
            Assert.That(skills.Count(), Is.EqualTo(1));
        }
    }
}