using System;
using System.Collections.Generic;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Feats
{
    [TestFixture]
    public class RacialFeatsTests : CollectionTests
    {
        protected override String tableName
        {
            get { return "RacialFeats"; }
        }

        protected override IEnumerable<String> nameCollection
        {
            get { return RaceConstants.GetAllRaces(); }
        }

        [TestCase(RaceConstants.BaseRaces.Aasimar)]
        public override void DistinctCollection(String name, params String[] collection)
        {
            Assert.Fail();
            base.DistinctCollection(name, collection);
        }
    }
}