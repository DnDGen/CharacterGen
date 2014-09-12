using System;
using NPCGen.Common.Abilities.Skills;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Skills.RacialAdjustments
{
    [TestFixture]
    public class OgreSkillAdjustmentsTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return "OgreSkillAdjustments"; }
        }

        [TestCase(SkillConstants.Climb, 3)]
        [TestCase(SkillConstants.Listen, 2)]
        [TestCase(SkillConstants.Spot, 2)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}