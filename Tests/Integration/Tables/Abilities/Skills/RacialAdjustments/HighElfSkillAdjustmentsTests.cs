using System;
using System.Collections.Generic;
using NPCGen.Common.Abilities.Skills;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Skills.RacialAdjustments
{
    [TestFixture]
    public class HighElfSkillAdjustmentsTests : CollectionTests
    {
        protected override String tableName
        {
            get { return "HighElfSkillAdjustments"; }
        }

        protected override IEnumerable<String> nameCollection
        {
            get { return new[] { SkillConstants.Listen, SkillConstants.Search, SkillConstants.Spot }; }
        }

        [TestCase(SkillConstants.Listen, 2)]
        [TestCase(SkillConstants.Search, 2)]
        [TestCase(SkillConstants.Spot, 2)]
        public void DistinctCollection(String name, Int32 adjustment)
        {
            var collection = new[] { Convert.ToString(adjustment) };
            base.DistinctCollection(name, collection);
        }
    }
}