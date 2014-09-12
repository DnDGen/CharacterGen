using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Skills.RacialAdjustments
{
    [TestFixture]
    public class HalfOrcSkillAdjustmentsTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return "HalfOrcSkillAdjustments"; }
        }

        [Test]
        public override void NoAdjustments()
        {
            base.NoAdjustments();
        }
    }
}