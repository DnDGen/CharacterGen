using System;
using NPCGen.Common.Abilities.Skills;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Skills.RacialAdjustments
{
    [TestFixture]
    public class GnollSkillAdjustmentsTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return "GnollSkillAdjustments"; }
        }

        [TestCase(SkillConstants.Listen, 2)]
        [TestCase(SkillConstants.Spot, 3)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}