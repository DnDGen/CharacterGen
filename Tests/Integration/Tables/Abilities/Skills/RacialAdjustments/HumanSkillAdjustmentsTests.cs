using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Skills.RacialAdjustments
{
    [TestFixture]
    public class HumanSkillAdjustmentsTests : CollectionTests
    {
        protected override String tableName
        {
            get { return "HumanSkillAdjustments"; }
        }

        [Test]
        public override void EmptyCollection()
        {
            base.EmptyCollection();
        }
    }
}