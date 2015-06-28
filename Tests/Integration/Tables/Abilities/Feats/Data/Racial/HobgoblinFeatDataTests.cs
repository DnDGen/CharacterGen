using System;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Abilities.Skills;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Feats.Data.Racial
{
    [TestFixture]
    public class HobgoblinFeatDataTests : CollectionTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Collection.RACEFeatData, RaceConstants.BaseRaces.HobgoblinId); }
        }

        [TestCase(FeatConstants.DarkvisionId,
            FeatConstants.DarkvisionId,
            "",
            "0",
            "60",
            "",
            "0",
            "")]
        [TestCase(FeatConstants.SkillBonusId,
            FeatConstants.SkillBonusId,
            "",
            "0",
            "4",
            SkillConstants.MoveSilently,
            "0",
            "")]
        public override void OrderedCollection(String name, params String[] collection)
        {
            base.OrderedCollection(name, collection);
        }
    }
}
