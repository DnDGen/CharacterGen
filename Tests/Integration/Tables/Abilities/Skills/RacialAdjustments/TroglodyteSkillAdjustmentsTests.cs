using System;
using System.Collections.Generic;
using NPCGen.Common.Abilities.Skills;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Skills.RacialAdjustments
{
    [TestFixture]
    public class TroglodyteSkillAdjustmentsTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return "TroglodyteSkillAdjustments"; }
        }

        [TestCase(SkillConstants.Hide, 6)]
        [TestCase(SkillConstants.Listen, 3)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}