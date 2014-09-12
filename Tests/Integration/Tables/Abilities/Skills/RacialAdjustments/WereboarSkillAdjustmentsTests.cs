using System;
using NPCGen.Common.Abilities.Skills;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Skills.RacialAdjustments
{
    [TestFixture]
    public class WereboarSkillAdjustmentsTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return "WereboarSkillAdjustments"; }
        }

        [TestCase(SkillConstants.Listen, 3)]
        [TestCase(SkillConstants.Spot, 3)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}