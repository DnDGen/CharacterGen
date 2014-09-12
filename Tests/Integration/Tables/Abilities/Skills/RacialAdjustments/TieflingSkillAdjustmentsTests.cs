using System;
using NPCGen.Common.Abilities.Skills;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Skills.RacialAdjustments
{
    [TestFixture]
    public class TieflingSkillAdjustmentsTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return "TieflingSkillAdjustments"; }
        }

        [TestCase(SkillConstants.Bluff, 2)]
        [TestCase(SkillConstants.Hide, 2)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}