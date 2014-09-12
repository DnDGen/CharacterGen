using System;
using NPCGen.Common.Abilities.Skills;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Skills.RacialAdjustments
{
    [TestFixture]
    public class KoboldSkillAdjustmentsTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return "KoboldSkillAdjustments"; }
        }

        [TestCase(SkillConstants.Search, 2)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}