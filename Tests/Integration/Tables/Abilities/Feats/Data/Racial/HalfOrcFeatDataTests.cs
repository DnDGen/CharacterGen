using System;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Feats.Data.Racial
{
    [TestFixture]
    public class HalfOrcFeatDataTests : CollectionTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Collection.RACEFeatData, RaceConstants.BaseRaces.HalfOrcId); }
        }

        [TestCase(FeatConstants.DarkvisionId,
            FeatConstants.DarkvisionId,
            "",
            "0",
            "60",
            "",
            "0",
            "")]
        [TestCase(FeatConstants.OrcBloodId,
            FeatConstants.OrcBloodId,
            "",
            "0",
            "0",
            "",
            "0",
            "")]
        public override void OrderedCollection(String name, params String[] collection)
        {
            base.OrderedCollection(name, collection);
        }
    }
}
