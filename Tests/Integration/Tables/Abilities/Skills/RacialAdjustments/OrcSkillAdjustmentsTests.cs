using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Skills.RacialAdjustments
{
    [TestFixture]
    public class OrcSkillAdjustmentsTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return "OrcSkillAdjustments"; }
        }

        [Test]
        public override void NoAdjustments()
        {
            base.NoAdjustments();
        }
    }
}