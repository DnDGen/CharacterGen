using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.Metaraces.Evil
{
    [TestFixture]
    public class EvilBardMetaracesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "EvilBardMetaraces"; }
        }

        [Test]
        public void EvilBardEmptyPercentile()
        {
            AssertPercentile(EmptyContent, 1, 99);
        }

        [Test]
        public void EvilBardWerewolfPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.Werewolf, 100);
        }
    }
}