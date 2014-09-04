using System;
using System.Collections.Generic;
using NPCGen.Common.Abilities.Skills;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Skills.RacialAdjustments
{
    [TestFixture]
    public class TroglodyteSkillAdjustmentsTests : CollectionTests
    {
        protected override String tableName
        {
            get { return "TroglodyteSkillAdjustments"; }
        }

        protected override IEnumerable<String> nameCollection
        {
            get { return new[] { SkillConstants.Hide, SkillConstants.Listen }; }
        }

        [TestCase(SkillConstants.Hide, 6)]
        [TestCase(SkillConstants.Listen, 3)]
        public void DistinctCollection(String name, Int32 adjustment)
        {
            var collection = new[] { Convert.ToString(adjustment) };
            base.DistinctCollection(name, collection);
        }
    }
}